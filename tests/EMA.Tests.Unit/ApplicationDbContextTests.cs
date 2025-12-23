using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using EMA.Data;
using EMA.Models.Entities;

namespace EMA.Tests.Unit;

public class ApplicationDbContextTests
{
    [Fact]
    public void ApplicationDbContext_CanAddAndRetrieveEmployee()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_Context")
            .Options;

        using var context = new ApplicationDbContext(options);
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Context Test",
            Email = "context@test.com",
            Phone = "111",
            Gender = "M",
            Salary = 1000
        };

        context.Employees.Add(employee);
        context.SaveChanges();

        var retrieved = context.Employees.Find(employee.Id);
        retrieved.Should().NotBeNull();
        retrieved!.Name.Should().Be("Context Test");
    }

    [Fact]
    public void ApplicationDbContext_TracksChanges()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_Tracking")
            .Options;

        using var context = new ApplicationDbContext(options);
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Original",
            Email = "original@test.com",
            Phone = "222",
            Gender = "F",
            Salary = 2000
        };

        context.Employees.Add(employee);
        context.SaveChanges();

        employee.Name = "Updated";
        context.SaveChanges();

        var retrieved = context.Employees.Find(employee.Id);
        retrieved!.Name.Should().Be("Updated");
    }
}
