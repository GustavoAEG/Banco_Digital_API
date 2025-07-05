using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContaCorrente.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfToContaCorrente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "CONTACORRENTE",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "CONTACORRENTE");
        }
    }
}
