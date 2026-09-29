using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToFacultyRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "StudyPrograms",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "ScientificResearches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Lectures",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Labs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "GraduatedStudents",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Galleries",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Courses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            // Backfill slugs for existing rows so the unique indexes can be created
            migrationBuilder.Sql("UPDATE dbo.Teachers SET Slug = CONCAT('teacher-', Id) WHERE Slug = N'';");
            migrationBuilder.Sql("UPDATE dbo.StudyPrograms SET Slug = CONCAT('study-program-', Id) WHERE Slug = N'';");
            migrationBuilder.Sql("UPDATE dbo.ScientificResearches SET Slug = CONCAT('research-', Id) WHERE Slug = N'';");
            migrationBuilder.Sql("UPDATE dbo.Lectures SET Slug = CONCAT('lecture-', Id) WHERE Slug = N'';");
            migrationBuilder.Sql("UPDATE dbo.Labs SET Slug = CONCAT('lab-', Id) WHERE Slug = N'';");
            migrationBuilder.Sql("UPDATE dbo.GraduatedStudents SET Slug = CONCAT('graduate-', Id) WHERE Slug = N'';");
            migrationBuilder.Sql("UPDATE dbo.Galleries SET Slug = CONCAT('gallery-', Id) WHERE Slug = N'';");
            migrationBuilder.Sql("UPDATE dbo.Courses SET Slug = CONCAT('course-', Id) WHERE Slug = N'';");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Slug",
                schema: "dbo",
                table: "Teachers",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyPrograms_Slug",
                schema: "dbo",
                table: "StudyPrograms",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScientificResearches_Slug",
                schema: "dbo",
                table: "ScientificResearches",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_Slug",
                schema: "dbo",
                table: "Lectures",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labs_Slug",
                schema: "dbo",
                table: "Labs",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GraduatedStudents_Slug",
                schema: "dbo",
                table: "GraduatedStudents",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_Slug",
                schema: "dbo",
                table: "Galleries",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Slug",
                schema: "dbo",
                table: "Courses",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teachers_Slug",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_StudyPrograms_Slug",
                schema: "dbo",
                table: "StudyPrograms");

            migrationBuilder.DropIndex(
                name: "IX_ScientificResearches_Slug",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_Slug",
                schema: "dbo",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Labs_Slug",
                schema: "dbo",
                table: "Labs");

            migrationBuilder.DropIndex(
                name: "IX_GraduatedStudents_Slug",
                schema: "dbo",
                table: "GraduatedStudents");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_Slug",
                schema: "dbo",
                table: "Galleries");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Slug",
                schema: "dbo",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "dbo",
                table: "StudyPrograms");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "dbo",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "dbo",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "dbo",
                table: "GraduatedStudents");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "dbo",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "dbo",
                table: "Courses");
        }
    }
}
