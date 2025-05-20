using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAdditionalColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyGroupUserId",
                table: "StudyGroups");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Timelogs",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "StudySessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Attachments",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Timelogs");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Attachments");

            migrationBuilder.AddColumn<Guid>(
                name: "StudyGroupUserId",
                table: "StudyGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
