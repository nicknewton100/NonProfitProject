using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class addedBillingAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "InvoiceDonorInformation");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddr1",
                table: "SavedPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddr2",
                table: "SavedPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingCity",
                table: "SavedPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillingPostalCode",
                table: "SavedPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BillingState",
                table: "SavedPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddr1",
                table: "InvoicePayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddr2",
                table: "InvoicePayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingCity",
                table: "InvoicePayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillingPostalCode",
                table: "InvoicePayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BillingState",
                table: "InvoicePayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "8ecbe39f-60d3-4da3-9691-3a97cfcb46ed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80207f0f-48f5-4c02-ac09-6955c4e969b0", "AQAAAAEAACcQAAAAEM9C1Bytfod3xdRNIRx9aVbY9QcpH0etzuW95bvlRdpRssfkJTrvzHjyRgTPydjOhQ==", "a865bf77-29a6-4b5a-a765-9377f5f793dd" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 16, 20, 40, 36, 338, DateTimeKind.Utc).AddTicks(4231));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 16, 20, 40, 36, 338, DateTimeKind.Utc).AddTicks(4695));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 16, 20, 40, 36, 338, DateTimeKind.Utc).AddTicks(4706));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddr1",
                table: "SavedPayments");

            migrationBuilder.DropColumn(
                name: "BillingAddr2",
                table: "SavedPayments");

            migrationBuilder.DropColumn(
                name: "BillingCity",
                table: "SavedPayments");

            migrationBuilder.DropColumn(
                name: "BillingPostalCode",
                table: "SavedPayments");

            migrationBuilder.DropColumn(
                name: "BillingState",
                table: "SavedPayments");

            migrationBuilder.DropColumn(
                name: "BillingAddr1",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "BillingAddr2",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "BillingCity",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "BillingPostalCode",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "BillingState",
                table: "InvoicePayments");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "InvoiceDonorInformation",
                type: "nvarchar(max)",
                nullable: true);

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
