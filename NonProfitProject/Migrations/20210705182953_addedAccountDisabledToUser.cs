using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class addedAccountDisabledToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Employees",
                newName: "Position");

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                table: "Events",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventDescription",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FinishedAccountSetup",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserBirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "AccountDisabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CommitteeMembers",
                keyColumn: "CommitteeMbrID",
                keyValue: 1,
                column: "CommitteePosition",
                value: "Committee President");

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 5, 18, 29, 52, 898, DateTimeKind.Utc).AddTicks(6417));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 5, 18, 29, 52, 898, DateTimeKind.Utc).AddTicks(6929));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 5, 18, 29, 52, 898, DateTimeKind.Utc).AddTicks(6939));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmpID",
                keyValue: "1",
                column: "Position",
                value: "Accountant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedAccountSetup",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AccountDisabled",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Employees",
                newName: "Department");

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "EventDescription",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UserBirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "CommitteeMembers",
                keyColumn: "CommitteeMbrID",
                keyValue: 1,
                column: "CommitteePosition",
                value: "Committee Manager");

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 1, 17, 38, 23, 164, DateTimeKind.Utc).AddTicks(7349));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 1, 17, 38, 23, 164, DateTimeKind.Utc).AddTicks(7872));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 1, 17, 38, 23, 164, DateTimeKind.Utc).AddTicks(7882));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmpID",
                keyValue: "1",
                column: "Department",
                value: "Finance");
        }
    }
}
