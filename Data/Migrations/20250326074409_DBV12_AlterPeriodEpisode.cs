using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_AlterPeriodEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndTimeEpisode",
                table: "Episodes",
                newName: "Period");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Period",
                table: "Episodes",
                newName: "EndTimeEpisode");
        }
    }
}
