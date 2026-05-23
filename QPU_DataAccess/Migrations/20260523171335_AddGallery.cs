using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGallery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BestEmployees_FacultyId",
                schema: "dbo",
                table: "BestEmployees");

            migrationBuilder.CreateTable(
                name: "Galleries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Title_AR = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Title_AR = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_AR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryAttachments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryId = table.Column<int>(type: "int", nullable: false),
                    FileManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryAttachments_FileManagers_FileManagerId",
                        column: x => x.FileManagerId,
                        principalSchema: "dbo",
                        principalTable: "FileManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GalleryAttachments_Galleries_GalleryId",
                        column: x => x.GalleryId,
                        principalSchema: "dbo",
                        principalTable: "Galleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteRequests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    VacancyId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    CvFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteRequests_FileManagers_CvFileId",
                        column: x => x.CvFileId,
                        principalSchema: "dbo",
                        principalTable: "FileManagers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SiteRequests_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalSchema: "dbo",
                        principalTable: "Vacancies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryAttachments_FileManagerId",
                schema: "dbo",
                table: "GalleryAttachments",
                column: "FileManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryAttachments_GalleryId",
                schema: "dbo",
                table: "GalleryAttachments",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteRequests_CvFileId",
                schema: "dbo",
                table: "SiteRequests",
                column: "CvFileId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteRequests_VacancyId",
                schema: "dbo",
                table: "SiteRequests",
                column: "VacancyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryAttachments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SiteRequests",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Galleries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Vacancies",
                schema: "dbo");

            migrationBuilder.CreateIndex(
                name: "IX_BestEmployees_FacultyId",
                schema: "dbo",
                table: "BestEmployees",
                column: "FacultyId");
        }
    }
}
