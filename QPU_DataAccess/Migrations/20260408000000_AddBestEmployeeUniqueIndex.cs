using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBestEmployeeUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BestEmployees_FacultyId_StudyYearId",
                schema: "dbo",
                table: "BestEmployees",
                columns: new[] { "FacultyId", "StudyYearId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BestEmployees_FacultyId_StudyYearId",
                schema: "dbo",
                table: "BestEmployees");
        }
    }
}
