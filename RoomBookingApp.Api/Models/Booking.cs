using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBookingApp.api.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to Room table (normalized)
        [Required]
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        [NotMapped]
        // kept for JSON compatibility in responses (computed from navigation)
        public string RoomName => Room?.Name ?? string.Empty;

        [Required]
        public string RequesterName { get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public string Purpose { get; set; } = string.Empty;

        // Status: "Menunggu", "Disetujui", "Ditolak"
        [Required]
        public string Status { get; set; } = "Menunggu";

        public DateTime CreatedAt { get; set; } = new DateTime(2026, 2, 11, 10, 0, 0);
    }
}