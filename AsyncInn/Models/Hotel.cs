using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models
{
    public class Hotel
    {
        [Key]

        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
    }
}
