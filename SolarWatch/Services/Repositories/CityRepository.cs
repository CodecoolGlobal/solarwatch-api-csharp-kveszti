
using Microsoft.EntityFrameworkCore;
using SolarWatch.Context;
using SolarWatch.Model;

namespace SolarWatch.Services.Repositories;

public class CityRepository : ICityRepository
{
    private SolarApiContext dbContext;
    public CityRepository(SolarApiContext context)
    {
        dbContext = context;
    }

    public async Task<IEnumerable<City>> GetAllCities()
    {
        return await dbContext.Cities.ToListAsync();
    }
    
    public async Task<City?> GetByName(string name)
    {
        return await dbContext.Cities.FirstOrDefaultAsync(c => c.Name == name);
    }
    
    public async Task<City?> GetById(int id)
    {
        return await dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Add(City city)
    {
        var existingCity = await dbContext.Cities.FirstOrDefaultAsync(c => c.Name == city.Name);
        if (existingCity != null)
        {
            Console.WriteLine($"City {city.Name} already exists in the database.");
            return; 
        }

        Console.WriteLine($"Adding {city.Name} to database...");
        dbContext.Add(city);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(City city)
    {
        var cityDbRepresentation = await dbContext.Cities.FirstOrDefaultAsync(data => data.Id == city.Id);
        
        if (cityDbRepresentation == null)
        {
            throw new InvalidOperationException($"City with ID {city.Id} not found.");
        }

        cityDbRepresentation.Name = city.Name;
        cityDbRepresentation.Country = city.Country;
        cityDbRepresentation.State = city.State;
        cityDbRepresentation.Latitude = city.Latitude;
        cityDbRepresentation.Longitude = city.Longitude;

        await dbContext.SaveChangesAsync();
    }
    
    public async Task Delete(int id)
    {
        var dbRepresentation = await dbContext.Cities.FirstOrDefaultAsync(data => data.Id == id);
        
        if (dbRepresentation == null)
        {
            throw new InvalidOperationException($"City with ID {id} not found.");
        }

        dbContext.Remove(dbRepresentation);
        await dbContext.SaveChangesAsync();
    }
}