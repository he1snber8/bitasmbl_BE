using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Backend_2024.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class _1503 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d01ed1dd-2fdf-4a8a-9d20-e85d090e64f0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14bd6bc1-5f73-429b-a909-4a2069a14cb1", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "328b0a22-f22b-4665-9cd8-5f12ca5176aa", new byte[] { 65, 81, 65, 65, 65, 65, 73, 65, 65, 89, 97, 103, 65, 65, 65, 65, 69, 67, 89, 78, 67, 54, 119, 85, 88, 53, 110, 52, 82, 75, 104, 83, 80, 53, 99, 89, 101, 98, 98, 79, 118, 78, 112, 110, 77, 48, 56, 102, 81, 76, 110, 47, 83, 100, 115, 90, 99, 100, 50, 101, 83, 103, 82, 122, 49, 99, 114, 71, 83, 49, 78, 66, 66, 65, 43, 51, 75, 119, 79, 74, 57, 103, 61, 61 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14bd6bc1-5f73-429b-a909-4a2069a14cb1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d01ed1dd-2fdf-4a8a-9d20-e85d090e64f0", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "82d25eae-a3c6-4aaa-ac01-aa83370e2ad6", new byte[] { 65, 81, 65, 65, 65, 65, 73, 65, 65, 89, 97, 103, 65, 65, 65, 65, 69, 75, 112, 74, 65, 100, 71, 114, 88, 78, 49, 73, 47, 47, 122, 50, 72, 99, 117, 97, 50, 66, 79, 82, 79, 83, 106, 81, 74, 48, 65, 69, 108, 53, 57, 52, 113, 103, 47, 72, 98, 107, 78, 98, 68, 51, 80, 120, 86, 117, 100, 98, 113, 98, 113, 77, 109, 56, 103, 71, 86, 99, 82, 90, 114, 65, 61, 61 } });
        }
    }
}
