using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using EMA.Controllers;
using EMA.Models.Entities;

namespace Sample1.Tests.Unit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsOk_WithFiveForecasts()
    {
        var controller = new WeatherForecastController();

        var result = controller.Get();

        result.Should().NotBeNull();
        result.Should().HaveCount(5);
    }

    [Fact]
    public void Get_ReturnsValidForecasts()
    {
        var controller = new WeatherForecastController();

        var result = controller.Get().ToList();

        result.Should().AllSatisfy(forecast =>
        {
            forecast.Date.Should().BeAfter(DateOnly.FromDateTime(DateTime.Now));
            forecast.TemperatureC.Should().BeInRange(-20, 55);
            forecast.Summary.Should().NotBeNullOrEmpty();
        });
    }
}
