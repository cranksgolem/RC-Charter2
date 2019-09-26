using Microsoft.EntityFrameworkCore.Migrations;

namespace RC_Charter2.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CheckNumber",
                table: "Check",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CheckNumber",
                table: "Check",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
