using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCommToolsToContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleMeetUrl",
                table: "DepartmentContacts",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredCommTool",
                table: "DepartmentContacts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "slack");

            migrationBuilder.AddColumn<string>(
                name: "SlackMemberId",
                table: "DepartmentContacts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/pri-ya-sharma", "slack", "U012345678A" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/lia-m-obrien", "meet", "U012345678B" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/yuk-i-tanaka", "slack", "U012345678C" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/mar-c-us-webb", "meet", "U022345678A" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/sof-i-a-reyes", "slack", "U022345678B" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/jam-e-s-okafor", "slack", "U022345678C" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/ama-r-a-diallo", "meet", "U032345678A" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/chr-i-s-park", "slack", "U032345678B" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/ele-n-a-morel", "slack", "U032345678C" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/dav-i-d-kim", "meet", "U042345678A" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/fat-i-m-a", "slack", "U042345678B" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/tom-bren-nan", "slack", "U042345678C" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/nad-i-a-petrov", "meet", "U052345678A" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/arj-u-n-mehta", "slack", "U052345678B" });

            migrationBuilder.UpdateData(
                table: "DepartmentContacts",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "GoogleMeetUrl", "PreferredCommTool", "SlackMemberId" },
                values: new object[] { "https://meet.google.com/lau-r-a-bennett", "slack", "U052345678C" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMeetUrl",
                table: "DepartmentContacts");

            migrationBuilder.DropColumn(
                name: "PreferredCommTool",
                table: "DepartmentContacts");

            migrationBuilder.DropColumn(
                name: "SlackMemberId",
                table: "DepartmentContacts");
        }
    }
}
