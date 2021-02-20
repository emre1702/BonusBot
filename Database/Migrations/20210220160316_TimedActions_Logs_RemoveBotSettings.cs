using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusBot.Database.Migrations
{
    public partial class TimedActions_Logs_RemoveBotSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotSettings");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TargetId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    SourceId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    GuildId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Module = table.Column<string>(type: "TEXT", nullable: false),
                    AddedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimedActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TargetId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    SourceId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    ActionType = table.Column<string>(type: "TEXT", nullable: false),
                    AtDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GuildId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Module = table.Column<string>(type: "TEXT", nullable: false),
                    AddedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedActions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_GuildId_Module_Type_TargetId",
                table: "Logs",
                columns: new[] { "GuildId", "Module", "Type", "TargetId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimedActions_GuildId_Module_ActionType_TargetId",
                table: "TimedActions",
                columns: new[] { "GuildId", "Module", "ActionType", "TargetId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "TimedActions");

            migrationBuilder.CreateTable(
                name: "BotSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DefaultName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BotSettings",
                columns: new[] { "Id", "DefaultName" },
                values: new object[] { -1, "BonusBot" });
        }
    }
}
