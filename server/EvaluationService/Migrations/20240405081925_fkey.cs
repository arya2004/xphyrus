using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.EvaluationAPI.Migrations
{
    /// <inheritdoc />
    public partial class fkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Input",
                table: "CodingAssessmentResult");

            migrationBuilder.AddColumn<Guid>(
                name: "AssessmentId",
                table: "CodingAssessmentResult",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentId",
                table: "CodingAssessmentResult");

            migrationBuilder.AddColumn<string>(
                name: "Input",
                table: "CodingAssessmentResult",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
