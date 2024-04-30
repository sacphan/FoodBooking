using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSourceCrawlId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SourceCrawlId",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceCrawlId",
                table: "Restaurants");
        }
    }
}
