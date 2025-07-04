using MediatR;

namespace ContaCorrente.Application.Commands.CriarConta;

public class CriarContaCommand : IRequest<CriarContaResult>
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
