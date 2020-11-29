using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class removeCustomRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesUser");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19ab0a76-d256-498d-9b14-ba4f5bdcbc95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce2666ff-20c7-4fb4-b33a-a789abb1fe3d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "be678800-f332-4d8b-8e75-9b4eb67367f0", "fb94771a-75c4-497c-8f3a-ee0ea22037b1", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c4a32ee0-2e43-403d-98af-e9eb37c63998", "1aa64fd3-9510-4f1c-8f52-d31046543df3", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be678800-f332-4d8b-8e75-9b4eb67367f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4a32ee0-2e43-403d-98af-e9eb37c63998");

            migrationBuilder.CreateTable(
                name: "RolesUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativa = table.Column<bool>(type: "bit", nullable: false),
                    IDUsuarioRole = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RolesUser_Usuarios_IDUsuarioRole",
                        column: x => x.IDUsuarioRole,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19ab0a76-d256-498d-9b14-ba4f5bdcbc95", "7cd0dc83-2b90-4c59-8ddc-3b3f5b247d71", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce2666ff-20c7-4fb4-b33a-a789abb1fe3d", "7e35681b-b69e-4c75-b891-37aee0528588", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_RolesUser_IDUsuarioRole",
                table: "RolesUser",
                column: "IDUsuarioRole");
        }
    }
}
