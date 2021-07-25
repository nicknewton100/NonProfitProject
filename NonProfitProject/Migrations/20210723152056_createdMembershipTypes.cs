using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class createdMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemAmount",
                table: "MembershipDues");

            migrationBuilder.AddColumn<string>(
                name: "MembershipTypeID",
                table: "MembershipDues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CommitteePosition",
                table: "CommitteeMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                columns: table => new
                {
                    MembershipTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTypes", x => x.MembershipTypeID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "473a683d-cbec-4cb1-94f0-523dece737eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e85e09cc-f7b9-40e3-848a-70a15f9740ab", "AQAAAAEAACcQAAAAEJz5CbHb4ATLl4cWyHyIjCJMXGQBr7WakOB3MPkVMLOZ5J8AAyEbsb+r2iF2mD7pOA==", "4b378cc7-94a1-4948-9db3-e3a869e63e3e" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 20, 55, 455, DateTimeKind.Utc).AddTicks(7747));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 20, 55, 455, DateTimeKind.Utc).AddTicks(8019));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 20, 55, 455, DateTimeKind.Utc).AddTicks(8029));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipTypes");

            migrationBuilder.DropColumn(
                name: "MembershipTypeID",
                table: "MembershipDues");

            migrationBuilder.AddColumn<decimal>(
                name: "MemAmount",
                table: "MembershipDues",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "CommitteePosition",
                table: "CommitteeMembers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
