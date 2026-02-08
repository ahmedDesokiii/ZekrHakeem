using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteStartEpTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTimeEpisode",
                table: "Episodes");

            migrationBuilder.AlterColumn<string>(
                name: "EndTimeEpisode",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EndTimeEpisode",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTimeEpisode",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
