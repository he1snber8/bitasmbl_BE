using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Backend_2024.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class _1203 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "958623c0-eb79-4153-8a74-35d359c16470");

            migrationBuilder.AddColumn<bool>(
                name: "IsTestEnabled",
                table: "ProjectRequirements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    PaypalTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5ad8297-9145-445f-a4e3-99d8c123e0f3", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f91190b1-336d-4f0b-a4e1-6b6c92115240", new byte[] { 65, 81, 65, 65, 65, 65, 73, 65, 65, 89, 97, 103, 65, 65, 65, 65, 69, 76, 53, 107, 122, 100, 80, 71, 100, 51, 119, 99, 50, 118, 98, 73, 70, 113, 43, 106, 100, 49, 69, 87, 90, 87, 111, 120, 115, 106, 98, 57, 71, 57, 107, 75, 65, 110, 74, 110, 108, 105, 118, 74, 97, 110, 65, 74, 105, 101, 102, 105, 106, 102, 54, 98, 113, 76, 72, 115, 116, 57, 85, 69, 65, 81, 61, 61 } });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId1",
                table: "Transaction",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5ad8297-9145-445f-a4e3-99d8c123e0f3");

            migrationBuilder.DropColumn(
                name: "IsTestEnabled",
                table: "ProjectRequirements");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "958623c0-eb79-4153-8a74-35d359c16470", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bfcd6dba-c761-49a2-83e5-56f87cc33c56", new byte[] { 65, 81, 65, 65, 65, 65, 73, 65, 65, 89, 97, 103, 65, 65, 65, 65, 69, 76, 108, 107, 118, 121, 70, 66, 74, 69, 107, 89, 56, 78, 72, 97, 108, 47, 114, 72, 111, 115, 117, 90, 67, 68, 57, 82, 55, 90, 117, 85, 88, 49, 104, 110, 77, 74, 115, 120, 102, 70, 98, 120, 48, 87, 55, 70, 73, 48, 66, 53, 97, 68, 51, 101, 119, 56, 43, 57, 66, 108, 47, 72, 100, 81, 61, 61 } });
        }
    }
}
