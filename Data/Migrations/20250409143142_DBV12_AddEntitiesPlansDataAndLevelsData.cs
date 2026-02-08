using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_AddEntitiesPlansDataAndLevelsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LevelDataId",
                table: "LessonsData",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Centers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "PlansData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlansData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LevelsData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelsData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelsData_PlansData_PlanDataId",
                        column: x => x.PlanDataId,
                        principalTable: "PlansData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogPlansData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlanDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPlansData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogPlansData_PlansData_PlanDataId",
                        column: x => x.PlanDataId,
                        principalTable: "PlansData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogLevelsData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LevelDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLevelsData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogLevelsData_LevelsData_LevelDataId",
                        column: x => x.LevelDataId,
                        principalTable: "LevelsData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonsData_LevelDataId",
                table: "LessonsData",
                column: "LevelDataId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelsData_PlanDataId",
                table: "LevelsData",
                column: "PlanDataId");

            migrationBuilder.CreateIndex(
                name: "IX_LogLevelsData_LevelDataId",
                table: "LogLevelsData",
                column: "LevelDataId");

            migrationBuilder.CreateIndex(
                name: "IX_LogPlansData_PlanDataId",
                table: "LogPlansData",
                column: "PlanDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonsData_LevelsData_LevelDataId",
                table: "LessonsData",
                column: "LevelDataId",
                principalTable: "LevelsData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonsData_LevelsData_LevelDataId",
                table: "LessonsData");

            migrationBuilder.DropTable(
                name: "LogLevelsData");

            migrationBuilder.DropTable(
                name: "LogPlansData");

            migrationBuilder.DropTable(
                name: "LevelsData");

            migrationBuilder.DropTable(
                name: "PlansData");

            migrationBuilder.DropIndex(
                name: "IX_LessonsData_LevelDataId",
                table: "LessonsData");

            migrationBuilder.DropColumn(
                name: "LevelDataId",
                table: "LessonsData");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Centers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
