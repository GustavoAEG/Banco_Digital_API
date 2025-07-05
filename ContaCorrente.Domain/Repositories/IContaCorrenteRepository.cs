using EntityContaCorrente = ContaCorrente.Domain.Entities.ContaCorrente;


namespace ContaCorrente.Domain.Repositories;

public interface IContaCorrenteRepository
{
    Task<EntityContaCorrente?> ObterContaPorCpfAsync(string cpfOuNumero);
    Task<EntityContaCorrente?> ObterPorIdAsync(Guid id);
    Task<EntityContaCorrente?> ObterContaPorNumeroAsync(string numeroConta);
    Task<bool> ExisteContaComNumeroAsync(int numero);
    Task CriarAsync(EntityContaCorrente conta);
    Task AtualizarAsync(EntityContaCorrente conta);
}
