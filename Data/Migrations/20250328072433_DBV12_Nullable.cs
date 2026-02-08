using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_EpisodesTypes_EpisodeTypeId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Locations_EpLocationId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Students_EvStudentId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Centers_LoCenterId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_LogCenters_Centers_CenterId",
                table: "LogCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_LogEpisodes_Episodes_EpisodeId",
                table: "LogEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_LogEvaluations_Evaluations_EvaluationId",
                table: "LogEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_LogLevels_Levels_LevelId",
                table: "LogLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_LogLocations_Locations_LocationId",
                table: "LogLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_LogPlans_Plans_PlanId",
                table: "LogPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_LogStudents_Students_StudentId",
                table: "LogStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Episodes_StEpisodeId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_LessonsData_LessonDataId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Levels_StLevelId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Plans_StPlanId",
                table: "Students");

            migrationBuilder.AlterColumn<bool>(
                name: "newLessonState",
                table: "Students",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<Guid>(
                name: "StPlanId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "StLevelId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "StEpisodeId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "NationalityId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "LessonDataId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Plans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Nationalities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogStudents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "LogStudents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogStudents",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogPlans",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanId",
                table: "LogPlans",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogPlans",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogLocations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "LogLocations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogLocations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogLevels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "LevelId",
                table: "LogLevels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogLevels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogEvaluations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvaluationId",
                table: "LogEvaluations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogEvaluations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogEpisodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EpisodeId",
                table: "LogEpisodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogEpisodes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogCenters",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "CenterId",
                table: "LogCenters",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "LoCenterId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DistrictId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Locations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Levels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Save_ToSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Save_ToAyah",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Save_Satr",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Save_FromSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Save_FromAyah",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Rev_ToSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Rev_ToAyah",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Rev_Satr",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Rev_FromSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Rev_FromAyah",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Rabt_ToSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Rabt_ToAyah",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Rabt_Satr",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Rabt_FromSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Rabt_FromAyah",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "LessonsData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "HealthConditions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "EvStudentId",
                table: "Evaluations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Evaluations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "EpisodesTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "EpisodeTypeId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EpLocationId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Episodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Districts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_EpisodesTypes_EpisodeTypeId",
                table: "Episodes",
                column: "EpisodeTypeId",
                principalTable: "EpisodesTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Locations_EpLocationId",
                table: "Episodes",
                column: "EpLocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Students_EvStudentId",
                table: "Evaluations",
                column: "EvStudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Centers_LoCenterId",
                table: "Locations",
                column: "LoCenterId",
                principalTable: "Centers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogCenters_Centers_CenterId",
                table: "LogCenters",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogEpisodes_Episodes_EpisodeId",
                table: "LogEpisodes",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogEvaluations_Evaluations_EvaluationId",
                table: "LogEvaluations",
                column: "EvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogLevels_Levels_LevelId",
                table: "LogLevels",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogLocations_Locations_LocationId",
                table: "LogLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogPlans_Plans_PlanId",
                table: "LogPlans",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogStudents_Students_StudentId",
                table: "LogStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Episodes_StEpisodeId",
                table: "Students",
                column: "StEpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_LessonsData_LessonDataId",
                table: "Students",
                column: "LessonDataId",
                principalTable: "LessonsData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Levels_StLevelId",
                table: "Students",
                column: "StLevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Plans_StPlanId",
                table: "Students",
                column: "StPlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_EpisodesTypes_EpisodeTypeId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Locations_EpLocationId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Students_EvStudentId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Centers_LoCenterId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_LogCenters_Centers_CenterId",
                table: "LogCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_LogEpisodes_Episodes_EpisodeId",
                table: "LogEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_LogEvaluations_Evaluations_EvaluationId",
                table: "LogEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_LogLevels_Levels_LevelId",
                table: "LogLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_LogLocations_Locations_LocationId",
                table: "LogLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_LogPlans_Plans_PlanId",
                table: "LogPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_LogStudents_Students_StudentId",
                table: "LogStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Episodes_StEpisodeId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_LessonsData_LessonDataId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Levels_StLevelId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Plans_StPlanId",
                table: "Students");

            migrationBuilder.AlterColumn<bool>(
                name: "newLessonState",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StPlanId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StLevelId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StEpisodeId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "NationalityId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LessonDataId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Plans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Nationalities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogStudents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "LogStudents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogStudents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanId",
                table: "LogPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogLocations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "LogLocations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogLocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogLevels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LevelId",
                table: "LogLevels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogLevels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogEvaluations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EvaluationId",
                table: "LogEvaluations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogEvaluations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "LogEpisodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EpisodeId",
                table: "LogEpisodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogEpisodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LogCenters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CenterId",
                table: "LogCenters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LoCenterId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DistrictId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Levels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Save_ToSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Save_ToAyah",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Save_Satr",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Save_FromSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Save_FromAyah",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rev_ToSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rev_ToAyah",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rev_Satr",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rev_FromSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rev_FromAyah",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rabt_ToSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rabt_ToAyah",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rabt_Satr",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rabt_FromSorh",
                table: "LessonsData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rabt_FromAyah",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "LessonsData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "HealthConditions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EvStudentId",
                table: "Evaluations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "EpisodesTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EpisodeTypeId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EpLocationId",
                table: "Episodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "Districts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_EpisodesTypes_EpisodeTypeId",
                table: "Episodes",
                column: "EpisodeTypeId",
                principalTable: "EpisodesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Locations_EpLocationId",
                table: "Episodes",
                column: "EpLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Students_EvStudentId",
                table: "Evaluations",
                column: "EvStudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Centers_LoCenterId",
                table: "Locations",
                column: "LoCenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogCenters_Centers_CenterId",
                table: "LogCenters",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogEpisodes_Episodes_EpisodeId",
                table: "LogEpisodes",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogEvaluations_Evaluations_EvaluationId",
                table: "LogEvaluations",
                column: "EvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogLevels_Levels_LevelId",
                table: "LogLevels",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogLocations_Locations_LocationId",
                table: "LogLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogPlans_Plans_PlanId",
                table: "LogPlans",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogStudents_Students_StudentId",
                table: "LogStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Episodes_StEpisodeId",
                table: "Students",
                column: "StEpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_LessonsData_LessonDataId",
                table: "Students",
                column: "LessonDataId",
                principalTable: "LessonsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Levels_StLevelId",
                table: "Students",
                column: "StLevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Nationalities_NationalityId",
                table: "Students",
                column: "NationalityId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Plans_StPlanId",
                table: "Students",
                column: "StPlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
