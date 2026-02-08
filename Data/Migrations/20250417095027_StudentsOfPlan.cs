using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentsOfPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonsData_PlansData_PlanDataId",
                table: "LessonsData");

            migrationBuilder.DropIndex(
                name: "IX_LessonsData_PlanDataId",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "PlanDataId",
                table: "LessonsData");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanDataId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_PlanDataId",
                table: "Students",
                column: "PlanDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_PlansData_PlanDataId",
                table: "Students",
                column: "PlanDataId",
                principalTable: "PlansData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_PlansData_PlanDataId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_PlanDataId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PlanDataId",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanDataId",
                table: "LessonsData",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonsData_PlanDataId",
                table: "LessonsData",
                column: "PlanDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonsData_PlansData_PlanDataId",
                table: "LessonsData",
                column: "PlanDataId",
                principalTable: "PlansData",
                principalColumn: "Id");
        }
    }
}
