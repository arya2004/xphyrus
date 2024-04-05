using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeTaabl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssesmentAdmins");

            migrationBuilder.DropTable(
                name: "AssesmentParticipants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssesmentAdmins",
                columns: table => new
                {
                    AssesmentAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssesmentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasResultDeclared = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssesmentAdmins", x => x.AssesmentAdminsId);
                });

            migrationBuilder.CreateTable(
                name: "AssesmentParticipants",
                columns: table => new
                {
                    AssesmentParticipantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssesmentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasCompleted = table.Column<bool>(type: "bit", nullable: false),
                    HasStarted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssesmentParticipants", x => x.AssesmentParticipantId);
                });
        }
    }
}
