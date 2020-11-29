using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ProdutoClassificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioClassificacaoProduto_Produtos_ProdutoID",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioClassificacaoProduto_ProdutoID",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "070967e6-9c54-4382-b3e8-f8531f3549a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83d03dd1-3b83-4c21-9001-9eb8a6b3ee52");

            migrationBuilder.DropColumn(
                name: "ProdutoID",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoClassificacaoId",
                table: "UsuarioClassificacaoProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "715661e7-8a71-4d2d-80d9-a1c5285b01ea", "cc11f846-dc5d-4049-a59b-9747421dcdd4", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3078c79d-3008-4b47-bf6a-0f9964a46461", "5115faaa-5688-4dd1-86fd-57789c17e13b", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioClassificacaoProduto_ProdutoClassificacaoId",
                table: "UsuarioClassificacaoProduto",
                column: "ProdutoClassificacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioClassificacaoProduto_Produtos_ProdutoClassificacaoId",
                table: "UsuarioClassificacaoProduto",
                column: "ProdutoClassificacaoId",
                principalTable: "Produtos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioClassificacaoProduto_Produtos_ProdutoClassificacaoId",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioClassificacaoProduto_ProdutoClassificacaoId",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3078c79d-3008-4b47-bf6a-0f9964a46461");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "715661e7-8a71-4d2d-80d9-a1c5285b01ea");

            migrationBuilder.DropColumn(
                name: "ProdutoClassificacaoId",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoID",
                table: "UsuarioClassificacaoProduto",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "070967e6-9c54-4382-b3e8-f8531f3549a6", "fc3b819b-b008-4158-86ed-feab37a79632", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83d03dd1-3b83-4c21-9001-9eb8a6b3ee52", "0b9e100f-ef17-459a-b7a4-308b985f2a48", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioClassificacaoProduto_ProdutoID",
                table: "UsuarioClassificacaoProduto",
                column: "ProdutoID");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioClassificacaoProduto_Produtos_ProdutoID",
                table: "UsuarioClassificacaoProduto",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
