using Microsoft.EntityFrameworkCore.Migrations;

namespace RC_Charter2.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HourlyCharge",
                table: "License",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyCharge",
                table: "License");
        }
    }
}
