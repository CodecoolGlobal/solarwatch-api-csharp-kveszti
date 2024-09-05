using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controller;
[ApiController]
[Route("/api/[controller]")]
public class SolarDataController : ControllerBase
{
    private readonly ISolarDataRepository _solarDataRepository;
    private readonly ILogger<SolarDataController> _logger;

    public SolarDataController(ISolarDataRepository solarDataRepository, ILogger<SolarDataController> logger)
    {
        _solarDataRepository = solarDataRepository;
        _logger = logger;
    }
    
    [HttpGet("GetAllSolarData"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<City>>> GetAllSolarData()
    {
        try
        {
            var allData = await _solarDataRepository.GetAllSolarData();
            return Ok(allData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all solar data");
            return NotFound();
        }
    }
    
    [HttpPut("UpdateSolarData"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<SolarData>> UpdateSolarData([Required]int id, [Required]int  cityId, [Required]string timeZone, [Required]DateTime sunrise, [Required]DateTime sunset)
    {
        try
        {
            var newData = new SolarData(sunrise, sunset, cityId, timeZone);
            newData.Id = id;

            await _solarDataRepository.Update(newData);

            return Ok(newData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating Solar Data");
            return NotFound("Error updating Solar Data"); 
        }
    }
    
    [HttpDelete("DeleteSolarData"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> DeleteSolarData(int id)
    {
        try
        {
            await _solarDataRepository.Delete(id);
            return Ok(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting Solar Data");
            return NotFound("Error deleting Solar Data"); 
        }
    }
    
    [HttpPost("PostSolarDataToDb"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<SolarData>> PostSolarData([Required]int  cityId, [Required]string timeZone, [Required]DateTime sunrise, [Required]DateTime sunset)
    {
        try
        {
            var newData = new SolarData(sunrise, sunset, cityId, timeZone);

            _solarDataRepository.Add(newData);

            return Ok(newData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding Solar Data");
            return NotFound("Error adding Solar Data"); 
        }
    }
}