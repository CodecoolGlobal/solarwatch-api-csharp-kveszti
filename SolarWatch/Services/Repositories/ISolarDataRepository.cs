
using SolarWatch.Model;

namespace SolarWatch.Services.Repositories;

public interface ISolarDataRepository
{
    public Task<IEnumerable<SolarData>> GetAllSolarData();
    public Task<SolarData?> GetSolarData(int cityId, DateTime? date, string TimeZone);

    public Task Add(SolarData data);

    public Task Update(SolarData newData);

    public Task Delete(int id);
}