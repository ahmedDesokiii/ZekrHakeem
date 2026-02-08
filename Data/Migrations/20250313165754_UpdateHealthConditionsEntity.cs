using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHealthConditionsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_HealthConditions_HealthConditionId",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "HealthConditionId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "HealthConditions",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HealthConditions_HealthConditionId",
                table: "Students",
                column: "HealthConditionId",
                principalTable: "HealthConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_HealthConditions_HealthConditionId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "HealthConditions");

            migrationBuilder.AlterColumn<Guid>(
                name: "HealthConditionId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HealthConditions_HealthConditionId",
                table: "Students",
                column: "HealthConditionId",
                principalTable: "HealthConditions",
                principalColumn: "Id");
        }
    }
}
