using ContaCorrente.Domain.Repositories;
using MediatR;

namespace ContaCorrente.Application.Queries.ConsultarSaldo
{
    public class ConsultarSaldoQueryHandler : IRequestHandler<ConsultarSaldoQuery, decimal>
    {
        private readonly IContaCorrenteRepository _repository;

        public ConsultarSaldoQueryHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
        {
            var conta = await _repository.ObterContaPorCpfAsync(request.Cpf);

            if (conta == null)
                throw new ArgumentException("Conta não encontrada.");

            // Por enquanto retornando 0 como saldo fixo
            return 0.0m;
        }
    }
}
