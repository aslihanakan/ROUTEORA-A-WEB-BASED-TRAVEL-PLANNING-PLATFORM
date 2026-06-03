using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelPlanner.Migrations
{
    /// <inheritdoc />
    public partial class AddRouteNameToTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RouteName",
                table: "Trips",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteName",
                table: "Trips");
        }
    }
}
