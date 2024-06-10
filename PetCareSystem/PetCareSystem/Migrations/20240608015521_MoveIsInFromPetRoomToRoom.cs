using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Migrations
{
    /// <inheritdoc />
    public partial class MoveIsInFromPetRoomToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIn",
                table: "PetRooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsIn",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIn",
                table: "Rooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsIn",
                table: "PetRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
