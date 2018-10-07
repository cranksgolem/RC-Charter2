using Microsoft.EntityFrameworkCore.Migrations;

namespace RC_Charter2.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharterFlightCharge_Flight_FlightId",
                table: "CharterFlightCharge");

            migrationBuilder.DropIndex(
                name: "IX_CharterFlightCharge_FlightId",
                table: "CharterFlightCharge");

            migrationBuilder.DropColumn(
                name: "CharterFlightChargeId",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "CharterFlightCharge");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharterFlightChargeId",
                table: "Flight",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "CharterFlightCharge",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharterFlightCharge_FlightId",
                table: "CharterFlightCharge",
                column: "FlightId",
                unique: true,
                filter: "[FlightId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CharterFlightCharge_Flight_FlightId",
                table: "CharterFlightCharge",
                column: "FlightId",
                principalTable: "Flight",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
