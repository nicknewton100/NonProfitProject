using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class databaseSpellingErrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Committees_CommitteeID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Committees_CommitteeID",
                table: "News");

            migrationBuilder.DropTable(
                name: "DonationRecipts");

            migrationBuilder.DropIndex(
                name: "IX_News_CommitteeID",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Events_CommitteeID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CommitteeID",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CommitteeID",
                table: "Events");

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveWeeklyNewsletter",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DonationReceipts",
                columns: table => new
                {
                    DonationRecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    ReceiptDonationID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiptDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationReceipts");

            migrationBuilder.RenameColumn(
                name: "ReceiveWeeklyNewsletter",
                table: "AspNetUsers",
                newName: "recieveWeeklyNewsletter");

            migrationBuilder.AddColumn<int>(
                name: "CommitteeID",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommitteeID",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DonationRecipts",
                columns: table => new
                {
                    DonationRecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    ReciptDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReciptDonationID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationRecipts", x => x.DonationRecId);
                    table.ForeignKey(
                        name: "FK_DonationRecipts_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonationRecipts_Donations_ReciptDonationID",
                        column: x => x.ReciptDonationID,
                        principalTable: "Donations",
                        principalColumn: "DonationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonationRecipts_MembershipDues_ReciptDonationID",
                        column: x => x.ReciptDonationID,
                        principalTable: "MembershipDues",
                        principalColumn: "MemDuesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonationRecipts_PaymentInformation_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "PaymentInformation",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_CommitteeID",
                table: "News",
                column: "CommitteeID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CommitteeID",
                table: "Events",
                column: "CommitteeID");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRecipts_PaymentID",
                table: "DonationRecipts",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRecipts_ReciptDonationID",
                table: "DonationRecipts",
                column: "ReciptDonationID",
                unique: true,
                filter: "[ReciptDonationID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DonationRecipts_UserID",
                table: "DonationRecipts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Committees_CommitteeID",
                table: "Events",
                column: "CommitteeID",
                principalTable: "Committees",
                principalColumn: "CommitteesID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Committees_CommitteeID",
                table: "News",
                column: "CommitteeID",
                principalTable: "Committees",
                principalColumn: "CommitteesID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
