using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NonProfitProject.Migrations
{
    public partial class fixedUserEmployeeMigrationError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "370d9876-b6ab-4694-baa9-ecc7bc5b451c", "a5488fad-62f9-4eba-941f-402f596b8904", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserActive", "UserAddr1", "UserAddr2", "UserBirthDate", "UserCity", "UserCountry", "UserFirstName", "UserGender", "UserLastName", "UserName", "UserPostalCode", "UserState" },
                values: new object[] { "6b87b89f-0f9a-4e2d-b696-235e99655521", 0, "125ec4dc-46b1-492a-b1db-9b972d41ed46", "JohnJones@gmail.com", false, false, null, null, null, "AQAAAAEAACcQAAAAEEH6WU4Oz7oYJehlivSaJ3PkrBAFmHmHzVz1gAx9ZJNsIvSDQj9hy46a+PC+sYLMRQ==", null, false, "a4106d5a-09e0-4741-bbdb-3ade2177f0df", false, true, "513 S Augusta St", null, new DateTime(1987, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greenville", "United States Of America", "John", "Male", "Jones", "JohnJones", 29607, "South Carolina" });

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "370d9876-b6ab-4694-baa9-ecc7bc5b451c", "6b87b89f-0f9a-4e2d-b696-235e99655521" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmpID", "Position", "HireDate", "ReleaseDate", "Salary", "UserID" },
                values: new object[] { "1", "Finance", new DateTime(2020, 2, 4, 11, 14, 0, 0, DateTimeKind.Unspecified), null, 54000m, "6b87b89f-0f9a-4e2d-b696-235e99655521" });

            migrationBuilder.InsertData(
                table: "CommitteeMembers",
                columns: new[] { "CommitteeMbrID", "CommitteeID", "CommitteePosition", "EmpID" },
                values: new object[] { 1, 1, "Committee Manager", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "370d9876-b6ab-4694-baa9-ecc7bc5b451c", "6b87b89f-0f9a-4e2d-b696-235e99655521" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "370d9876-b6ab-4694-baa9-ecc7bc5b451c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b87b89f-0f9a-4e2d-b696-235e99655521");

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 1,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 5, 19, 17, 34, 360, DateTimeKind.Utc).AddTicks(236));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 2,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 5, 19, 17, 34, 360, DateTimeKind.Utc).AddTicks(759));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "CommitteesID",
                keyValue: 3,
                column: "CommitteeCreationDate",
                value: new DateTime(2021, 7, 5, 19, 17, 34, 360, DateTimeKind.Utc).AddTicks(769));
        }
    }
}
