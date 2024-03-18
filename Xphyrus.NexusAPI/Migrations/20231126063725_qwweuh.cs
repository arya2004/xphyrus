using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class qwweuh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    HasStarted = table.Column<bool>(type: "bit", nullable: false),
                    HasCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssesmentParticipants", x => x.AssesmentParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "Assesments",
                columns: table => new
                {
                    CodingAssesmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assesments", x => x.CodingAssesmentId);
                });

            migrationBuilder.CreateTable(
                name: "EvliationCases",
                columns: table => new
                {
                    EvaluationCaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Output = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvliationCases", x => x.EvaluationCaseId);
                    table.ForeignKey(
                        name: "FK_EvliationCases_Assesments_CodingId",
                        column: x => x.CodingId,
                        principalTable: "Assesments",
                        principalColumn: "CodingAssesmentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvliationCases_CodingId",
                table: "EvliationCases",
                column: "CodingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssesmentAdmins");

            migrationBuilder.DropTable(
                name: "AssesmentParticipants");

            migrationBuilder.DropTable(
                name: "EvliationCases");

            migrationBuilder.DropTable(
                name: "Assesments");
        }
    }
}
