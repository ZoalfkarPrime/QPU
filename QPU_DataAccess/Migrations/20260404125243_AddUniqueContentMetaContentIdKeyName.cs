using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QPU_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueContentMetaContentIdKeyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentMetas_ContentId",
                schema: "dbo",
                table: "ContentMetas");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "dbo",
                table: "Contents",
                newName: "Section");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceId",
                schema: "dbo",
                table: "Contents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetas_ContentId_KeyName",
                schema: "dbo",
                table: "ContentMetas",
                columns: new[] { "ContentId", "KeyName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentMetas_ContentId_KeyName",
                schema: "dbo",
                table: "ContentMetas");

            migrationBuilder.RenameColumn(
                name: "Section",
                schema: "dbo",
                table: "Contents",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "ReferenceId",
                schema: "dbo",
                table: "Contents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_ContentMetas_ContentId",
                schema: "dbo",
                table: "ContentMetas",
                column: "ContentId");
        }
    }
}
