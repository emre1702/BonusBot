using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusBot.Database.Migrations
{
    public partial class Add_MaxDelay_To_TimedActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "MaxDelay",
                table: "TimedActions",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDelay",
                table: "TimedActions");
        }
    }
}
