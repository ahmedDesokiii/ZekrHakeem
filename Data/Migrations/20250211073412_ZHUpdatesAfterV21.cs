using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class ZHUpdatesAfterV21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ApplicationUserId",
                table: "Students",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_ApplicationUserId",
                table: "Students",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_ApplicationUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ApplicationUserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Students");
        }
    }
}
