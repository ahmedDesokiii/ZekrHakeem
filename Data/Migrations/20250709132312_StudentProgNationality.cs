using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentProgNationality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NationalityId",
                table: "StudentProgs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudentProgs_NationalityId",
                table: "StudentProgs",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProgs_Nationalities_NationalityId",
                table: "StudentProgs",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProgs_Nationalities_NationalityId",
                table: "StudentProgs");

            migrationBuilder.DropIndex(
                name: "IX_StudentProgs_NationalityId",
                table: "StudentProgs");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "StudentProgs");
        }
    }
}
