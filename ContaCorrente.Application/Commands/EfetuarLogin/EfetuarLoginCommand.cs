using MediatR;

namespace ContaCorrente.Application.Commands.EfetuarLogin;

public class EfetuarLoginCommand : IRequest<EfetuarLoginResult>
{
    public string? Cpf { get; set; }
    public string? NumeroConta { get; set; }
    public string Senha { get; set; } = string.Empty;

}
