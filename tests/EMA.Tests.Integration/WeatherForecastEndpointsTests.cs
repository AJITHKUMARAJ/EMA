using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using EMA.Models.Entities;

namespace EMA.Tests.Integration;

public class WeatherForecastEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public WeatherForecastEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/WeatherForecast");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var data = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
        data.Should().NotBeNull();
        data!.Count.Should().Be(5);
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsValidData()
    {
        var response = await _client.GetAsync("/api/WeatherForecast");
        var data = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
        
        data.Should().AllSatisfy(forecast =>
        {
            forecast.Date.Should().BeAfter(DateOnly.FromDateTime(DateTime.Now));
            forecast.TemperatureC.Should().BeInRange(-20, 55);
            forecast.Summary.Should().NotBeNullOrEmpty();
        });
    }
}
