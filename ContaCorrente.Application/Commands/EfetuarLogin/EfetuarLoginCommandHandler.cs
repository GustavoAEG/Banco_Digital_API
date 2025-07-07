using ContaCorrente.Domain.Repositories;
using ContaCorrente.Application.Commands;
using MediatR;
using ContaCorrente.Application.Services;
using System.Security.Cryptography;
using System.Text;

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
            ? await _contaRepository.ObterContaPorCpfAsync(request.Cpf)
            : await _contaRepository.ObterContaPorNumeroAsync(request.NumeroConta.ToString());

        if (conta == null)
            throw new UnauthorizedAccessException("Conta não encontrada.");

        string senhaHash = GerarHashSenha(request.Senha, conta.Salt);

        if (conta.Senha != senhaHash)
            throw new UnauthorizedAccessException("Senha inválida. Tipo de falha: USER_UNAUTHORIZED");

        var token = _tokenService.GerarToken(conta);

        return new EfetuarLoginResult
        {
            Token = token
        };
    }

    private string GerarHashSenha(string senha, string salt)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha + salt);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
