using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.AssesmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class initMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coding",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputFormat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "constrainss",
                columns: table => new
                {
                    COnstraintId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Constraint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_constrainss", x => x.COnstraintId);
                    table.ForeignKey(
                        name: "FK_constrainss_Coding_CodingId",
                        column: x => x.CodingId,
                        principalTable: "Coding",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "evliationCases",
                columns: table => new
                {
                    EvliationCaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InputCase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputCase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evliationCases", x => x.EvliationCaseId);
                    table.ForeignKey(
                        name: "FK_evliationCases_Coding_CodingId",
                        column: x => x.CodingId,
                        principalTable: "Coding",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "examples",
                columns: table => new
                {
                    ExampleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Output = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explaination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examples", x => x.ExampleId);
                    table.ForeignKey(
                        name: "FK_examples_Coding_CodingId",
                        column: x => x.CodingId,
                        principalTable: "Coding",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MasterCode",
                columns: table => new
                {
                    MasterCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_constrainss_CodingId",
                table: "constrainss",
                column: "CodingId");

            migrationBuilder.CreateIndex(
                name: "IX_evliationCases_CodingId",
                table: "evliationCases",
                column: "CodingId");

            migrationBuilder.CreateIndex(
                name: "IX_examples_CodingId",
                table: "examples",
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
                name: "constrainss");

            migrationBuilder.DropTable(
                name: "evliationCases");

            migrationBuilder.DropTable(
                name: "examples");

            migrationBuilder.DropTable(
                name: "MasterCode");

            migrationBuilder.DropTable(
                name: "Coding");
        }
    }
}
