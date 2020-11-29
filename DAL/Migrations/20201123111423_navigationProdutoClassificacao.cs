using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class navigationProdutoClassificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProdutosClassificacao_ProdutoClassificacaoID",
                table: "ProdutosClassificacao");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoClassificacaoId",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosClassificacao_ProdutoClassificacaoID",
                table: "ProdutosClassificacao",
                column: "ProdutoClassificacaoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ProdutoClassificacaoId",
                table: "Produtos",
                column: "ProdutoClassificacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_ProdutosClassificacao_ProdutoClassificacaoId",
                table: "Produtos",
                column: "ProdutoClassificacaoId",
                principalTable: "ProdutosClassificacao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_ProdutosClassificacao_ProdutoClassificacaoId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_ProdutosClassificacao_ProdutoClassificacaoID",
                table: "ProdutosClassificacao");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_ProdutoClassificacaoId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ProdutoClassificacaoId",
                table: "Produtos");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosClassificacao_ProdutoClassificacaoID",
                table: "ProdutosClassificacao",
                column: "ProdutoClassificacaoID");
        }
    }
}
