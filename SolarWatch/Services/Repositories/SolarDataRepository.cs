using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolarWatch.Context;
using SolarWatch.Model;

namespace SolarWatch.Services.Repositories;

public class SolarDataRepository : ISolarDataRepository
{
    private SolarApiContext dbContext;
    
    public SolarDataRepository(SolarApiContext context)
    {
        dbContext = context;
    }
    public async Task<SolarData?> GetSolarData(int cityId, DateTime? date, string timeZone)
    {
            if (date == null)
            {
                date = DateTime.Today;
            }
            
            var startOfPrevDay = date.Value.Date.AddDays(-1);
            var endOfNextDay = startOfPrevDay.AddDays(2);

            return await dbContext.SolarDatas
                .FirstOrDefaultAsync(data => data.Sunrise.Date >= startOfPrevDay && data.Sunset.Date <= endOfNextDay
                                                                   && data.CityId == cityId 
                                                                   && data.TimeZone == timeZone);

    }

    public async Task<IEnumerable<SolarData>> GetAllSolarData()
    {
        return await dbContext.SolarDatas.ToListAsync();
    }
    
    public async Task Add(SolarData data)
    {
        dbContext.Add(data);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(SolarData newData)
    {
        var dbRepresentation = await dbContext.SolarDatas.FirstOrDefaultAsync(dataInDb => newData.Id == dataInDb.Id);
        
        if (dbRepresentation == null)
        {
            throw new InvalidOperationException($"Product with ID {newData.Id} not found.");
        }
        
        dbRepresentation.Sunrise = newData.Sunrise;
        dbRepresentation.Sunset = newData.Sunset;
        dbRepresentation.CityId = newData.CityId;
        dbRepresentation.TimeZone = newData.TimeZone;
        dbRepresentation.SearchDate = newData.SearchDate;

        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var dbRepresentation = await dbContext.SolarDatas.FirstOrDefaultAsync(data => data.Id == id);
        
        if (dbRepresentation == null)
        {
            throw new InvalidOperationException($"Product with ID {id} not found.");
        }

        dbContext.Remove(dbRepresentation);
        await dbContext.SaveChangesAsync();
    }
    
}