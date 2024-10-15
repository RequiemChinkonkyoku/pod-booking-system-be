using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "BookingsDetails");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Slots",
                newName: "ArrivalDate");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "Slots",
                newName: "Date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ArrivalDate",
                table: "BookingsDetails",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 15, 11, 6, 37, 307, DateTimeKind.Local).AddTicks(7553));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 15, 11, 6, 37, 307, DateTimeKind.Local).AddTicks(7573));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$tLa.5HjRT.9Gnzq.zGmuL.cLuVAAxc0Ug4luLcWcbq27.x5tVGRMa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$4T9uXOJUVYEcPPB2taeHoeuNTKfIa8HldLiTF7S3LXZFBdo1U2j8a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$3Xl9A/nS.ILuCu1go/vcd.6oufzpfh4B6m7RpoJAhBcba678Pp37y");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$pw5j3DIRbVrjE/yyVHZEr.s4ACBwFYo9vjJjaNbtuu6zsHU7y1M0u");
        }
    }
}
