using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Backend_2024.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class _1403 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "915e77b8-829c-4c54-b70b-7f4999ea79d1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<short>(
                name: "Balance",
                table: "AspNetUsers",
                type: "smallint",
                nullable: false,
                defaultValue: (short)15);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d01ed1dd-2fdf-4a8a-9d20-e85d090e64f0");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "915e77b8-829c-4c54-b70b-7f4999ea79d1", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2dbbc433-fa63-4487-a100-22238572bc61", new byte[] { 65, 81, 65, 65, 65, 65, 73, 65, 65, 89, 97, 103, 65, 65, 65, 65, 69, 71, 98, 71, 77, 66, 74, 47, 73, 72, 67, 109, 115, 98, 88, 76, 77, 103, 98, 122, 116, 70, 79, 78, 113, 51, 65, 83, 106, 49, 52, 68, 70, 86, 122, 76, 74, 57, 109, 73, 118, 118, 90, 85, 51, 55, 65, 81, 71, 100, 90, 43, 72, 104, 54, 75, 52, 113, 114, 75, 115, 71, 103, 102, 89, 119, 61, 61 } });
        }
    }
}
