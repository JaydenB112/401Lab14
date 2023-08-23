using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models
{




    public class Amenities
    {
        [Key]

        public int ID { get; set; }
        [Required]

        public string AmenityName { get; set; }

        public List<RoomAmens> RoomAmenity { get; set; }


    }


}
