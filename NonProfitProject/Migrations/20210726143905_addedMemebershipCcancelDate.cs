using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class addedMemebershipCcancelDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MemCancelDate",
                table: "MembershipDues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "729c05a6-9c71-473a-b98b-73aff7b9f297");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2f8d9a8-8554-4c61-bdd0-987073286dff", "AQAAAAEAACcQAAAAEGsyaQ6mbbI9OdAnwznGLzgfTM8hgr7vYs+yXzQPUVI4ww6jRKWpEx/VY3/WuKISLg==", "6e9f609d-4af3-4dfd-8e32-891758ba961b" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 26, 14, 39, 5, 209, DateTimeKind.Utc).AddTicks(7588));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 26, 14, 39, 5, 209, DateTimeKind.Utc).AddTicks(7867));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 26, 14, 39, 5, 209, DateTimeKind.Utc).AddTicks(7978));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1,
                columns: new[] { "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { new DateTime(2021, 7, 26, 14, 39, 5, 209, DateTimeKind.Utc).AddTicks(3532), new DateTime(2021, 7, 26, 14, 39, 5, 209, DateTimeKind.Utc).AddTicks(3218) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2,
                columns: new[] { "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { new DateTime(2021, 7, 26, 14, 39, 5, 209, DateTimeKind.Utc).AddTicks(4322), new DateTime(2021, 7, 26, 14, 39, 5, 209, DateTimeKind.Utc).AddTicks(4304) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemCancelDate",
                table: "MembershipDues");

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
    }
}
