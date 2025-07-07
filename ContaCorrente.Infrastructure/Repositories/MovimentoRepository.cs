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
            _context.Transferencias.Add(transferencia);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transferencia>> ObterTransferenciasPorContaAsync(Guid contaId)
        {
            return await _context.Transferencias
                .Where(t => t.ContaOrigemId == contaId || t.ContaDestinoId == contaId)
                .ToListAsync();
        }
        public async Task SalvarAsync(Movimento movimento)
        {
            _context.Movimentos.Add(movimento);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Movimento>> ObterMovimentosPorContaAsync(Guid contaId)
        {
            return await _context.Movimentos
                .Where(m => m.ContaCorrenteId == contaId)
                .ToListAsync();
        }


    }
}
