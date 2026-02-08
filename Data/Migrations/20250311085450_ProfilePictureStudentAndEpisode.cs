using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePictureStudentAndEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Episodes");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "EpisodePicture",
                table: "Episodes",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EpisodePicture",
                table: "Episodes");

            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
