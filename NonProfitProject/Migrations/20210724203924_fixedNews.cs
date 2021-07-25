using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class fixedNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewsDescription",
                table: "News",
                newName: "NewsBody");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "0d12e372-8665-44a4-b06a-b07a31e84094");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "000885f9-c0cc-4476-a9ad-d1d8ec123495", "AQAAAAEAACcQAAAAEMgFWWlmVQebNMdrYrYtUbh5aEIsodtoaTfAeyx5PCnY1dc9FXhfeKU1sWcbNxKWTQ==", "53117a30-bba1-4e04-932c-27d0ce3d2d8b" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 24, 20, 39, 24, 87, DateTimeKind.Utc).AddTicks(8980));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 24, 20, 39, 24, 87, DateTimeKind.Utc).AddTicks(9246));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 24, 20, 39, 24, 87, DateTimeKind.Utc).AddTicks(9258));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1,
                columns: new[] { "NewsBody", "NewsHeader", "NewsTitle" },
                values: new object[] { "The Non-PAW-Fit Animal Rescue started their non-profit organization to raise awareness of abandoned pets across the United States.  Then mission:  To Rescue Pets from unwanted homes and provide them new home where they are become part of the family.", "New Members are Wecome", "Non-PAW-Fit Raise Awareness" });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2,
                columns: new[] { "NewsBody", "NewsHeader", "NewsTitle" },
                values: new object[] { "Pets from cats and dogs to parrots and snakes are being rescued from unwanted homes and given a place to stay until they find their forever home.", "50 Animals Rescued From Unwanted Homes", "Non-PAW-Fit Rescued Over 50" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewsBody",
                table: "News",
                newName: "NewsDescription");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "434db00d-6c5e-4f13-a620-f91894a406d3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0bd4b50-751c-473c-89fa-b06e19caf2a5", "AQAAAAEAACcQAAAAECmmnOqVptgPsKXvprg0xQloQEYbaqY89G0k6cqnPWQzp4DhpJVbUnNC5yIVefZkQw==", "b76cc1f7-f720-48b4-9a19-402cecfb6a21" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 41, 11, 191, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 41, 11, 191, DateTimeKind.Utc).AddTicks(3082));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 41, 11, 191, DateTimeKind.Utc).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1,
                columns: new[] { "NewsDescription", "NewsHeader", "NewsTitle" },
                values: new object[] { "Run for a whole week straight for cancer at the end of July. 15 Dollar Admission Fee. Event runs from July 24-31!", "Come Join Us!", "10k Run for Cancer!" });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2,
                columns: new[] { "NewsDescription", "NewsHeader", "NewsTitle" },
                values: new object[] { "Finding Nemo with yummy snacks and any drink of choice. Cost of entry is $5 for movie and snacks! Snacks: Popcorn. Event runs on July 26!", "Movie Time!", "Movie Night" });
        }
    }
}
