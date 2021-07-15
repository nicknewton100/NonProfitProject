using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class DeletedUserCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCountry",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "4f634df2-a0ba-4000-ae50-071f12ac1d1e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e55cd9cb-acd1-4e17-8a91-a07280e00f37", "AQAAAAEAACcQAAAAEG/vtQavCtLxEqF2zPcUnmN3eVPjMafZDN8uWpMRsocz6Pinledu16/kX+JCg9umJQ==", "9c429695-41ab-488b-8896-ec65cfa5a147" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 15, 15, 49, 34, 564, DateTimeKind.Utc).AddTicks(9937));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 15, 15, 49, 34, 565, DateTimeKind.Utc).AddTicks(947));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 15, 15, 49, 34, 565, DateTimeKind.Utc).AddTicks(962));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCountry",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "07964a3d-1832-433d-b216-41f42721b06f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserCountry" },
                values: new object[] { "d7e3c647-03f1-4224-94ae-bf09fae4a75a", "AQAAAAEAACcQAAAAEE5t8X6iNB8lPbopi/NcmkSdOsgfY8NT3Sp0l3dl5k7VFl0Fi+uFkjTV4ubDgYp1PQ==", "912c19ed-71f2-4adc-808d-3d537adc53e8", "United States Of America" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 13, 20, 7, 5, 936, DateTimeKind.Utc).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 13, 20, 7, 5, 936, DateTimeKind.Utc).AddTicks(923));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 13, 20, 7, 5, 936, DateTimeKind.Utc).AddTicks(934));
        }
    }
}
