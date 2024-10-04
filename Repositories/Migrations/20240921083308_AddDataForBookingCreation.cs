using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddDataForBookingCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Slots",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "Description", "Location", "Name" },
                values: new object[] { 1, "Beautiful location.", "Floor 1", "Area A" });

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

            migrationBuilder.InsertData(
                table: "PodTypes",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[] { 1, "Pod for one.", "Single Pod", 10000 });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "EndTime", "StartTime", "Status" },
                values: new object[] { 1, new TimeOnly(8, 0, 0), new TimeOnly(7, 0, 0), 1 });

            migrationBuilder.InsertData(
                table: "Pods",
                columns: new[] { "Id", "AreaId", "Description", "Name", "PodTypeId", "Status" },
                values: new object[] { 1, 1, "Clean Pod", "Pod 1", 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PodTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Slots");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 12, 39, 27, 63, DateTimeKind.Local).AddTicks(1656));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 9, 21, 12, 39, 27, 63, DateTimeKind.Local).AddTicks(1670));
        }
    }
}
