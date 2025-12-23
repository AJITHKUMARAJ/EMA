using FluentAssertions;
using EMA.Models;
using EMA.Models.Entities;

namespace EMA.Tests.Unit;

public class EntityAndDtoTests
{
    [Fact]
    public void Employee_Properties_CanBeSet()
    {
        var id = Guid.NewGuid();
        var employee = new Employee
        {
            Id = id,
            Name = "Test User",
            Email = "test@example.com",
            Phone = "555-1234",
            Gender = "M",
            Salary = 75000
        };

        employee.Id.Should().Be(id);
        employee.Name.Should().Be("Test User");
        employee.Email.Should().Be("test@example.com");
        employee.Phone.Should().Be("555-1234");
        employee.Gender.Should().Be("M");
        employee.Salary.Should().Be(75000);
    }

    [Fact]
    public void EmployeeDto_Properties_CanBeSet()
    {
        var dto = new EmployeeDto
        {
            Name = "Test User",
            Email = "test@example.com",
            Phone = "555-1234",
            Gender = "F",
            Salary = 85000
        };

        dto.Name.Should().Be("Test User");
        dto.Email.Should().Be("test@example.com");
        dto.Phone.Should().Be("555-1234");
        dto.Gender.Should().Be("F");
        dto.Salary.Should().Be(85000);
    }

    [Fact]
    public void WeatherForecast_Properties_CanBeSet()
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var forecast = new WeatherForecast(date, 25, "Sunny");

        forecast.Date.Should().Be(date);
        forecast.TemperatureC.Should().Be(25);
        forecast.Summary.Should().Be("Sunny");
        forecast.TemperatureF.Should().Be(76); // Conversion check
    }

    [Fact]
    public void WeatherForecast_TemperatureF_CalculatesCorrectly()
    {
        var forecast = new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 0, "Cold");
        forecast.TemperatureF.Should().Be(32);

        var forecast2 = new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 100, "Hot");
        forecast2.TemperatureF.Should().Be(32 + (int)(100 / 0.5556)); // 211 based on actual formula
    }
}
