using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class DeletedOldTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationReceipts");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "MembershipDues");

            migrationBuilder.DropTable(
                name: "PaymentInformation");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "4b7624c9-98d1-4c51-acea-f17fba081e9e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c26c7630-920d-4764-b5c4-cc26062c1d9e", "AQAAAAEAACcQAAAAENpqBlMuOoS1Z/stdg36Z3iox2uPqXnv8FWUWO+8LffSdbXSH6XY9NZbkhsgSx+m0Q==", "392bdb75-a51f-4a4c-ae83-98f420fb66c8" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 20, 8, 31, 240, DateTimeKind.Utc).AddTicks(7034));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 20, 8, 31, 240, DateTimeKind.Utc).AddTicks(7502));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 20, 8, 31, 240, DateTimeKind.Utc).AddTicks(7512));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationID);
                });

            migrationBuilder.CreateTable(
                name: "MembershipDues",
                columns: table => new
                {
                    MemDuesID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MemActive = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    MemDueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemStartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipDues", x => x.MemDuesID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentInformation",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    CardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardholderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInformation", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_PaymentInformation_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonationReceipts",
                columns: table => new
                {
                    DonationRecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    ReceiptDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptDonationID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationReceipts", x => x.DonationRecId);
                    table.ForeignKey(
                        name: "FK_DonationReceipts_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonationReceipts_Donations_ReceiptDonationID",
                        column: x => x.ReceiptDonationID,
                        principalTable: "Donations",
                        principalColumn: "DonationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonationReceipts_MembershipDues_ReceiptDonationID",
                        column: x => x.ReceiptDonationID,
                        principalTable: "MembershipDues",
                        principalColumn: "MemDuesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonationReceipts_PaymentInformation_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "PaymentInformation",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "a5488fad-62f9-4eba-941f-402f596b8904");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "125ec4dc-46b1-492a-b1db-9b972d41ed46", "AQAAAAEAACcQAAAAEEH6WU4Oz7oYJehlivSaJ3PkrBAFmHmHzVz1gAx9ZJNsIvSDQj9hy46a+PC+sYLMRQ==", "a4106d5a-09e0-4741-bbdb-3ade2177f0df" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 1, 10, 14, 656, DateTimeKind.Utc).AddTicks(4802));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 1, 10, 14, 656, DateTimeKind.Utc).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 1, 10, 14, 656, DateTimeKind.Utc).AddTicks(5401));

            migrationBuilder.CreateIndex(
                name: "IX_DonationReceipts_PaymentID",
                table: "DonationReceipts",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_DonationReceipts_ReceiptDonationID",
                table: "DonationReceipts",
                column: "ReceiptDonationID",
                unique: true,
                filter: "[ReceiptDonationID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DonationReceipts_UserID",
                table: "DonationReceipts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInformation_UserID",
                table: "PaymentInformation",
                column: "UserID");
        }
    }
}
