using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class AddedNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptID);
                    table.ForeignKey(
                        name: "FK_Receipts_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SavedPayments",
                columns: table => new
                {
                    SavedPaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CardholderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    Last4Digits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedPayments", x => x.SavedPaymentID);
                    table.ForeignKey(
                        name: "FK_SavedPayments_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiptID = table.Column<int>(type: "int", nullable: false),
                    DonationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationID);
                    table.ForeignKey(
                        name: "FK_Donations_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_Receipts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDonorInformation",
                columns: table => new
                {
                    DonorInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addr1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addr2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDonorInformation", x => x.DonorInfoID);
                    table.ForeignKey(
                        name: "FK_InvoiceDonorInformation_Receipts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayments",
                columns: table => new
                {
                    InvPaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptID = table.Column<int>(type: "int", nullable: false),
                    CardholderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    Last4Digits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayments", x => x.InvPaymentID);
                    table.ForeignKey(
                        name: "FK_InvoicePayments_Receipts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipDues",
                columns: table => new
                {
                    MemDuesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiptID = table.Column<int>(type: "int", nullable: false),
                    MemAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemRenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipDues", x => x.MemDuesID);
                    table.ForeignKey(
                        name: "FK_MembershipDues_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembershipDues_Receipts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                column: "ConcurrencyStamp",
                value: "3f80f121-13fa-4401-a1d8-14e9c7d8686e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14982664-8590-44de-bf48-2cda2f520118", "AQAAAAEAACcQAAAAEPN8eGFKE3BwbIHi+xoPMGEdLwAXHzdARPl+k853Gtupe8RPKUR/d7R2X75tCUJRUw==", "58ba1345-e61d-4726-8227-04b8cddd6949" });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 22, 3, 36, 939, DateTimeKind.Utc).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 22, 3, 36, 939, DateTimeKind.Utc).AddTicks(9271));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 8, 22, 3, 36, 939, DateTimeKind.Utc).AddTicks(9281));

            migrationBuilder.CreateIndex(
                name: "IX_Donations_ReceiptID",
                table: "Donations",
                column: "ReceiptID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_UserID",
                table: "Donations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDonorInformation_ReceiptID",
                table: "InvoiceDonorInformation",
                column: "ReceiptID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_ReceiptID",
                table: "InvoicePayments",
                column: "ReceiptID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipDues_ReceiptID",
                table: "MembershipDues",
                column: "ReceiptID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipDues_UserID",
                table: "MembershipDues",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_UserID",
                table: "Receipts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPayments_UserID",
                table: "SavedPayments",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "InvoiceDonorInformation");

            migrationBuilder.DropTable(
                name: "InvoicePayments");

            migrationBuilder.DropTable(
                name: "MembershipDues");

            migrationBuilder.DropTable(
                name: "SavedPayments");

            migrationBuilder.DropTable(
                name: "Receipts");

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
    }
}
