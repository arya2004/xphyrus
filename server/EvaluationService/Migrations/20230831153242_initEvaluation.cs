using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.EvaluationAPI.Migrations
{
    /// <inheritdoc />
    public partial class initEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userSubmissionandSulitions",
                columns: table => new
                {
                    UserSubmissionandSulitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageCode = table.Column<int>(type: "int", nullable: false),
                    CreatedON = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    AssesmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userSubmissionandSulitions", x => x.UserSubmissionandSulitionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userSubmissionandSulitions");
        }
    }
}
