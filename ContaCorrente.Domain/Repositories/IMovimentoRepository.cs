using ContaCorrente.Domain.Entities;

namespace ContaCorrente.Domain.Repositories
{
    public interface IMovimentoRepository
    {
        Task RegistrarTransferenciaAsync(Transferencia transferencia);

        Task<List<Transferencia>> ObterTransferenciasPorContaAsync(Guid contaId);

        Task SalvarAsync(Movimento movimento);

        Task<List<Movimento>> ObterMovimentosPorContaAsync(Guid contaId);

    }
}
