using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatchTESTS.SolarApi;

public class SolarApiTEST
{
    
    [Test]
    public void GetSunriseAndSunset_WithValidInput_ReturnsData()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<SunriseSunsetApi>>();
        var sunriseSunsetApi = new SunriseSunsetApi(mockLogger.Object);
        var coordinate = new Coordinate(47.4979, 19.0402); // Budapest
        var timeZone = "Europe/Budapest";
        var date = DateTime.Today;

        // Act
        var result = sunriseSunsetApi.GetSunriseAndSunset(coordinate, timeZone, date);

        // Assert
        Assert.NotNull(result);
        Assert.IsTrue(result.Contains("sunrise"));
        Assert.IsTrue(result.Contains("sunset"));
    }
    
    [Test]
    public void GetSunriseAndSunset_WithInvalidInput_ReturnsException()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<SunriseSunsetApi>>();
        var sunriseSunsetApi = new SunriseSunsetApi(mockLogger.Object);
        var coordinate = new Coordinate(47.4979, 2341234.0402); 
        var timeZone = "Europe/Budapest";
        

        // Assert
        Assert.Throws<ArgumentException>(() => sunriseSunsetApi.GetSunriseAndSunset(coordinate, timeZone, DateTime.Today));

    }
}