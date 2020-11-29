using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcasCartaoCredito",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasCartaoCredito", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPFCNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    Largura = table.Column<int>(type: "int", nullable: false),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    Comprimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnderecoEntrega",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoEntrega", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnderecoEntrega_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioPedidoId = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FreteEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreteCodRastreamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormaPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DadosTransaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DadosProdutos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NFE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_UsuarioPedidoId",
                        column: x => x.UsuarioPedidoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDUsuarioRole = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Roles_Usuarios_IDUsuarioRole",
                        column: x => x.IDUsuarioRole,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disponivel = table.Column<int>(type: "int", nullable: false),
                    ProdutoEstoqueId = table.Column<int>(type: "int", nullable: false),
                    Transitorio = table.Column<int>(type: "int", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantidadeMinima = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Estoques_Produtos_ProdutoEstoqueId",
                        column: x => x.ProdutoEstoqueId,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoSituacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dados = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoSituacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoSituacoes_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Horario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuarioVendaID = table.Column<int>(type: "int", nullable: false),
                    IDPedidoVenda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendas_Pedidos_IDPedidoVenda",
                        column: x => x.IDPedidoVenda,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendas_Usuarios_usuarioVendaID",
                        column: x => x.usuarioVendaID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsVenda",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preço = table.Column<double>(type: "float", nullable: false),
                    VendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsVenda", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemsVenda_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsVenda_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimentacoesEstoque",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoMovId = table.Column<int>(type: "int", nullable: false),
                    MovUsuarioID = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DataMovimentacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendaMovID = table.Column<int>(type: "int", nullable: true),
                    StatusMovimetacao = table.Column<int>(type: "int", nullable: false),
                    TipoMovimentacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesEstoque", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Produtos_ProdutoMovId",
                        column: x => x.ProdutoMovId,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Usuarios_MovUsuarioID",
                        column: x => x.MovUsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Vendas_VendaMovID",
                        column: x => x.VendaMovID,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoEntrega_UsuarioId",
                table: "EnderecoEntrega",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoEstoqueId",
                table: "Estoques",
                column: "ProdutoEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsVenda_ProdutoId",
                table: "ItemsVenda",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsVenda_VendaId",
                table: "ItemsVenda",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_MovUsuarioID",
                table: "MovimentacoesEstoque",
                column: "MovUsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_ProdutoMovId",
                table: "MovimentacoesEstoque",
                column: "ProdutoMovId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_VendaMovID",
                table: "MovimentacoesEstoque",
                column: "VendaMovID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioPedidoId",
                table: "Pedidos",
                column: "UsuarioPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoSituacoes_PedidoId",
                table: "PedidoSituacoes",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IDUsuarioRole",
                table: "Roles",
                column: "IDUsuarioRole");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_IDPedidoVenda",
                table: "Vendas",
                column: "IDPedidoVenda");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_usuarioVendaID",
                table: "Vendas",
                column: "usuarioVendaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnderecoEntrega");

            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "ItemsVenda");

            migrationBuilder.DropTable(
                name: "MarcasCartaoCredito");

            migrationBuilder.DropTable(
                name: "MovimentacoesEstoque");

            migrationBuilder.DropTable(
                name: "PedidoSituacoes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
