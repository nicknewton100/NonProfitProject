using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class createdMembershipTypeValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "MembershipTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MembershipTypeID",
                table: "MembershipDues",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "434db00d-6c5e-4f13-a620-f91894a406d3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0bd4b50-751c-473c-89fa-b06e19caf2a5", "AQAAAAEAACcQAAAAECmmnOqVptgPsKXvprg0xQloQEYbaqY89G0k6cqnPWQzp4DhpJVbUnNC5yIVefZkQw==", "b76cc1f7-f720-48b4-9a19-402cecfb6a21" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 41, 11, 191, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 41, 11, 191, DateTimeKind.Utc).AddTicks(3082));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 23, 15, 41, 11, 191, DateTimeKind.Utc).AddTicks(3094));

            migrationBuilder.InsertData(
                table: "MembershipTypes",
                columns: new[] { "MembershipTypeID", "Amount", "Name" },
                values: new object[,]
                {
                    { "fdsfdf-n53g3g-h3j9xe768w-nm4b35", 10.00m, "Basic" },
                    { "fs8t4h-chgje6-dshuv57d8-sng94v", 20.00m, "Advanced" },
                    { "dk5k4g-df5h7d-v5y8s2ch5t-f5h5db", 50.00m, "Premium" },
                    { "jk5dgd-eh4d6h-f5sf4g77h5-dfs4g", 100.00m, "Paw-fect" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipDues_MembershipTypeID",
                table: "MembershipDues",
                column: "MembershipTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipDues_MembershipTypes_MembershipTypeID",
                table: "MembershipDues",
                column: "MembershipTypeID",
                principalTable: "MembershipTypes",
                principalColumn: "MembershipTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipDues_MembershipTypes_MembershipTypeID",
                table: "MembershipDues");

            migrationBuilder.DropIndex(
                name: "IX_MembershipDues_MembershipTypeID",
                table: "MembershipDues");

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "MembershipTypeID",
                keyValue: "dk5k4g-df5h7d-v5y8s2ch5t-f5h5db");

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "MembershipTypeID",
                keyValue: "fdsfdf-n53g3g-h3j9xe768w-nm4b35");

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "MembershipTypeID",
                keyValue: "fs8t4h-chgje6-dshuv57d8-sng94v");

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "MembershipTypeID",
                keyValue: "jk5dgd-eh4d6h-f5sf4g77h5-dfs4g");

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                table: "MembershipTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "MembershipTypeID",
                table: "MembershipDues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
    }
}
