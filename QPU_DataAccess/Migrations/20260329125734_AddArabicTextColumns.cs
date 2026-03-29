using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddArabicTextColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicDegree_AR",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certificates_AR",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experiences_AR",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position_AR",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScientificDegree_AR",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialist_AR",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                schema: "dbo",
                table: "StudyYears",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                schema: "dbo",
                table: "StudyPrograms",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details_AR",
                schema: "dbo",
                table: "ScientificResearches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_AR",
                schema: "dbo",
                table: "ScientificResearches",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content_AR",
                schema: "dbo",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_AR",
                schema: "dbo",
                table: "Lectures",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content_AR",
                schema: "dbo",
                table: "Labs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                schema: "dbo",
                table: "Labs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName_AR",
                schema: "dbo",
                table: "GraduatedStudents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                schema: "dbo",
                table: "FileManagers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                schema: "dbo",
                table: "Faculties",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_AR",
                schema: "dbo",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_AR",
                schema: "dbo",
                table: "Courses",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value_AR",
                schema: "dbo",
                table: "ContentMetas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicDegree_AR",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Certificates_AR",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Experiences_AR",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Position_AR",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ScientificDegree_AR",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Specialist_AR",
                schema: "dbo",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                schema: "dbo",
                table: "StudyYears");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                schema: "dbo",
                table: "StudyPrograms");

            migrationBuilder.DropColumn(
                name: "Details_AR",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropColumn(
                name: "Title_AR",
                schema: "dbo",
                table: "ScientificResearches");

            migrationBuilder.DropColumn(
                name: "Content_AR",
                schema: "dbo",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Title_AR",
                schema: "dbo",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Content_AR",
                schema: "dbo",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                schema: "dbo",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "FullName_AR",
                schema: "dbo",
                table: "GraduatedStudents");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                schema: "dbo",
                table: "FileManagers");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                schema: "dbo",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "Description_AR",
                schema: "dbo",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Name_AR",
                schema: "dbo",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Value_AR",
                schema: "dbo",
                table: "ContentMetas");
        }
    }
}
