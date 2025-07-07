using ContaCorrente.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ContaCorrente.Application.Commands.InativarConta
{
    public class InativarContaCommandHandler : IRequestHandler<ContaCorrente.Application.Commands.InativarConta.InativarContaCommand, Unit>
    {
        private readonly IContaCorrenteRepository _contaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InativarContaCommandHandler(
            IContaCorrenteRepository contaRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _contaRepository = contaRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(InativarContaCommand request, CancellationToken cancellationToken)
        {
            var cpf = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(cpf))
                throw new UnauthorizedAccessException("Token inválido.");

            var conta = await _contaRepository.ObterContaPorCpfAsync(cpf)
                        ?? throw new ArgumentException("Conta inválida.", "INVALID_ACCOUNT");

            if (!conta.Ativo)
                throw new ArgumentException("Conta já está inativa.", "INACTIVE_ACCOUNT");

            // Recria o hash da senha com o salt salvo no banco
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashBytes = System.Text.Encoding.UTF8.GetBytes(request.Senha + conta.Salt);
            var senhaHash = Convert.ToBase64String(sha256.ComputeHash(hashBytes));

            if (conta.Senha != senhaHash)
                throw new UnauthorizedAccessException("Senha incorreta.");

            conta.Inativar(); // método na entidade

            await _contaRepository.AtualizarAsync(conta);
            return Unit.Value;
        }
    }
}
