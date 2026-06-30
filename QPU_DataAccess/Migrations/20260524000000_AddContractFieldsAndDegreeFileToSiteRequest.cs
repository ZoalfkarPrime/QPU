using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddContractFieldsAndDegreeFileToSiteRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractFacultyId",
                schema: "dbo",
                table: "SiteRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractScientificDegree",
                schema: "dbo",
                table: "SiteRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractSpecialist",
                schema: "dbo",
                table: "SiteRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractJob",
                schema: "dbo",
                table: "SiteRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasContractScientificDegreeApproved",
                schema: "dbo",
                table: "SiteRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasContractExperience",
                schema: "dbo",
                table: "SiteRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractExperiences",
                schema: "dbo",
                table: "SiteRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractLanguages",
                schema: "dbo",
                table: "SiteRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractCurrentPlace",
                schema: "dbo",
                table: "SiteRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ContractFulltimeJob",
                schema: "dbo",
                table: "SiteRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasContractAnotherJob",
                schema: "dbo",
                table: "SiteRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DegreeFileId",
                schema: "dbo",
                table: "SiteRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteRequests_ContractFacultyId",
                schema: "dbo",
                table: "SiteRequests",
                column: "ContractFacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteRequests_DegreeFileId",
                schema: "dbo",
                table: "SiteRequests",
                column: "DegreeFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteRequests_Faculties_ContractFacultyId",
                schema: "dbo",
                table: "SiteRequests",
                column: "ContractFacultyId",
                principalSchema: "dbo",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteRequests_FileManagers_DegreeFileId",
                schema: "dbo",
                table: "SiteRequests",
                column: "DegreeFileId",
                principalSchema: "dbo",
                principalTable: "FileManagers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteRequests_Faculties_ContractFacultyId",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteRequests_FileManagers_DegreeFileId",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropIndex(
                name: "IX_SiteRequests_ContractFacultyId",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropIndex(
                name: "IX_SiteRequests_DegreeFileId",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "ContractFacultyId",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "ContractScientificDegree",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "ContractSpecialist",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "ContractJob",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "HasContractScientificDegreeApproved",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "HasContractExperience",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "ContractExperiences",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "ContractLanguages",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "ContractCurrentPlace",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "ContractFulltimeJob",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "HasContractAnotherJob",
                schema: "dbo",
                table: "SiteRequests");

            migrationBuilder.DropColumn(
                name: "DegreeFileId",
                schema: "dbo",
                table: "SiteRequests");
        }
    }
}
