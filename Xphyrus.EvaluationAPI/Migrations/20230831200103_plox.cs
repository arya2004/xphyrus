using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.EvaluationAPI.Migrations
{
    /// <inheritdoc />
    public partial class plox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "userSubmissionandSulitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "submissionRequests",
                columns: table => new
                {
                    SubmissionRequestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    source_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    language_id = table.Column<int>(type: "int", nullable: true),
                    number_of_runs = table.Column<int>(type: "int", nullable: true),
                    stdin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expected_output = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cpu_time_limit = table.Column<int>(type: "int", nullable: true),
                    cpu_extra_time = table.Column<int>(type: "int", nullable: true),
                    wall_time_limit = table.Column<int>(type: "int", nullable: true),
                    memory_limit = table.Column<int>(type: "int", nullable: true),
                    stack_limit = table.Column<int>(type: "int", nullable: true),
                    max_processes_and_or_threads = table.Column<int>(type: "int", nullable: true),
                    enable_per_process_and_thread_time_limit = table.Column<bool>(type: "bit", nullable: true),
                    enable_per_process_and_thread_memory_limit = table.Column<bool>(type: "bit", nullable: true),
                    max_file_size = table.Column<int>(type: "int", nullable: true),
                    enable_network = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submissionRequests", x => x.SubmissionRequestId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "userSubmissionandSulitions");
        }
    }
}
