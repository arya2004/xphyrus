using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class userSchemaJobUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserNexus");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Nexus",
                newName: "Title");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptApplicantsWhoNeedToRelocate",
                table: "Nexus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualSalaryMax",
                table: "Nexus",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualSalaryMin",
                table: "Nexus",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Nexus",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Nexus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Equity",
                table: "Nexus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "EquityMax",
                table: "Nexus",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EquityMin",
                table: "Nexus",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Nexus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryRole",
                table: "Nexus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RelocationAssistance",
                table: "Nexus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RemotePolicy",
                table: "Nexus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Skills",
                table: "Nexus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfPosition",
                table: "Nexus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkExperience",
                table: "Nexus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanySize",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Market",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OneLinePitch",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkEmail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nexus_ApplicationUserId",
                table: "Nexus",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nexus_AspNetUsers_ApplicationUserId",
                table: "Nexus",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nexus_AspNetUsers_ApplicationUserId",
                table: "Nexus");

            migrationBuilder.DropIndex(
                name: "IX_Nexus_ApplicationUserId",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "AcceptApplicantsWhoNeedToRelocate",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "AnnualSalaryMax",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "AnnualSalaryMin",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "Equity",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "EquityMax",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "EquityMin",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "PrimaryRole",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "RelocationAssistance",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "RemotePolicy",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "TypeOfPosition",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "WorkExperience",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanySize",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedinUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Market",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OneLinePitch",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkEmail",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Nexus",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "ApplicationUserNexus",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NexusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserNexus", x => new { x.ApplicationUsersId, x.NexusId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserNexus_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserNexus_Nexus_NexusId",
                        column: x => x.NexusId,
                        principalTable: "Nexus",
                        principalColumn: "NexusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserNexus_NexusId",
                table: "ApplicationUserNexus",
                column: "NexusId");
        }
    }
}
