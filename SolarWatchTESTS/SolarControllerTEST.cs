// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Moq;
// using SolarWatch.Controller;
// using SolarWatch.Model;
// using SolarWatch.Services;
//
// namespace SolarWatchTESTS;
//
// public class SolarControllerTEST
// {
//         private Mock<ILogger<SunriseSunsetController>> _mockLogger;
//         private Mock<IJsonProcessor> _mockJsonProcessor;
//         private Mock<IGeocoding> _mockGeocoding;
//         private Mock<ISolarApi> _mockSunriseSunsetApi;
//         private SunriseSunsetController _controller;
//
//         [SetUp]
//         public void Setup()
//         {
//                 _mockLogger = new Mock<ILogger<SunriseSunsetController>>();
//                 _mockJsonProcessor = new Mock<IJsonProcessor>();
//                 _mockGeocoding = new Mock<IGeocoding>();
//                 _mockSunriseSunsetApi = new Mock<ISolarApi>();
//                 _controller = new SunriseSunsetController(_mockJsonProcessor.Object, _mockLogger.Object, _mockGeocoding.Object, _mockSunriseSunsetApi.Object);
//         }
//
//
//         [Test]
//         public async Task GetSunrise_WithValidInput_ReturnsOkResult()
//         {
//                 var expectedResult = new DateTime(2024,06,05, 04,46,17);
//                 var solarData = "{\n\"results\": {\n\"sunrise\": \"2024-06-05T04:46:17+02:00\",\n\"sunset\": \"2024-06-05T20:38:36+02:00\",\n\"solar_noon\": \"2024-06-05T12:42:26+02:00\",\n\"day_length\": 57139,\n\"civil_twilight_begin\": \"2024-06-05T04:08:29+02:00\",\n\"civil_twilight_end\": \"2024-06-05T21:16:23+02:00\",\n\"nautical_twilight_begin\": \"2024-06-05T03:14:24+02:00\",\n\"nautical_twilight_end\": \"2024-06-05T22:10:29+02:00\",\n\"astronomical_twilight_begin\": \"2024-06-05T01:55:02+02:00\",\n\"astronomical_twilight_end\": \"2024-06-05T23:29:50+02:00\"\n},\n\"status\": \"OK\",\n\"tzid\": \"Europe/Budapest\"\n}";
//                 _mockSunriseSunsetApi.Setup(x => x.GetSunriseAndSunset(It.IsAny<Coordinate>(), It.IsAny<string>(), It.IsAny<DateTime>()))
//                         .ReturnsAsync(solarData);
//                 _mockJsonProcessor.Setup(x => x.GetSunrise(solarData)).Returns(expectedResult);
//
//                 var result = await _controller.GetSunrise("Budapest", "Europe/Budapest", DateTime.Today);
//                 
//                 Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
//                 Assert.That(((OkObjectResult)result.Result).Value, Is.EqualTo(expectedResult));
//         }
//
//         [Test]
//         public async Task GetSunrise_WithInvalidInput_Returns_NotFoundObject()
//         {
//                 var solarData = "{}";
//                 _mockSunriseSunsetApi.Setup(x => x.GetSunriseAndSunset(It.IsAny<Coordinate>(), It.IsAny<string>(), It.IsAny<DateTime>()))
//                         .ReturnsAsync(solarData);
//                 _mockJsonProcessor.Setup(x => x.GetSunrise(solarData)).Throws<Exception>();
//
//                 var result = await _controller.GetSunrise("Tamtaramm", "Salalalala", DateTime.Today);
//                 
//                 Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
//
//
//         }
//         
// }
