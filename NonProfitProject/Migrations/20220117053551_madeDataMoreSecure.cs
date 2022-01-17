using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class madeDataMoreSecure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CommitteeName",
                table: "Committees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommitteeDescription",
                table: "Committees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommitteeCreationDate",
                table: "Committees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "a6c6eb77-255f-4be9-9446-c12f0531c30d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eed24e04-5dbe-4c16-8c4c-224d5cdf0a77", null, "37a14d03-4652-4a26-a204-ff7e1e424084" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2022, 1, 17, 5, 35, 50, 471, DateTimeKind.Utc).AddTicks(4014));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2022, 1, 17, 5, 35, 50, 471, DateTimeKind.Utc).AddTicks(4295));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2022, 1, 17, 5, 35, 50, 471, DateTimeKind.Utc).AddTicks(4305));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 1,
                columns: new[] { "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { new DateTime(2022, 1, 17, 5, 35, 50, 471, DateTimeKind.Utc).AddTicks(828), new DateTime(2022, 1, 17, 5, 35, 50, 471, DateTimeKind.Utc).AddTicks(623) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "NewsID",
                keyValue: 2,
                columns: new[] { "NewsLastUpdated", "NewsPublishDate" },
                values: new object[] { new DateTime(2022, 1, 17, 5, 35, 50, 471, DateTimeKind.Utc).AddTicks(1353), new DateTime(2022, 1, 17, 5, 35, 50, 471, DateTimeKind.Utc).AddTicks(1343) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CommitteeName",
                table: "Committees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CommitteeDescription",
                table: "Committees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommitteeCreationDate",
                table: "Committees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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
    }
}
