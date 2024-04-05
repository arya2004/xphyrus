using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class Misclennaous8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssesmentAdmins_AspNetUsers_ApplicationUserId",
                table: "AssesmentAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_AssesmentParticipants_AspNetUsers_ApplicationUserId",
                table: "AssesmentParticipants");

            migrationBuilder.DropIndex(
                name: "IX_AssesmentParticipants_ApplicationUserId",
                table: "AssesmentParticipants");

            migrationBuilder.DropIndex(
                name: "IX_AssesmentAdmins_ApplicationUserId",
                table: "AssesmentAdmins");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AssesmentParticipants");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AssesmentAdmins");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser",
                table: "AssesmentParticipants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser",
                table: "AssesmentAdmins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUser",
                table: "AssesmentParticipants");

            migrationBuilder.DropColumn(
                name: "ApplicationUser",
                table: "AssesmentAdmins");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AssesmentParticipants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AssesmentAdmins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AssesmentParticipants_ApplicationUserId",
                table: "AssesmentParticipants",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssesmentAdmins_ApplicationUserId",
                table: "AssesmentAdmins",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssesmentAdmins_AspNetUsers_ApplicationUserId",
                table: "AssesmentAdmins",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssesmentParticipants_AspNetUsers_ApplicationUserId",
                table: "AssesmentParticipants",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
