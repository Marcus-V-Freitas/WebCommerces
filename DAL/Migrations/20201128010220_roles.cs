using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19ab0a76-d256-498d-9b14-ba4f5bdcbc95", "7cd0dc83-2b90-4c59-8ddc-3b3f5b247d71", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce2666ff-20c7-4fb4-b33a-a789abb1fe3d", "7e35681b-b69e-4c75-b891-37aee0528588", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19ab0a76-d256-498d-9b14-ba4f5bdcbc95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce2666ff-20c7-4fb4-b33a-a789abb1fe3d");
        }
    }
}
