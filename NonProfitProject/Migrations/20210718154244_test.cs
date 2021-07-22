using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "05f5d12c-8098-4422-841d-37f0d3c47bdd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a448536-2d44-49a0-8022-609db1310054", "AQAAAAEAACcQAAAAEG0HcKAlgIHXaA6ONcWzgpZbgMzdhRgXBpPDpi4iTHv0W034JYAXb+RRslkeFdEjAQ==", "a74a7e77-79cd-4d7b-8dfb-776f15dd065d" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 18, 15, 42, 43, 461, DateTimeKind.Utc).AddTicks(1344));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 18, 15, 42, 43, 461, DateTimeKind.Utc).AddTicks(1794));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 18, 15, 42, 43, 461, DateTimeKind.Utc).AddTicks(1804));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "aa1bc4e2-2663-4b94-8c88-161cd7b1c6fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbcb0afb-387e-411c-a5a4-625a1daa154f", "AQAAAAEAACcQAAAAEDNez4pIcTJDw+JI+vBFcIZriNexCWAG0qKvisKljbB4rxb2ZTEVeFKa31RFMQP2Bg==", "6e448099-9617-4508-b3f4-6a0b2fba09d6" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 17, 0, 52, 47, 280, DateTimeKind.Utc).AddTicks(4969));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 17, 0, 52, 47, 280, DateTimeKind.Utc).AddTicks(5763));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 17, 0, 52, 47, 280, DateTimeKind.Utc).AddTicks(5781));
        }
    }
}
