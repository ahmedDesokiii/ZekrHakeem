using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditStudentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StAdress",
                table: "Demands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StIdentityNum",
                table: "Demands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StName",
                table: "Demands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StNotes",
                table: "Demands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StParPhoneNum",
                table: "Demands",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StAdress",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "StIdentityNum",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "StName",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "StNotes",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "StParPhoneNum",
                table: "Demands");
        }
    }
}
