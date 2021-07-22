using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class newspage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "8f8dc08f-b7da-416c-8ddd-30748dbfdeae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9c17f00-28b9-40d6-aced-4b386f2d7418", "AQAAAAEAACcQAAAAEPOtr1t0XFG/ZR6lyPrlyn48CeW2nFQ8ggtLRfYQ5yLBlICZgNwA0HPsm17pIHMe2g==", "dcd36c94-d24f-4f27-8487-e1236c8f34e2" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 18, 14, 57, 39, 429, DateTimeKind.Utc).AddTicks(2738));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 18, 14, 57, 39, 429, DateTimeKind.Utc).AddTicks(3200));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 18, 14, 57, 39, 429, DateTimeKind.Utc).AddTicks(3210));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
