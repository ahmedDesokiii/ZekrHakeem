using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class EpisodeIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodePicture",
                table: "Episodes");

            migrationBuilder.AddColumn<byte[]>(
                name: "EpisodePicture",
                table: "EpisodesTypes",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodePicture",
                table: "EpisodesTypes");

            migrationBuilder.AddColumn<byte[]>(
                name: "EpisodePicture",
                table: "Episodes",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
