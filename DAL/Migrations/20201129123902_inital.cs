using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be678800-f332-4d8b-8e75-9b4eb67367f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4a32ee0-2e43-403d-98af-e9eb37c63998");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "967ce53e-317b-4a70-94b2-717513f4ec7f", "e10df957-8e04-48ae-b5b1-3cbf7a5b4796", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df601814-0df4-4a31-a490-a6d424f24baf", "c4e25e55-8dc1-48ed-aa40-bb2f2b6b915e", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "967ce53e-317b-4a70-94b2-717513f4ec7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df601814-0df4-4a31-a490-a6d424f24baf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "be678800-f332-4d8b-8e75-9b4eb67367f0", "fb94771a-75c4-497c-8f3a-ee0ea22037b1", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c4a32ee0-2e43-403d-98af-e9eb37c63998", "1aa64fd3-9510-4f1c-8f52-d31046543df3", "Administrator", "ADMINISTRATOR" });
        }
    }
}
