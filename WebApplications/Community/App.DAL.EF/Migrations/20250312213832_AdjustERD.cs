using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AdjustERD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroups_Conversations_ConversationId",
                table: "StudyGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroups_Users_UserId",
                table: "StudyGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudySessions_StudyGroups_StudyGroupId",
                table: "StudySessions");

            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_StudySessions_StudyGroupId",
                table: "StudySessions");

            migrationBuilder.DropIndex(
                name: "IX_StudyGroups_ConversationId",
                table: "StudyGroups");

            migrationBuilder.DropColumn(
                name: "StudyGroupId",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "StudyGroups");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StudyGroups",
                newName: "StudySessionId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyGroups_UserId",
                table: "StudyGroups",
                newName: "IX_StudyGroups_StudySessionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Timelogs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Timelogs",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudyGroupId",
                table: "Conversations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Link = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_StudyGroupId",
                table: "Conversations",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AssignmentId",
                table: "Attachments",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_StudyGroups_StudyGroupId",
                table: "Conversations",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroups_StudySessions_StudySessionId",
                table: "StudyGroups",
                column: "StudySessionId",
                principalTable: "StudySessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_StudyGroups_StudyGroupId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyGroups_StudySessions_StudySessionId",
                table: "StudyGroups");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_StudyGroupId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "StudyGroupId",
                table: "Conversations");

            migrationBuilder.RenameColumn(
                name: "StudySessionId",
                table: "StudyGroups",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyGroups_StudySessionId",
                table: "StudyGroups",
                newName: "IX_StudyGroups_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Timelogs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Timelogs",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudyGroupId",
                table: "StudySessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ConversationId",
                table: "StudyGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Link = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_StudyGroupId",
                table: "StudySessions",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_ConversationId",
                table: "StudyGroups",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_AssignmentId",
                table: "Bookmarks",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId",
                table: "Bookmarks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroups_Conversations_ConversationId",
                table: "StudyGroups",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyGroups_Users_UserId",
                table: "StudyGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudySessions_StudyGroups_StudyGroupId",
                table: "StudySessions",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
