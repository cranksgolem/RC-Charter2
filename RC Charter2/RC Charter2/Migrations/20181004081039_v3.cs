using Microsoft.EntityFrameworkCore.Migrations;

namespace RC_Charter2.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_CharterTrip_CharterTripid",
                table: "Flight");

            migrationBuilder.RenameColumn(
                name: "CharterTripid",
                table: "Flight",
                newName: "CharterTripId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_CharterTripid",
                table: "Flight",
                newName: "IX_Flight_CharterTripId");

            migrationBuilder.AddColumn<int>(
                name: "CharterFlightChargeId",
                table: "Flight",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "CharterFlightCharge",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "CharterFlightCharge",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharterFlightCharge_EmployeeId",
                table: "CharterFlightCharge",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharterFlightCharge_FlightId",
                table: "CharterFlightCharge",
                column: "FlightId",
                unique: true,
                filter: "[FlightId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CharterFlightCharge_Employee_EmployeeId",
                table: "CharterFlightCharge",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharterFlightCharge_Flight_FlightId",
                table: "CharterFlightCharge",
                column: "FlightId",
                principalTable: "Flight",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_CharterTrip_CharterTripId",
                table: "Flight",
                column: "CharterTripId",
                principalTable: "CharterTrip",
                principalColumn: "CharterTripId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharterFlightCharge_Employee_EmployeeId",
                table: "CharterFlightCharge");

            migrationBuilder.DropForeignKey(
                name: "FK_CharterFlightCharge_Flight_FlightId",
                table: "CharterFlightCharge");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_CharterTrip_CharterTripId",
                table: "Flight");

            migrationBuilder.DropIndex(
                name: "IX_CharterFlightCharge_EmployeeId",
                table: "CharterFlightCharge");

            migrationBuilder.DropIndex(
                name: "IX_CharterFlightCharge_FlightId",
                table: "CharterFlightCharge");

            migrationBuilder.DropColumn(
                name: "CharterFlightChargeId",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CharterFlightCharge");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "CharterFlightCharge");

            migrationBuilder.RenameColumn(
                name: "CharterTripId",
                table: "Flight",
                newName: "CharterTripid");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_CharterTripId",
                table: "Flight",
                newName: "IX_Flight_CharterTripid");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_CharterTrip_CharterTripid",
                table: "Flight",
                column: "CharterTripid",
                principalTable: "CharterTrip",
                principalColumn: "CharterTripId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
