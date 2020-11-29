using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class OptionalRefresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8f2ba65-2471-413b-8179-47cfd25228a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f36ef1ea-9fa9-4b02-8872-545227e1e733");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49858556-aae4-46a4-9507-6ddcd01708e2", "6e605e10-d59c-4e29-829b-8fbd8643d10f", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "658b356c-717e-4a3e-94dc-2158c26d2953", "b099585f-6f03-488b-bc4b-63d9fffe240f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49858556-aae4-46a4-9507-6ddcd01708e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "658b356c-717e-4a3e-94dc-2158c26d2953");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Usuarios",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8f2ba65-2471-413b-8179-47cfd25228a1", "8ded29dd-04c0-45a2-b96b-91e28eea6ef9", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f36ef1ea-9fa9-4b02-8872-545227e1e733", "6f265029-9213-4007-a0a9-d12460c1a55c", "Administrator", "ADMINISTRATOR" });
        }
    }
}
