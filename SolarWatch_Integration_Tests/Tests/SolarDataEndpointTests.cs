
using System.Net.Http;
using System.Threading.Tasks;
using SolarWatch.IntegrationTests.Factories;
using Xunit;


namespace SolarWatch.IntegrationTests.Tests;

[Collection("IntegrationTests/Solar")]
public class SolarDataEndpointTests
{
        private readonly SolarWatchWebApplicationFactory _app;
        private readonly HttpClient _client;

        public SolarDataEndpointTests()
        {
            _app = new SolarWatchWebApplicationFactory();
            _client = _app.CreateClient();
        }

        [Fact]
        public async Task TestAdminEndpointWithoutRole()
        {    
            var userToken = JwtTestTokenGenerator.CreateTestToken("testUserId", "testUser", "test@example.com", "User");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);
            
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/SunriseSunset/DeleteCity?id=54");
            var response = await _client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
            
        }
        
        [Fact]
        public async Task TestAdminEndpointWithRole()
        {    
            var userToken = JwtTestTokenGenerator.CreateTestToken("testAdminId", "testAdmin", "testAdmin@example.com", "Admin");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);
            
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/SunriseSunset/PostCityToDb?country=Hungary&name=Budapest&latitude=47.4979937&longitude=19.0403594");
            
            var response = await _client.SendAsync(request);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            
            var city =  _app.CityRepository.GetByName("Budapest");
            Assert.NotNull(city);
            Assert.Equal("Budapest", city.Name);
            Assert.Equal("Hungary", city.Country);
            Assert.Equal(47.4979937, city.Latitude);
            Assert.Equal(19.0403594, city.Longitude);
            
        }
}