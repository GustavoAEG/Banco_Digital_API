using MediatR;

namespace ContaCorrente.Application.Queries.ConsultarSaldo
{
    public class ConsultarSaldoQuery : IRequest<decimal>
    {
        public string Cpf { get; set; }

        public ConsultarSaldoQuery(string cpf)
        {
            Cpf = cpf;
        }
    }
}
