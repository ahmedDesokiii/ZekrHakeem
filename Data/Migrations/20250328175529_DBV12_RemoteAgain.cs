using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekrHakeem_v11.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBV12_RemoteAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Centers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Name", "Type" },
                values: new object[] { "C_RemoteM", "men" });

            migrationBuilder.InsertData(
                table: "Centers",
                columns: new[] { "Id", "ApplicationUserId", "CurrentState", "Description", "Name", "PauseState", "Type" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), null, 0, null, "C_RemoteW", 0, "women" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "Name", "Type" },
                values: new object[] { "L_RemoteM", "mosque" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CurrentState", "Description", "DistrictId", "Latitude", "LoCenterId", "Longitude", "Name", "PauseState", "TotalStudents", "Type" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000005"), 0, null, new Guid("00000000-0000-0000-0000-000000000002"), "25.043773164267765", new Guid("00000000-0000-0000-0000-000000000004"), "37.25986633254091", "L_RemoteW", 0, "0", "dar" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Centers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.UpdateData(
                table: "Centers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "Name", "Type" },
                values: new object[] { "C_Remote", "remote" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "Name", "Type" },
                values: new object[] { "L_Remote", "remote" });
        }
    }
}
