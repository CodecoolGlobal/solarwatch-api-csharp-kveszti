using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch.Controller;
using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatchTESTS;

public class SolarControllerTEST
{
        private Mock<ILogger<SunriseSunsetController>> _mockLogger;
        private Mock<JsonProcessorForSunriseSunset> _mockJsonProcessorForSunrise;
        private Mock<JsonProcessorForGeocoding> _mockJsonProcessorForGeocoding;
        private Mock<SolarWatch.Services.Geocoding> _mockGeocoding;
        private Mock<SunriseSunsetApi> _mockSunriseSunsetApi;

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GetSunrise_WithValidInput_ReturnsOkResult()
        {
        }
}
