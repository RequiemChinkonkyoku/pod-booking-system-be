using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataForPodTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 15, 48, 16, 839, DateTimeKind.Local).AddTicks(2495));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 15, 48, 16, 839, DateTimeKind.Local).AddTicks(2514));

            migrationBuilder.UpdateData(
                table: "PodTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 50000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 15, 46, 30, 589, DateTimeKind.Local).AddTicks(9993));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 15, 46, 30, 590, DateTimeKind.Local).AddTicks(5));

            migrationBuilder.UpdateData(
                table: "PodTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 10000);
        }
    }
}
