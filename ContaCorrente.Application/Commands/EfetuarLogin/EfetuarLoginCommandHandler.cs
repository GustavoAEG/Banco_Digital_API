using ContaCorrente.Domain.Repositories;
using ContaCorrente.Application.Commands;
using MediatR;

namespace ContaCorrente.Application.Commands.EfetuarLogin;

public class EfetuarLoginCommandHandler : IRequestHandler<EfetuarLoginCommand, EfetuarLoginResult>
{
    private readonly IContaCorrenteRepository _contaRepository;
    private readonly ITokenService _tokenService;

    public EfetuarLoginCommandHandler(IContaCorrenteRepository contaRepository, ITokenService tokenService)
    {
        _contaRepository = contaRepository;
        _tokenService = tokenService;
    }

    public async Task<EfetuarLoginResult> Handle(EfetuarLoginCommand request, CancellationToken cancellationToken)
    {
        var conta = !string.IsNullOrWhiteSpace(request.Cpf)
            ? await _contaRepository.ObterPorCpfAsync(request.Cpf)
            : await _contaRepository.ObterPorNumeroContaAsync(request.NumeroConta);

        if (conta == null || conta.Senha != request.Senha) // Aqui seria melhor usar hash de senha
            throw new UnauthorizedAccessException("E-mail ou senha inválidos. Tipo de falha: USER_UNAUTHORIZED");

        var token = _tokenService.GerarToken(conta);

        return new EfetuarLoginResult
        {
            Token = token
        };
    }
}
