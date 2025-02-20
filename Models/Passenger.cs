using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _3LabaCSharp.Models
{
    public class Passenger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PassengerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string middleName { get; set; } = "";

        public ICollection<FlightPassenger> FlightPassengers { get; set; } = new List<FlightPassenger>();// Связь многие ко многим
    }
}
