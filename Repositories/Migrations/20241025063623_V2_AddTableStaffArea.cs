using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V2_AddTableStaffArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffAreas_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffAreas_Users_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_StaffAreas_AreaId",
                table: "StaffAreas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAreas_StaffId",
                table: "StaffAreas",
                column: "StaffId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffAreas");

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
    }
}
