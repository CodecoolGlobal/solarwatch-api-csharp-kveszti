namespace SolarWatch.Model;

public class SolarData
{
    public SolarData(DateTime sunrise, DateTime sunset, int cityId, string timeZone)
    {
        Sunrise = sunrise;
        Sunset = sunset;
        CityId = cityId;
        TimeZone = timeZone;
    }

    public int Id { get; private set; }
    public DateTime Sunrise { get; init; }
    public DateTime Sunset { get; init; }
    public int CityId { get; init; }
    public string TimeZone { get; init; }
}