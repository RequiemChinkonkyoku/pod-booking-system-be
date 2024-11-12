using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V2_UpdateMembershipTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoyaltyPoints",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointsRequirement",
                table: "Memberships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 11, 11, 23, 13, 40, 392, DateTimeKind.Local).AddTicks(1509));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 11, 11, 23, 13, 40, 392, DateTimeKind.Local).AddTicks(1524));

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 1,
                column: "PointsRequirement",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 2,
                column: "PointsRequirement",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 3,
                column: "PointsRequirement",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LoyaltyPoints", "PasswordHash" },
                values: new object[] { 0, "$2a$11$Z3EqOmgSuCTgdamMckJrZ.4w/58B8GVSF71cB5/jnOb5OfEC8WHee" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LoyaltyPoints", "PasswordHash" },
                values: new object[] { 0, "$2a$11$eimNbgrLaK/MZAmgnjRIKe0Adj2ucRgAXKKr74GUFs3520DOwkpx." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LoyaltyPoints", "PasswordHash" },
                values: new object[] { 0, "$2a$11$xT7miq/oZKUOBV/PJJ5LbOMebJqkQvCPr2w9Aw/qqD.dyZM1dNyzu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LoyaltyPoints", "PasswordHash" },
                values: new object[] { 0, "$2a$11$LrucLf3WcqC40ov2ovtBwO9wEokU04M3Q6eUwSC.WuJsCWWxMiYVW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoyaltyPoints",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PointsRequirement",
                table: "Memberships");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 25, 14, 34, 2, 28, DateTimeKind.Local).AddTicks(7723));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 10, 25, 14, 34, 2, 28, DateTimeKind.Local).AddTicks(7740));

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
        }
    }
}
