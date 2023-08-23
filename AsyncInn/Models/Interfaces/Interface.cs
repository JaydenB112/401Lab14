using Microsoft.AspNetCore.Mvc;
using AsyncInn.Models;

namespace AsyncInn.Models.Interfaces
{
   
    
        public interface IHotel
        {
            Task<ActionResult<IEnumerable<Hotel>>> GetHotel();
            Task<ActionResult<Hotel>> GetHotel(int id);

            Task<IActionResult> PutHotel(int id, Hotel hotel);
            Task<ActionResult<Hotel>> PostHotel(Hotel hotel);
            Task<IActionResult> DeleteHotel(int id);

            bool HotelExists(int id);
        }
    
}
