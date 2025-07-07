using ContaCorrente.Domain.Repositories;
using ContaCorrente.Domain.Entities;
using MediatR;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ContaCorrente.Application.Commands.EfetuarTransferencia
{
    public class EfetuarTransferenciaCommandHandler : IRequestHandler<EfetuarTransferenciaCommand>
    {
        private readonly IContaCorrenteRepository _contaRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EfetuarTransferenciaCommandHandler(
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

        public async Task Handle(EfetuarTransferenciaCommand request, CancellationToken cancellationToken)
        {
            var chave = request.IdRequisicao.ToString();

            // 🔒 Verifica se a requisição já foi processada
            var existente = await _idempotenciaRepository.ObterPorChaveAsync(chave);
            if (existente is not null)
            {
                throw new InvalidOperationException("Essa requisição já foi processada anteriormente.");
            }

            var cpfOrigem = _httpContextAccessor.HttpContext?.User?.FindFirst("cpf")?.Value;
            if (string.IsNullOrEmpty(cpfOrigem))
                throw new UnauthorizedAccessException("Token inválido ou CPF não encontrado.");

            var contaOrigem = await _contaRepository.ObterContaPorCpfAsync(cpfOrigem);
            var contaDestino = await _contaRepository.ObterContaPorNumeroAsync(request.NumeroContaDestino);

            if (contaOrigem is null || contaDestino is null)
                throw new InvalidOperationException("Conta de origem ou destino não encontrada.");

            var transferencia = new Transferencia(
                id: Guid.NewGuid(),
                contaOrigemId: contaOrigem.Id,
                contaDestinoId: contaDestino.Id,
                dataMovimento: DateTime.Now,
                valor: request.Valor
            );

            await _movimentoRepository.RegistrarTransferenciaAsync(transferencia);

            var resultado = JsonSerializer.Serialize(new { transferencia.Id });
            var requisicao = JsonSerializer.Serialize(request);

            await _idempotenciaRepository.SalvarAsync(new Idempotencia
            {
                ChaveIdempotencia = chave,
                Requisicao = requisicao,
                Resultado = resultado
            });
        }
    }
}
