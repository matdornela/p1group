﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Migrations
{
    [DbContext(typeof(FlightsContext))]
    [Migration("20210505172237_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Domain.Aggregates.AirportAggregate.Airport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Arrival")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("Departure")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("_destinationAirportId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("_originAirportId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("_destinationAirportId");

                    b.HasIndex("_originAirportId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.FlightRate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Available")
                        .HasColumnType("integer");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("FlightRates");
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.Flight", b =>
                {
                    b.HasOne("Domain.Aggregates.AirportAggregate.Airport", null)
                        .WithMany()
                        .HasForeignKey("_destinationAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Aggregates.AirportAggregate.Airport", null)
                        .WithMany()
                        .HasForeignKey("_originAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Aggregates.FlightAggregate.FlightRate", b =>
                {
                    b.HasOne("Domain.Aggregates.FlightAggregate.Flight", null)
                        .WithMany("Rates")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Common.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("FlightRateId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Currency")
                                .HasColumnType("integer");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric");

                            b1.HasKey("FlightRateId");

                            b1.ToTable("FlightRates");

                            b1.WithOwner()
                                .HasForeignKey("FlightRateId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
