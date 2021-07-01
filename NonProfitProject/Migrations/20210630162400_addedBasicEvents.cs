using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class addedBasicEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "EventDescription", "EventEndDate", "EventName", "EventStartDate" },
                values: new object[] { 1, "Walking for a good cause.", new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Walk-a-thon", new DateTime(2021, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "EventDescription", "EventEndDate", "EventName", "EventStartDate" },
                values: new object[] { 2, "Fun event coming soon!", new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Triathon", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "EventDescription", "EventEndDate", "EventName", "EventStartDate" },
                values: new object[] { 3, "Fun event coming soon!", new DateTime(2022, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Five days of Help", new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3);
        }
    }
}
