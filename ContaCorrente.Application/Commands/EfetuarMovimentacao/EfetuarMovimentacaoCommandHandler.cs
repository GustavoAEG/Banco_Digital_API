using ContaCorrente.Domain.Repositories;
using MediatR;
using System.Security.Claims;
using ContaCorrente.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ContaCorrente.Application.Commands.EfetuarMovimentacao;

public class EfetuarMovimentacaoCommandHandler : IRequestHandler<EfetuarMovimentacaoCommand, Unit>
{
    private readonly IContaCorrenteRepository _contaRepository;
    private readonly IMovimentoRepository _movimentoRepository;
    private readonly IIdempotenciaRepository _idempotenciaRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EfetuarMovimentacaoCommandHandler(
        IContaCorrenteRepository contaRepository,
        IMovimentoRepository movimentoRepository,
        IIdempotenciaRepository idempotenciaRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _contaRepository = contaRepository;
        _movimentoRepository = movimentoRepository;
        _idempotenciaRepository = idempotenciaRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(EfetuarMovimentacaoCommand request, CancellationToken cancellationToken)
   {
        // Idempotência
        if (await _idempotenciaRepository.JaProcessadoAsync(request.Idempotencia))
            return Unit.Value;

        // Obter CPF do token JWT
        var cpfUsuario = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(cpfUsuario))
            throw new UnauthorizedAccessException("Token inválido.");

        // Obter conta de origem
        var contaOrigem = await _contaRepository.ObterContaPorCpfAsync(cpfUsuario)
            ?? throw new ArgumentException("Conta inválida.", "INVALID_ACCOUNT");

        if (!contaOrigem.Ativo)
            throw new ArgumentException("Conta inativa.", "INACTIVE_ACCOUNT");

        // Verificar conta de destino se número foi informado
        ContaCorrente.Domain.Entities.ContaCorrente contaDestino = contaOrigem;
        if (request.NumeroConta is not null && request.NumeroConta != contaOrigem.Numero)
        {
            contaDestino = await _contaRepository.ObterContaPorNumeroAsync(request.NumeroConta.Value.ToString())
                ?? throw new ArgumentException("Conta de destino inválida.", "INVALID_ACCOUNT");

            if (!contaDestino.Ativo)
                throw new ArgumentException("Conta destino inativa.", "INACTIVE_ACCOUNT");

            if (request.Tipo != "C")
                throw new ArgumentException("Somente crédito permitido para outras contas.", "INVALID_TYPE");
        }

        // Validações
        if (request.Valor <= 0)
            throw new ArgumentException("Valor deve ser positivo.", "INVALID_VALUE");

        if (request.Tipo != "C" && request.Tipo != "D")
            throw new ArgumentException("Tipo de operação inválido.", "INVALID_TYPE");

        // Criar movimento
          var movimento = new Movimento(
          id: Guid.NewGuid(),
          contaCorrenteId: contaDestino.Id,
          tipoMovimento: request.Tipo,
          valor: request.Valor,
          idempotencia: request.Idempotencia,
          dataMovimento: DateTime.UtcNow
      );


        // Persistência
        await _movimentoRepository.SalvarAsync(movimento);
        await _idempotenciaRepository.RegistrarAsync(request.Idempotencia);

        return Unit.Value;
    }
}
