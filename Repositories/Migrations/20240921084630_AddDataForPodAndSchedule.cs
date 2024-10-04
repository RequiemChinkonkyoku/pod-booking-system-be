using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddDataForPodAndSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "PodTypes",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 2, "Pod for two.", "Double Pod", 20000 },
                    { 3, "Luxurious Pod.", "Premium Pod", 10000 }
                });

            migrationBuilder.InsertData(
                table: "Pods",
                columns: new[] { "Id", "AreaId", "Description", "Name", "PodTypeId", "Status" },
                values: new object[,]
                {
                    { 2, 1, "Nice Pod", "Pod 2", 1, 1 },
                    { 3, 1, "Premium Pod", "Pod 3", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "EndTime", "StartTime", "Status" },
                values: new object[,]
                {
                    { 2, new TimeOnly(9, 0, 0), new TimeOnly(8, 0, 0), 1 },
                    { 3, new TimeOnly(10, 0, 0), new TimeOnly(9, 0, 0), 1 },
                    { 4, new TimeOnly(11, 0, 0), new TimeOnly(10, 0, 0), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PodTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PodTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 15, 33, 8, 477, DateTimeKind.Local).AddTicks(3904));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 15, 33, 8, 477, DateTimeKind.Local).AddTicks(3915));
        }
    }
}
