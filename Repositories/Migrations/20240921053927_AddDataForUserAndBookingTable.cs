using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddDataForUserAndBookingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 12, 39, 27, 63, DateTimeKind.Local).AddTicks(1656));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "MembershipId", "Name", "Password", "PasswordHash", "RoleId", "Status" },
                values: new object[] { 2, "Adam@gmail.com", null, "Adam", null, null, null, 1 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingPrice", "BookingStatusId", "CreatedTime", "UserId" },
                values: new object[] { 2, 10000, 5, new DateTime(2024, 9, 21, 12, 39, 27, 63, DateTimeKind.Local).AddTicks(1670), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 12, 18, 37, 759, DateTimeKind.Local).AddTicks(5874));
        }
    }
}
