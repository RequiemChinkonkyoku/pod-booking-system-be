using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddPodTypeAndAreaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Pods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PodTypeId",
                table: "Pods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PodTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pods_AreaId",
                table: "Pods",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pods_PodTypeId",
                table: "Pods",
                column: "PodTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pods_Areas_AreaId",
                table: "Pods",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pods_PodTypes_PodTypeId",
                table: "Pods",
                column: "PodTypeId",
                principalTable: "PodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pods_Areas_AreaId",
                table: "Pods");

            migrationBuilder.DropForeignKey(
                name: "FK_Pods_PodTypes_PodTypeId",
                table: "Pods");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "PodTypes");

            migrationBuilder.DropIndex(
                name: "IX_Pods_AreaId",
                table: "Pods");

            migrationBuilder.DropIndex(
                name: "IX_Pods_PodTypeId",
                table: "Pods");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Pods");

            migrationBuilder.DropColumn(
                name: "PodTypeId",
                table: "Pods");
        }
    }
}
