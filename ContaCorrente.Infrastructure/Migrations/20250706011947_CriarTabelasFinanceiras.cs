using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContaCorrente.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelasFinanceiras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Idempotencias",
                columns: table => new
                {
                    ChaveIdempotencia = table.Column<string>(type: "TEXT", nullable: false),
                    Requisicao = table.Column<string>(type: "TEXT", nullable: true),
                    Resultado = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idempotencias", x => x.ChaveIdempotencia);
                });

            migrationBuilder.CreateTable(
                name: "Movimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaCorrenteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataMovimento = table.Column<string>(type: "TEXT", nullable: false),
                    TipoMovimento = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarifas",
                columns: table => new
                {
                    IdTarifa = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdContaCorrente = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifas", x => x.IdTarifa);
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaOrigemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaDestinoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Idempotencias");

            migrationBuilder.DropTable(
                name: "Movimentos");

            migrationBuilder.DropTable(
                name: "Tarifas");

            migrationBuilder.DropTable(
                name: "Transferencias");
        }
    }
}
