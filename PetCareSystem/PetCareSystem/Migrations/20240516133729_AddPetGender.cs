using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPetGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Male");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Pets");
        }
    }
}
