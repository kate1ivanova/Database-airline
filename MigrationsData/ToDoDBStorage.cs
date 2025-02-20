using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _3LabaCSharp.Models;
using TodoApi.Models;
using _3LabaCSharp.Migrations;

public class TodoDbStorage<T> where T : class
{
    private readonly AirlineContext _context;//экземпляр контекста бд для доступа к данным
    private readonly DbSet<T> _dbSet;//набор данных

    public TodoDbStorage(AirlineContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<List<Airline>> GetAllWithDetailsAndFlightsAsync()
    {
        return await _context.Airlines
            .Include(a => a.AirlineDetails) 
            .Include(a => a.Flights) 
            .ToListAsync();
    }
    public async Task<List<Passenger>> GetAllWithFlightsAsync()
    {
        return await _context.Passengers
            .Include(p => p.FlightPassengers)
            .ThenInclude(fp => fp.Flight) 
            .ToListAsync();
    }

    public async Task<List<Flight>> GetAllWithPassengersAsync()
    {
        return await _context.Flights
            .Include(f => f.FlightPassengers)
            .ThenInclude(fp => fp.Passenger) // Загрузка связанных пассажиров
            .ToListAsync();
    }

   
    public async Task<T?> GetByIdAsync(int id)
    {
        // Используем FirstOrDefaultAsync с Include для загрузки связанных данных
        return await _dbSet
            .FindAsync(id); // Используем FindAsync для получения записи по идентификатору
    }
    //вместе с сущностью все связи тянуть 
    //.include mb

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    internal object Include(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}


