using MediatR;

namespace ContaCorrente.Application.Commands.InativarConta
{
    public class InativarContaCommand : IRequest<Unit>
    {
        public string Senha { get; set; } = string.Empty;
    }
}
