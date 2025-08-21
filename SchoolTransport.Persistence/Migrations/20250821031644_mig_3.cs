using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolTransport.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Activities_VehicleId",
                table: "Activities",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "897b906c-927d-4085-891f-3a89ad49b157", new DateTime(2025, 8, 19, 23, 49, 55, 198, DateTimeKind.Utc).AddTicks(8796), "AQAAAAIAAYagAAAAEDLjoUsA2mcejH69ehLwiUiWkv3ViIFzS68mf2jRejXwRnJKJUInVY+EPK/N3bJc3A==", "abe9e4ab-cabd-48a2-814e-f1b9dfeebe75" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e12afded-7b79-4f5a-922e-d920dd21435e", new DateTime(2025, 8, 19, 23, 49, 55, 328, DateTimeKind.Utc).AddTicks(3240), "AQAAAAIAAYagAAAAEH/douKSG+GcoAnl2Q4izuQn2U1TnPc1POJ+tGhzaFABIq6ULUYxwg4mEKwoxVyAnw==", "5b471881-a6a9-4737-9e27-a26f462af850" });
        }
    }
}
