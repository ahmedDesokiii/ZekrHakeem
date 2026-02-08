using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProgsCont2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndProg",
                table: "Progs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Progs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartProg",
                table: "Progs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progs_PlanId",
                table: "Progs",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progs_PlansData_PlanId",
                table: "Progs",
                column: "PlanId",
                principalTable: "PlansData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progs_PlansData_PlanId",
                table: "Progs");

            migrationBuilder.DropIndex(
                name: "IX_Progs_PlanId",
                table: "Progs");

            migrationBuilder.DropColumn(
                name: "EndProg",
                table: "Progs");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Progs");

            migrationBuilder.DropColumn(
                name: "StartProg",
                table: "Progs");
        }
    }
}
