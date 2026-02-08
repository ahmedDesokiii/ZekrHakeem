using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class EvProgEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationProgs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Ev_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ev_Save = table.Column<int>(type: "int", nullable: false),
                    Ev_Rabt = table.Column<int>(type: "int", nullable: false),
                    Ev_Rev = table.Column<int>(type: "int", nullable: false),
                    Ev_Attend = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ev_RevSurahIdFrom = table.Column<int>(type: "int", nullable: true),
                    Ev_RevAyahIdFrom = table.Column<int>(type: "int", nullable: true),
                    Ev_RevSurahIdTo = table.Column<int>(type: "int", nullable: true),
                    Ev_RevAyahIdTo = table.Column<int>(type: "int", nullable: true),
                    Ev_SaveSurahIdTo = table.Column<int>(type: "int", nullable: true),
                    Ev_SaveAyahIdTo = table.Column<int>(type: "int", nullable: true),
                    EvStudentProgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationProgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationProgs_StudentProgs_EvStudentProgId",
                        column: x => x.EvStudentProgId,
                        principalTable: "StudentProgs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationProgs_EvStudentProgId",
                table: "EvaluationProgs",
                column: "EvStudentProgId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationProgs");
        }
    }
}
