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
    private readonly JsonProcessorForGeocoding _jsonProcessorForGeocoding;
    private readonly JsonProcessorForSunriseSunset _jsonProcessorForSunrise;
    private readonly Geocoding _geocoding;
    private readonly SunriseSunsetApi _sunriseSunsetApi;

    public SunriseSunsetController(JsonProcessorForSunriseSunset jsonProcessorForSunrise, JsonProcessorForGeocoding jsonProcessorForGeocoding, ILogger<SunriseSunsetController> logger, Geocoding geocoding, SunriseSunsetApi sunriseSunsetApi)
    {
        _jsonProcessorForSunrise = jsonProcessorForSunrise;
        _jsonProcessorForGeocoding = jsonProcessorForGeocoding;
        _logger = logger;
        _geocoding = geocoding;
        _sunriseSunsetApi = sunriseSunsetApi;
    }

    [HttpGet("GetSunrise")]
    public ActionResult<string> GetSunrise([Required]string city, [Required]string timeZone)
    {
        try
        {
            timeZone = Uri.UnescapeDataString(timeZone);
            Coordinate coordinateForCity =
                _jsonProcessorForGeocoding.ConvertDataToCoordinate(_geocoding.GetGeocodeForCity(city));

            var SunriseSunsetData = _sunriseSunsetApi.GetSunriseAndSunset(coordinateForCity, timeZone);
            return Ok(_jsonProcessorForSunrise.GetSunrise(SunriseSunsetData));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise data");
            return NotFound("Error getting sunrise data");
        }
    }
    
    [HttpGet("GetSunset")]
    public ActionResult<string> GetSunset([Required]string city, [Required]string timeZone)
    {
        try
        {
            timeZone = Uri.UnescapeDataString(timeZone);
            Coordinate coordinateForCity =
                _jsonProcessorForGeocoding.ConvertDataToCoordinate(_geocoding.GetGeocodeForCity(city));

            var SunriseSunsetData = _sunriseSunsetApi.GetSunriseAndSunset(coordinateForCity, timeZone);
            return Ok(_jsonProcessorForSunrise.GetSunset(SunriseSunsetData));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise data");
            return NotFound("Error getting sunrise data");
        }
    }
}