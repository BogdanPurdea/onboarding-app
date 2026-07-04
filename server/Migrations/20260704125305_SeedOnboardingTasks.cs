using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnboardingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedOnboardingTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OnboardingTasks",
                columns: new[] { "Id", "DepartmentId", "Description", "DisplayOrder", "TimelinePhase", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office.", 1, 0, "Attend Day 1 Welcome Session" },
                    { 2, 1, "Install the required tools for your workstation: VS Code (or your preferred IDE), Git, Node.js, and the .NET SDK. Follow the setup guide in the internal wiki.", 2, 0, "Set Up Your Development Environment" },
                    { 3, 1, "Request access to the Meridian GitHub organisation from your team lead. Accept the invitation email and confirm you can see the private repositories.", 3, 0, "Get GitHub Organisation Access" },
                    { 4, 1, "Have a 30-minute intro call with your team lead and assigned buddy. Discuss your role, current projects, and how the team operates day-to-day.", 4, 0, "Meet Your Team Lead & Onboarding Buddy" },
                    { 5, 1, "Install Docker Desktop and verify it is running correctly by executing `docker run hello-world` in your terminal. Docker is required to run the local development stack.", 1, 1, "Install Docker" },
                    { 6, 1, "Clone the main backend repository from GitHub and install its dependencies. Follow the README to confirm the project structure looks correct.", 2, 1, "Clone the Core API Repository" },
                    { 7, 1, "Use `docker compose up` to spin up the full local stack (API + database). Verify the API responds at http://localhost:5000/healthz.", 3, 1, "Run the Application Locally" },
                    { 8, 1, "Complete the mandatory security awareness module on the company LMS. This covers data handling policies, password management, and phishing awareness.", 4, 1, "Complete Security & Compliance Training" },
                    { 9, 1, "Pick up a good-first-issue ticket from the backlog, implement the fix or feature, and open a PR against the main branch following the contribution guidelines.", 1, 2, "Submit Your First Pull Request" },
                    { 10, 1, "Review a colleague's open pull request. Leave at least one constructive comment or approval. This helps you understand the codebase and team coding standards.", 2, 2, "Complete Your First Code Review" },
                    { 11, 1, "Participate in the full sprint ceremony cycle: planning, daily stand-ups, review, and retrospective. Share one observation or suggestion during the retro.", 3, 2, "Attend Sprint Planning & Retrospective" },
                    { 12, 2, "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office.", 1, 0, "Attend Day 1 Welcome Session" },
                    { 13, 2, "Log in to the company CRM (Salesforce) using the credentials sent to your work email. Complete your profile and familiarise yourself with the dashboard.", 2, 0, "Set Up Your CRM Account" },
                    { 14, 2, "Have a 30-minute intro call with your sales manager. Discuss your territory, targets, and the team's current pipeline.", 3, 0, "Meet Your Sales Manager" },
                    { 15, 2, "Work through the product training modules on the LMS. By the end you should be able to explain Meridian's core offering, pricing tiers, and key differentiators.", 1, 1, "Complete Product & Service Training" },
                    { 16, 2, "Join a live discovery or demo call as a silent listener. Afterwards, debrief with the rep and note three things you would like to replicate.", 2, 1, "Shadow a Senior Sales Rep on a Call" },
                    { 17, 2, "Create your first opportunity records in Salesforce. Configure your personal pipeline view and set up task reminders for your first week's outreach.", 3, 1, "Set Up Your Sales Pipeline in the CRM" },
                    { 18, 2, "Send personalised outreach to at least 10 prospects in your territory using the approved email templates. Log all activity in the CRM.", 1, 2, "Make Your First Outbound Outreach" },
                    { 19, 2, "Present the current state of your pipeline in a 1:1 with your manager. Discuss deal health, blockers, and what support you need to hit your first target.", 2, 2, "Review Pipeline with Your Manager" },
                    { 20, 3, "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office.", 1, 0, "Attend Day 1 Welcome Session" },
                    { 21, 3, "Request access to HubSpot, Canva, and the shared Google Drive marketing folder from your manager. Verify you can log in to each tool.", 2, 0, "Get Access to Marketing Tools" },
                    { 22, 3, "Read through the Meridian brand playbook (fonts, colours, tone of voice, logo usage). This is your reference for all content you produce.", 3, 0, "Review Brand Guidelines" },
                    { 23, 3, "Review the current quarter's content calendar in Google Sheets. Identify the next three upcoming pieces you might contribute to.", 1, 1, "Get Familiar with the Content Calendar" },
                    { 24, 3, "Work through the internal brand messaging course on the LMS. Complete the short quiz at the end to confirm your understanding.", 2, 1, "Complete Brand & Messaging Training" },
                    { 25, 3, "Take ownership of at least one deliverable in an active marketing campaign — a social post, blog draft, or email copy. Get it reviewed and published.", 1, 2, "Contribute to an Active Campaign" },
                    { 26, 3, "Compile performance metrics for content you have worked on (impressions, clicks, conversions) and present a 5-minute summary to the marketing team.", 2, 2, "Present Your First Marketing Insights Report" },
                    { 27, 4, "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office.", 1, 0, "Attend Day 1 Welcome Session" },
                    { 28, 4, "Sign and submit all required employment documents: contract, tax forms, and emergency contact details. Keep digital copies for your records.", 2, 0, "Complete Employment Paperwork" },
                    { 29, 4, "Collect your employee ID badge from reception and confirm access to the HR information system (HRIS) and the payroll portal.", 3, 0, "Get Your Employee ID & System Access" },
                    { 30, 4, "Finish the mandatory compliance modules on the LMS: anti-harassment, GDPR data handling, and workplace safety. All three must be completed within week one.", 1, 1, "Complete Mandatory Compliance Training" },
                    { 31, 4, "Log in to the benefits portal and select your health, dental, and pension options before the enrolment deadline. Contact the benefits provider with any questions.", 2, 1, "Enrol in Company Benefits" },
                    { 32, 4, "Read the employee handbook, leave policy, remote work policy, and code of conduct. Confirm you have reviewed them by signing the acknowledgement form.", 3, 1, "Review All Company HR Policies" },
                    { 33, 4, "Schedule and attend a structured 30-day review with your manager. Discuss your onboarding experience, early wins, and goals for the next two months.", 1, 2, "Complete 30-Day Check-in with Your Manager" },
                    { 34, 5, "Join the company-wide welcome meeting. You will meet the founders, understand Meridian's mission, and get a tour of the office.", 1, 0, "Attend Day 1 Welcome Session" },
                    { 35, 5, "Request access to the accounting software (QuickBooks) and the financial reporting suite from your manager. Verify login and confirm your permission level.", 2, 0, "Get Access to Financial Systems" },
                    { 36, 5, "Have a 30-minute intro call with the Finance Lead. Discuss your responsibilities, the monthly close schedule, and how finance collaborates with other departments.", 3, 0, "Meet the Finance Team Lead" },
                    { 37, 5, "Finish the mandatory financial compliance and anti-fraud training modules on the LMS. Take note of the expense submission deadlines covered in the course.", 1, 1, "Complete Financial Compliance Training" },
                    { 38, 5, "Review the expense policy and submit a test expense report in QuickBooks to verify your access and understand the approval workflow.", 2, 1, "Understand the Expense Reporting Process" },
                    { 39, 5, "Open the standard monthly and quarterly budget report templates in the shared Finance drive. Familiarise yourself with the structure before the next reporting cycle.", 3, 1, "Review Budget Reporting Templates" },
                    { 40, 5, "Take ownership of one section of the monthly financial report. Prepare the data, validate figures with a colleague, and submit it ahead of the close deadline.", 1, 2, "Contribute to Your First Monthly Report" }
                });

            migrationBuilder.InsertData(
                table: "TaskPrerequisites",
                columns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                values: new object[,]
                {
                    { 6, 5 },
                    { 7, 6 },
                    { 9, 7 },
                    { 10, 9 },
                    { 16, 15 },
                    { 18, 16 },
                    { 25, 24 },
                    { 31, 28 },
                    { 33, 30 },
                    { 40, 38 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 9, 7 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 10, 9 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 16, 15 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 18, 16 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 25, 24 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 31, 28 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 33, 30 });

            migrationBuilder.DeleteData(
                table: "TaskPrerequisites",
                keyColumns: new[] { "PostDependentTaskId", "PreDependentTaskId" },
                keyValues: new object[] { 40, 38 });

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "OnboardingTasks",
                keyColumn: "Id",
                keyValue: 40);
        }
    }
}
