using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class init_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nexus",
                columns: table => new
                {
                    NexusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nexus", x => x.NexusId);
                });

            migrationBuilder.CreateTable(
                name: "CodingAssessments",
                columns: table => new
                {
                    CodingAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NexusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingAssessments", x => x.CodingAssessmentId);
                    table.ForeignKey(
                        name: "FK_CodingAssessments_Nexus_NexusId",
                        column: x => x.NexusId,
                        principalTable: "Nexus",
                        principalColumn: "NexusId");
                });

            migrationBuilder.CreateTable(
                name: "TestCases",
                columns: table => new
                {
                    TestCaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputCase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutputCase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodingAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCases", x => x.TestCaseId);
                    table.ForeignKey(
                        name: "FK_TestCases_CodingAssessments_CodingAssessmentId",
                        column: x => x.CodingAssessmentId,
                        principalTable: "CodingAssessments",
                        principalColumn: "CodingAssessmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodingAssessments_NexusId",
                table: "CodingAssessments",
                column: "NexusId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_CodingAssessmentId",
                table: "TestCases",
                column: "CodingAssessmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestCases");

            migrationBuilder.DropTable(
                name: "CodingAssessments");

            migrationBuilder.DropTable(
                name: "Nexus");
        }
    }
}
