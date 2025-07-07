using MediatR;

namespace ContaCorrente.Application.Commands.EfetuarMovimentacao
{
    public class EfetuarMovimentacaoCommand : IRequest<Unit>
    {
        public string Idempotencia { get; set; } = string.Empty;
        public int? NumeroConta { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
