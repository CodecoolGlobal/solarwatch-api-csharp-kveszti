using SolarWatch.Model;

namespace SolarWatch.Services.Repositories;

public class SolarDataRepository : ISolarDataRepository
{
    private SolarApiContext dbContext;
    
    public SolarDataRepository(SolarApiContext context)
    {
        dbContext = context;
    }
    public SolarData? GetSolarData(int cityId, DateTime? date, string TimeZone)
    {
        if (date == null)
        {
            date = DateTime.Today;
        }
        return dbContext.SolarDatas.FirstOrDefault(data => data.Sunrise.Date == date.Value.Date && cityId == data.CityId && TimeZone == data.TimeZone, null);
    }
    
    public void Add(SolarData data)
    {
        dbContext.Add(data);
        dbContext.SaveChanges();
    }
}