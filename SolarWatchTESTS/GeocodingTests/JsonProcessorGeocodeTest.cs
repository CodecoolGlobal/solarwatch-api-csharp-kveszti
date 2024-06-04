using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatchTESTS.Geocoding;

public class JsonProcessorGeocodeTest
{
    private static JsonProcessorForGeocoding _jsonProcessor = new();
    [Test]
    public void ConvertDataToCoordinate_WithValidJson_ReturnsCoordinate()
    {
        // Arrange
        string validJson = "[{\"lat\": 47.4979, \"lon\": 19.0402}]"; 
        var expectedCoordinate = new Coordinate(47.4979, 19.0402);

        // Act
        var result = _jsonProcessor.ConvertDataToCoordinate(validJson);

        // Assert
        Assert.That(expectedCoordinate.Latitude == result.Latitude && expectedCoordinate.Longitude == result.Longitude);
    }
}