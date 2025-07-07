using ContaCorrente.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ContaCorrente.Application.Queries.GetSaldo;

namespace ContaCorrente.Application.Queries.ConsultarSaldo
{
    public class ConsultarSaldoQueryHandler : IRequestHandler<ConsultarSaldoQuery, GetSaldoResult>
    {
        private readonly IContaCorrenteRepository _contaRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConsultarSaldoQueryHandler(
            IContaCorrenteRepository contaRepository,
            IMovimentoRepository movimentoRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _contaRepository = contaRepository;
            _movimentoRepository = movimentoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetSaldoResult> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
        {
            var cpf = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(cpf))
                throw new UnauthorizedAccessException("Token inválido.");

            var conta = await _contaRepository.ObterContaPorCpfAsync(cpf)
                        ?? throw new ArgumentException("Conta inválida.", "INVALID_ACCOUNT");

            if (!conta.Ativo)
                throw new ArgumentException("Conta inativa.", "INACTIVE_ACCOUNT");

            var movimentos = await _movimentoRepository.ObterMovimentosPorContaAsync(conta.Id);

            var creditos = movimentos.Where(m => m.TipoMovimento == "C").Sum(m => m.Valor);
            var debitos = movimentos.Where(m => m.TipoMovimento == "D").Sum(m => m.Valor);
            var saldo = creditos - debitos;

            return new GetSaldoResult
            {
                NumeroConta = conta.Numero,
                NomeTitular = conta.Nome,
                DataHoraConsulta = DateTime.UtcNow,
                SaldoAtual = saldo
            };
        }
    }
}