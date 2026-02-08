using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentDemandsCurrentstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "Demands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LogDemands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DemandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogDemands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogDemands_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogDemands_DemandId",
                table: "LogDemands",
                column: "DemandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogDemands");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "Demands");
        }
    }
}
