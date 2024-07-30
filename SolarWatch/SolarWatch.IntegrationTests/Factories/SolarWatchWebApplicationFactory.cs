using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SolarWatch.Context;
using SolarWatch.Model;
using Xunit;

namespace SolarWatch.IntegrationTests.Factories;

public class SolarWatchWebApplicationFactory : WebApplicationFactory<Program>
{
     //Create a new db name for each SolarWatchWebApplicationFactory. This is to prevent tests failing from changes done in db by a previous test. 
     private readonly string _solarApiDbName = Guid.NewGuid().ToString();
     private readonly string _usersDbName = Guid.NewGuid().ToString();


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            //Get the previous DbContextOptions registrations 
            var solarApiDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SolarApiContext>));
            var usersDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UsersContext>));
            
            //Remove the previous DbContextOptions registrations
            services.Remove(solarApiDbContextDescriptor);
            services.Remove(usersDbContextDescriptor);
            
            //Add new DbContextOptions for our two contexts, this time with inmemory db 
            services.AddDbContext<SolarApiContext>(options =>
            {
                options.UseInMemoryDatabase(_solarApiDbName);
            });
            
            services.AddDbContext<UsersContext>(options =>
            {
                options.UseInMemoryDatabase(_usersDbName);
            });
            
            //We will need to initialize our in memory databases. 
            using var scope = services.BuildServiceProvider().CreateScope();
            
            //We use this scope to request the registered dbcontexts, and initialize the schemas
            var solarContext = scope.ServiceProvider.GetRequiredService<SolarApiContext>();
            solarContext.Database.EnsureDeleted();
            solarContext.Database.EnsureCreated();

            var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
            userContext.Database.EnsureDeleted();
            userContext.Database.EnsureCreated();
            
        });
    }
    
    [Collection("IntegrationTests")] 
    public class MyControllerIntegrationTest
    {
        private readonly SolarWatchWebApplicationFactory _app;
        private readonly HttpClient _client;
    
        public MyControllerIntegrationTest()
        {
            _app = new SolarWatchWebApplicationFactory();
            _client = _app.CreateClient();
            var token = JwtTestTokenGenerator.CreateTestToken("testUserId", "testUser", "test@example.com", "Admin");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task TestEndPoint()
        {
            var response = await _client.GetAsync("/api/SunriseSunset/GetSunrise?city=Budapest&timeZone=Europe/Budapest&date=2024.07.30.");

            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<string>();
            
            Assert.Equal("2024-07-30T05:19:56+02:00", data);
        }
    }
}