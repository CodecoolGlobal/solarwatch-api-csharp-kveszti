using System;
using Microsoft.Extensions.Logging;
using Moq;
using static System.String;

namespace SolarWatchTESTS.Geocoding;

public class GeocodingTEST
{
    [Test]
    public async Task GetGeocodeForCity_WithValidCity_ReturnsJsonResponse()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<SolarWatch.Services.Geocoding>>();
        var geocodingService = new SolarWatch.Services.Geocoding(mockLogger.Object);

        // Act
        var result = await geocodingService.GetGeocodeForCity("Budapest");

        // Assert
        Assert.NotNull(result);        
        Assert.That(result, Is.Not.EqualTo(string.Empty)); 
        Assert.True(result.StartsWith("["), "Expected JSON response to start with '['");
    }
    
    [Test]
    public async Task GetGeocodeForCity_WithInvalidCity_ThrowsArgumentException()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<SolarWatch.Services.Geocoding>>();
        var geocodingService = new SolarWatch.Services.Geocoding(mockLogger.Object);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => geocodingService.GetGeocodeForCity("InvalidCity"));
    }
}