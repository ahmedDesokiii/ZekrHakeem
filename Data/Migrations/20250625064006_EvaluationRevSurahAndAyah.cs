using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class EvaluationRevSurahAndAyah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Episodes_StEpisodeId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "StEpisodeId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "NationalityId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_RevAyahIdFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ev_RevSurahIdFrom",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Episodes_StEpisodeId",
                table: "Students",
                column: "StEpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Episodes_StEpisodeId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Ev_RevAyahIdFrom",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Ev_RevSurahIdFrom",
                table: "Evaluations");

            migrationBuilder.AlterColumn<Guid>(
                name: "StEpisodeId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "NationalityId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Episodes_StEpisodeId",
                table: "Students",
                column: "StEpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id");
        }
    }
}
