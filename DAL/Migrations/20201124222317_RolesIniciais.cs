using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RolesIniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "65989cdc-f6ed-4599-afe7-4b9e14f0e1e1", "dcbb002b-27d7-4a06-9901-897b812da417", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28b8c73b-5400-4e5a-ad37-859d41ba41c2", "0bd6f4d2-6fc2-40a7-a45e-ba2ae94615a6", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28b8c73b-5400-4e5a-ad37-859d41ba41c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65989cdc-f6ed-4599-afe7-4b9e14f0e1e1");
        }
    }
}
