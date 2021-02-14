using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusBot.Database.Migrations
{
    public partial class Remove_GuildCoresettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildCoreSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildCoreSettings",
                columns: table => new
                {
                    GuildId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    CommandMentionAllowed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CommandPrefix = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildCoreSettings", x => x.GuildId);
                });
        }
    }
}
