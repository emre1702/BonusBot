﻿// <auto-generated />
using BonusBot.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BonusBot.Database.Migrations
{
    [DbContext(typeof(FunDbContext))]
    [Migration("20210208070827_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BonusBot.Database.Entities.Settings.BotSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DefaultName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BotSettings");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            DefaultName = "BonusBot",
                            Token = ""
                        });
                });

            modelBuilder.Entity("BonusBot.Database.Entities.Settings.GuildSettings", b =>
                {
                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CommandMentionAllowed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommandPrefix")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GuildId");

                    b.ToTable("GuildSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
