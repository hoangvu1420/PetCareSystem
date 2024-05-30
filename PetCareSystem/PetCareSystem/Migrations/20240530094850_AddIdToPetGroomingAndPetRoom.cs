using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToPetGroomingAndPetRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PetRooms",
                table: "PetRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetGroomingServices",
                table: "PetGroomingServices");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PetRooms",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PetRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PetRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PetGroomingServices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PetGroomingServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PetGroomingServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetRooms",
                table: "PetRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetGroomingServices",
                table: "PetGroomingServices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PetRooms_PetId",
                table: "PetRooms",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetGroomingServices_PetId",
                table: "PetGroomingServices",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PetRooms",
                table: "PetRooms");

            migrationBuilder.DropIndex(
                name: "IX_PetRooms_PetId",
                table: "PetRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetGroomingServices",
                table: "PetGroomingServices");

            migrationBuilder.DropIndex(
                name: "IX_PetGroomingServices_PetId",
                table: "PetGroomingServices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PetRooms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PetRooms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PetRooms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PetGroomingServices");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PetGroomingServices");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PetGroomingServices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetRooms",
                table: "PetRooms",
                columns: new[] { "PetId", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetGroomingServices",
                table: "PetGroomingServices",
                columns: new[] { "PetId", "GroomingServiceId" });
        }
    }
}
