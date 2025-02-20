
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using _3LabaCSharp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using TodoApi.Models;

namespace _3LabaCSharp.Controllers
{
    [Route("api/Airline")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly TodoDbStorage<Airline> _airlineStorage;
        private readonly TodoDbStorage<AirlineDetails> _airlineDetailsStorage;
        private readonly TodoDbStorage<Flight> _flightStorage;
        private readonly TodoDbStorage<Passenger> _passengerStorage;

        public AirlineController(
            TodoDbStorage<Airline> airlineStorage,
            TodoDbStorage<AirlineDetails> airlineDetailsStorage,
            TodoDbStorage<Flight> flightStorage,
            TodoDbStorage<Passenger> passengerStorage)
        {
            _airlineStorage = airlineStorage;
            _airlineDetailsStorage = airlineDetailsStorage;
            _flightStorage = flightStorage;
            _passengerStorage = passengerStorage;
        }

        // Airline Routes
        [HttpGet("airlines")]
        public async Task<ActionResult<IEnumerable<Airline>>> GetAirlines()
        {
            var airlines = await _airlineStorage.GetAllWithDetailsAndFlightsAsync();
            return Ok(airlines);
        }

        [HttpGet("airlines/{id}")]
        public async Task<ActionResult<Airline>> GetAirline(int id)
        {
            var airline = await _airlineStorage.GetByIdAsync(id);
            if (airline == null)
            {
                return NotFound();
            }
            return Ok(airline);
        }

        [HttpPost("airlines")]
        public async Task<ActionResult<Airline>> PostAirline(Airline airline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _airlineStorage.AddAsync(airline);
            return CreatedAtAction(nameof(GetAirline), new { id = airline.AirlineId }, airline);
        }

        [HttpPut("airlines/{id}")]
        public async Task<IActionResult> PutAirline(int id, Airline airline)
        {
            if (id != airline.AirlineId)
            {
                return BadRequest();
            }

            await _airlineStorage.EditAsync(airline);
            return NoContent();
        }

        [HttpDelete("airlines/{id}")]
        public async Task<IActionResult> DeleteAirline(int id)
        {
            var airline = await _airlineStorage.GetByIdAsync(id);
            if (airline == null)
            {
                return NotFound();
            }

            await _airlineStorage.RemoveAsync(airline);
            return NoContent();
        }
       // AirlineDetails Routes
        [HttpGet("airlinedetails")]
        public async Task<ActionResult<IEnumerable<AirlineDetails>>> GetAirlineDetails()
        {
            var details = await _airlineDetailsStorage.GetAllAsync();
            return Ok(details);
        }

        [HttpGet("airlinedetails/{id}")]
        public async Task<ActionResult<AirlineDetails>> GetAirlineDetails(int id)
        {
            var details = await _airlineDetailsStorage.GetByIdAsync(id);
            if (details == null)
            {
                return NotFound();
            }
            return Ok(details);
        }

        [HttpPost("airlinedetails")]
        public async Task<ActionResult<AirlineDetails>> PostAirlineDetails(AirlineDetails details)
        {
            await _airlineDetailsStorage.AddAsync(details);
            return CreatedAtAction(nameof(GetAirlineDetails), new { id = details.AirlineDetailsId }, details);
        }

        [HttpPut("airlinedetails/{id}")]
        public async Task<IActionResult> PutAirlineDetails(int id, AirlineDetails details)
        {
            if (id != details.AirlineDetailsId)
            {
                return BadRequest();
            }

            await _airlineDetailsStorage.EditAsync(details);
            return NoContent();
        }

        [HttpDelete("airlinedetails/{id}")]
        public async Task<IActionResult> DeleteAirlineDetails(int id)
        {
            var details = await _airlineDetailsStorage.GetByIdAsync(id);
            if (details == null)
            {
                return NotFound();
            }

            await _airlineDetailsStorage.RemoveAsync(details);
            return NoContent();
        }

        // Flight Routes
        [HttpGet("flights")]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            var flights = await _flightStorage.GetAllAsync();
            return Ok(flights);
        }

        [HttpGet("flights/{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flights = await _flightStorage.GetAllWithPassengersAsync(); // Получаем все рейсы
            var foundFlight = flights.FirstOrDefault(f => f.FlightId == id); // Находим рейс по ID
            if (foundFlight == null)
            {
                return NotFound();
            }
            return Ok(foundFlight);
        }

        [HttpPost("flights")]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
            await _flightStorage.AddAsync(flight);
            return CreatedAtAction(nameof(GetFlight), new { id = flight.FlightId }, flight);
        }

        [HttpPut("flights/{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.FlightId)
            {
                return BadRequest();
            }

            await _flightStorage.EditAsync(flight);
            return NoContent();
        }

        [HttpDelete("flights/{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _flightStorage.GetByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            await _flightStorage.RemoveAsync(flight);
            return NoContent();
        }

        // Passenger Routes
        [HttpGet("passengers")]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassengers()
        {
            var passengers = await _passengerStorage.GetAllAsync();
            return Ok(passengers);
        }

        [HttpGet("passengers/{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int id)
        {
            var passenger = await _passengerStorage.GetAllWithFlightsAsync(); // Получаем всех пассажиров
            var foundPassenger = passenger.FirstOrDefault(p => p.PassengerId == id); // Находим пассажира по ID
            if (foundPassenger == null)
            {
                return NotFound();
            }
           

            return Ok(foundPassenger);
        }

        [HttpPost("passengers")]
        public async Task<ActionResult<Passenger>> PostPassenger(Passenger passenger)
        {
            await _passengerStorage.AddAsync(passenger);
            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.PassengerId }, passenger);
        }

        [HttpPut("passengers/{id}")]
        public async Task<IActionResult> PutPassenger(int id, Passenger passenger)
        {
            if (id != passenger.PassengerId)
            {
                return BadRequest();
            }

            await _passengerStorage.EditAsync(passenger);
            return NoContent();
        }
       


        [HttpDelete("passengers/{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            var passenger = await _passengerStorage.GetByIdAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }

            await _passengerStorage.RemoveAsync(passenger);
            return NoContent();
        }
    }
 
}


