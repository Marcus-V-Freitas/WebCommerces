using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ManipularClassificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataRegistro",
                table: "UsuarioClassificacaoProduto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "PermitirVisualizar",
                table: "UsuarioClassificacaoProduto",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataRegistro",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DropColumn(
                name: "PermitirVisualizar",
                table: "UsuarioClassificacaoProduto");
        }
    }
}
