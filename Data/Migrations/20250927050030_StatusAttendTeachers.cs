using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class StatusAttendTeachers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Centers_CenterId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_EpisodesTypes_EpisodeTypeId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Locations_EpLocationId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Centers_LoCenterId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_PlansData_PlanDataId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Districts_CenterId",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "Districts");

            migrationBuilder.RenameColumn(
                name: "Statement",
                table: "Attendances",
                newName: "Status_AttOut");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanDataId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
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

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
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

            migrationBuilder.AlterColumn<string>(
                name: "AttendType",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status_AttIn",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

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
                name: "FK_Students_PlansData_PlanDataId",
                table: "Students",
                column: "PlanDataId",
                principalTable: "PlansData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "FK_Locations_Centers_LoCenterId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Districts_DistrictId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_PlansData_PlanDataId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Status_AttIn",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "Status_AttOut",
                table: "Attendances",
                newName: "Statement");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlanDataId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AlterColumn<string>(
                name: "AttendType",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "CenterId",
                table: "Districts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CenterId",
                value: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CenterId",
                table: "Districts",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Centers_CenterId",
                table: "Districts",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id");

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
                name: "FK_Students_PlansData_PlanDataId",
                table: "Students",
                column: "PlanDataId",
                principalTable: "PlansData",
                principalColumn: "Id");
        }
    }
}
