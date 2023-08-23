using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using Microsoft.Identity.Client;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly AsyncInnContext _context;

        public HotelRoomsController(AsyncInnContext context)
        {
            _context = context;
        }

        // GET: api/HotelRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms()
        {
          if (_context.HotelRooms == null)
          {
              return NotFound();
          }
            return await _context.HotelRooms.ToListAsync();
        }

        [HttpGet]
        [Route("/api/HotelRoom/{hotelID}/Rooms")]

        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetAllRoomsforHotel(int hotelID)
        {
            if(hotelID == 0)
            {
                return NotFound();
            }
            var hotelRoom = await _context.HotelRooms.Where(hr => hr.HotelID == hotelID).ToListAsync();
            return hotelRoom;
        }

        // GET: api/HotelRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int id)
        {
          if (_context.HotelRooms == null)
          {
              return NotFound();
          }
            var hotelRoom = await _context.HotelRooms.FindAsync(id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        [HttpGet]
        [Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetSpecificRoom(int hotelId)
        {
            if (hotelId == 0)
            {
                return NotFound();
            }
            var hotelRoom = await _context.HotelRooms.Where(hr => hr.HotelID == hotelId).ToListAsync();
            return hotelRoom;  
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom(int id, HotelRoom hotelRoom)
        {
            if (id != hotelRoom.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelRoomExists(id))
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

        [HttpPut]
        [Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult>UpdateRoom(int id, HotelRoom hotelRoom)
        {
            if (id != hotelRoom.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelRoomExists(id))
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

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
          if (_context.HotelRooms == null)
          {
              return Problem("Entity set 'AsyncInnContext.HotelRooms'  is null.");
          }
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.Id }, hotelRoom);
            
        }

        [HttpPost]
        [Route("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> NewRoom([FromQuery]HotelRoom hotelRoom, int id,[FromBody]int RoomId)
        {
            var hotel = _context.Hotel.FindAsync(id);
            var room = _context.Rooms.FindAsync(id);
            
            if(hotel == null || room == null)
            {
                return NotFound($"Hotel ID{id} is not found! You're an idiot!");
            }
            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.Id }, hotelRoom);
        }


        // DELETE: api/HotelRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelRoom(int id)
        {
            if (_context.HotelRooms == null)
            {
                return NotFound();
            }
            var hotelRoom = await _context.HotelRooms.FindAsync(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    [HttpDelete("{id}")]
    [Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
    public async Task<IActionResult> DeleteSpecificRoom(int id, int roomID)
    {
        var hotelroom = await  _context.HotelRooms.FirstOrDefaultAsync(r => r.HotelID == id && r.RoomID == roomID);
            if (hotelroom == null)
            {
                return NotFound();
            }
           _context.HotelRooms.Remove(hotelroom);
            await _context.SaveChangesAsync();
            return NoContent();
    }
        private bool HotelRoomExists(int id )
        {
            return (_context.HotelRooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
