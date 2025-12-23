using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using EMA.Models;
using EMA.Models.Entities;

namespace EMA.Tests.Integration;

public class EmployeeEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EmployeeEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllEmployees_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/Employees/GetAllEmployees");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var data = await response.Content.ReadFromJsonAsync<List<Employee>>();
        data.Should().NotBeNull();
        data!.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task AddUpdateDelete_Employee_Works()
    {
        var dto = new EmployeeDto { Name = "C", Email = "c@c.com", Phone = "3", Gender = "M", Salary = 300 };

        var createResp = await _client.PostAsJsonAsync("/api/Employees/AddEmployee", dto);
        createResp.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await createResp.Content.ReadFromJsonAsync<Employee>();
        created.Should().NotBeNull();

        var id = created!.Id;
        var getResp = await _client.GetAsync($"/api/Employees/GetEmployeeById/{id}");
        getResp.StatusCode.Should().Be(HttpStatusCode.OK);

        var updateDto = new EmployeeDto { Name = "C2", Email = "c2@c.com", Phone = "33", Gender = "F", Salary = 330 };
        var updateResp = await _client.PutAsJsonAsync($"/api/Employees/UpdateEmployee/{id}", updateDto);
        updateResp.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var deleteResp = await _client.DeleteAsync($"/api/Employees/DeleteEmployee/{id}");
        deleteResp.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetEmployeeById_ReturnsNotFound_WhenMissing()
    {
        var response = await _client.GetAsync($"/api/Employees/GetEmployeeById/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateEmployee_ReturnsNotFound_WhenMissing()
    {
        var dto = new EmployeeDto { Name = "X", Email = "x@x.com", Phone = "999", Gender = "M", Salary = 999 };
        var response = await _client.PutAsJsonAsync($"/api/Employees/UpdateEmployee/{Guid.NewGuid()}", dto);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteEmployee_ReturnsNotFound_WhenMissing()
    {
        var response = await _client.DeleteAsync($"/api/Employees/DeleteEmployee/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
