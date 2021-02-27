﻿// <auto-generated />
using System;
using BonusBot.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BonusBot.Database.Migrations
{
    [DbContext(typeof(BonusDbContext))]
    [Migration("20210227160000_Add_AdditionalInfo_To_TimedActions")]
    partial class Add_AdditionalInfo_To_TimedActions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("BonusBot.Database.Entities.Cases.TimedActions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<ulong?>("AdditionalId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AtDateTime")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan?>("MaxDelay")
                        .HasColumnType("TEXT");

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<ulong>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("TargetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GuildId", "Module", "ActionType", "TargetId");

                    b.ToTable("TimedActions");
                });

            modelBuilder.Entity("BonusBot.Database.Entities.Logging.Logs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<ulong>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("TargetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GuildId", "Module", "Type", "TargetId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("BonusBot.Database.Entities.Settings.GuildsSettings", b =>
                {
                    b.Property<ulong>("GuildId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<string>("Module")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GuildId", "Key", "Module");

                    b.ToTable("GuildsSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
