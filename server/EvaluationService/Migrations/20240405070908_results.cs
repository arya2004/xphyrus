using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.EvaluationAPI.Migrations
{
    /// <inheritdoc />
    public partial class results : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanguageCode",
                table: "userSubmissionandSulitions",
                newName: "LangugeCode");

            migrationBuilder.CreateTable(
                name: "CodingAssessmentResult",
                columns: table => new
                {
                    CodingAssessmentResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Source_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memory = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingAssessmentResult", x => x.CodingAssessmentResultId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodingAssessmentResult");

            migrationBuilder.RenameColumn(
                name: "LangugeCode",
                table: "userSubmissionandSulitions",
                newName: "LanguageCode");
        }
    }
}
