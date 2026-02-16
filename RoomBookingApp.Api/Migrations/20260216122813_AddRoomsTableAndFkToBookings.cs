using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoomBookingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomsTableAndFkToBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // create Rooms table
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            // add nullable RoomId to Bookings first (we'll populate it from existing RoomName values)
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Bookings",
                type: "INTEGER",
                nullable: true);

            // populate Rooms from distinct existing RoomName values (if any) and map Bookings -> Rooms
            // Note: this preserves pre-existing booking data when migrating
            migrationBuilder.Sql(@"
                INSERT INTO Rooms (Name)
                SELECT DISTINCT RoomName FROM Bookings
                WHERE RoomName IS NOT NULL AND trim(RoomName) <> '';
            ");

            migrationBuilder.Sql(@"
                UPDATE Bookings
                SET RoomId = (SELECT Id FROM Rooms WHERE Rooms.Name = Bookings.RoomName)
                WHERE RoomName IS NOT NULL AND trim(RoomName) <> '';
            ");

            // make RoomId non-nullable now that values are populated (SQLite will recreate table under the hood)
            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Bookings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            // create index + FK
            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // finally drop the old RoomName column (data already migrated)
            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // add RoomName back
            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "Bookings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            // copy room names back from Rooms (if any)
            migrationBuilder.Sql(@"
                UPDATE Bookings
                SET RoomName = (SELECT Name FROM Rooms WHERE Rooms.Id = Bookings.RoomId)
                WHERE RoomId IS NOT NULL;
            ");

            // drop FK and index
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings");

            // drop RoomId column
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Bookings");

            // drop Rooms table
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
