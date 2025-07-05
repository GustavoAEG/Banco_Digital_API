using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Repositories;
using ContaCorrente.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContaCorrente.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly AppDbContext _context;

        public ContaCorrenteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ContaCorrente.Domain.Entities.ContaCorrente?> ObterContaPorCpfAsync(string cpf)
        {
            return await _context.ContasCorrentes.FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<ContaCorrente.Domain.Entities.ContaCorrente?> ObterContaPorNumeroAsync(string numeroConta)
        {
            return await _context.ContasCorrentes.FirstOrDefaultAsync(c => c.Numero.ToString() == numeroConta);
        }

        public async Task<ContaCorrente.Domain.Entities.ContaCorrente?> ObterPorIdAsync(Guid id)
        {
            return await _context.ContasCorrentes.FindAsync(id);
        }

        public async Task<bool> ExisteContaComNumeroAsync(int numero)
        {
            return await _context.ContasCorrentes.AnyAsync(c => c.Numero == numero);
        }

        public async Task CriarAsync(ContaCorrente.Domain.Entities.ContaCorrente conta)
        {
            await _context.ContasCorrentes.AddAsync(conta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ContaCorrente.Domain.Entities.ContaCorrente conta)
        {
            _context.ContasCorrentes.Update(conta);
            await _context.SaveChangesAsync();
        }
    }
}
