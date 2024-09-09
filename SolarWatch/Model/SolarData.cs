using System;

namespace SolarWatch.Model;
//todo: creating a new field for search date, migrating the database again, etc.
public class SolarData
{
    public SolarData(DateTime sunrise, DateTime sunset, int cityId, string timeZone)
    {
        Sunrise = sunrise;
        Sunset = sunset;
        CityId = cityId;
        TimeZone = timeZone;
    }

    public int Id { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public int CityId { get; set; }
    public string TimeZone { get; set; }
}