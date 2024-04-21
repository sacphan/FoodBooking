using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAddress_ProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Restaurants");
        }
    }
}
