using Microsoft.EntityFrameworkCore;
using RoomBookingApp.api.Models;

namespace RoomBookingApp.api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Ruang Teater" },
                new Room { Id = 2, Name = "Aula" },
                new Room { Id = 3, Name = "Audit" },
                new Room { Id = 4, Name = "D4 IT A" },
                new Room { Id = 5, Name = "Lt 6 Mini teater" }
            );

            // Seeder Awal untuk bookings (referencing seeded rooms)
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    RoomId = 1,
                    RequesterName = "Adryan",
                    StartTime = new DateTime(2026, 2, 11, 9, 0, 0),
                    EndTime = new DateTime(2026, 2, 11, 11, 0, 0),
                    Purpose = "Rapat HIMIT",
                    Status = "Approved",
                    CreatedAt = new DateTime(2026, 2, 11, 10, 0, 0)
                },
                new Booking
                {
                    Id = 3,
                    RoomId = 2,
                    RequesterName = "Fahmi",
                    StartTime = DateTime.MinValue,
                    EndTime = DateTime.MinValue,
                    Purpose = "Rapat kerja",
                    Status = "Approved",
                    CreatedAt = new DateTime(2026, 2, 11, 10, 0, 0)
                },
                new Booking
                {
                    Id = 4,
                    RoomId = 3,
                    RequesterName = "Pak Arif",
                    StartTime = DateTime.MinValue,
                    EndTime = DateTime.MinValue,
                    Purpose = "Seminar Basis Data",
                    Status = "Approved",
                    CreatedAt = new DateTime(2026, 2, 11, 10, 0, 0)
                },
                new Booking
                {
                    Id = 6,
                    RoomId = 4,
                    RequesterName = "Fahmi",
                    StartTime = DateTime.MinValue,
                    EndTime = DateTime.MinValue,
                    Purpose = "rapat it",
                    Status = "Pending",
                    CreatedAt = new DateTime(2026, 2, 11, 10, 0, 0)
                }
            );
        }
    }
}