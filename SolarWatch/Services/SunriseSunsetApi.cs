using System.Net;
using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class SunriseSunsetApi : ISolarApi
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
        

        try
        {
            _logger.LogInformation("Calling Sunrise-Sunset API with url: {url}", url);
            var response = client.DownloadString(url);
    
            using var document = JsonDocument.Parse(response);
            var root = document.RootElement;
            
            if (root.EnumerateObject().Any())
            {
                _logger.LogError("Solar API: No valid data found for coordinates '{coordinate.Latitude}, {coordinate.Longitude}', and timezone: {timeZone}" );
                throw new ArgumentException("Invalid input data. Please check coordinates and timezone."); 
            }

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred during Sunrise-Sunset API call.");
            throw;
        }
        
     
    }
    

    private string generateURL(Coordinate coordinate, string timeZone)
    {
        return
            $"https://api.sunrise-sunset.org/json?lat={coordinate.Latitude}&lng={coordinate.Longitude}&date=today&tzid={timeZone}&formatted=0";
    }
}