using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V2_AddDiscountToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActualPrice",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualPrice", "CreatedTime", "Discount", "MembershipId" },
                values: new object[] { 0, new DateTime(2024, 10, 25, 14, 34, 2, 28, DateTimeKind.Local).AddTicks(7723), 0, null });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualPrice", "CreatedTime", "Discount", "MembershipId" },
                values: new object[] { 0, new DateTime(2024, 10, 25, 14, 34, 2, 28, DateTimeKind.Local).AddTicks(7740), 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$qIMhjWkgYBS4qzCDIIGt2OAp63Osl8orKTLEvdbv1mtj10RvIhCzO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$EjpwSTEUxeLmBwQwCH5leejZ.P3CxJBVh4zuK/ySUWj/Hh2m8NKjC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$m5sHnsQp1x99QOzkmdDb.eGUfma65NAl2na5UITFzDPy2U7lTzZnC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$ChOcxEpk7AO7VwYmxkDSrOJG8K.ENzKc6DYynm8dcpa5bLGV7U6/.");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MembershipId",
                table: "Bookings",
                column: "MembershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Memberships_MembershipId",
                table: "Bookings",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Memberships_MembershipId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_MembershipId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ActualPrice",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Bookings");

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
    }
}
