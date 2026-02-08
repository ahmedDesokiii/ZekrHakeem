using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class HealthConditionsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HealthConditionId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HealthConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthConditions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_HealthConditionId",
                table: "Students",
                column: "HealthConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HealthConditions_HealthConditionId",
                table: "Students",
                column: "HealthConditionId",
                principalTable: "HealthConditions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_HealthConditions_HealthConditionId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "HealthConditions");

            migrationBuilder.DropIndex(
                name: "IX_Students_HealthConditionId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HealthConditionId",
                table: "Students");
        }
    }
}
