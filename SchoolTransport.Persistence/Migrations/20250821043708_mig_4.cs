using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolTransport.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a86b238-3f2d-414f-86be-fe15ccc4b877", new DateTime(2025, 8, 21, 4, 37, 7, 586, DateTimeKind.Utc).AddTicks(6491), "AQAAAAIAAYagAAAAEEtEsBv1yW94rOxlCud25iQIHpk09/UQTS6tiF67ewD/ZhJY5jOICyy5FPjLy5isDg==", "fd58543f-bd9c-4f4f-95bd-ade511cd5109" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1b32f82-fb93-4e23-a016-17199b29ba47", new DateTime(2025, 8, 21, 4, 37, 7, 663, DateTimeKind.Utc).AddTicks(9516), "AQAAAAIAAYagAAAAEP6OUpDUjqYpe8kxXGvdMrgiWm5sBjyGcFMW36lhpBl8oqWv4+U4jFhxlH7Dpm87dw==", "4eab94d3-1dd4-45c5-b963-4ebb086f3300" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ad30223-12a2-4c0b-b54a-24fce580df51", new DateTime(2025, 8, 21, 3, 16, 43, 412, DateTimeKind.Utc).AddTicks(5467), "AQAAAAIAAYagAAAAEKoLRdIL1/O2DF+fazx0Pzgfa5n03BN6M60+++EEWg3njxLr0CS6qrz3RgfYOWY94g==", "b438cb87-26c1-480b-8c8c-a9105e610b1f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "831969bc-06a8-4ed0-bcc6-291cd3b73803", new DateTime(2025, 8, 21, 3, 16, 43, 481, DateTimeKind.Utc).AddTicks(9946), "AQAAAAIAAYagAAAAEF4kJ3z2WWgN8w8h1Qz22ISS4WMnxPz+2Mc0sLPyXSuK/JopDqko2HgUu6q7bA8l6w==", "85577ee5-2288-4cb4-8046-9196deb8ad1c" });
        }
    }
}
