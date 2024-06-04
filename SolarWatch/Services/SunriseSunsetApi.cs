using System.Net;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class SunriseSunsetApi
{
    private readonly ILogger<SunriseSunsetApi> _logger;
    public SunriseSunsetApi(ILogger<SunriseSunsetApi> logger)
    {
        _logger = logger;
    }
    
    public string GetSunriseAndSunset(Coordinate coordinate, string timeZone)
    {
        var url = generateURL(coordinate, timeZone);
        using var client = new WebClient();
        
        _logger.LogInformation("Calling Sunrise-Sunset API with url: {url}", url);
        return client.DownloadString(url);
    }
    

    private string generateURL(Coordinate coordinate, string timeZone)
    {
        return
            $"https://api.sunrise-sunset.org/json?lat={coordinate.Latitude}&lng={coordinate.Longitude}&date=today&tzid={timeZone}";
    }
}