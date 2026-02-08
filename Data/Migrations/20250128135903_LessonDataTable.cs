using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class LessonDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonDataId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LessonsData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Save_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Save_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Save_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Save_Satr = table.Column<int>(type: "int", nullable: false),
                    Rabt_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rabt_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rabt_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rabt_Satr = table.Column<int>(type: "int", nullable: false),
                    Rev_FromSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_FromAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_ToSorh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rev_ToAyah = table.Column<int>(type: "int", nullable: false),
                    Rev_Satr = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonsData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_LessonDataId",
                table: "Students",
                column: "LessonDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_LessonsData_LessonDataId",
                table: "Students",
                column: "LessonDataId",
                principalTable: "LessonsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_LessonsData_LessonDataId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LessonsData");

            migrationBuilder.DropIndex(
                name: "IX_Students_LessonDataId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LessonDataId",
                table: "Students");
        }
    }
}
