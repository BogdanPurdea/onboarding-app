using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnboardingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskInstructionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskInstructions_OnboardingTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "OnboardingTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskInstructions",
                columns: new[] { "Id", "StepNumber", "TaskId", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, "Read and understand all requirements for this task." },
                    { 2, 2, 1, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 3, 3, 1, "Verify and check off the item in your workspace dashboard once done." },
                    { 4, 1, 2, "Read and understand all requirements for this task." },
                    { 5, 2, 2, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 6, 3, 2, "Verify and check off the item in your workspace dashboard once done." },
                    { 7, 1, 3, "Read and understand all requirements for this task." },
                    { 8, 2, 3, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 9, 3, 3, "Verify and check off the item in your workspace dashboard once done." },
                    { 10, 1, 4, "Read and understand all requirements for this task." },
                    { 11, 2, 4, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 12, 3, 4, "Verify and check off the item in your workspace dashboard once done." },
                    { 13, 1, 5, "Read and understand all requirements for this task." },
                    { 14, 2, 5, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 15, 3, 5, "Verify and check off the item in your workspace dashboard once done." },
                    { 16, 1, 6, "Read and understand all requirements for this task." },
                    { 17, 2, 6, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 18, 3, 6, "Verify and check off the item in your workspace dashboard once done." },
                    { 19, 1, 7, "Read and understand all requirements for this task." },
                    { 20, 2, 7, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 21, 3, 7, "Verify and check off the item in your workspace dashboard once done." },
                    { 22, 1, 8, "Read and understand all requirements for this task." },
                    { 23, 2, 8, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 24, 3, 8, "Verify and check off the item in your workspace dashboard once done." },
                    { 25, 1, 9, "Read and understand all requirements for this task." },
                    { 26, 2, 9, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 27, 3, 9, "Verify and check off the item in your workspace dashboard once done." },
                    { 28, 1, 10, "Read and understand all requirements for this task." },
                    { 29, 2, 10, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 30, 3, 10, "Verify and check off the item in your workspace dashboard once done." },
                    { 31, 1, 11, "Read and understand all requirements for this task." },
                    { 32, 2, 11, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 33, 3, 11, "Verify and check off the item in your workspace dashboard once done." },
                    { 34, 1, 12, "Read and understand all requirements for this task." },
                    { 35, 2, 12, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 36, 3, 12, "Verify and check off the item in your workspace dashboard once done." },
                    { 37, 1, 13, "Read and understand all requirements for this task." },
                    { 38, 2, 13, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 39, 3, 13, "Verify and check off the item in your workspace dashboard once done." },
                    { 40, 1, 14, "Read and understand all requirements for this task." },
                    { 41, 2, 14, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 42, 3, 14, "Verify and check off the item in your workspace dashboard once done." },
                    { 43, 1, 15, "Read and understand all requirements for this task." },
                    { 44, 2, 15, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 45, 3, 15, "Verify and check off the item in your workspace dashboard once done." },
                    { 46, 1, 16, "Read and understand all requirements for this task." },
                    { 47, 2, 16, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 48, 3, 16, "Verify and check off the item in your workspace dashboard once done." },
                    { 49, 1, 17, "Read and understand all requirements for this task." },
                    { 50, 2, 17, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 51, 3, 17, "Verify and check off the item in your workspace dashboard once done." },
                    { 52, 1, 18, "Read and understand all requirements for this task." },
                    { 53, 2, 18, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 54, 3, 18, "Verify and check off the item in your workspace dashboard once done." },
                    { 55, 1, 19, "Read and understand all requirements for this task." },
                    { 56, 2, 19, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 57, 3, 19, "Verify and check off the item in your workspace dashboard once done." },
                    { 58, 1, 20, "Read and understand all requirements for this task." },
                    { 59, 2, 20, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 60, 3, 20, "Verify and check off the item in your workspace dashboard once done." },
                    { 61, 1, 21, "Read and understand all requirements for this task." },
                    { 62, 2, 21, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 63, 3, 21, "Verify and check off the item in your workspace dashboard once done." },
                    { 64, 1, 22, "Read and understand all requirements for this task." },
                    { 65, 2, 22, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 66, 3, 22, "Verify and check off the item in your workspace dashboard once done." },
                    { 67, 1, 23, "Read and understand all requirements for this task." },
                    { 68, 2, 23, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 69, 3, 23, "Verify and check off the item in your workspace dashboard once done." },
                    { 70, 1, 24, "Read and understand all requirements for this task." },
                    { 71, 2, 24, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 72, 3, 24, "Verify and check off the item in your workspace dashboard once done." },
                    { 73, 1, 25, "Read and understand all requirements for this task." },
                    { 74, 2, 25, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 75, 3, 25, "Verify and check off the item in your workspace dashboard once done." },
                    { 76, 1, 26, "Read and understand all requirements for this task." },
                    { 77, 2, 26, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 78, 3, 26, "Verify and check off the item in your workspace dashboard once done." },
                    { 79, 1, 27, "Read and understand all requirements for this task." },
                    { 80, 2, 27, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 81, 3, 27, "Verify and check off the item in your workspace dashboard once done." },
                    { 82, 1, 28, "Read and understand all requirements for this task." },
                    { 83, 2, 28, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 84, 3, 28, "Verify and check off the item in your workspace dashboard once done." },
                    { 85, 1, 29, "Read and understand all requirements for this task." },
                    { 86, 2, 29, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 87, 3, 29, "Verify and check off the item in your workspace dashboard once done." },
                    { 88, 1, 30, "Read and understand all requirements for this task." },
                    { 89, 2, 30, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 90, 3, 30, "Verify and check off the item in your workspace dashboard once done." },
                    { 91, 1, 31, "Read and understand all requirements for this task." },
                    { 92, 2, 31, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 93, 3, 31, "Verify and check off the item in your workspace dashboard once done." },
                    { 94, 1, 32, "Read and understand all requirements for this task." },
                    { 95, 2, 32, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 96, 3, 32, "Verify and check off the item in your workspace dashboard once done." },
                    { 97, 1, 33, "Read and understand all requirements for this task." },
                    { 98, 2, 33, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 99, 3, 33, "Verify and check off the item in your workspace dashboard once done." },
                    { 100, 1, 34, "Read and understand all requirements for this task." },
                    { 101, 2, 34, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 102, 3, 34, "Verify and check off the item in your workspace dashboard once done." },
                    { 103, 1, 35, "Read and understand all requirements for this task." },
                    { 104, 2, 35, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 105, 3, 35, "Verify and check off the item in your workspace dashboard once done." },
                    { 106, 1, 36, "Read and understand all requirements for this task." },
                    { 107, 2, 36, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 108, 3, 36, "Verify and check off the item in your workspace dashboard once done." },
                    { 109, 1, 37, "Read and understand all requirements for this task." },
                    { 110, 2, 37, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 111, 3, 37, "Verify and check off the item in your workspace dashboard once done." },
                    { 112, 1, 38, "Read and understand all requirements for this task." },
                    { 113, 2, 38, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 114, 3, 38, "Verify and check off the item in your workspace dashboard once done." },
                    { 115, 1, 39, "Read and understand all requirements for this task." },
                    { 116, 2, 39, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 117, 3, 39, "Verify and check off the item in your workspace dashboard once done." },
                    { 118, 1, 40, "Read and understand all requirements for this task." },
                    { 119, 2, 40, "Sync with your onboarding buddy or team lead if any blockers arise." },
                    { 120, 3, 40, "Verify and check off the item in your workspace dashboard once done." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskInstructions_TaskId",
                table: "TaskInstructions",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskInstructions");
        }
    }
}
