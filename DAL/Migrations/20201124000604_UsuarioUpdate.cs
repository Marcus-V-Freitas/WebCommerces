using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UsuarioUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioClassificacaoId",
                table: "UsuarioClassificacaoProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioClassificacaoProduto_UsuarioClassificacaoId",
                table: "UsuarioClassificacaoProduto",
                column: "UsuarioClassificacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioClassificacaoProduto_Usuarios_UsuarioClassificacaoId",
                table: "UsuarioClassificacaoProduto",
                column: "UsuarioClassificacaoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioClassificacaoProduto_Usuarios_UsuarioClassificacaoId",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioClassificacaoProduto_UsuarioClassificacaoId",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DropColumn(
                name: "UsuarioClassificacaoId",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "UsuarioClassificacaoProduto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
