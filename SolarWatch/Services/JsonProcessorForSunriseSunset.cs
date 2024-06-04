using System.Text.Json;

namespace SolarWatch.Services;

public class JsonProcessorForSunriseSunset
{
    public string GetSunrise(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        JsonElement sunrise = results.GetProperty("sunrise");
        
        return sunrise.GetString();
    }

    public string GetSunset(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        JsonElement sunset = results.GetProperty("sunset");
        
        return sunset.GetString();
    }
}