using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserAssesmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "AssesmentAdmins",
                columns: table => new
                {
                    AssesmentAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssesmentId = table.Column<int>(type: "int", nullable: false),
                    HasResultDeclared = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssesmentAdmins", x => x.AssesmentAdminsId);
                    table.ForeignKey(
                        name: "FK_AssesmentAdmins_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssesmentParticipants",
                columns: table => new
                {
                    AssesmentParticipantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssesmentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasStarted = table.Column<bool>(type: "bit", nullable: false),
                    HasCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssesmentParticipants", x => x.AssesmentParticipantId);
                    table.ForeignKey(
                        name: "FK_AssesmentParticipants_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssesmentAdmins_ApplicationUserId",
                table: "AssesmentAdmins",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssesmentParticipants_ApplicationUserId",
                table: "AssesmentParticipants",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssesmentAdmins");

            migrationBuilder.DropTable(
                name: "AssesmentParticipants");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
