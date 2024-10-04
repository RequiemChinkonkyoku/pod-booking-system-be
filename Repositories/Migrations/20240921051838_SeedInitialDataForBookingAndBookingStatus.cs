using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialDataForBookingAndBookingStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookingsStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cancelled" },
                    { 2, "Pending" },
                    { 3, "Reserved" },
                    { 4, "On-going" },
                    { 5, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingPrice", "BookingStatusId", "CreatedTime", "UserId" },
                values: new object[] { 1, 10000, 5, new DateTime(2024, 9, 21, 12, 18, 37, 759, DateTimeKind.Local).AddTicks(5874), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookingsStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookingsStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookingsStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookingsStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookingsStatuses",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
