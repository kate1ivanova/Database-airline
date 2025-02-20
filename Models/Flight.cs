using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace _3LabaCSharp.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightId { get; set; }

        [Required]
        [StringLength(10)]
        public string FlightNumber { get; set; } = "";

        public int AirlineId { get; set; }
        public Airline? Airline { get; set; } // Связь один ко многим

        public ICollection<FlightPassenger> FlightPassengers { get; set; } = new List<FlightPassenger>(); // Связь многие ко многим
        
    }
}
