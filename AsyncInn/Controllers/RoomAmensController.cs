using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomAmensController : ControllerBase
    {
        private readonly AsyncInnContext _context;

        public RoomAmensController(AsyncInnContext context)
        {
            _context = context;
        }

        // GET: api/RoomAmens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomAmens>>> GetRoomAmens()
        {
            if (_context.RoomAmens == null)
            {
                return NotFound();
            }
            return await _context.RoomAmens.ToListAsync();
        }

        // GET: api/RoomAmens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomAmens>> GetRoomAmens(int id)
        {
            if (_context.RoomAmens == null)
            {
                return NotFound();
            }
            var roomAmens = await _context.RoomAmens.FindAsync(id);

            if (roomAmens == null)
            {
                return NotFound();
            }

            return roomAmens;
        }

        // PUT: api/RoomAmens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomAmens(int id, RoomAmens roomAmens)
        {
            if (id != roomAmens.ID)
            {
                return BadRequest();
            }

            _context.Entry(roomAmens).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomAmensExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RoomAmens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomAmens>> PostRoomAmens(RoomAmens roomAmens)
        {
            if (_context.RoomAmens == null)
            {
                return Problem("Entity set 'AsyncInnContext.RoomAmens'  is null.");
            }
            _context.RoomAmens.Add(roomAmens);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomAmens", new { id = roomAmens.ID }, roomAmens);
        }

        // DELETE: api/RoomAmens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAmens(int id)
        {
            if (_context.RoomAmens == null)
            {
                return NotFound();
            }
            var roomAmens = await _context.RoomAmens.FindAsync(id);
            if (roomAmens == null)
            {
                return NotFound();
            }

            _context.RoomAmens.Remove(roomAmens);
            await _context.SaveChangesAsync();

            return NoContent();
        }
     
        [Route("/api/Hotels/{hotelId}/Rooms/{roomID}")]
        public async Task<IActionResult> DeleteSpecificRoom(int hotelId, int roomID)
        {
            var hotelRoom = await _context.HotelRooms.FirstOrDefaultAsync(r => r.HotelID == hotelId && r.RoomID == roomID);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost]
        [Route("/api/HotelRoom/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> AddRoomToHotel(int hotelId, [FromQuery] int roomId, [FromBody] HotelRoom hotelRoom)
        {
            var hotel = await _context.Hotel.FindAsync(hotelId);
            var room = await _context.Rooms.FindAsync(roomId);

            if (hotel == null)
            {
                return NotFound($"Hotel with ID {hotelId} not found.");
            }
            else if (room == null)
            {
                return NotFound();
            }

            //hotelRoom.HotelID = hotelId;

            HotelRoom NewHotelRoom = new HotelRoom() 
            { HotelID = hotel.ID,
                RoomID = room.ID, 
                Name = room.Name, 
                Price = hotelRoom.Price 
            };
            _context.HotelRooms.Add(NewHotelRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AddRoomToHotel", new { id = NewHotelRoom.Id }, NewHotelRoom);

        }


        private bool RoomAmensExists(int id)
        {
            return (_context.RoomAmens?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
