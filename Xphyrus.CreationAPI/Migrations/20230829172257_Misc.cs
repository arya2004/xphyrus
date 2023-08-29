using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.AssesmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class Misc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assesments",
                columns: table => new
                {
                    AssesmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStrict = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assesments", x => x.AssesmentId);
                });

            migrationBuilder.CreateTable(
                name: "Coding",
                columns: table => new
                {
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prompt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InputFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutputFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssesmentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coding", x => x.CodingId);
                    table.ForeignKey(
                        name: "FK_Coding_Assesments_AssesmentId",
                        column: x => x.AssesmentId,
                        principalTable: "Assesments",
                        principalColumn: "AssesmentId");
                });

            migrationBuilder.CreateTable(
                name: "Constrainss",
                columns: table => new
                {
                    COnstraintId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Constraint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constrainss", x => x.COnstraintId);
                    table.ForeignKey(
                        name: "FK_Constrainss_Coding_CodingId",
                        column: x => x.CodingId,
                        principalTable: "Coding",
                        principalColumn: "CodingId");
                });

            migrationBuilder.CreateTable(
                name: "EvliationCases",
                columns: table => new
                {
                    EvliationCaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InputCase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputCase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvliationCases", x => x.EvliationCaseId);
                    table.ForeignKey(
                        name: "FK_EvliationCases_Coding_CodingId",
                        column: x => x.CodingId,
                        principalTable: "Coding",
                        principalColumn: "CodingId");
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    ExampleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Output = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explaination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.ExampleId);
                    table.ForeignKey(
                        name: "FK_Examples_Coding_CodingId",
                        column: x => x.CodingId,
                        principalTable: "Coding",
                        principalColumn: "CodingId");
                });

            migrationBuilder.CreateTable(
                name: "MasterCode",
                columns: table => new
                {
                    MasterCodeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<int>(type: "int", nullable: false),
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCode", x => x.MasterCodeId);
                    table.ForeignKey(
                        name: "FK_MasterCode_Coding_CodingId",
                        column: x => x.CodingId,
                        principalTable: "Coding",
                        principalColumn: "CodingId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coding_AssesmentId",
                table: "Coding",
                column: "AssesmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Constrainss_CodingId",
                table: "Constrainss",
                column: "CodingId");

            migrationBuilder.CreateIndex(
                name: "IX_EvliationCases_CodingId",
                table: "EvliationCases",
                column: "CodingId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_CodingId",
                table: "Examples",
                column: "CodingId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCode_CodingId",
                table: "MasterCode",
                column: "CodingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Constrainss");

            migrationBuilder.DropTable(
                name: "EvliationCases");

            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "MasterCode");

            migrationBuilder.DropTable(
                name: "Coding");

            migrationBuilder.DropTable(
                name: "Assesments");
        }
    }
}
