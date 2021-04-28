using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BonusBot.Database.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildsSettings",
                columns: table => new
                {
                    GuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Module = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildsSettings", x => new { x.GuildId, x.Key, x.Module });
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TargetId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    SourceId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    GuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Module = table.Column<string>(type: "text", nullable: false),
                    AddedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimedActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TargetId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    SourceId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    ActionType = table.Column<string>(type: "text", nullable: false),
                    AtDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MaxDelay = table.Column<TimeSpan>(type: "interval", nullable: true),
                    AdditionalId = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    GuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Module = table.Column<string>(type: "text", nullable: false),
                    AddedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "datetime('now')")
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
                name: "GuildsSettings");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "TimedActions");
        }
    }
}
