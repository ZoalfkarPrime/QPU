using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityAndNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                schema: "dbo",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Teachers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "dbo",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "ScientificResearches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                schema: "dbo",
                table: "ScientificResearches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "ScientificResearches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StudyYearId",
                schema: "dbo",
                table: "ScientificResearches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "dbo",
                table: "ScientificResearches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "Labs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Labs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "dbo",
                table: "Labs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "dbo",
                table: "FileManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                schema: "dbo",
                table: "FileManagers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "FileManagers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "dbo",
                table: "FileManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Faculties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LogoId",
                schema: "dbo",
                table: "Faculties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "dbo",
                table: "Faculties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "FacultyTeachers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyTeachers_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalSchema: "dbo",
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "dbo",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyYears",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    StudyYearId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalSchema: "dbo",
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_StudyYears_StudyYearId",
                        column: x => x.StudyYearId,
                        principalSchema: "dbo",
                        principalTable: "StudyYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseTeachers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTeachers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "dbo",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "dbo",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LectureNumber = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "dbo",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lectures_FileManagers_FileId",
                        column: x => x.FileId,
                        principalSchema: "dbo",
                        principalTable: "FileManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lectures_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "dbo",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScientificResearches_StudyYearId",
                schema: "dbo",
                table: "ScientificResearches",
                column: "StudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_LogoId",
                schema: "dbo",
                table: "Faculties",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FacultyId",
                schema: "dbo",
                table: "Courses",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudyYearId",
                schema: "dbo",
                table: "Courses",
                column: "StudyYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeachers_CourseId_TeacherId",
                schema: "dbo",
                table: "CourseTeachers",
                columns: new[] { "CourseId", "TeacherId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeachers_TeacherId",
                schema: "dbo",
                table: "CourseTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTeachers_FacultyId_TeacherId",
                schema: "dbo",
                table: "FacultyTeachers",
                columns: new[] { "FacultyId", "TeacherId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacultyTeachers_TeacherId",
                schema: "dbo",
                table: "FacultyTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_CourseId",
                schema: "dbo",
                table: "Lectures",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_FileId",
                schema: "dbo",
                table: "Lectures",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_TeacherId",
                schema: "dbo",
                table: "Lectures",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_FileManagers_LogoId",
                schema: "dbo",
                table: "Faculties",
                column: "LogoId",
                principalSchema: "dbo",
                principalTable: "FileManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScientificResearches_StudyYears_StudyYearId",
                schema: "dbo",
                table: "ScientificResearches",
                column: "StudyYearId",
                principalSchema: "dbo",
                principalTable: "StudyYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_FileManagers_LogoId",
                schema: "dbo",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_ScientificResearches_StudyYears_StudyYearId",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropTable(
                name: "CourseTeachers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FacultyTeachers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Lectures",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudyYears",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_ScientificResearches_StudyYearId",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_LogoId",
                schema: "dbo",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropColumn(
                name: "StudyYearId",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "dbo",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "dbo",
                table: "FileManagers");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                schema: "dbo",
                table: "FileManagers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "FileManagers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "dbo",
                table: "FileManagers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "LogoId",
                schema: "dbo",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "dbo",
                table: "Faculties");
        }
    }
}
