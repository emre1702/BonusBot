using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusBot.Database.Migrations
{
    public partial class Add_AdditionalInfo_To_TimedActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "AdditionalId",
                table: "TimedActions",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalId",
                table: "TimedActions");
        }
    }
}
