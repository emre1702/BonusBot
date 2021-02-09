using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusBot.Database.Migrations
{
    public partial class GuildsSettings_Proper_Keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GuildsSettings",
                table: "GuildsSettings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuildsSettings",
                table: "GuildsSettings",
                columns: new[] { "GuildId", "Key", "Module" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GuildsSettings",
                table: "GuildsSettings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuildsSettings",
                table: "GuildsSettings",
                column: "GuildId");
        }
    }
}