using System.ComponentModel.DataAnnotations;

namespace RoomBookingApp.api.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // optional description or metadata can be added later
    }
}