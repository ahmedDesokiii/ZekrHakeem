using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class Location_itude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Locations",
                type: "decimal(18,12)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Locations",
                type: "decimal(18,12)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Locations");
        }
    }
}
