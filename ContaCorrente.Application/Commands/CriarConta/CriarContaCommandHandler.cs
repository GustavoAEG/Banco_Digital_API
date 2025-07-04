using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Repositories;
using MediatR;

namespace ContaCorrente.Application.Commands.CriarConta;

public class CriarContaCommandHandler : IRequestHandler<CriarContaCommand, CriarContaResult>
{
    private readonly IContaCorrenteRepository _repository;

    public CriarContaCommandHandler(IContaCorrenteRepository repository)
    {
        _repository = repository;
    }

    public async Task<CriarContaResult> Handle(CriarContaCommand request, CancellationToken cancellationToken)
    {
        if (CpfValido(request.Cpf))
            throw new ArgumentException("CPF não pode estar em branco.");

        var contaExistente = await _repository.ObterPorCpfOuNumeroAsync(request.Cpf);
        if (contaExistente is not null)
            throw new ArgumentException("Já existe uma conta com esse CPF.", "DUPLICATE_CPF");

        var numeroConta = await GerarNumeroContaUnicoAsync();

        var salt = Guid.NewGuid().ToString("N").Substring(0, 8);
        var senhaCriptografada = GerarHashSenha(request.Senha, salt);

        var conta = new Domain.Entities.ContaCorrente(
            id: Guid.NewGuid(),
            numero: numeroConta,
            nome: request.Nome,
            senha: senhaCriptografada,
            salt: salt
        );

        await _repository.CriarAsync(conta);

        return new CriarContaResult
        {
            Id = conta.Id,
            Numero = conta.Numero
        }; 
    }
    private bool CpfValido(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());
        if (cpf.Length != 11) return false;
        if (cpf.All(c => c == cpf[0])) return false;

        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        var digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }
    private async Task<int> GerarNumeroContaUnicoAsync()
    {
        var random = new Random();
        int numero;
        do
        {
            numero = random.Next(100000, 999999);
        } while (await _repository.ExisteContaComNumeroAsync(numero));

        return numero;
    }
    private string GerarHashSenha(string senha, string salt)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes($"{senha}:{salt}");
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

}
