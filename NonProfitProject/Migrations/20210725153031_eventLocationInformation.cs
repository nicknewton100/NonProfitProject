using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class eventLocationInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventAddr1",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventAddr2",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventCity",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EventPostalCode",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EventState",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "b33a9c42-989c-47c0-9995-fb5e30fe5b3e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "039c8654-f6af-4432-9c36-27ab129fc797", "AQAAAAEAACcQAAAAEBIEUqj/jTFw1ObByNVwVlO40YmjHjMd8wGIjvK9APVNfXi467jZMYW5FRC/PQKWKg==", "142d7c94-1232-4001-9ecd-b64d4ddcc900" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 25, 15, 30, 30, 508, DateTimeKind.Utc).AddTicks(8170));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 25, 15, 30, 30, 508, DateTimeKind.Utc).AddTicks(8448));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 25, 15, 30, 30, 508, DateTimeKind.Utc).AddTicks(8458));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "EventAddr1", "EventCity", "EventEndDate", "EventPostalCode", "EventStartDate", "EventState" },
                values: new object[] { "222 Magnolia Shaw A St", "North Augusta", new DateTime(2021, 1, 25, 16, 0, 0, 0, DateTimeKind.Unspecified), 29841, new DateTime(2021, 1, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "South Carolina" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "EventAddr1", "EventCity", "EventEndDate", "EventPostalCode", "EventStartDate", "EventState" },
                values: new object[] { "881 Glenn Rd", "Clover", new DateTime(2022, 3, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), 29710, new DateTime(2022, 3, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), "South Carolina" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "EventAddr1", "EventCity", "EventEndDate", "EventPostalCode", "EventStartDate", "EventState" },
                values: new object[] { "739 Gaillard Rd", "Moncks Corner", new DateTime(2022, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 29461, new DateTime(2022, 5, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "South Carolina" });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1,
                columns: new[] { "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { new DateTime(2021, 7, 25, 15, 30, 30, 508, DateTimeKind.Utc).AddTicks(4200), new DateTime(2021, 7, 25, 15, 30, 30, 508, DateTimeKind.Utc).AddTicks(3899) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2,
                columns: new[] { "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { new DateTime(2021, 7, 25, 15, 30, 30, 508, DateTimeKind.Utc).AddTicks(4888), new DateTime(2021, 7, 25, 15, 30, 30, 508, DateTimeKind.Utc).AddTicks(4870) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventAddr1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventAddr2",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventCity",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventPostalCode",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventState",
                table: "Events");

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
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "EventEndDate", "EventStartDate" },
                values: new object[] { new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "EventEndDate", "EventStartDate" },
                values: new object[] { new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "EventEndDate", "EventStartDate" },
                values: new object[] { new DateTime(2022, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1,
                columns: new[] { "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(5235), new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(4858) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2,
                columns: new[] { "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(5972), new DateTime(2021, 7, 25, 0, 12, 6, 0, DateTimeKind.Utc).AddTicks(5952) });
        }
    }
}
