using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class ZH_AllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActiveUser",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Centers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
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
                    Rev_Satr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrevLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    PrevDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
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
                    Rev_Satr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrevLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Centers_LoCenterId",
                        column: x => x.LoCenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogCenters_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogCurrentLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogCurrentLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogCurrentLessons_CurrentLessons_CurrentLessonId",
                        column: x => x.CurrentLessonId,
                        principalTable: "CurrentLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LePlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_Plans_LePlanId",
                        column: x => x.LePlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogPlans_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogPrevLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrevLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPrevLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogPrevLessons_PrevLessons_PrevLessonId",
                        column: x => x.PrevLessonId,
                        principalTable: "PrevLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttendType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LimitStudents = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EpLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Locations_EpLocationId",
                        column: x => x.EpLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogLevels_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEpisodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EpisodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEpisodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEpisodes_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    StEpisodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StCurrentLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StPrevLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_CurrentLessons_StCurrentLessonId",
                        column: x => x.StCurrentLessonId,
                        principalTable: "CurrentLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Episodes_StEpisodeId",
                        column: x => x.StEpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Levels_StLevelId",
                        column: x => x.StLevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_PrevLessons_StPrevLessonId",
                        column: x => x.StPrevLessonId,
                        principalTable: "PrevLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Ev_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ev_Save = table.Column<int>(type: "int", nullable: false),
                    Ev_Rabt = table.Column<int>(type: "int", nullable: false),
                    Ev_Rev = table.Column<int>(type: "int", nullable: false),
                    Ev_Attend = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_Students_EvStudentId",
                        column: x => x.EvStudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEvaluations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEvaluations_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_EpLocationId",
                table: "Episodes",
                column: "EpLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EvStudentId",
                table: "Evaluations",
                column: "EvStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_LePlanId",
                table: "Levels",
                column: "LePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LoCenterId",
                table: "Locations",
                column: "LoCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_LogCenters_CenterId",
                table: "LogCenters",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_LogCurrentLessons_CurrentLessonId",
                table: "LogCurrentLessons",
                column: "CurrentLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEpisodes_EpisodeId",
                table: "LogEpisodes",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEvaluations_EvaluationId",
                table: "LogEvaluations",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_LogLevels_LevelId",
                table: "LogLevels",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LogLocations_LocationId",
                table: "LogLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LogPlans_PlanId",
                table: "LogPlans",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LogPrevLessons_PrevLessonId",
                table: "LogPrevLessons",
                column: "PrevLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LogStudents_StudentId",
                table: "LogStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StCurrentLessonId",
                table: "Students",
                column: "StCurrentLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StEpisodeId",
                table: "Students",
                column: "StEpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StLevelId",
                table: "Students",
                column: "StLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StPrevLessonId",
                table: "Students",
                column: "StPrevLessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogCenters");

            migrationBuilder.DropTable(
                name: "LogCurrentLessons");

            migrationBuilder.DropTable(
                name: "LogEpisodes");

            migrationBuilder.DropTable(
                name: "LogEvaluations");

            migrationBuilder.DropTable(
                name: "LogLevels");

            migrationBuilder.DropTable(
                name: "LogLocations");

            migrationBuilder.DropTable(
                name: "LogPlans");

            migrationBuilder.DropTable(
                name: "LogPrevLessons");

            migrationBuilder.DropTable(
                name: "LogStudents");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "CurrentLessons");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "PrevLessons");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Centers");

            migrationBuilder.DropColumn(
                name: "ActiveUser",
                table: "AspNetUsers");
        }
    }
}
