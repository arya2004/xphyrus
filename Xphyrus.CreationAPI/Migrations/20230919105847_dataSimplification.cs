using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.AssesmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class dataSimplification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coding_Assesments_AssesmentId",
                table: "Coding");

            migrationBuilder.DropForeignKey(
                name: "FK_Constrainss_Coding_CodingId",
                table: "Constrainss");

            migrationBuilder.DropForeignKey(
                name: "FK_Examples_Coding_CodingId",
                table: "Examples");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterCode_Coding_CodingId",
                table: "MasterCode");

            migrationBuilder.DropIndex(
                name: "IX_MasterCode_CodingId",
                table: "MasterCode");

            migrationBuilder.DropIndex(
                name: "IX_Examples_CodingId",
                table: "Examples");

            migrationBuilder.DropIndex(
                name: "IX_Constrainss_CodingId",
                table: "Constrainss");

            migrationBuilder.DropIndex(
                name: "IX_Coding_AssesmentId",
                table: "Coding");

            migrationBuilder.DropColumn(
                name: "CodingId",
                table: "MasterCode");

            migrationBuilder.DropColumn(
                name: "CodingId",
                table: "Examples");

            migrationBuilder.DropColumn(
                name: "CodingId",
                table: "Constrainss");

            migrationBuilder.DropColumn(
                name: "AssesmentId",
                table: "Coding");

            migrationBuilder.AddColumn<string>(
                name: "Constrain1",
                table: "Coding",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Constrain2",
                table: "Coding",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Constrain3",
                table: "Coding",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assesments_Coding_CodingsCodingId",
                table: "Assesments");

            migrationBuilder.DropIndex(
                name: "IX_Assesments_CodingsCodingId",
                table: "Assesments");

            migrationBuilder.DropColumn(
                name: "Constrain1",
                table: "Coding");

            migrationBuilder.DropColumn(
                name: "Constrain2",
                table: "Coding");

            migrationBuilder.DropColumn(
                name: "Constrain3",
                table: "Coding");

            migrationBuilder.DropColumn(
                name: "CodingsCodingId",
                table: "Assesments");

            migrationBuilder.AddColumn<string>(
                name: "CodingId",
                table: "MasterCode",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodingId",
                table: "Examples",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodingId",
                table: "Constrainss",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssesmentId",
                table: "Coding",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterCode_CodingId",
                table: "MasterCode",
                column: "CodingId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_CodingId",
                table: "Examples",
                column: "CodingId");

            migrationBuilder.CreateIndex(
                name: "IX_Constrainss_CodingId",
                table: "Constrainss",
                column: "CodingId");

            migrationBuilder.CreateIndex(
                name: "IX_Coding_AssesmentId",
                table: "Coding",
                column: "AssesmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coding_Assesments_AssesmentId",
                table: "Coding",
                column: "AssesmentId",
                principalTable: "Assesments",
                principalColumn: "AssesmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Constrainss_Coding_CodingId",
                table: "Constrainss",
                column: "CodingId",
                principalTable: "Coding",
                principalColumn: "CodingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examples_Coding_CodingId",
                table: "Examples",
                column: "CodingId",
                principalTable: "Coding",
                principalColumn: "CodingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCode_Coding_CodingId",
                table: "MasterCode",
                column: "CodingId",
                principalTable: "Coding",
                principalColumn: "CodingId");
        }
    }
}
