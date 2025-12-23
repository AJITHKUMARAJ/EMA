using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMA.Controllers;
using EMA.Data;
using EMA.Models;
using EMA.Models.Entities;
using EMA.Profiles;

namespace EMA.Tests.Unit;

public class EmployeeControllerTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        return config.CreateMapper();
    }

    private static ApplicationDbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        var ctx = new ApplicationDbContext(options);
        return ctx;
    }

    private static EmployeeController CreateController(ApplicationDbContext ctx)
        => new EmployeeController(ctx, CreateMapper());

    [Fact]
    public async Task GetAllEmployees_ReturnsOk_WithSeededItems()
    {
        using var ctx = CreateDbContext(nameof(GetAllEmployees_ReturnsOk_WithSeededItems));
        ctx.Employees.AddRange(new Employee { Id = Guid.NewGuid(), Name = "A", Email = "a@a.com", Phone = "1", Gender = "M", Salary = 1 },
                               new Employee { Id = Guid.NewGuid(), Name = "B", Email = "b@b.com", Phone = "2", Gender = "F", Salary = 2 });
        await ctx.SaveChangesAsync();

        var controller = CreateController(ctx);
        var result = await controller.GetAllEmployees();

        result.Result.Should().BeOfType<OkObjectResult>();
        var value = (result.Result as OkObjectResult)!.Value as IEnumerable<Employee>;
        value.Should().NotBeNull();
        value!.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetEmployeeById_ReturnsNotFound_WhenMissing()
    {
        using var ctx = CreateDbContext(nameof(GetEmployeeById_ReturnsNotFound_WhenMissing));
        var controller = CreateController(ctx);

        var result = await controller.GetEmployeeById(Guid.NewGuid());
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetEmployeeById_ReturnsOk_WhenFound()
    {
        using var ctx = CreateDbContext(nameof(GetEmployeeById_ReturnsOk_WhenFound));
        var id = Guid.NewGuid();
        ctx.Employees.Add(new Employee { Id = id, Name = "Test", Email = "test@test.com", Phone = "123", Gender = "M", Salary = 100 });
        await ctx.SaveChangesAsync();

        var controller = CreateController(ctx);
        var result = await controller.GetEmployeeById(id);

        result.Result.Should().BeOfType<OkObjectResult>();
        var value = (result.Result as OkObjectResult)!.Value as Employee;
        value!.Id.Should().Be(id);
    }

    [Fact]
    public async Task AddEmployee_CreatesAndReturnsCreatedAt()
    {
        using var ctx = CreateDbContext(nameof(AddEmployee_CreatesAndReturnsCreatedAt));
        var controller = CreateController(ctx);
        var dto = new EmployeeDto { Name = "A", Email = "a@a.com", Phone = "1", Gender = "M", Salary = 100 };

        var result = await controller.AddEmployee(dto);

        result.Result.Should().BeOfType<CreatedAtActionResult>();
        ctx.Employees.Count().Should().Be(1);
    }

    [Fact]
    public async Task UpdateEmployee_ModifiesExisting()
    {
        using var ctx = CreateDbContext(nameof(UpdateEmployee_ModifiesExisting));
        var id = Guid.NewGuid();
        ctx.Employees.Add(new Employee { Id = id, Name = "A", Email = "a@a.com", Phone = "1", Gender = "M", Salary = 1 });
        await ctx.SaveChangesAsync();

        var controller = CreateController(ctx);
        var dto = new EmployeeDto { Name = "A2", Email = "a2@a.com", Phone = "9", Gender = "F", Salary = 9 };

        var result = await controller.UpdateEmployee(id, dto);

        result.Should().BeOfType<NoContentResult>();
        var updated = await ctx.Employees.FindAsync(id);
        updated!.Name.Should().Be("A2");
    }

    [Fact]
    public async Task UpdateEmployee_ReturnsNotFound_WhenMissing()
    {
        using var ctx = CreateDbContext(nameof(UpdateEmployee_ReturnsNotFound_WhenMissing));
        var controller = CreateController(ctx);
        var dto = new EmployeeDto { Name = "X", Email = "x@x.com", Phone = "999", Gender = "M", Salary = 999 };

        var result = await controller.UpdateEmployee(Guid.NewGuid(), dto);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteEmployee_RemovesExisting()
    {
        using var ctx = CreateDbContext(nameof(DeleteEmployee_RemovesExisting));
        var id = Guid.NewGuid();
        ctx.Employees.Add(new Employee { Id = id, Name = "A", Email = "a@a.com", Phone = "1", Gender = "M", Salary = 1 });
        await ctx.SaveChangesAsync();

        var controller = CreateController(ctx);
        var result = await controller.DeleteEmployee(id);

        result.Should().BeOfType<NoContentResult>();
        (await ctx.Employees.FindAsync(id)).Should().BeNull();
    }

    [Fact]
    public async Task DeleteEmployee_ReturnsNotFound_WhenMissing()
    {
        using var ctx = CreateDbContext(nameof(DeleteEmployee_ReturnsNotFound_WhenMissing));
        var controller = CreateController(ctx);

        var result = await controller.DeleteEmployee(Guid.NewGuid());

        result.Should().BeOfType<NotFoundResult>();
    }
}
