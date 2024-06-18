namespace SolarWatch.Model;

public class SolarData
{
    public int Id { get; init; }
    public DateTime Sunrise { get; init; }
    public DateTime Sunset { get; init; }
    public int CityId { get; init; }
    public string TimeZone { get; init; }
}