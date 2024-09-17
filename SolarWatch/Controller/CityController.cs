using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controller;

[ApiController]
[Route("/api/[controller]")]
public class CityController: ControllerBase
{
    private readonly ICityRepository _cityRepository;
    private readonly ILogger<CityController> _logger;

    public CityController(ILogger<CityController> logger, ICityRepository cityRepository)
    {
        _logger = logger;
        _cityRepository = cityRepository;
    }

    [HttpGet("GetByName"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<City>> GetByName([FromQuery] string name)
    {
        try
        {
           var city = await _cityRepository.GetByName(name);
           return Ok(city);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting city by name");
            return NotFound();
        }
    }

    [HttpGet("GetById"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<City>> GetById([FromQuery]int id)
    {
        try
        {
            var city = await _cityRepository.GetById(id);
            return Ok(city);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting city from db");
            return NotFound();
        }
    }

    [HttpGet("GetAllCities"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<City>>> GetAllCities()
    {
        try
        {
            var allCities = await _cityRepository.GetAllCities();
            return Ok(allCities);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all cities");
            return NotFound();
        }
    }
    
    [HttpPut("UpdateCity"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<City>> UpdateCity([FromBody, Required] City newCity)
    {
        try
        {
            await _cityRepository.Update(newCity);
        
            return Ok(newCity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating City");
            return NotFound("Error updating City"); 
        }
       
    }
    
    [HttpDelete("DeleteCity"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> DeleteCity([FromQuery]int id)
    {
        try
        {
            await _cityRepository.Delete(id);
            return Ok(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting City");
            return NotFound("Error deleting City"); 
        }
    }
    
    [HttpPost("PostCityToDb"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<SolarData>> PostCity([FromQuery, Required]string country, [FromQuery, Required]string name, [FromQuery] string? state, [FromQuery, Required]double latitude,[FromQuery, Required]double longitude)
    {
        try
        {
            var newData = new City(name, latitude, longitude, country, state);

            await _cityRepository.Add(newData);

            return Ok(newData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding City");
            return NotFound("Error adding City"); 
        }
    }
}