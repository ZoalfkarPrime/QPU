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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryAttachments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Galleries",
                schema: "dbo");
        }
    }
}
