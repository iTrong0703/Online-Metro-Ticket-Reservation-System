﻿// <auto-generated />
using MetroTicketReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MetroTicketReservation.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MetroTicketReservation.Domain.Entities.Line", b =>
                {
                    b.Property<int>("LineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LineID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("LineName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LineID");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("MetroTicketReservation.Domain.Entities.Station", b =>
                {
                    b.Property<int>("StationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StationID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("StationID");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("MetroTicketReservation.Domain.Entities.StationLine", b =>
                {
                    b.Property<int>("StationID")
                        .HasColumnType("integer");

                    b.Property<int>("LineID")
                        .HasColumnType("integer");

                    b.Property<int>("StationOrder")
                        .HasColumnType("integer");

                    b.HasKey("StationID", "LineID");

                    b.HasIndex("LineID");

                    b.HasIndex("StationID", "LineID");

                    b.ToTable("StationLines");
                });

            modelBuilder.Entity("MetroTicketReservation.Domain.Entities.StationLine", b =>
                {
                    b.HasOne("MetroTicketReservation.Domain.Entities.Line", "Line")
                        .WithMany("StationLines")
                        .HasForeignKey("LineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MetroTicketReservation.Domain.Entities.Station", "Station")
                        .WithMany("StationLines")
                        .HasForeignKey("StationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Line");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("MetroTicketReservation.Domain.Entities.Line", b =>
                {
                    b.Navigation("StationLines");
                });

            modelBuilder.Entity("MetroTicketReservation.Domain.Entities.Station", b =>
                {
                    b.Navigation("StationLines");
                });
#pragma warning restore 612, 618
        }
    }
}
