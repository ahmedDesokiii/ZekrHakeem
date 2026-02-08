using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNationalityAndEpTypeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "TitleType",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ActiveUser",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "NationalityId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "PrayName",
                table: "PrayerTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "PrayerTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plans",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "TotalStudents",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Levels",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Evaluations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Episodes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "EpisodeTypeId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Centers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "NationalityId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EpisodesTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodesTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_IdentityNumber",
                table: "Students",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_NationalityId",
                table: "Students",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_EpisodeTypeId",
                table: "Episodes",
                column: "EpisodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityNum",
                table: "AspNetUsers",
                column: "IdentityNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NationalityId",
                table: "AspNetUsers",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nationalities_NationalityId",
                table: "AspNetUsers",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_EpisodesTypes_EpisodeTypeId",
                table: "Episodes",
                column: "EpisodeTypeId",
                principalTable: "EpisodesTypes",
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
                name: "FK_AspNetUsers_Nationalities_NationalityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_EpisodesTypes_EpisodeTypeId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "EpisodesTypes");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropIndex(
                name: "IX_Students_IdentityNumber",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_NationalityId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_EpisodeTypeId",
                table: "Episodes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdentityNum",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NationalityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "PrayerTimes");

            migrationBuilder.DropColumn(
                name: "TotalStudents",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "EpisodeTypeId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrayName",
                table: "PrayerTimes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plans",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Levels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Evaluations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Episodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleType",
                table: "Episodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Centers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveUser",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
