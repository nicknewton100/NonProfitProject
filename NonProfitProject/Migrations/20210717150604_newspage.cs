using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class newspage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewsDescription",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "4998f149-a675-4ae5-a0a5-bc729126fb74");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46fd83e4-7978-4809-85b9-883cbff42097", "AQAAAAEAACcQAAAAENvwbwixB9CkA450Msp6Zgo52Cko/zyb4LRlA7VgEMmrwaRM+DfEW12jqWTg/xZZDw==", "6bf0fbf0-0566-4152-8601-7da132ccdefb" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 17, 15, 6, 3, 624, DateTimeKind.Utc).AddTicks(5946));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 17, 15, 6, 3, 624, DateTimeKind.Utc).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 17, 15, 6, 3, 624, DateTimeKind.Utc).AddTicks(6444));

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsID", "NewsCreationDate", "NewsDescription", "NewsFooter", "NewsHeader", "NewsLastUpdated", "NewsPublishDate", "NewsTitle" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Run for a whole week straight for cancer at the end of July. 15 Dollar Admission Fee. Event runs from July 24-31!", "News Comittee", "Come Join Us!", new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "10k Run for Cancer!" },
                    { 2, new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finding Nemo with yummy snacks and any drink of choice. Cost of entry is $5 for movie and snacks! Snacks: Popcorn. Event runs on July 26!", "News Comittee", "Movie Time!", new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Night" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "NewsDescription",
                table: "News");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "b6139a74-87f9-4a0b-bbf7-b808e59f4833");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "962fa750-0583-4a13-b38e-2e127f35c632", "AQAAAAEAACcQAAAAEB8mZwBVLmxUOj7wE6Ov3PBqQUR1KOSFv+1JphDHD/mYKMtDyUTuSh9ILRVdukvNCg==", "721ef096-68c8-436e-b85f-999e12047f0b" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 15, 17, 52, 58, 637, DateTimeKind.Utc).AddTicks(963));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 15, 17, 52, 58, 637, DateTimeKind.Utc).AddTicks(1452));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 15, 17, 52, 58, 637, DateTimeKind.Utc).AddTicks(1462));
        }
    }
}
