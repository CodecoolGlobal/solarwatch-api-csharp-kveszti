using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class JsonProcessorForGeocoding
{
    public Coordinate ConvertDataToCoordinate(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement lat = json.RootElement[0].GetProperty("lat");
        JsonElement lon = json.RootElement[0].GetProperty("lon");

        return new Coordinate(lat.GetDouble(), lon.GetDouble());
    }
}