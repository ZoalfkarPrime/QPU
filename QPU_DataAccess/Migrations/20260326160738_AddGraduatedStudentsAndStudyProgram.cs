using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGraduatedStudentsAndStudyProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GraduatedStudents",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyYearId = table.Column<int>(type: "int", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Average = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraduatedStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GraduatedStudents_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalSchema: "dbo",
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GraduatedStudents_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalSchema: "dbo",
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyPrograms",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyYearId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPrograms_FileManagers_FileId",
                        column: x => x.FileId,
                        principalSchema: "dbo",
                        principalTable: "FileManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyPrograms_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalSchema: "dbo",
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GraduatedStudents_FacultyId",
                schema: "dbo",
                table: "GraduatedStudents",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_GraduatedStudents_StudyYearId",
                schema: "dbo",
                table: "GraduatedStudents",
                column: "StudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPrograms_FileId",
                schema: "dbo",
                table: "StudyPrograms",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPrograms_StudyYearId",
                schema: "dbo",
                table: "StudyPrograms",
                column: "StudyYearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GraduatedStudents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudyPrograms",
                schema: "dbo");
        }
    }
}
