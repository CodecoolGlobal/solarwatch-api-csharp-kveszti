using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SolarWatch.Controller;
using SolarWatch.Model;
using SolarWatch.Services;
using SolarWatch.Services.Repositories;

namespace SolarWatchTESTS;

public class SolarControllerTEST
{
        private Mock<ILogger<SunriseSunsetController>> _mockLogger;
        private Mock<IJsonProcessor> _mockJsonProcessor;
        private Mock<IGeocoding> _mockGeocoding;
        private Mock<ISolarApi> _mockSunriseSunsetApi;
        private SunriseSunsetController _controller;
        private Mock<ICityRepository> _mockCityRepository; 
        private Mock<ISolarDataRepository> _mockSolarDataRepository; 

        [SetUp]
        public void Setup()
        {
                _mockLogger = new Mock<ILogger<SunriseSunsetController>>();
                _mockJsonProcessor = new Mock<IJsonProcessor>();
                _mockGeocoding = new Mock<IGeocoding>();
                _mockSunriseSunsetApi = new Mock<ISolarApi>();
                _mockCityRepository = new Mock<ICityRepository>();
                _mockSolarDataRepository = new Mock<ISolarDataRepository>();
                _controller = new SunriseSunsetController(_mockJsonProcessor.Object, _mockLogger.Object, _mockGeocoding.Object, _mockSunriseSunsetApi.Object, _mockCityRepository.Object, _mockSolarDataRepository.Object);
        }


        [Test]
        public async Task GetSunrise_WithValidInput_NotPresentInDB_ReturnsOkResult()
        {
                var expectedResult = new DateTime(2024,06,05, 04,46,17);
                var solarData = "{\n\"results\": {\n\"sunrise\": \"2024-06-05T04:46:17+02:00\",\n\"sunset\": \"2024-06-05T20:38:36+02:00\",\n\"solar_noon\": \"2024-06-05T12:42:26+02:00\",\n\"day_length\": 57139,\n\"civil_twilight_begin\": \"2024-06-05T04:08:29+02:00\",\n\"civil_twilight_end\": \"2024-06-05T21:16:23+02:00\",\n\"nautical_twilight_begin\": \"2024-06-05T03:14:24+02:00\",\n\"nautical_twilight_end\": \"2024-06-05T22:10:29+02:00\",\n\"astronomical_twilight_begin\": \"2024-06-05T01:55:02+02:00\",\n\"astronomical_twilight_end\": \"2024-06-05T23:29:50+02:00\"\n},\n\"status\": \"OK\",\n\"tzid\": \"Europe/Budapest\"\n}";
                _mockSunriseSunsetApi.Setup(x => x.GetSunriseAndSunset(It.IsAny<Coordinate>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                        .ReturnsAsync(solarData);
                _mockJsonProcessor.Setup(x => x.GetSunrise(solarData)).Returns(expectedResult);
                _mockJsonProcessor.Setup(x => x.GetSunset(solarData)).Returns(new DateTime(2024,06,05,20,38,36));
                _mockJsonProcessor.Setup(x => x.ConvertDataToCity(It.IsAny<string>())).Returns(new City("Budapest", 47.497912,19.040235,"Hungary", null));
                _mockCityRepository.Setup(x => x.GetByName(It.IsAny<string>())).Returns((City)null);
                _mockSolarDataRepository.Setup(x => x.GetSolarData(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<string>())).Returns((SolarData)null);
                _mockSolarDataRepository.Setup(x => x.Add(It.IsAny<SolarData>()));
               _mockCityRepository.Setup(x => x.Add(It.IsAny<City>()));

                var result = await _controller.GetSunrise("Budapest", "Europe/Budapest", new DateTime(2024,06,05));
                
                Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
                Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task GetSunrise_WithInvalidInput_Returns_NotFoundObject()
        {
                var solarData = "{}";
                _mockSunriseSunsetApi.Setup(x => x.GetSunriseAndSunset(It.IsAny<Coordinate>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                        .ReturnsAsync(solarData);
                _mockJsonProcessor.Setup(x => x.GetSunrise(solarData)).Throws<Exception>();

                var result = await _controller.GetSunrise("Tamtaramm", "Salalalala", DateTime.Today);
                
                Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);


        }
        
}
