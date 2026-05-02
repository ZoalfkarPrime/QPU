using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyIdToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                schema: "dbo",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacultyId",
                schema: "dbo",
                table: "AspNetUsers",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyId",
                schema: "dbo",
                table: "AspNetUsers",
                column: "FacultyId",
                principalSchema: "dbo",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacultyId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                schema: "dbo",
                table: "AspNetUsers");
        }
    }
}
