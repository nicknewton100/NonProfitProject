using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class addedCreatedByFieldNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NewsTitle",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewsHeader",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewsBody",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "News",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "News");

            migrationBuilder.AlterColumn<string>(
                name: "NewsTitle",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NewsHeader",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NewsBody",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
        }
    }
}
