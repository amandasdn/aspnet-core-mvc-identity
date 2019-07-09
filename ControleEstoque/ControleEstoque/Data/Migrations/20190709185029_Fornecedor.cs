using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleEstoque.Data.Migrations
{
    public partial class Fornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Desconto",
                table: "Fornecedor",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Fornecedor");
        }
    }
}
