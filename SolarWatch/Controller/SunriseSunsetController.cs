using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatch.Controller;

[ApiController]
[Route("[controller]")]
public class SunriseSunsetController : ControllerBase
{
    private readonly ILogger<SunriseSunsetController> _logger;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly IGeocoding _geocoding;
    private readonly ISolarApi _sunriseSunsetApi;

    public SunriseSunsetController(IJsonProcessor jsonProcessorForSunrise, ILogger<SunriseSunsetController> logger, IGeocoding geocoding, ISolarApi sunriseSunsetApi)
    {
        _jsonProcessor = jsonProcessorForSunrise;
        _logger = logger;
        _geocoding = geocoding;
        _sunriseSunsetApi = sunriseSunsetApi;
    }

    [HttpGet("GetSunrise")]
    public ActionResult<DateTime> GetSunrise([Required]string city, [Required]string timeZone, DateTime? date = null)
    {
        try
        {
            DateTime? sunriseDate = date.HasValue ? date.Value : null; 
            timeZone = Uri.UnescapeDataString(timeZone);
            Coordinate coordinateForCity =
                _jsonProcessor.ConvertDataToCoordinate(_geocoding.GetGeocodeForCity(city));

            var sunriseSunsetData = _sunriseSunsetApi.GetSunriseAndSunset(coordinateForCity, timeZone, sunriseDate);
            return Ok(_jsonProcessor.GetSunrise(sunriseSunsetData));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise data");
            return NotFound("Error getting sunrise data");
        }
    }
    
    [HttpGet("GetSunset")]
    public ActionResult<string> GetSunset([Required]string city, [Required]string timeZone, DateTime? date = null)
    {
        try
        {
            timeZone = Uri.UnescapeDataString(timeZone);
            Coordinate coordinateForCity =
                _jsonProcessor.ConvertDataToCoordinate(_geocoding.GetGeocodeForCity(city));

            var sunriseSunsetData = _sunriseSunsetApi.GetSunriseAndSunset(coordinateForCity, timeZone, date.Value);
            return Ok(_jsonProcessor.GetSunset(sunriseSunsetData));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise data");
            return NotFound("Error getting sunrise data");
        }
    }
}