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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeder Awal untuk data dummy
            modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                Id = 1,
                RoomName = "Ruang Teater",
                RequesterName = "Adryan",
                StartTime = new DateTime(2026, 2, 11, 9, 0, 0),
                EndTime = new DateTime(2026, 2, 11, 11, 0, 0),
                Purpose = "Rapat HIMIT",
                Status = "Approved"
            }
        );
        }
    }
}