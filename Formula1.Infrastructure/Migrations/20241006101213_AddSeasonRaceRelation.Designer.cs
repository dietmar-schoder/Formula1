﻿// <auto-generated />
using System;
using Formula1.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Formula1.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241006101213_AddSeasonRaceRelation")]
    partial class AddSeasonRaceRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Formula1.Domain.Entities.Circuit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ErgastCircuitId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.HasKey("Id");

                    b.ToTable("FORMULA1_Circuits");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Constructor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.HasKey("Id");

                    b.ToTable("FORMULA1_Constructors");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.HasKey("Id");

                    b.ToTable("FORMULA1_Drivers");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Race", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CircuitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Round")
                        .HasColumnType("int");

                    b.Property<int>("SeasonYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CircuitId");

                    b.HasIndex("SeasonYear");

                    b.ToTable("FORMULA1_Races");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Result", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConstructorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ConstructorId");

                    b.HasIndex("DriverId");

                    b.HasIndex("SessionId");

                    b.ToTable("FORMULA1_Results");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Season", b =>
                {
                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<string>("WikipediaUrl")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("nvarchar(1023)");

                    b.HasKey("Year");

                    b.ToTable("FORMULA1_Seasons");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SessionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateTimeUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.HasIndex("SessionTypeId");

                    b.ToTable("FORMULA1_Sessions");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.SessionType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("FORMULA1_SessionTypes");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Race", b =>
                {
                    b.HasOne("Formula1.Domain.Entities.Circuit", "Circuit")
                        .WithMany("Races")
                        .HasForeignKey("CircuitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Formula1.Domain.Entities.Season", "Season")
                        .WithMany("Races")
                        .HasForeignKey("SeasonYear")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Circuit");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Result", b =>
                {
                    b.HasOne("Formula1.Domain.Entities.Constructor", "Constructor")
                        .WithMany("Results")
                        .HasForeignKey("ConstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Formula1.Domain.Entities.Driver", "Driver")
                        .WithMany("Results")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Formula1.Domain.Entities.Session", "Session")
                        .WithMany("Results")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Constructor");

                    b.Navigation("Driver");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Session", b =>
                {
                    b.HasOne("Formula1.Domain.Entities.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Formula1.Domain.Entities.SessionType", "SessionType")
                        .WithMany()
                        .HasForeignKey("SessionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Race");

                    b.Navigation("SessionType");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Circuit", b =>
                {
                    b.Navigation("Races");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Constructor", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Driver", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Season", b =>
                {
                    b.Navigation("Races");
                });

            modelBuilder.Entity("Formula1.Domain.Entities.Session", b =>
                {
                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
