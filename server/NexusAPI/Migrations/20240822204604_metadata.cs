using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class metadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentAnswerMetadataId",
                table: "StudentAnswers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentAnswerMetadatas",
                columns: table => new
                {
                    StudentAnswerMetadataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentIdId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswerMetadatas", x => x.StudentAnswerMetadataId);
                    table.ForeignKey(
                        name: "FK_StudentAnswerMetadatas_AspNetUsers_StudentIdId",
                        column: x => x.StudentIdId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAnswerMetadatas_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "TestId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_StudentAnswerMetadataId",
                table: "StudentAnswers",
                column: "StudentAnswerMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerMetadatas_StudentIdId",
                table: "StudentAnswerMetadatas",
                column: "StudentIdId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerMetadatas_TestId",
                table: "StudentAnswerMetadatas",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_StudentAnswerMetadatas_StudentAnswerMetadataId",
                table: "StudentAnswers",
                column: "StudentAnswerMetadataId",
                principalTable: "StudentAnswerMetadatas",
                principalColumn: "StudentAnswerMetadataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_StudentAnswerMetadatas_StudentAnswerMetadataId",
                table: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "StudentAnswerMetadatas");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_StudentAnswerMetadataId",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "StudentAnswerMetadataId",
                table: "StudentAnswers");
        }
    }
}
