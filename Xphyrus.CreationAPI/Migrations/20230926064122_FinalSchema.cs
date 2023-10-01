using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.AssesmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class FinalSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assesments_Coding_CodingsCodingId",
                table: "Assesments");

            migrationBuilder.DropIndex(
                name: "IX_Assesments_CodingsCodingId",
                table: "Assesments");

            migrationBuilder.DropColumn(
                name: "CodingsCodingId",
                table: "Assesments");

            migrationBuilder.AlterColumn<string>(
                name: "OutputCase",
                table: "EvliationCases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "InputCase",
                table: "EvliationCases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AssesmentId",
                table: "Coding",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MCQ",
                columns: table => new
                {
                    MCQId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: false),
                    AssesmentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MCQ", x => x.MCQId);
                    table.ForeignKey(
                        name: "FK_MCQ_Assesments_AssesmentId",
                        column: x => x.AssesmentId,
                        principalTable: "Assesments",
                        principalColumn: "AssesmentId");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OptionNumber = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MCQId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionsId);
                    table.ForeignKey(
                        name: "FK_Options_MCQ_MCQId",
                        column: x => x.MCQId,
                        principalTable: "MCQ",
                        principalColumn: "MCQId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coding_AssesmentId",
                table: "Coding",
                column: "AssesmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MCQ_AssesmentId",
                table: "MCQ",
                column: "AssesmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_MCQId",
                table: "Options",
                column: "MCQId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coding_Assesments_AssesmentId",
                table: "Coding",
                column: "AssesmentId",
                principalTable: "Assesments",
                principalColumn: "AssesmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coding_Assesments_AssesmentId",
                table: "Coding");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "MCQ");

            migrationBuilder.DropIndex(
                name: "IX_Coding_AssesmentId",
                table: "Coding");

            migrationBuilder.DropColumn(
                name: "AssesmentId",
                table: "Coding");

            migrationBuilder.AlterColumn<string>(
                name: "OutputCase",
                table: "EvliationCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InputCase",
                table: "EvliationCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodingsCodingId",
                table: "Assesments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assesments_CodingsCodingId",
                table: "Assesments",
                column: "CodingsCodingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assesments_Coding_CodingsCodingId",
                table: "Assesments",
                column: "CodingsCodingId",
                principalTable: "Coding",
                principalColumn: "CodingId");
        }
    }
}
