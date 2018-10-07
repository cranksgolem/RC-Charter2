using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RC_Charter2.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AircraftProperty",
                columns: table => new
                {
                    AircraftPropertiesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AircraftType = table.Column<string>(nullable: true),
                    AircraftCategory = table.Column<string>(nullable: true),
                    EngineType = table.Column<string>(nullable: true),
                    AircraftWeight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftProperty", x => x.AircraftPropertiesId);
                });

            migrationBuilder.CreateTable(
                name: "Cash",
                columns: table => new
                {
                    CashId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentModeId = table.Column<int>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cash", x => x.CashId);
                });

            migrationBuilder.CreateTable(
                name: "Check",
                columns: table => new
                {
                    CheckId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentModeId = table.Column<int>(nullable: true),
                    CheckNumber = table.Column<int>(nullable: true),
                    CheckDate = table.Column<DateTime>(type: "date", nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Check", x => x.CheckId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AvailableCredits = table.Column<decimal>(type: "money", nullable: true),
                    UsedCredits = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    LicenseType = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.LicenseType);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    TestCode = table.Column<string>(nullable: false),
                    TestDescription = table.Column<string>(nullable: true),
                    TestFrequency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.TestCode);
                });

            migrationBuilder.CreateTable(
                name: "Aircraft",
                columns: table => new
                {
                    AircraftNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AircraftPropertiesId = table.Column<int>(nullable: true),
                    ChargePerMile = table.Column<decimal>(type: "money", nullable: true),
                    HourlyWaitingCharge = table.Column<decimal>(type: "money", nullable: true),
                    Model = table.Column<string>(nullable: true),
                    NumberOfSeats = table.Column<int>(nullable: true),
                    YearManufactured = table.Column<int>(nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircraft", x => x.AircraftNumber);
                    table.ForeignKey(
                        name: "FK_Aircraft_AircraftProperty_AircraftPropertiesId",
                        column: x => x.AircraftPropertiesId,
                        principalTable: "AircraftProperty",
                        principalColumn: "AircraftPropertiesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrewRequirement",
                columns: table => new
                {
                    CrewRequirementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LicenseType = table.Column<string>(nullable: true),
                    AircraftPropertiesId = table.Column<int>(nullable: true),
                    RequiredNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewRequirement", x => x.CrewRequirementId);
                    table.ForeignKey(
                        name: "FK_CrewRequirement_AircraftProperty_AircraftPropertiesId",
                        column: x => x.AircraftPropertiesId,
                        principalTable: "AircraftProperty",
                        principalColumn: "AircraftPropertiesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrewRequirement_License_LicenseType",
                        column: x => x.LicenseType,
                        principalTable: "License",
                        principalColumn: "LicenseType",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Licensure",
                columns: table => new
                {
                    LicensureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LicenseType = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    DateEarned = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licensure", x => x.LicensureId);
                    table.ForeignKey(
                        name: "FK_Licensure_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Licensure_License_LicenseType",
                        column: x => x.LicenseType,
                        principalTable: "License",
                        principalColumn: "LicenseType",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    ResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TestCode = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    TestDate = table.Column<DateTime>(type: "date", nullable: true),
                    TestResult = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Result_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_Test_TestCode",
                        column: x => x.TestCode,
                        principalTable: "Test",
                        principalColumn: "TestCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharterTrip",
                columns: table => new
                {
                    CharterTripId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AircraftNumber = table.Column<int>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true),
                    TotalDistanceFlown = table.Column<double>(type: "float", nullable: true),
                    TotalWaitingTime = table.Column<int>(nullable: true),
                    TotalFuelUsage = table.Column<double>(type: "float", nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    FinalDestination = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    TotalCharge = table.Column<decimal>(type: "money", nullable: true),
                    RemainingBalance = table.Column<decimal>(type: "money", nullable: true),
                    DateLastPaid = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharterTrip", x => x.CharterTripId);
                    table.ForeignKey(
                        name: "FK_CharterTrip_Aircraft_AircraftNumber",
                        column: x => x.AircraftNumber,
                        principalTable: "Aircraft",
                        principalColumn: "AircraftNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharterTrip_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BalanceHistory",
                columns: table => new
                {
                    BalanceHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharterTripId = table.Column<int>(nullable: true),
                    RemainingBalance = table.Column<decimal>(type: "money", nullable: true),
                    DatePaid = table.Column<DateTime>(type: "date", nullable: true),
                    ModeOfPayment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceHistory", x => x.BalanceHistoryId);
                    table.ForeignKey(
                        name: "FK_BalanceHistory_CharterTrip_CharterTripId",
                        column: x => x.CharterTripId,
                        principalTable: "CharterTrip",
                        principalColumn: "CharterTripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharterFlightCharge",
                columns: table => new
                {
                    CharterFlightChargeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharterTripId = table.Column<int>(nullable: true),
                    ChargeType = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharterFlightCharge", x => x.CharterFlightChargeId);
                    table.ForeignKey(
                        name: "FK_CharterFlightCharge_CharterTrip_CharterTripId",
                        column: x => x.CharterTripId,
                        principalTable: "CharterTrip",
                        principalColumn: "CharterTripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrewAssignment",
                columns: table => new
                {
                    CrewAssignmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    CharterTripId = table.Column<int>(nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "date", nullable: true),
                    Role = table.Column<string>(nullable: true),
                    WorkHours = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewAssignment", x => x.CrewAssignmentId);
                    table.ForeignKey(
                        name: "FK_CrewAssignment_CharterTrip_CharterTripId",
                        column: x => x.CharterTripId,
                        principalTable: "CharterTrip",
                        principalColumn: "CharterTripId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrewAssignment_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharterTripid = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    TimeDeparture = table.Column<DateTime>(type: "date", nullable: true),
                    TimeArrival = table.Column<DateTime>(type: "date", nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    DistanceFlown = table.Column<double>(type: "float", nullable: true),
                    WaitingTime = table.Column<int>(nullable: true),
                    FuelUsage = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flight_CharterTrip_CharterTripid",
                        column: x => x.CharterTripid,
                        principalTable: "CharterTrip",
                        principalColumn: "CharterTripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMode",
                columns: table => new
                {
                    PaymentModeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true),
                    CharterTripId = table.Column<int>(nullable: true),
                    CheckId = table.Column<int>(nullable: true),
                    CashId = table.Column<int>(nullable: true),
                    ModeOfPayment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMode", x => x.PaymentModeId);
                    table.ForeignKey(
                        name: "FK_PaymentMode_Cash_CashId",
                        column: x => x.CashId,
                        principalTable: "Cash",
                        principalColumn: "CashId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMode_CharterTrip_CharterTripId",
                        column: x => x.CharterTripId,
                        principalTable: "CharterTrip",
                        principalColumn: "CharterTripId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMode_Check_CheckId",
                        column: x => x.CheckId,
                        principalTable: "Check",
                        principalColumn: "CheckId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMode_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aircraft_AircraftPropertiesId",
                table: "Aircraft",
                column: "AircraftPropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceHistory_CharterTripId",
                table: "BalanceHistory",
                column: "CharterTripId");

            migrationBuilder.CreateIndex(
                name: "IX_CharterFlightCharge_CharterTripId",
                table: "CharterFlightCharge",
                column: "CharterTripId");

            migrationBuilder.CreateIndex(
                name: "IX_CharterTrip_AircraftNumber",
                table: "CharterTrip",
                column: "AircraftNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CharterTrip_CustomerId",
                table: "CharterTrip",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewAssignment_CharterTripId",
                table: "CrewAssignment",
                column: "CharterTripId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewAssignment_EmployeeId",
                table: "CrewAssignment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewRequirement_AircraftPropertiesId",
                table: "CrewRequirement",
                column: "AircraftPropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewRequirement_LicenseType",
                table: "CrewRequirement",
                column: "LicenseType");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_CharterTripid",
                table: "Flight",
                column: "CharterTripid");

            migrationBuilder.CreateIndex(
                name: "IX_Licensure_EmployeeId",
                table: "Licensure",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Licensure_LicenseType",
                table: "Licensure",
                column: "LicenseType");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMode_CashId",
                table: "PaymentMode",
                column: "CashId",
                unique: true,
                filter: "[CashId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMode_CharterTripId",
                table: "PaymentMode",
                column: "CharterTripId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMode_CheckId",
                table: "PaymentMode",
                column: "CheckId",
                unique: true,
                filter: "[CheckId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMode_CustomerId",
                table: "PaymentMode",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_EmployeeId",
                table: "Result",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_TestCode",
                table: "Result",
                column: "TestCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceHistory");

            migrationBuilder.DropTable(
                name: "CharterFlightCharge");

            migrationBuilder.DropTable(
                name: "CrewAssignment");

            migrationBuilder.DropTable(
                name: "CrewRequirement");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Licensure");

            migrationBuilder.DropTable(
                name: "PaymentMode");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "Cash");

            migrationBuilder.DropTable(
                name: "CharterTrip");

            migrationBuilder.DropTable(
                name: "Check");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Aircraft");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "AircraftProperty");
        }
    }
}
