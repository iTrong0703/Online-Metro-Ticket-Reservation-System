using MetroTicketReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTicketReservation.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Station> Stations { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<StationLine> StationLines { get; set; }
        public DbSet<StationFare> StationFares { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Station
            modelBuilder.Entity<Station>()
                .Property(s => s.StationID)
                .UseIdentityColumn();

            // Line
            modelBuilder.Entity<Line>()
                .Property(l => l.LineID)
                .UseIdentityColumn();

            // StationLine
            modelBuilder.Entity<StationLine>()
                .HasKey(sl => new {  sl.StationID, sl.LineID });
            modelBuilder.Entity<StationLine>()
                .HasOne(sl => sl.Station)
                .WithMany(sl => sl.StationLines)
                .HasForeignKey(sl => sl.StationID);
            modelBuilder.Entity<StationLine>()
                .HasOne(sl => sl.Line)
                .WithMany(sl => sl.StationLines)
                .HasForeignKey(sl => sl.LineID);
            modelBuilder.Entity<StationLine>()
                .HasIndex(sl => new { sl.StationID, sl.LineID });

            // StationFare
            modelBuilder.Entity<StationFare>()
                .Property(s => s.StationFareID)
                .UseIdentityColumn();
            modelBuilder.Entity<StationFare>()
                .HasOne(s => s.StartStation)
                .WithMany()
                .HasForeignKey(s => s.StartStationID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StationFare>()
                .HasOne(s => s.EndStation)
                .WithMany()
                .HasForeignKey(s => s.EndStationID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<StationFare>()
                .HasOne(t => t.TicketType)
                .WithMany()
                .HasForeignKey(t => t.TicketTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            // TicketType
            modelBuilder.Entity<TicketType>()
                .Property(t => t.TicketTypeID)
                .UseIdentityColumn();

        }
    }
}
