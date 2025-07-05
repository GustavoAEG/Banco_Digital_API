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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContaCorrente.Domain.Entities.ContaCorrente>().ToTable("CONTACORRENTE");

            // mapeie outras entidades/tabelas aqui se quiser

            base.OnModelCreating(modelBuilder);
        }
    }
}
