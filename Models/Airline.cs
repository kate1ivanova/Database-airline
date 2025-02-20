using System.ComponentModel.DataAnnotations;
using _3LabaCSharp.Models;

namespace TodoApi.Models
{
    public class Airline
    {
        [Key]
        public int AirlineId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Airlinename { get; set; }

        public AirlineDetails? AirlineDetails { get; set; } // Связь один к одному
        public ICollection<Flight> Flights { get; set; } = new List<Flight>(); // Связь один ко многим
    }
}
