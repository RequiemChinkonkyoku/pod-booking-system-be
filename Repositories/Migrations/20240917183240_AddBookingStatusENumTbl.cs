using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingStatusENumTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "BookingStatusId");

            migrationBuilder.CreateTable(
                name: "BookingsStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingsStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingsStatuses_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId",
                principalTable: "BookingsStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingsStatuses_BookingStatusId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "BookingsStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingStatusId",
                table: "Bookings",
                newName: "Status");
        }
    }
}
