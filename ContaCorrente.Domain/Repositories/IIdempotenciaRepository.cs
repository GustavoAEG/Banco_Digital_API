using ContaCorrente.Domain.Entities;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Repositories
{
    public interface IIdempotenciaRepository
    {
        Task<Idempotencia?> ObterPorChaveAsync(string chave);
        Task SalvarAsync(Idempotencia idempotencia);
        Task<bool> JaProcessadoAsync(string chave);
        Task RegistrarAsync(string chave);
    }
}
