using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models
{
    public class Room
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int Layout { get; set; }

        public List<HotelRoom> HotelRooms { get; set; }

        public List<RoomAmens> RoomAmens { get; set; }
    }
}
