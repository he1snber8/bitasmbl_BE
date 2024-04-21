using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Backend_2024.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Username = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "varbinary", maxLength: 35, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Picture = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Bio = table.Column<string>(type: "NVARCHAR", maxLength: 255, nullable: true),
                    DateJoined = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    LastLogin = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    PrincipalID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_PrincipalID",
                        column: x => x.PrincipalID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UserSkills_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkills_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedProjects",
                columns: table => new
                {
                    ApplicationStatus = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    DateApplied = table.Column<DateTime>(type: "datetime", nullable: true),
                    CoverLetter = table.Column<string>(type: "nvarchar", maxLength: 255, nullable: true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_AppliedProjects_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedProjects_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedProjects_ProjectID_UserID",
                table: "AppliedProjects",
                columns: new[] { "ProjectID", "UserID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppliedProjects_UserID",
                table: "AppliedProjects",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PrincipalID",
                table: "Projects",
                column: "PrincipalID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_SkillID",
                table: "UserSkills",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserID_SkillID",
                table: "UserSkills",
                columns: new[] { "UserID", "SkillID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedProjects");

            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
