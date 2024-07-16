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
    public City? GetByName(string name)
    {
        return dbContext.Cities.FirstOrDefault(c => c.Name == name);
    }

    public void Add(City city)
    {
        Console.WriteLine($"Adding {city.Name} to database...");
        dbContext.Add(city);
        dbContext.SaveChanges();
    }
}