using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class simplificarClassificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_ProdutosClassificacao_ProdutoClassificacaoId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioClassificacaoProduto_ProdutosClassificacao_ProdutoClassificacaoID",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DropTable(
                name: "ProdutosClassificacao");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_ProdutoClassificacaoId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ProdutoClassificacaoId",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "ProdutoClassificacaoID",
                table: "UsuarioClassificacaoProduto",
                newName: "ProdutoID");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioClassificacaoProduto_ProdutoClassificacaoID",
                table: "UsuarioClassificacaoProduto",
                newName: "IX_UsuarioClassificacaoProduto_ProdutoID");

            migrationBuilder.AddColumn<int>(
                name: "MediaProduto",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioClassificacaoProduto_Produtos_ProdutoID",
                table: "UsuarioClassificacaoProduto",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioClassificacaoProduto_Produtos_ProdutoID",
                table: "UsuarioClassificacaoProduto");

            migrationBuilder.DropColumn(
                name: "MediaProduto",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "ProdutoID",
                table: "UsuarioClassificacaoProduto",
                newName: "ProdutoClassificacaoID");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioClassificacaoProduto_ProdutoID",
                table: "UsuarioClassificacaoProduto",
                newName: "IX_UsuarioClassificacaoProduto_ProdutoClassificacaoID");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoClassificacaoId",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProdutosClassificacao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaProduto = table.Column<int>(type: "int", nullable: false),
                    ProdutoClassificacaoID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ProdutoClassificacaoId",
                table: "Produtos",
                column: "ProdutoClassificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosClassificacao_ProdutoClassificacaoID",
                table: "ProdutosClassificacao",
                column: "ProdutoClassificacaoID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_ProdutosClassificacao_ProdutoClassificacaoId",
                table: "Produtos",
                column: "ProdutoClassificacaoId",
                principalTable: "ProdutosClassificacao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioClassificacaoProduto_ProdutosClassificacao_ProdutoClassificacaoID",
                table: "UsuarioClassificacaoProduto",
                column: "ProdutoClassificacaoID",
                principalTable: "ProdutosClassificacao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
