using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class updatedPaymentTablesAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingFirstName",
                table: "SavedPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingLastName",
                table: "SavedPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingFirstName",
                table: "InvoicePayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingLastName",
                table: "InvoicePayments",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingFirstName",
                table: "SavedPayments");

            migrationBuilder.DropColumn(
                name: "BillingLastName",
                table: "SavedPayments");

            migrationBuilder.DropColumn(
                name: "BillingFirstName",
                table: "InvoicePayments");

            migrationBuilder.DropColumn(
                name: "BillingLastName",
                table: "InvoicePayments");

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
    }
}
