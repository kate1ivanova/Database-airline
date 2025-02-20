using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace _3LabaCSharp.Models
{
    public class AirlineDetails
    {
        [Key]
        [ForeignKey("Airline")]
        public int AirlineDetailsId { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string ContactNumber { get; set; } = "";

        public Airline? Airline { get; set; } // Связь один к одному
    }
}
