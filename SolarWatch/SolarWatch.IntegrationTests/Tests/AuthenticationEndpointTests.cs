using System.Net.Http.Json;
using SolarWatch.IntegrationTests.Factories;
using Xunit;

namespace SolarWatch.IntegrationTests.Tests;

[Collection("IntegrationTests/Auth")]
public class AuthenticationEndpointTests
{
    private readonly SolarWatchWebApplicationFactory _app;
    private readonly HttpClient _client;

    public AuthenticationEndpointTests()
    {
        _app = new SolarWatchWebApplicationFactory();
        _client = _app.CreateClient();
    }
    
    [Fact]
    public async Task TestRegisterAndLoginEndpoint()
    {
        var registrationRequest = new { Email = "test@test.com", Username = "testUser",
            Password = "testPassword" };
        
        var registerResponse = await _client.PostAsJsonAsync("/api/register", registrationRequest);
        
        Assert.Equal(System.Net.HttpStatusCode.Created, registerResponse.StatusCode);
       
        var loginRequest = new
        {
            Email = "test@test.com",
            Password = "testPassword"
        };
        
        var response = await _client.PostAsJsonAsync("/api/login", loginRequest);
        
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        
        var responseData = await response.Content.ReadAsAsync<dynamic>();
        Assert.NotNull(responseData);
        
        string token = responseData?.token;
        Assert.NotNull(token);
        
        Assert.True(IsValidJwt(token), "The token should be a valid JWT.");
    }

    private bool IsValidJwt(string token)
    {
        var parts = token.Split('.');
        return parts.Length == 3;
    }
}
