using AutoMapper;
using TodoList.WebService.Dtos;

namespace TodoList.WebService.AutoMapperProfiles;

public class ServiceMappings: Profile
{
    public ServiceMappings()
    {
        CreateMap<Models.TodoList, TodoListSelectDto>()
            .ForMember(x => x.InsertEmployeeName, y => y.MapFrom(e => e.InsertEmployee.EmployeeId))
            .ForMember(x => x.UpdateEmployeeName, y => y.MapFrom(e => e.UpdateEmployee.EmployeeId))
            .ReverseMap();
    }
}