using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        var cityFromDb = _cityRepository.GetByName(city);

        if (cityFromDb != null)
        {
            var solarData = _solarDataRepository.GetSolarData(cityFromDb.Id, date, timeZone);

            if (solarData != null)
            {
                return Ok(solarData.Sunrise);
            }
        }
        try
        {
            DateTime? sunriseDate = date.HasValue ? date.Value : null;
            timeZone = Uri.UnescapeDataString(timeZone);
            var geocodingResponse = await _geocoding.GetGeocodeForCity(city);
            Coordinate coordinateForCity =
                _jsonProcessor.ConvertDataToCoordinate(geocodingResponse);
            var cityToAdd = _jsonProcessor.ConvertDataToCity(geocodingResponse);

            if (cityFromDb == null)
            {
                _cityRepository.Add(cityToAdd);
            }
            
            var sunriseSunsetData = await _sunriseSunsetApi.GetSunriseAndSunset(coordinateForCity, timeZone, sunriseDate);

            var sunrise = _jsonProcessor.GetSunrise(sunriseSunsetData);
            var sunset = _jsonProcessor.GetSunset(sunriseSunsetData);
            
           _solarDataRepository.Add(new SolarData(sunrise, sunset, cityToAdd.Id, timeZone)); 
            
            return Ok(sunrise);
        }
        catch (ArgumentException ae) //lehet saját ex típust is specifikálni
        {
            return BadRequest(ae.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise data");
            return NotFound("Error getting sunrise data");
        }
    }
    
    [HttpGet("GetSunset"),Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<string>> GetSunset([FromQuery, Required]string city, [FromQuery, Required]string timeZone, [FromQuery]DateTime? date = null)
    {
        var cityFromDb = _cityRepository.GetByName(city);

        if (cityFromDb != null)
        {
            var solarData = _solarDataRepository.GetSolarData(cityFromDb.Id, date, timeZone);

            if (solarData != null)
            {
                return Ok(solarData.Sunset);
            }
        }
        try
        {
            timeZone = Uri.UnescapeDataString(timeZone);
            DateTime? sunsetDate = date.HasValue ? date.Value : null;
            var geocodingResponse = await _geocoding.GetGeocodeForCity(city);
            Coordinate coordinateForCity =
                _jsonProcessor.ConvertDataToCoordinate(geocodingResponse);
            
            var cityToAdd = _jsonProcessor.ConvertDataToCity(geocodingResponse);
            
            _cityRepository.Add(cityToAdd);

            var sunriseSunsetData = await _sunriseSunsetApi.GetSunriseAndSunset(coordinateForCity, timeZone, sunsetDate);
            
            var sunrise = _jsonProcessor.GetSunrise(sunriseSunsetData);
            var sunset = _jsonProcessor.GetSunset(sunriseSunsetData);
            
            _solarDataRepository.Add(new SolarData(sunrise, sunset, cityToAdd.Id, timeZone));
            
            return Ok(sunset);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunset data");
            return NotFound("Error getting sunset data");
        }
    }
}