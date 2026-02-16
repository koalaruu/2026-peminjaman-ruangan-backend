using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.api.Data;
using RoomBookingApp.api.Models;

namespace RoomBookingApp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public BookingsController(ApiDbContext context)
        {
            _context = context;
        }

        // 1. GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings([FromQuery] string? searchTerm)
        {
            // include Room navigation so client can read RoomName
            var query = _context.Bookings.Include(b => b.Room).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();
                query = query.Where(b => 
                    (b.Room != null && b.Room.Name.ToLower().Contains(lowerCaseSearchTerm)) || 
                    b.RequesterName.ToLower().Contains(lowerCaseSearchTerm) || 
                    b.Purpose.ToLower().Contains(lowerCaseSearchTerm) ||
                    b.Status.ToLower().Contains(lowerCaseSearchTerm)
                );
            }

            return await query.OrderByDescending(b => b.CreatedAt).ToListAsync();
        }

        // 2. GET 
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.Include(b => b.Room).FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null) return NotFound();
            return booking;
        }

        // 3. POST: 
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            // validate room exists
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null) return BadRequest("Invalid RoomId");

            if (booking.CreatedAt == default) booking.CreatedAt = DateTime.UtcNow;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            // include Room before returning
            await _context.Entry(booking).Reference(b => b.Room).LoadAsync();
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        // 4. PUT:
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id) return BadRequest();

            // validate room
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room == null) return BadRequest("Invalid RoomId");

            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 5. DELETE:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}