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
    public class RoomsController : ControllerBase
    {
        private readonly AsyncInnContext _context;

        public RoomsController(AsyncInnContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
          if (_context.Rooms == null)
          {
              return NotFound();
          }
            return await _context.Rooms.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
          if (_context.Rooms == null)
          {
              return NotFound();
          }
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.ID)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
          if (_context.Rooms == null)
          {
              return Problem("Entity set 'AsyncInnContext.Rooms'  is null.");
          }
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.ID }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (_context.Rooms == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Route("/api/Hotels/{hotelId}/Rooms/{roomID}")]
        [HttpDelete]
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
            {
                HotelID = hotel.ID,
                RoomID = room.ID,
                Name = room.Name,
                Price = hotelRoom.Price
            };
            _context.HotelRooms.Add(NewHotelRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AddRoomToHotel", new { id = NewHotelRoom.HotelID }, NewHotelRoom);

        }



        private bool RoomExists(int id)
        {
            return (_context.Rooms?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
