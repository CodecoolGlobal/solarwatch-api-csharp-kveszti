using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatchTESTS.SolarApi;

public class SolarApiTEST
{
    
    [Test]
    public async Task GetSunriseAndSunset_WithValidInput_ReturnsData()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<SunriseSunsetApi>>();
        var sunriseSunsetApi = new SunriseSunsetApi(mockLogger.Object);
        var coordinate = new Coordinate(47.4979, 19.0402); // Budapest
        var timeZone = "Europe/Budapest";
        var date = DateTime.Today;

        // Act
        var result = await sunriseSunsetApi.GetSunriseAndSunset(coordinate, timeZone, date);

        // Assert
        Assert.NotNull(result);
        Assert.IsTrue(result.Contains("sunrise"));
        Assert.IsTrue(result.Contains("sunset"));
    }
    
    [Test]
    public async Task GetSunriseAndSunset_WithInvalidInput_ReturnsException()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<SunriseSunsetApi>>();
        var sunriseSunsetApi = new SunriseSunsetApi(mockLogger.Object);
        var coordinate = new Coordinate(47.4979, 2341234.0402); 
        var timeZone = "Europe/Budapest";
        

        // Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await sunriseSunsetApi.GetSunriseAndSunset(coordinate, timeZone, DateTime.Today));

    }
}