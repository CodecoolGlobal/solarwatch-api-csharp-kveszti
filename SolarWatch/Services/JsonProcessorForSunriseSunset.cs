using System.Text.Json;

namespace SolarWatch.Services;

public class JsonProcessorForSunriseSunset
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
}