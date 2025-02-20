using System.Collections.Generic;
using _3LabaCSharp.Models;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace _3LabaCSharp.Migrations;

public class AirlineContext : DbContext
{
    public DbSet<Airline> Airlines { get; set; } = null!;
    public DbSet<AirlineDetails> AirlineDetails { get; set; } = null!;
    public DbSet<Flight> Flights { get; set; } = null!;
    public DbSet<Passenger> Passengers { get; set; } = null!;
    public DbSet<FlightPassenger> FlightPassengers { get; set; } = null!;

    public AirlineContext(DbContextOptions<AirlineContext> options)
    : base(options)
    {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Airline>().HasData(
                new Airline { AirlineId = 1, Airlinename = "Utair" },
                new Airline { AirlineId = 2, Airlinename = "Pobeda" },
                new Airline { AirlineId = 3, Airlinename = "S7Airlines" }
        );
        // Заполнение таблицы AirlineDetails
        modelBuilder.Entity<AirlineDetails>().HasData(
            new AirlineDetails { AirlineDetailsId = 1, Description = "Utair", ContactNumber = "123-456-7890" },
            new AirlineDetails { AirlineDetailsId = 2, Description = "Pobeda", ContactNumber = "098-765-4321" },
            new AirlineDetails { AirlineDetailsId = 3, Description = "S7Airlines", ContactNumber = "555-555-5555" }
            );
        // Заполнение таблицы Flight
        modelBuilder.Entity<Flight>().HasData(
            new Flight { FlightId = 1, FlightNumber = "D123", AirlineId = 1 },
            new Flight { FlightId = 2, FlightNumber = "V456", AirlineId = 2 },
            new Flight { FlightId = 3, FlightNumber = "P789", AirlineId = 3 }
        );

        // Заполнение таблицы Passenger
        modelBuilder.Entity<Passenger>().HasData(
            new Passenger { PassengerId = 1, FirstName = "John", LastName = "Doe", middleName = "Duij" },
            new Passenger { PassengerId = 2, FirstName = "Jane", LastName = "Smith", middleName = "Rot" },
            new Passenger { PassengerId = 3, FirstName = "Alice", LastName = "Johnson", middleName = "Eds" }
        );

        // Заполнение таблицы FlightPassenger
        modelBuilder.Entity<FlightPassenger>()
        .HasKey(fp => new { fp.FlightId, fp.PassengerId });
        modelBuilder.Entity<FlightPassenger>().HasData(
            new FlightPassenger { FlightId = 1, PassengerId = 1 },
            new FlightPassenger { FlightId = 1, PassengerId = 2 },
            new FlightPassenger { FlightId = 2, PassengerId = 2 },
            new FlightPassenger { FlightId = 3, PassengerId = 3 }
        );

       

        modelBuilder.Entity<Airline>()//удаление airline удаляет airlinedetails
            .HasOne(a => a.AirlineDetails)
            .WithOne(ad => ad.Airline)
            .HasForeignKey<AirlineDetails>(ad => ad.AirlineDetailsId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Airline>()//удаление airline удаляет flights
            .HasMany(a => a.Flights)
            .WithOne(f => f.Airline)
            .HasForeignKey(f => f.AirlineId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Flight>()//delete flight->delete passengers
            .HasMany(f => f.FlightPassengers)
            .WithOne(fp => fp.Flight)
            .HasForeignKey(fp => fp.FlightId)
            .OnDelete(DeleteBehavior.Cascade); 

    }

}

