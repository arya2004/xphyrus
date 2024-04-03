using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xphyrus.NexusService.Migrations
{
    /// <inheritdoc />
    public partial class nexus_o_to_m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nexus_NexusId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Nexus_AspNetUsers_CreatorId",
                table: "Nexus");

            migrationBuilder.DropIndex(
                name: "IX_Nexus_CreatorId",
                table: "Nexus");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NexusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Nexus");

            migrationBuilder.DropColumn(
                name: "NexusId",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserNexus");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Nexus",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NexusId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nexus_CreatorId",
                table: "Nexus",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NexusId",
                table: "AspNetUsers",
                column: "NexusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nexus_NexusId",
                table: "AspNetUsers",
                column: "NexusId",
                principalTable: "Nexus",
                principalColumn: "NexusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nexus_AspNetUsers_CreatorId",
                table: "Nexus",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
