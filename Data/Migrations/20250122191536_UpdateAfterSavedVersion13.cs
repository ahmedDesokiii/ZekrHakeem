using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAfterSavedVersion13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Plans_LePlanId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_LePlanId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "LePlanId",
                table: "Levels");

            migrationBuilder.AddColumn<Guid>(
                name: "StPlanId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Students_StPlanId",
                table: "Students",
                column: "StPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Plans_StPlanId",
                table: "Students",
                column: "StPlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Plans_StPlanId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StPlanId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StPlanId",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "LePlanId",
                table: "Levels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Levels_LePlanId",
                table: "Levels",
                column: "LePlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Plans_LePlanId",
                table: "Levels",
                column: "LePlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
