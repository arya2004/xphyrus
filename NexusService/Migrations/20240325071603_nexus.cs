using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.NexusService.Migrations
{
    /// <inheritdoc />
    public partial class nexus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NexusId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Nexus",
                columns: table => new
                {
                    NexusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nexus", x => x.NexusId);
                    table.ForeignKey(
                        name: "FK_Nexus_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                name: "IX_AspNetUsers_NexusId",
                table: "AspNetUsers",
                column: "NexusId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingAssessments_NexusId",
                table: "CodingAssessments",
                column: "NexusId");

            migrationBuilder.CreateIndex(
                name: "IX_Nexus_CreatorId",
                table: "Nexus",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_CodingAssessmentId",
                table: "TestCases",
                column: "CodingAssessmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nexus_NexusId",
                table: "AspNetUsers",
                column: "NexusId",
                principalTable: "Nexus",
                principalColumn: "NexusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nexus_NexusId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TestCases");

            migrationBuilder.DropTable(
                name: "CodingAssessments");

            migrationBuilder.DropTable(
                name: "Nexus");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NexusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NexusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "AspNetUsers");
        }
    }
}
