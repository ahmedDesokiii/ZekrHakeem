using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class EvaluationEntityCont : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ev_RabtAyahIdFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_RabtAyahIdTo",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_RabtSurahIdFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_RabtSurahIdTo",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_SaveAyahIdFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_SaveSurahIdFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ev_RabtAyahIdFrom",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_RabtAyahIdTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_RabtSurahIdFrom",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_RabtSurahIdTo",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_SaveAyahIdFrom",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_SaveSurahIdFrom",
                table: "Evaluations");
        }
    }
}
