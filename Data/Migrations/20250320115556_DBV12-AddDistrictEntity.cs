using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12AddDistrictEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "EpisodePicture",
                table: "EpisodesTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_DistrictId",
                table: "Locations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CenterId",
                table: "Districts",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Locations_DistrictId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Locations");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ProfilePicture",
                table: "Students",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "EpisodePicture",
                table: "EpisodesTypes",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
