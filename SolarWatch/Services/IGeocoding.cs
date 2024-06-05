namespace SolarWatch.Services;

public interface IGeocoding
{
    public string GetGeocodeForCity(string cityName);
}