using MediatR;

namespace ContaCorrente.Application.Commands.EfetuarTransferencia;

public class EfetuarTransferenciaCommand : IRequest
{
    public string CpfOrigem { get; set; } = string.Empty;
    public string NumeroContaDestino { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}
