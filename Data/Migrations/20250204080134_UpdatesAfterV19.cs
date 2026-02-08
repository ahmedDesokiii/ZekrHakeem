using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesAfterV19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Locations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Centers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Centers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Centers_ApplicationUserId",
                table: "Centers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Centers_AspNetUsers_ApplicationUserId",
                table: "Centers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Centers_AspNetUsers_ApplicationUserId",
                table: "Centers");

            migrationBuilder.DropIndex(
                name: "IX_Centers_ApplicationUserId",
                table: "Centers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Centers");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Centers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
