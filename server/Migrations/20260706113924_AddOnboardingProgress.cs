using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOnboardingProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnboardingProgresses",
                columns: table => new
                {
                    SessionToken = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnboardingProgresses", x => new { x.SessionToken, x.TaskId });
                    table.ForeignKey(
                        name: "FK_OnboardingProgresses_OnboardingTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "OnboardingTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnboardingProgresses_TaskId",
                table: "OnboardingProgresses",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnboardingProgresses");
        }
    }
}
