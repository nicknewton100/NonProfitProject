using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class removedUnUsedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsCreationDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsFooter",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Donations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "449c12d3-164f-4c93-bf65-d238a6a32a26");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f18d22b-91cf-4adb-a7ef-3bd423763a66", "AQAAAAEAACcQAAAAED4VV6V1x6WhV2/Nq0fPqVLnruf+6w1ZCVw5f+IOSkeKeW0V1oiFUG9j4oxhdmCoJg==", "886aa38f-36d2-44e0-96fe-e3d8e871be55" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(9546));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(9831));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1,
                columns: new[] { "CreatedBy", "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { "Unknown", new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(5235), new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(4858) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2,
                columns: new[] { "CreatedBy", "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { "Unknown", new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(5972), new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(5952) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NewsCreationDate",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NewsFooter",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "e49c2b1f-dc20-4f7d-8e37-fff21537b564");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cab2dfb0-80ec-43ee-9ad5-51f34eab610a", "AQAAAAEAACcQAAAAEPHWlQDi39ztIFucyh3xrbrLU0z8awB7e2zsrYnl8F4h9l4rQqpPq7gVAV2gvIfcvw==", "9a1a7494-f94c-4e46-a810-870a15aff32c" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 24, 21, 3, 28, 192, DateTimeKind.Utc).AddTicks(8927));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 24, 21, 3, 28, 192, DateTimeKind.Utc).AddTicks(9262));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 24, 21, 3, 28, 192, DateTimeKind.Utc).AddTicks(9271));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1,
                columns: new[] { "CreatedBy", "NewsCreationDate", "NewsFooter", "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { null, new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "News Comittee", new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2,
                columns: new[] { "CreatedBy", "NewsCreationDate", "NewsFooter", "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { null, new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "News Comittee", new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
