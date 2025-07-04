using EntityContaCorrente = ContaCorrente.Domain.Entities.ContaCorrente;


namespace ContaCorrente.Domain.Repositories;

public interface IContaCorrenteRepository
{
    Task<EntityContaCorrente?> ObterPorCpfOuNumeroAsync(string cpfOuNumero);
    Task<EntityContaCorrente?> ObterPorIdAsync(Guid id);
    Task<EntityContaCorrente?> ObterPorNumeroAsync(int numero);
    Task<bool> ExisteContaComNumeroAsync(int numero);
    Task CriarAsync(EntityContaCorrente conta);
    Task AtualizarAsync(EntityContaCorrente conta);
}
