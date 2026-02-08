using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class EvaluationSurahName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ev_RabtSurahNameFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_RabtSurahNameTo",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_RevSurahNameFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_RevSurahNameTo",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_SaveSurahNameFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_SaveSurahNameTo",
                table: "Evaluations",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ev_RabtSurahNameFrom",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_RabtSurahNameTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_RevSurahNameFrom",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_RevSurahNameTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_SaveSurahNameFrom",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_SaveSurahNameTo",
                table: "Evaluations");
        }
    }
}
