
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;
using SolarWatch.Services;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controller;

[ApiController]
[Route("/api/[controller]")]
public class SunriseSunsetController : ControllerBase
{
    private readonly ILogger<SunriseSunsetController> _logger;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly IGeocoding _geocoding;
    private readonly ISolarApi _sunriseSunsetApi;
    private readonly ICityRepository _cityRepository;
    private readonly ISolarDataRepository _solarDataRepository;

    public SunriseSunsetController(IJsonProcessor jsonProcessorForSunrise, ILogger<SunriseSunsetController> logger, IGeocoding geocoding, ISolarApi sunriseSunsetApi, ICityRepository cityRepository, ISolarDataRepository solarDataRepository)
    {
        _jsonProcessor = jsonProcessorForSunrise;
        _logger = logger;
        _geocoding = geocoding;
        _sunriseSunsetApi = sunriseSunsetApi;
        _cityRepository = cityRepository;
        _solarDataRepository = solarDataRepository;
    }

    [HttpGet("GetSunrise"), Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<DateTime>> GetSunrise([FromQuery, Required]string city, [FromQuery, Required]string timeZone, [FromQuery] DateTime? date = null)
    {
        var actionResult = await getSunriseSunsetData("sunrise", city, timeZone, date);
        return actionResult;
    }
    
    [HttpGet("GetSunset"),Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<DateTime>> GetSunset([FromQuery, Required]string city, [FromQuery, Required]string timeZone, [FromQuery]DateTime? date = null)
    {
       var actionResult = await getSunriseSunsetData("sunset", city, timeZone, date);
       return actionResult;
    }

    private async Task<ActionResult<DateTime>> getSunriseSunsetData(string type, string city, string timeZone, DateTime? date)
    {
        var cityFromDb = _cityRepository.GetByName(city);

        if (cityFromDb != null)
        {
            var solarData = _solarDataRepository.GetSolarData(cityFromDb.Id, date, timeZone);
            Console.WriteLine(solarData);

            if (solarData != null)
            {
                return Ok(type == "sunrise" ? solarData.Sunrise : solarData.Sunset);
            }
        }
        try
        {
            DateTime? sunriseOrSunsetDate = date;
            timeZone = Uri.UnescapeDataString(timeZone);
            var geocodingResponse = await _geocoding.GetGeocodeForCity(city);
            Coordinate coordinateForCity =
                _jsonProcessor.ConvertDataToCoordinate(geocodingResponse);
            var cityToAdd = _jsonProcessor.ConvertDataToCity(geocodingResponse);
            int cityIdToAdd;

            if (cityFromDb == null)
            {
                _cityRepository.Add(cityToAdd);
                cityIdToAdd = cityToAdd.Id;
            }
            else
            {
                cityIdToAdd = cityFromDb.Id;
            }
            
            var sunriseSunsetData = await _sunriseSunsetApi.GetSunriseAndSunset(coordinateForCity, timeZone, sunriseOrSunsetDate);

            var sunrise = _jsonProcessor.GetSunrise(sunriseSunsetData);
            var sunset = _jsonProcessor.GetSunset(sunriseSunsetData);
            
            _solarDataRepository.Add(new SolarData(sunrise, sunset, cityIdToAdd, timeZone)); 
            
            
            return Ok(type == "sunrise" ? sunrise : sunset);
        }
        catch (ArgumentException ae) 
        {
            return BadRequest(ae.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, type == "sunrise" ? "Error getting sunrise data" : "Error getting sunset data");
            return NotFound(type == "sunrise" ? "Error getting sunrise data" : "Error getting sunset data");
        }
    }
}