using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_registration_and_certificate_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                schema: "dbo",
                table: "StudentRegistrations");

            migrationBuilder.DropColumn(
                name: "CertificateDate",
                schema: "dbo",
                table: "StudentHighSchoolCertificates");

            migrationBuilder.AddColumn<int>(
                name: "CertificateDate",
                schema: "dbo",
                table: "StudentHighSchoolCertificates",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AdmissionTypes",
                columns: new[] { "Id", "CreatedAt", "DisplayOrder", "IsActive", "Name", "Name_AR", "UpdatedAt" },
                values: new object[] { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, true, "Arab and Foreign Students Admission", "مفاضلة عرب واجانب", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AdmissionTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AddColumn<DateOnly>(
                name: "RegistrationDate",
                schema: "dbo",
                table: "StudentRegistrations",
                type: "date",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "CertificateDate",
                schema: "dbo",
                table: "StudentHighSchoolCertificates");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CertificateDate",
                schema: "dbo",
                table: "StudentHighSchoolCertificates",
                type: "date",
                nullable: true);
        }
    }
}
