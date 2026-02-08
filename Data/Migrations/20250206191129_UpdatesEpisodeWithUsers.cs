using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesEpisodeWithUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Episodes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_ApplicationUserId",
                table: "Episodes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_AspNetUsers_ApplicationUserId",
                table: "Episodes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_AspNetUsers_ApplicationUserId",
                table: "Episodes");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_ApplicationUserId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Episodes");
        }
    }
}
