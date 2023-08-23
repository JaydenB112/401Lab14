using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models
{
    public class RoomAmens
    {
        [Key]

        public int ID { get; set; }
        [Required]

        public int RoomsID { get; set; }
        [Required]

        public int AmenID { get; set; }

        public Room Rooms { get; set; }

        public Amenities Amenity { get; set; }
    }
}
