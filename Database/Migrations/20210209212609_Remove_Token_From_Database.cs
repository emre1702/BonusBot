using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusBot.Database.Migrations
{
    public partial class Remove_Token_From_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "BotSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "BotSettings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "BotSettings",
                keyColumn: "Id",
                keyValue: -1,
                column: "Token",
                value: "");
        }
    }
}