using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_UpdateEntitiesPlansDataAndLevelsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Levels_StLevelId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Plans_StPlanId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LogLevels");

            migrationBuilder.DropTable(
                name: "LogPlans");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Students_StLevelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StPlanId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StLevelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StPlanId",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StLevelId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StPlanId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CurrentState = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CurrentState = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogLevels_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogPlans_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StLevelId",
                table: "Students",
                column: "StLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StPlanId",
                table: "Students",
                column: "StPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LogLevels_LevelId",
                table: "LogLevels",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LogPlans_PlanId",
                table: "LogPlans",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Levels_StLevelId",
                table: "Students",
                column: "StLevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Plans_StPlanId",
                table: "Students",
                column: "StPlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
