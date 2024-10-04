using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataForPodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 15, 55, 11, 554, DateTimeKind.Local).AddTicks(873));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 15, 55, 11, 554, DateTimeKind.Local).AddTicks(888));

            migrationBuilder.UpdateData(
                table: "Pods",
                keyColumn: "Id",
                keyValue: 2,
                column: "PodTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pods",
                keyColumn: "Id",
                keyValue: 3,
                column: "PodTypeId",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Pods",
                keyColumn: "Id",
                keyValue: 2,
                column: "PodTypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Pods",
                keyColumn: "Id",
                keyValue: 3,
                column: "PodTypeId",
                value: 1);
        }
    }
}
