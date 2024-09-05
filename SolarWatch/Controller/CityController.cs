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
    public async Task<ActionResult<City>> UpdateCity([Required]int id,  [Required]string country, [Required]string name, string? state, [Required]double latitude,[Required]double longitude)
    {
        try
        {
            var newData = new City(name, latitude, longitude, country, state)
            {
                Id = id
            };

            await _cityRepository.Update(newData);

            return Ok(newData);
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

            _cityRepository.Add(newData);

            return Ok(newData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding City");
            return NotFound("Error adding City"); 
        }
    }
}