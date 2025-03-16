using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixUserTimelog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timelogs_Users_UserId",
                table: "Timelogs");

            migrationBuilder.DropIndex(
                name: "IX_Timelogs_UserId",
                table: "Timelogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Timelogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Timelogs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Timelogs_UserId",
                table: "Timelogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timelogs_Users_UserId",
                table: "Timelogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
