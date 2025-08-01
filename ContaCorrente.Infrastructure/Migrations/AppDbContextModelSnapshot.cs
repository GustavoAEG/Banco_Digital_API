﻿// <auto-generated />
using System;
using ContaCorrente.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContaCorrente.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("ContaCorrente.Domain.Entities.ContaCorrente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CONTACORRENTE", (string)null);
                });

            modelBuilder.Entity("ContaCorrente.Domain.Entities.Idempotencia", b =>
                {
                    b.Property<string>("ChaveIdempotencia")
                        .HasColumnType("TEXT");

                    b.Property<string>("Requisicao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resultado")
                        .HasColumnType("TEXT");

                    b.HasKey("ChaveIdempotencia");

                    b.ToTable("Idempotencias");
                });

            modelBuilder.Entity("ContaCorrente.Domain.Entities.Movimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ContaCorrenteId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataMovimento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoMovimento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Movimentos");
                });

            modelBuilder.Entity("ContaCorrente.Domain.Entities.Tarifa", b =>
                {
                    b.Property<Guid>("IdTarifa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataMovimento")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdContaCorrente")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("IdTarifa");

                    b.ToTable("Tarifas");
                });

            modelBuilder.Entity("ContaCorrente.Domain.Entities.Transferencia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ContaDestinoId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ContaOrigemId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataMovimento")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Valor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Transferencias");
                });
#pragma warning restore 612, 618
        }
    }
}
