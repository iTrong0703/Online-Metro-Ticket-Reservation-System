using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetroTicketReservation.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class TicketTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    TicketTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TicketName = table.Column<string>(type: "text", nullable: false),
                    TicketPrice = table.Column<int>(type: "integer", nullable: false),
                    ValidityDays = table.Column<int>(type: "integer", nullable: false),
                    IsStudentOnly = table.Column<bool>(type: "boolean", nullable: false),
                    IsTimeBased = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.TicketTypeID);
                });

            migrationBuilder.CreateTable(
                name: "StationFares",
                columns: table => new
                {
                    StationFareID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartStationID = table.Column<int>(type: "integer", nullable: false),
                    EndStationID = table.Column<int>(type: "integer", nullable: false),
                    TicketTypeID = table.Column<int>(type: "integer", nullable: false),
                    Fare = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationFares", x => x.StationFareID);
                    table.ForeignKey(
                        name: "FK_StationFares_Stations_EndStationID",
                        column: x => x.EndStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StationFares_Stations_StartStationID",
                        column: x => x.StartStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StationFares_TicketTypes_TicketTypeID",
                        column: x => x.TicketTypeID,
                        principalTable: "TicketTypes",
                        principalColumn: "TicketTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StationFares_EndStationID",
                table: "StationFares",
                column: "EndStationID");

            migrationBuilder.CreateIndex(
                name: "IX_StationFares_StartStationID",
                table: "StationFares",
                column: "StartStationID");

            migrationBuilder.CreateIndex(
                name: "IX_StationFares_TicketTypeID",
                table: "StationFares",
                column: "TicketTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StationFares");

            migrationBuilder.DropTable(
                name: "TicketTypes");
        }
    }
}
