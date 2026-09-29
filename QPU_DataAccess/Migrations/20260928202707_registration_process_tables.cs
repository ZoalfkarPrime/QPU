using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class registration_process_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "StudyPrograms",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "ScientificResearches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Lectures",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Labs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "GraduatedStudents",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Galleries",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Courses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.CreateTable(
                name: "AdmissionTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name_AR = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamSessions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name_AR = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HighSchoolCertificateTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name_AR = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighSchoolCertificateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name_AR = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentRegistrations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    NationalNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdentityNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegistrationPlace = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
                    AdmissionTypeId = table.Column<int>(type: "int", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentRegistrations_AdmissionTypes_AdmissionTypeId",
                        column: x => x.AdmissionTypeId,
                        principalSchema: "dbo",
                        principalTable: "AdmissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentRegistrations_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalSchema: "dbo",
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentRegistrations_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalSchema: "dbo",
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentHighSchoolCertificates",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentRegistrationId = table.Column<int>(type: "int", nullable: false),
                    CertificateTypeId = table.Column<int>(type: "int", nullable: false),
                    CertificateSource = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CertificatePlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CertificateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CertificateOrSubscriptionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExamSessionId = table.Column<int>(type: "int", nullable: true),
                    GeneralTotal = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Average = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    AdmissionAverageAfterLanguageExclusion = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHighSchoolCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHighSchoolCertificates_ExamSessions_ExamSessionId",
                        column: x => x.ExamSessionId,
                        principalSchema: "dbo",
                        principalTable: "ExamSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentHighSchoolCertificates_HighSchoolCertificateTypes_CertificateTypeId",
                        column: x => x.CertificateTypeId,
                        principalSchema: "dbo",
                        principalTable: "HighSchoolCertificateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentHighSchoolCertificates_StudentRegistrations_StudentRegistrationId",
                        column: x => x.StudentRegistrationId,
                        principalSchema: "dbo",
                        principalTable: "StudentRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AdmissionTypes",
                columns: new[] { "Id", "CreatedAt", "DisplayOrder", "IsActive", "Name", "Name_AR", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "General", "عامة", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Vacancy Filling", "ملء شواغر", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Equivalent Transfer from Syrian Universities", "تحويل مماثل من جامعات سورية", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Equivalent Transfer from Non-Syrian Universities", "تحويل مماثل من جامعات غير سورية", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Change of Registration from Syrian Universities", "تغيير قيد من جامعات سوريا", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Change of Registration from Non-Syrian Universities", "تغيير قيد من جامعات غير سورية", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Institutes and Universities Comparative Admission", "مفاضلة المعاهد والجامعات", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ExamSessions",
                columns: new[] { "Id", "CreatedAt", "DisplayOrder", "IsActive", "Name", "Name_AR", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "First Session", "الدورة الأولى", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Second Session", "الدورة الثانية", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "HighSchoolCertificateTypes",
                columns: new[] { "Id", "CreatedAt", "DisplayOrder", "IsActive", "Name", "Name_AR", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Scientific", "علمي", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Literary", "أدبي", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Technical", "تقني", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Vocational", "فني", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Other", "أخرى", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Slug",
                schema: "dbo",
                table: "Teachers",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPrograms_Slug",
                schema: "dbo",
                table: "StudyPrograms",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificResearches_Slug",
                schema: "dbo",
                table: "ScientificResearches",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_Slug",
                schema: "dbo",
                table: "Lectures",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Labs_Slug",
                schema: "dbo",
                table: "Labs",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GraduatedStudents_Slug",
                schema: "dbo",
                table: "GraduatedStudents",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_Slug",
                schema: "dbo",
                table: "Galleries",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Slug",
                schema: "dbo",
                table: "Courses",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHighSchoolCertificates_CertificateTypeId",
                schema: "dbo",
                table: "StudentHighSchoolCertificates",
                column: "CertificateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHighSchoolCertificates_ExamSessionId",
                schema: "dbo",
                table: "StudentHighSchoolCertificates",
                column: "ExamSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHighSchoolCertificates_StudentRegistrationId",
                schema: "dbo",
                table: "StudentHighSchoolCertificates",
                column: "StudentRegistrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistrations_AdmissionTypeId",
                schema: "dbo",
                table: "StudentRegistrations",
                column: "AdmissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistrations_ApplicationNumber",
                schema: "dbo",
                table: "StudentRegistrations",
                column: "ApplicationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistrations_FacultyId",
                schema: "dbo",
                table: "StudentRegistrations",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistrations_NationalNumber",
                schema: "dbo",
                table: "StudentRegistrations",
                column: "NationalNumber");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistrations_OfficeId",
                schema: "dbo",
                table: "StudentRegistrations",
                column: "OfficeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentHighSchoolCertificates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ExamSessions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HighSchoolCertificateTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentRegistrations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AdmissionTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Offices",
                schema: "dbo");

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

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "StudyPrograms",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "ScientificResearches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Lectures",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Labs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "GraduatedStudents",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Galleries",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                schema: "dbo",
                table: "Courses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

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
    }
}
