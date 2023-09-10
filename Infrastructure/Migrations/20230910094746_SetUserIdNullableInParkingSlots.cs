using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetUserIdNullableInParkingSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSlots_AspNetUsers_UserId",
                table: "ParkingSlots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSlots_UserId",
                table: "ParkingSlots");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ParkingSlots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlots_UserId",
                table: "ParkingSlots",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSlots_AspNetUsers_UserId",
                table: "ParkingSlots",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSlots_AspNetUsers_UserId",
                table: "ParkingSlots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSlots_UserId",
                table: "ParkingSlots");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ParkingSlots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlots_UserId",
                table: "ParkingSlots",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSlots_AspNetUsers_UserId",
                table: "ParkingSlots",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
