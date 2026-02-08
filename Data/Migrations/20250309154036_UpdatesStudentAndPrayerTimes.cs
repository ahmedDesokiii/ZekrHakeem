using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesStudentAndPrayerTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationType",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentIdentityNumber",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentPhoneNumber",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EndEpisodeTime",
                table: "PrayerTimes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationType",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentIdentityNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentPhoneNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EndEpisodeTime",
                table: "PrayerTimes");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
