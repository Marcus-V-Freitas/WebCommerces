using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UsuarioEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoEntrega_Usuarios_UsuarioId",
                table: "EnderecoEntrega");

            migrationBuilder.DropIndex(
                name: "IX_EnderecoEntrega_UsuarioId",
                table: "EnderecoEntrega");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3078c79d-3008-4b47-bf6a-0f9964a46461");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "715661e7-8a71-4d2d-80d9-a1c5285b01ea");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "EnderecoEntrega");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioEnderecoId",
                table: "EnderecoEntrega",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoEntrega_UsuarioEnderecoId",
                table: "EnderecoEntrega",
                column: "UsuarioEnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoEntrega_Usuarios_UsuarioEnderecoId",
                table: "EnderecoEntrega",
                column: "UsuarioEnderecoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoEntrega_Usuarios_UsuarioEnderecoId",
                table: "EnderecoEntrega");

            migrationBuilder.DropIndex(
                name: "IX_EnderecoEntrega_UsuarioEnderecoId",
                table: "EnderecoEntrega");

            migrationBuilder.DropColumn(
                name: "UsuarioEnderecoId",
                table: "EnderecoEntrega");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "EnderecoEntrega",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "715661e7-8a71-4d2d-80d9-a1c5285b01ea", "cc11f846-dc5d-4049-a59b-9747421dcdd4", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3078c79d-3008-4b47-bf6a-0f9964a46461", "5115faaa-5688-4dd1-86fd-57789c17e13b", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoEntrega_UsuarioId",
                table: "EnderecoEntrega",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoEntrega_Usuarios_UsuarioId",
                table: "EnderecoEntrega",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
