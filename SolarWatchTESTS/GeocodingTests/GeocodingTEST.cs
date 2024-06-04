using Microsoft.Extensions.Logging;
using Moq;

namespace SolarWatchTESTS.Geocoding;

public class GeocodingTEST
{
    [Test]
    public void GetGeocodeForCity_WithValidCity_ReturnsJsonResponse()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<SolarWatch.Services.Geocoding>>();
        var geocodingService = new SolarWatch.Services.Geocoding(mockLogger.Object);

        // Act
        var result = geocodingService.GetGeocodeForCity("Budapest");

        // Assert
        Assert.NotNull(result);        
        Assert.That(result, Is.Not.EqualTo(string.Empty)); 
        Assert.True(result.StartsWith("["), "Expected JSON response to start with '['");
    }
    
    [Test]
    public void GetGeocodeForCity_WithInvalidCity_ThrowsArgumentException()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<SolarWatch.Services.Geocoding>>();
        var geocodingService = new SolarWatch.Services.Geocoding(mockLogger.Object);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => geocodingService.GetGeocodeForCity("InvalidCity"));
    }
}