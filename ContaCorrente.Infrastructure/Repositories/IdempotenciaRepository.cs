using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Repositories;
using ContaCorrente.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class IdempotenciaRepository : IIdempotenciaRepository
{
    private readonly AppDbContext _context;

    public IdempotenciaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> JaProcessadoAsync(string chave)
    {
        return await _context.Idempotencias.AnyAsync(x => x.ChaveIdempotencia == chave);
    }

    public async Task SalvarAsync(Idempotencia idempotencia)
    {
        _context.Idempotencias.Add(idempotencia);
        await _context.SaveChangesAsync();
    }

    public async Task<Idempotencia?> ObterPorChaveAsync(string chave)
    {
        return await _context.Idempotencias.FirstOrDefaultAsync(x => x.ChaveIdempotencia == chave);
    }

    public async Task RegistrarAsync(string chave)
    {
        var idempotencia = new Idempotencia
        {
            ChaveIdempotencia = chave,
            Requisicao = null,
            Resultado = null
        };

        _context.Idempotencias.Add(idempotencia);
        await _context.SaveChangesAsync();
    }
}
