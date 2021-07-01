using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class addedEmployeeAndCommittees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeMembers_Employees_employeeEmpID",
                table: "CommitteeMembers");

            migrationBuilder.DropIndex(
                name: "IX_CommitteeMembers_employeeEmpID",
                table: "CommitteeMembers");

            migrationBuilder.DropColumn(
                name: "employeeEmpID",
                table: "CommitteeMembers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Employees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "EmpID",
                table: "CommitteeMembers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserLastActivity",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getUTCDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserCreationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getUTCDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getDate()");

            migrationBuilder.InsertData(
                table: "Committees",
                columns: new[] { "CommitteesID", "CommitteeCreationDate", "CommitteeDescription", "CommitteeName" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 7, 1, 17, 38, 23, 164, DateTimeKind.Utc).AddTicks(7349), "Manages donations/membership dues", "Fundrasing Committee" },
                    { 2, new DateTime(2021, 7, 1, 17, 38, 23, 164, DateTimeKind.Utc).AddTicks(7872), "Manages news on the website", "News Committee" },
                    { 3, new DateTime(2021, 7, 1, 17, 38, 23, 164, DateTimeKind.Utc).AddTicks(7882), "Plans and organizes events", "Event and Planning Committee" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmpID", "Department", "HireDate", "ReleaseDate", "Salary", "UserID" },
                values: new object[] { "1", "Finance", new DateTime(2020, 2, 4, 11, 14, 0, 0, DateTimeKind.Unspecified), null, 54000m, "6b87b89f-0f9a-4e2d-b696-235e99655521" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                column: "EventDescription",
                value: "Biking, Swimming, and Running");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "EventDescription", "EventName" },
                values: new object[] { "Finding Nemo with yummy snacks and any drink of choice. Cost of entry is $5 for movie and snacks!", "Movie Night" });

            migrationBuilder.InsertData(
                table: "CommitteeMembers",
                columns: new[] { "CommitteeMbrID", "CommitteeID", "CommitteePosition", "EmpID" },
                values: new object[] { 1, 1, "Committee Manager", "1" });

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeMembers_EmpID",
                table: "CommitteeMembers",
                column: "EmpID",
                unique: true,
                filter: "[EmpID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeMembers_Employees_EmpID",
                table: "CommitteeMembers",
                column: "EmpID",
                principalTable: "Employees",
                principalColumn: "EmpID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommitteeMembers_Employees_EmpID",
                table: "CommitteeMembers");

            migrationBuilder.DropIndex(
                name: "IX_CommitteeMembers_EmpID",
                table: "CommitteeMembers");

            migrationBuilder.DeleteData(
                table: "CommitteeMembers",
                keyColumn: "CommitteeMbrID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmpID",
                keyValue: "1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmpID",
                table: "CommitteeMembers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "employeeEmpID",
                table: "CommitteeMembers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserLastActivity",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getUTCDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserCreationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getUTCDate()");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                column: "EventDescription",
                value: "Fun event coming soon!");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "EventDescription", "EventName" },
                values: new object[] { "Fun event coming soon!", "Five days of Help" });

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeMembers_employeeEmpID",
                table: "CommitteeMembers",
                column: "employeeEmpID");

            migrationBuilder.AddForeignKey(
                name: "FK_CommitteeMembers_Employees_employeeEmpID",
                table: "CommitteeMembers",
                column: "employeeEmpID",
                principalTable: "Employees",
                principalColumn: "EmpID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
