using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.EvaluationAPI.Migrations
{
    /// <inheritdoc />
    public partial class Done : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cpu_extra_time",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "cpu_time_limit",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "enable_network",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "enable_per_process_and_thread_memory_limit",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "enable_per_process_and_thread_time_limit",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "max_file_size",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "max_processes_and_or_threads",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "memory_limit",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "number_of_runs",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "stack_limit",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "wall_time_limit",
                table: "submissionRequests");

            migrationBuilder.AddColumn<int>(
                name: "AssesmentId",
                table: "submissionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "submissionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssesmentId",
                table: "submissionRequests");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "submissionRequests");

            migrationBuilder.AddColumn<int>(
                name: "cpu_extra_time",
                table: "submissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cpu_time_limit",
                table: "submissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "enable_network",
                table: "submissionRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "enable_per_process_and_thread_memory_limit",
                table: "submissionRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "enable_per_process_and_thread_time_limit",
                table: "submissionRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "max_file_size",
                table: "submissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "max_processes_and_or_threads",
                table: "submissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "memory_limit",
                table: "submissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "number_of_runs",
                table: "submissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stack_limit",
                table: "submissionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "wall_time_limit",
                table: "submissionRequests",
                type: "int",
                nullable: true);
        }
    }
}
