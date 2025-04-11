using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Backend_2024.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class _1703v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78f3ab38-a4a0-4efd-ab45-83a7ee820faf");

            migrationBuilder.RenameColumn(
                name: "SelectedAndAppliedRequirement",
                table: "ProjectApplications",
                newName: "SelectedAndAppliedRequirements");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0bb182a2-8289-4441-92ac-9e84e5e143f6", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7be08614-064d-42a6-8900-bff9e737397e", new byte[] { 65, 81, 65, 65, 65, 65, 73, 65, 65, 89, 97, 103, 65, 65, 65, 65, 69, 66, 70, 106, 48, 65, 88, 81, 49, 68, 115, 67, 51, 78, 90, 90, 114, 72, 106, 113, 106, 48, 47, 70, 81, 116, 119, 103, 112, 84, 57, 43, 109, 103, 84, 80, 117, 81, 111, 120, 116, 78, 88, 113, 111, 111, 122, 48, 117, 87, 43, 54, 67, 116, 76, 52, 67, 49, 120, 86, 78, 122, 80, 106, 57, 119, 61, 61 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bb182a2-8289-4441-92ac-9e84e5e143f6");

            migrationBuilder.RenameColumn(
                name: "SelectedAndAppliedRequirements",
                table: "ProjectApplications",
                newName: "SelectedAndAppliedRequirement");

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
    }
}
