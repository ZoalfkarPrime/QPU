using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    public partial class AddBestEmployeeAndTeacherHasHonor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasHonor",
                schema: "dbo",
                table: "Teachers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BestEmployees",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    StudyYearId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_AR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BestEmployees_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalSchema: "dbo",
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestEmployees_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalSchema: "dbo",
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BestEmployees_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "dbo",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestEmployees_FacultyId",
                schema: "dbo",
                table: "BestEmployees",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_BestEmployees_StudyYearId",
                schema: "dbo",
                table: "BestEmployees",
                column: "StudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_BestEmployees_TeacherId",
                schema: "dbo",
                table: "BestEmployees",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestEmployees",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "HasHonor",
                schema: "dbo",
                table: "Teachers");
        }
    }
}
