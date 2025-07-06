using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Repositories;
using ContaCorrente.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContaCorrente.Infrastructure.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly AppDbContext _context;

        public MovimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarTransferenciaAsync(Transferencia transferencia)
        {
            await _context.Transferencias.AddAsync(transferencia);
            await _context.SaveChangesAsync();
        }
    }
}
