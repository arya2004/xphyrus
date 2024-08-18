using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class refactor1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_AspNetUsers_FacultyId",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingQuestions_CodingAssessments_CodingAssessmentId",
                table: "CodingQuestions");

            migrationBuilder.DropTable(
                name: "CodingQuestionResults");

            migrationBuilder.DropTable(
                name: "TestCaseResults");

            migrationBuilder.DropTable(
                name: "CodingAssessmentResults");

            migrationBuilder.DropTable(
                name: "CodingAssessments");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_FacultyId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "EnrollmentKey",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "MaxStudents",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "TestCases",
                newName: "Marks");

            migrationBuilder.RenameColumn(
                name: "CodingAssessmentId",
                table: "CodingQuestions",
                newName: "TestId");

            migrationBuilder.RenameIndex(
                name: "IX_CodingQuestions_CodingAssessmentId",
                table: "CodingQuestions",
                newName: "IX_CodingQuestions_TestId");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "AspNetUsers",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "OutputCase",
                table: "TestCases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "InputCase",
                table: "TestCases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TestCases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Difficulty",
                table: "CodingQuestions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Classrooms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Division",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PRN",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentAnswers",
                columns: table => new
                {
                    StudentAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmittedCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarksAwarded = table.Column<int>(type: "int", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodingQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswers", x => x.StudentAnswerId);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAnswers_CodingQuestions_CodingQuestionId",
                        column: x => x.CodingQuestionId,
                        principalTable: "CodingQuestions",
                        principalColumn: "CodingQuestionId");
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_Tests_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_CodingQuestionId",
                table: "StudentAnswers",
                column: "CodingQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_StudentId",
                table: "StudentAnswers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ClassroomId",
                table: "Tests",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_AspNetUsers_TeacherId",
                table: "Classrooms",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingQuestions_Tests_TestId",
                table: "CodingQuestions",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_AspNetUsers_TeacherId",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingQuestions_Tests_TestId",
                table: "CodingQuestions");

            migrationBuilder.DropTable(
                name: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "Batch",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Division",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PRN",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Marks",
                table: "TestCases",
                newName: "Points");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "CodingQuestions",
                newName: "CodingAssessmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CodingQuestions_TestId",
                table: "CodingQuestions",
                newName: "IX_CodingQuestions_CodingAssessmentId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "AspNetUsers",
                newName: "Role");

            migrationBuilder.AlterColumn<string>(
                name: "OutputCase",
                table: "TestCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InputCase",
                table: "TestCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TestCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Difficulty",
                table: "CodingQuestions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrollmentKey",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacultyId",
                table: "Classrooms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxStudents",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CodingAssessments",
                columns: table => new
                {
                    CodingAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingAssessments", x => x.CodingAssessmentId);
                    table.ForeignKey(
                        name: "FK_CodingAssessments_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomId");
                });

            migrationBuilder.CreateTable(
                name: "TestCaseResults",
                columns: table => new
                {
                    TestCaseResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestCaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Passed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCaseResults", x => x.TestCaseResultId);
                    table.ForeignKey(
                        name: "FK_TestCaseResults_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "TestCaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodingAssessmentResults",
                columns: table => new
                {
                    CodingAssessmentResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodingAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingAssessmentResults", x => x.CodingAssessmentResultId);
                    table.ForeignKey(
                        name: "FK_CodingAssessmentResults_CodingAssessments_CodingAssessmentId",
                        column: x => x.CodingAssessmentId,
                        principalTable: "CodingAssessments",
                        principalColumn: "CodingAssessmentId");
                });

            migrationBuilder.CreateTable(
                name: "CodingQuestionResults",
                columns: table => new
                {
                    CodingQuestionResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodingAssessmentResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CodingQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Memory = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingQuestionResults", x => x.CodingQuestionResultId);
                    table.ForeignKey(
                        name: "FK_CodingQuestionResults_CodingAssessmentResults_CodingAssessmentResultId",
                        column: x => x.CodingAssessmentResultId,
                        principalTable: "CodingAssessmentResults",
                        principalColumn: "CodingAssessmentResultId");
                    table.ForeignKey(
                        name: "FK_CodingQuestionResults_CodingQuestions_CodingQuestionId",
                        column: x => x.CodingQuestionId,
                        principalTable: "CodingQuestions",
                        principalColumn: "CodingQuestionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_FacultyId",
                table: "Classrooms",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingAssessmentResults_CodingAssessmentId",
                table: "CodingAssessmentResults",
                column: "CodingAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingAssessments_ClassroomId",
                table: "CodingAssessments",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingQuestionResults_CodingAssessmentResultId",
                table: "CodingQuestionResults",
                column: "CodingAssessmentResultId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingQuestionResults_CodingQuestionId",
                table: "CodingQuestionResults",
                column: "CodingQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseResults_TestCaseId",
                table: "TestCaseResults",
                column: "TestCaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_AspNetUsers_FacultyId",
                table: "Classrooms",
                column: "FacultyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingQuestions_CodingAssessments_CodingAssessmentId",
                table: "CodingQuestions",
                column: "CodingAssessmentId",
                principalTable: "CodingAssessments",
                principalColumn: "CodingAssessmentId");
        }
    }
}
