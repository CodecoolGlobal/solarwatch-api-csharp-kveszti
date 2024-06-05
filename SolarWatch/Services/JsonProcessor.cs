using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class JsonProcessor : IJsonProcessor
{
    public DateTime GetSunrise(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        JsonElement sunrise = results.GetProperty("sunrise");
        
        return sunrise.GetDateTime();
    }

    public DateTime GetSunset(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        JsonElement sunset = results.GetProperty("sunset");
        
        return sunset.GetDateTime();
    }
    
    public Coordinate ConvertDataToCoordinate(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement lat = json.RootElement[0].GetProperty("lat");
        JsonElement lon = json.RootElement[0].GetProperty("lon");

        return new Coordinate(lat.GetDouble(), lon.GetDouble());
    }
}