using AutoMapper;
using FluentAssertions;
using EMA.Models;
using EMA.Models.Entities;
using EMA.Profiles;

namespace EMA.Tests.Unit;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void MappingProfile_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void Map_EmployeeDtoToEmployee_MapsAllProperties()
    {
        var dto = new EmployeeDto
        {
            Name = "John Doe",
            Email = "john@example.com",
            Phone = "123-456-7890",
            Gender = "M",
            Salary = 50000
        };

        var employee = _mapper.Map<Employee>(dto);

        employee.Name.Should().Be(dto.Name);
        employee.Email.Should().Be(dto.Email);
        employee.Phone.Should().Be(dto.Phone);
        employee.Gender.Should().Be(dto.Gender);
        employee.Salary.Should().Be(dto.Salary);
        employee.Id.Should().Be(Guid.Empty); // Should be ignored
    }

    [Fact]
    public void Map_EmployeeToEmployeeDto_MapsAllProperties()
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Jane Doe",
            Email = "jane@example.com",
            Phone = "098-765-4321",
            Gender = "F",
            Salary = 60000
        };

        var dto = _mapper.Map<EmployeeDto>(employee);

        dto.Name.Should().Be(employee.Name);
        dto.Email.Should().Be(employee.Email);
        dto.Phone.Should().Be(employee.Phone);
        dto.Gender.Should().Be(employee.Gender);
        dto.Salary.Should().Be(employee.Salary);
    }
}
