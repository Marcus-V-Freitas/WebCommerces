using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Classificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProdutosClassificacao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoClassificacaoID = table.Column<int>(type: "int", nullable: false),
                    MediaProduto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosClassificacao", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProdutosClassificacao_Produtos_ProdutoClassificacaoID",
                        column: x => x.ProdutoClassificacaoID,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioClassificacaoProduto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classificacao = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProdutoClassificacaoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioClassificacaoProduto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UsuarioClassificacaoProduto_ProdutosClassificacao_ProdutoClassificacaoID",
                        column: x => x.ProdutoClassificacaoID,
                        principalTable: "ProdutosClassificacao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosClassificacao_ProdutoClassificacaoID",
                table: "ProdutosClassificacao",
                column: "ProdutoClassificacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioClassificacaoProduto_ProdutoClassificacaoID",
                table: "UsuarioClassificacaoProduto",
                column: "ProdutoClassificacaoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioClassificacaoProduto");

            migrationBuilder.DropTable(
                name: "ProdutosClassificacao");
        }
    }
}
