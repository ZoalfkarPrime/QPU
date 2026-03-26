using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_table_faculty_add_colors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimaryColor",
                schema: "dbo",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryColor",
                schema: "dbo",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryColor",
                schema: "dbo",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "SecondaryColor",
                schema: "dbo",
                table: "Faculties");
        }
    }
}
