using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnboardingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentDashboardInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Departments_Name",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "FridaySchedule",
                table: "Departments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "remote");

            migrationBuilder.AddColumn<string>(
                name: "MondaySchedule",
                table: "Departments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "office");

            migrationBuilder.AddColumn<string>(
                name: "RoleKey",
                table: "Departments",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tagline",
                table: "Departments",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThursdaySchedule",
                table: "Departments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "remote");

            migrationBuilder.AddColumn<string>(
                name: "TuesdaySchedule",
                table: "Departments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "office");

            migrationBuilder.AddColumn<string>(
                name: "WednesdaySchedule",
                table: "Departments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "office");

            migrationBuilder.AddColumn<string>(
                name: "WelcomeMessage",
                table: "Departments",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DepartmentContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AvatarInitials = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Bio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentContacts_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DepartmentContacts",
                columns: new[] { "Id", "AvatarInitials", "Bio", "DepartmentId", "DisplayOrder", "Email", "Name", "Role" },
                values: new object[,]
                {
                    { 1, "PS", "Leads the backend platform team. Open-source enthusiast and conference speaker.", 1, 1, "priya.sharma@meridian.io", "Priya Sharma", "Engineering Manager" },
                    { 2, "LO", "Your onboarding buddy. Go-to for architecture questions and code reviews.", 1, 2, "liam.obrien@meridian.io", "Liam O'Brien", "Senior Engineer" },
                    { 3, "YT", "Owns CI/CD pipelines and cloud infrastructure. Ask her about the deployment process.", 1, 3, "yuki.tanaka@meridian.io", "Yuki Tanaka", "DevOps Engineer" },
                    { 4, "MW", "Leads the EMEA sales team. Passionate about consultative selling.", 2, 1, "marcus.webb@meridian.io", "Marcus Webb", "Sales Manager" },
                    { 5, "SR", "Your onboarding buddy. Top performer in enterprise deals.", 2, 2, "sofia.reyes@meridian.io", "Sofia Reyes", "Account Executive" },
                    { 6, "JO", "Manages inbound pipeline and prospect qualification.", 2, 3, "james.okafor@meridian.io", "James Okafor", "Sales Development Rep" },
                    { 7, "AD", "Drives brand strategy and growth campaigns across all channels.", 3, 1, "amara.diallo@meridian.io", "Amara Diallo", "Head of Marketing" },
                    { 8, "CP", "Your onboarding buddy. Owns the content calendar and editorial voice.", 3, 2, "chris.park@meridian.io", "Chris Park", "Content Strategist" },
                    { 9, "EM", "Runs paid acquisition campaigns and analyses conversion metrics.", 3, 3, "elena.morel@meridian.io", "Elena Morel", "Performance Marketer" },
                    { 10, "DK", "Oversees talent acquisition, L&D, and employee experience.", 4, 1, "david.kim@meridian.io", "David Kim", "HR Director" },
                    { 11, "FA", "Your onboarding buddy. Manages benefits, compliance, and HR ops.", 4, 2, "fatima.alsayed@meridian.io", "Fatima Al-Sayed", "People Operations Lead" },
                    { 12, "TB", "Leads recruiting efforts across all departments.", 4, 3, "tom.brennan@meridian.io", "Tom Brennan", "Talent Acquisition Spec." },
                    { 13, "NP", "Owns financial reporting, forecasting, and budget cycles.", 5, 1, "nadia.petrov@meridian.io", "Nadia Petrov", "Finance Lead" },
                    { 14, "AM", "Your onboarding buddy. Specialises in expense reporting and financial modelling.", 5, 2, "arjun.mehta@meridian.io", "Arjun Mehta", "Financial Analyst" },
                    { 15, "LB", "Handles payroll processing and employee compensation queries.", 5, 3, "laura.bennett@meridian.io", "Laura Bennett", "Payroll Specialist" }
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FridaySchedule", "MondaySchedule", "RoleKey", "Tagline", "ThursdaySchedule", "TuesdaySchedule", "WednesdaySchedule", "WelcomeMessage" },
                values: new object[] { "remote", "office", "engineering", "Build the future, one commit at a time", "remote", "office", "office", "Welcome to Engineering! We are excited to have you on board. Your first weeks will be all about getting your development environment ready and making your first contribution." });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FridaySchedule", "MondaySchedule", "RoleKey", "Tagline", "ThursdaySchedule", "TuesdaySchedule", "WednesdaySchedule", "WelcomeMessage" },
                values: new object[] { "office", "office", "sales", "Turn every conversation into an opportunity", "remote", "remote", "office", "Welcome to Sales! You'll be building relationships that drive Meridian's growth. Start by getting familiar with our CRM and shadowing top performers." });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FridaySchedule", "MondaySchedule", "RoleKey", "Tagline", "ThursdaySchedule", "TuesdaySchedule", "WednesdaySchedule", "WelcomeMessage" },
                values: new object[] { "remote", "remote", "marketing", "Tell the story that moves the market", "remote", "office", "office", "Welcome to Marketing! You'll shape how the world sees Meridian. Dive into the brand playbook and start exploring our content calendar." });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FridaySchedule", "MondaySchedule", "RoleKey", "Tagline", "ThursdaySchedule", "TuesdaySchedule", "WednesdaySchedule", "WelcomeMessage" },
                values: new object[] { "remote", "office", "hr", "People are our greatest product", "office", "office", "remote", "Welcome to HR! You'll play a key role in helping others succeed at Meridian. Start by completing your paperwork and meeting the team." });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FridaySchedule", "MondaySchedule", "RoleKey", "Tagline", "ThursdaySchedule", "TuesdaySchedule", "WednesdaySchedule", "WelcomeMessage" },
                values: new object[] { "office", "office", "finance", "The numbers that keep us moving forward", "remote", "office", "remote", "Welcome to Finance! Your work ensures Meridian's fiscal health. Get started by gaining system access and meeting the finance team." });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_RoleKey",
                table: "Departments",
                column: "RoleKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentContacts_DepartmentId",
                table: "DepartmentContacts",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentContacts");

            migrationBuilder.DropIndex(
                name: "IX_Departments_RoleKey",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "FridaySchedule",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "MondaySchedule",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "RoleKey",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Tagline",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ThursdaySchedule",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "TuesdaySchedule",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "WednesdaySchedule",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "WelcomeMessage",
                table: "Departments");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Departments_Name",
                table: "Departments",
                sql: "\"Name\" IN ('Engineering', 'Sales', 'Marketing', 'HR', 'Finance')");
        }
    }
}
