using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.AssesmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssesmentAdmins",
                table: "AssesmentAdmins");

            migrationBuilder.AlterColumn<int>(
                name: "AssesmentAdminId",
                table: "AssesmentAdmins",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AssesmentAdmins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssesmentAdmins",
                table: "AssesmentAdmins",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssesmentAdmins",
                table: "AssesmentAdmins");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssesmentAdmins");

            migrationBuilder.AlterColumn<string>(
                name: "AssesmentAdminId",
                table: "AssesmentAdmins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssesmentAdmins",
                table: "AssesmentAdmins",
                column: "AssesmentAdminId");
        }
    }
}
