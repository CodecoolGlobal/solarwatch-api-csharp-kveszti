using SolarWatch.Model;

namespace SolarWatch.Services;

public interface ISolarApi
{
    public string GetSunriseAndSunset(Coordinate coordinate, string timeZone, DateTime? date);
}