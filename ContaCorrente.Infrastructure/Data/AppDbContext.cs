using ContaCorrente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ContaCorrente.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<ContaCorrente.Domain.Entities.ContaCorrente> ContasCorrentes { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<Idempotencia> Idempotencias { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaCorrente.Domain.Entities.ContaCorrente>().ToTable("CONTACORRENTE");

            modelBuilder.Entity<Idempotencia>().HasKey(i => i.ChaveIdempotencia);

            modelBuilder.Entity<Tarifa>().HasKey(t => t.IdTarifa);

            modelBuilder.Entity<Transferencia>().HasKey(t => t.Id);

            modelBuilder.Entity<Movimento>().HasKey(m => m.Id);


            base.OnModelCreating(modelBuilder);
        }
    }
}
