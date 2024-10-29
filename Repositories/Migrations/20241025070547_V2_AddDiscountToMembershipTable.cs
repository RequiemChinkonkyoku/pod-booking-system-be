using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V2_AddDiscountToMembershipTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Memberships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 25, 14, 5, 46, 484, DateTimeKind.Local).AddTicks(5538));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 25, 14, 5, 46, 484, DateTimeKind.Local).AddTicks(5550));

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 1,
                column: "Discount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 2,
                column: "Discount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 3,
                column: "Discount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$8akfpwahrqFCHBHZOHGhJ.s/7E4Qx40hZN5ghvJLQ81WmtGXxdXvO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$dzD6fqEn3q4./1UL1zGV7.vmc0r8LscKDtEJLH0xPqeVpIMhQJL52");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$04shj6r/aL5r6LWmArU6De6Nl8WWEbWIwTxIBJcDC9uLr3uL9iEEy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$dibKWEwL6y2hDeQdC7tDKuVmAoWkJZZeJ1yD2Y492tU3lX8AwGUL.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Memberships");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 25, 13, 36, 22, 541, DateTimeKind.Local).AddTicks(765));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 25, 13, 36, 22, 541, DateTimeKind.Local).AddTicks(778));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$2L9B2.bKK2/FTiReUoxA8uJcoyrHLodCqNTq6Jnrn0tQchCRC279i");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$VOpEH/Z67qjCSkP1koePOuzY3tg8eh7mqZOafyshqJFVI5/O4PzW2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$JMtCDzX.LUc3UcnXalOPZeIS1imgmOBtTr/0IpzoUhYYVR8JOke8.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$LnEk3wMUq5YCPVsvZyOLwu/DZIi6nsQ.kqyDl.Zg4OpMRcrMnAhX2");
        }
    }
}
