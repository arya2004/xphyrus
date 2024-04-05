using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.EvaluationAPI.Migrations
{
    /// <inheritdoc />
    public partial class remove_other : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "submissionRequests");

            migrationBuilder.DropTable(
                name: "userSubmissionandSulitions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "submissionRequests",
                columns: table => new
                {
                    SubmissionRequestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssesmentId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    expected_output = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    language_id = table.Column<int>(type: "int", nullable: true),
                    source_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stdin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submissionRequests", x => x.SubmissionRequestId);
                });

            migrationBuilder.CreateTable(
                name: "userSubmissionandSulitions",
                columns: table => new
                {
                    UserSubmissionandSulitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssesmentId = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedON = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    LangugeCode = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userSubmissionandSulitions", x => x.UserSubmissionandSulitionId);
                });
        }
    }
}
