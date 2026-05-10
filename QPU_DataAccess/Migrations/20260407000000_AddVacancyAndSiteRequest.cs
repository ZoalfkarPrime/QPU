using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    public partial class AddVacancyAndSiteRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacancies",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 300, nullable: false),
                    Title_AR = table.Column<string>(maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_AR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Vacancies", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "SiteRequests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<int>(nullable: false),
                    VacancyId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(nullable: true),
                    PlaceOfBirth = table.Column<string>(maxLength: 300, nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    Nationality = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    MaritalStatus = table.Column<int>(nullable: true),
                    CvFileId = table.Column<Guid>(nullable: true),
                    MessageTitle = table.Column<string>(maxLength: 300, nullable: true),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteRequests", x => x.Id);
                    table.ForeignKey("FK_SiteRequests_Vacancies_VacancyId", x => x.VacancyId, principalSchema: "dbo", principalTable: "Vacancies", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
                    table.ForeignKey("FK_SiteRequests_FileManagers_CvFileId", x => x.CvFileId, principalSchema: "dbo", principalTable: "FileManagers", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex("IX_SiteRequests_VacancyId", schema: "dbo", table: "SiteRequests", column: "VacancyId");
            migrationBuilder.CreateIndex("IX_SiteRequests_CvFileId", schema: "dbo", table: "SiteRequests", column: "CvFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SiteRequests", schema: "dbo");
            migrationBuilder.DropTable(name: "Vacancies", schema: "dbo");
        }
    }
}
