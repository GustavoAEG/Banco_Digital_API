using MediatR;

namespace ContaCorrente.Application.Commands.EfetuarLogin;

public class EfetuarLoginCommand : IRequest<EfetuarLoginResult>
{
    public string CpfOuNumero { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
