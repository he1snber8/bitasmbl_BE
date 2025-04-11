using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Backend_2024.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class _1703 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14bd6bc1-5f73-429b-a909-4a2069a14cb1");

            migrationBuilder.AddColumn<string>(
                name: "SelectedAndAppliedRequirement",
                table: "ProjectApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "78f3ab38-a4a0-4efd-ab45-83a7ee820faf", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6bb7cbd1-889c-4c97-9386-dd6e2c0269b8", new byte[] { 65, 81, 65, 65, 65, 65, 73, 65, 65, 89, 97, 103, 65, 65, 65, 65, 69, 66, 76, 115, 75, 107, 69, 122, 76, 111, 51, 116, 66, 112, 112, 70, 106, 74, 82, 111, 82, 53, 106, 81, 110, 112, 114, 78, 113, 82, 84, 114, 118, 100, 106, 86, 102, 100, 75, 81, 111, 74, 120, 100, 69, 56, 89, 73, 70, 57, 85, 110, 80, 52, 67, 122, 74, 48, 109, 81, 107, 65, 54, 48, 65, 65, 61, 61 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78f3ab38-a4a0-4efd-ab45-83a7ee820faf");

            migrationBuilder.DropColumn(
                name: "SelectedAndAppliedRequirement",
                table: "ProjectApplications");

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
    }
}
