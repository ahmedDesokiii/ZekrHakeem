using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_Remote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Centers",
                columns: new[] { "Id", "ApplicationUserId", "CurrentState", "Description", "Name", "PauseState", "Type" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), null, 0, null, "C_Remote", 0, "remote" });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CenterId", "CurrentState", "Description", "Name" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), 0, null, "D_Remote" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CurrentState", "Description", "DistrictId", "Latitude", "LoCenterId", "Longitude", "Name", "PauseState", "TotalStudents", "Type" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), 0, null, new Guid("00000000-0000-0000-0000-000000000002"), "25.043773164267765", new Guid("00000000-0000-0000-0000-000000000001"), "37.25986633254091", "L_Remote", 0, "0", "remote" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Centers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
