using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class Misclennaous : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AssesmentId",
                table: "AssesmentAdmins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AssesmentId",
                table: "AssesmentAdmins",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
