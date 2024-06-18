using SolarWatch.Model;

namespace SolarWatch.Services.Repositories;

public class SolarDataRepository : ISolarDataRepository
{
    private SolarApiContext dbContext;
    
    public SolarDataRepository(SolarApiContext context)
    {
        dbContext = context;
    }
    public SolarData? GetSolarData(int cityId, DateTime date, string TimeZone)
    {
        return dbContext.SolarDatas.FirstOrDefault(data => data.Sunrise.Date == date.Date && cityId == data.CityId && TimeZone == data.TimeZone);
    }
    
    public void Add(SolarData data)
    {
        dbContext.Add(data);
        dbContext.SaveChanges();
    }
}