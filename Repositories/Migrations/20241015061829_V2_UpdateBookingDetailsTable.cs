using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V2_UpdateBookingDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotPrice",
                table: "BookingsDetails");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 15, 13, 18, 28, 496, DateTimeKind.Local).AddTicks(9718));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 15, 13, 18, 28, 496, DateTimeKind.Local).AddTicks(9730));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Na3EtpBysuWSvrNy4j38xuzTvAjOFJBgRBlgSVfuz/foweXqpz7.W");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$PtQzsaDn324o9LjfO.37rec5eqbzqKOKDMdZ9/Bvoa0RSb/j2lWYi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$1yTypC/ZXkkU4azv2MpvYO5/NtutLPBGQNmh6WI99sr8OJF4SO232");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$Ni58tMDLDzwx3riC6LBdtOD/st0TneJP29/bi22CLvUwJ5balcyDu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlotPrice",
                table: "BookingsDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 15, 13, 15, 4, 723, DateTimeKind.Local).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 15, 13, 15, 4, 723, DateTimeKind.Local).AddTicks(9276));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$pU3bl65ouhwL3L/7wW47PuWwsP7i30QAK8eNPkIshLeDzCqXlP8hi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$iazPSLENzo6Ydwv44TMm9e5QyZms9l1XdN0SGu7M5s56CxsZLL7a6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$2DeSBOnupbwvPnozwGOzUOSLV3/DvE5AukzJXKithBA98M8SvLh4y");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$3BKHBFZF6kVwOMzaUkbJjeCkTRgKLLJQysorzrVHLHj/.PlmpP44K");
        }
    }
}
