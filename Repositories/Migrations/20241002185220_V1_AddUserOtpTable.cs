using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V1_AddUserOtpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserOtps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OtpCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsExpiredOrUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOtps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$D/j3mUP8XnPBeXYX1srjt.GGQj9TkYoyaz2cVUJcZ3.OjVZOP22x2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$ofS3coZzZlgWmO.QMfnCNuYBbpZ47G8eGbX4naPfOi9VWpb3FySM2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$r5ayt5hom8OQUygD0XPhmeDyEtpRiKR/AhbIxYjwrx3LAmTRbwq8O");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$Z215tbwBLl8ojO6yR4ST6uoVD2Cfvgn6km/PXP36zBkcEBA.63aGe");

            migrationBuilder.CreateIndex(
                name: "IX_UserOtps_UserId",
                table: "UserOtps",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOtps");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$9LxHPbEnXmZvbjWIX5OIz.ozzatDgE.EOCWl0MqRAbar/frUinybu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$KMKRtWyFCKmShT1nz8DkhOHVOFme2MGjZWp9Wl9eOQpxtPv1pDzuK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$RunWU.yYiPBD4HQ/R7uNQufrax0oFvvxunX4T1mwdD/dWIXHhiPXa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$ZRknrBy3d7rEBc.ji3F2WeUBpT4cgt87BPQ7QSN5Aa6pLFfKbRq9q");
        }
    }
}
