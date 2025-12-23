using AutoMapper;
using EMA.Models;
using EMA.Models.Entities;

namespace EMA.Profiles
{
    /// <summary>
    /// AutoMapper profile for mapping between DTOs and entities.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map EmployeeDto to Employee (for create)
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id is generated server-side

            // Map Employee to EmployeeDto (for read)
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
