using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFKToStudyGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "StudyGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_UserId",
                table: "StudyGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroups_AspNetUsers_UserId",
                table: "StudyGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroups_AspNetUsers_UserId",
                table: "StudyGroups");

            migrationBuilder.DropIndex(
                name: "IX_StudyGroups_UserId",
                table: "StudyGroups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudyGroups");
        }
    }
}
