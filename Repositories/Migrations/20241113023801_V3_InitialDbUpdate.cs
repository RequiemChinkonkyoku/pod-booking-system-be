using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V3_InitialDbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pods",
                keyColumn: "Id",
                keyValue: 1);

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
                keyValue: 1);

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

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PodTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PodTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PodTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "Description", "Location", "Name" },
                values: new object[] { 1, "Beautiful location.", "Floor 1", "Area A" });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "Description", "Discount", "Name", "PointsRequirement", "Status" },
                values: new object[,]
                {
                    { 1, "N/A", 0, "N/A", 0, 1 },
                    { 2, "No bonuses", 0, "Regular", 0, 1 },
                    { 3, "VIPPRO", 0, "VIP", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "PodTypes",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Pod for one.", "Single Pod", 10000 },
                    { 2, "Pod for two.", "Double Pod", 20000 },
                    { 3, "Luxurious Pod.", "Premium Pod", 50000 }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "EndTime", "StartTime", "Status" },
                values: new object[,]
                {
                    { 1, new TimeOnly(8, 0, 0), new TimeOnly(7, 0, 0), 1 },
                    { 2, new TimeOnly(9, 0, 0), new TimeOnly(8, 0, 0), 1 },
                    { 3, new TimeOnly(10, 0, 0), new TimeOnly(9, 0, 0), 1 },
                    { 4, new TimeOnly(11, 0, 0), new TimeOnly(10, 0, 0), 1 }
                });

            migrationBuilder.InsertData(
                table: "Pods",
                columns: new[] { "Id", "AreaId", "Description", "Name", "PodTypeId", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Clean Pod", "Pod 1", 1, 1 },
                    { 2, 1, "Nice Pod", "Pod 2", 2, 1 },
                    { 3, 1, "Premium Pod", "Pod 3", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LoyaltyPoints", "MembershipId", "Name", "Password", "PasswordHash", "RoleId", "Status" },
                values: new object[,]
                {
                    { 1, "customer@gmail.com", 0, 2, "CUSTOMER", "Customer@1234", "$2a$11$Z3EqOmgSuCTgdamMckJrZ.4w/58B8GVSF71cB5/jnOb5OfEC8WHee", 1, 1 },
                    { 2, "staff@gmail.com", 0, 1, "STAFF", "Staff@1234", "$2a$11$eimNbgrLaK/MZAmgnjRIKe0Adj2ucRgAXKKr74GUFs3520DOwkpx.", 2, 1 },
                    { 3, "manager@gmail.com", 0, 1, "MANAGER", "Manager@1234", "$2a$11$xT7miq/oZKUOBV/PJJ5LbOMebJqkQvCPr2w9Aw/qqD.dyZM1dNyzu", 3, 1 },
                    { 4, "admin@gmail.com", 0, 1, "ADMIN", "Admin@1234", "$2a$11$LrucLf3WcqC40ov2ovtBwO9wEokU04M3Q6eUwSC.WuJsCWWxMiYVW", 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "ActualPrice", "BookingPrice", "BookingStatusId", "CreatedTime", "Discount", "MembershipId", "UserId" },
                values: new object[,]
                {
                    { 1, 0, 10000, 5, new DateTime(2024, 11, 11, 23, 13, 40, 392, DateTimeKind.Local).AddTicks(1509), 0, null, 1 },
                    { 2, 0, 10000, 5, new DateTime(2024, 11, 11, 23, 13, 40, 392, DateTimeKind.Local).AddTicks(1524), 0, null, 2 }
                });
        }
    }
}
