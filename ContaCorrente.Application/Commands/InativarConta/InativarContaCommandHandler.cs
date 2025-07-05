using ContaCorrente.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Commands.InativarConta
{
    public class InativarContaCommandHandler : IRequestHandler<InativarContaCommand, Unit>
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
            var contaIdStr = _httpContextAccessor.HttpContext?.User.FindFirst("ContaId")?.Value;

            if (string.IsNullOrEmpty(contaIdStr) || !Guid.TryParse(contaIdStr, out var contaId))
                throw new UnauthorizedAccessException("Token inválido ou expirado");

            var conta = await _contaRepository.ObterPorIdAsync(contaId);

            if (conta == null)
                throw new InvalidOperationException("Conta não encontrada. Tipo de falha: INVALID_ACCOUNT");

            if (!conta.Senha.Equals(request.Senha))
                throw new UnauthorizedAccessException("Senha inválida. Tipo de falha: USER_UNAUTHORIZED");

            conta.Inativar();

            await _contaRepository.AtualizarAsync(conta);

            return Unit.Value;
        }
    }
}
