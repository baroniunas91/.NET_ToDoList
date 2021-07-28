using AutoMapper;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodosGetDto>()
                .ForMember(i => i.Id, dto =>
                dto.MapFrom(c => c.Id))
                .ReverseMap();
            CreateMap<TodoDto, Todo>()
                .ForMember(i => i.Id, dto =>
                dto.MapFrom(c => c.Id))
                .ReverseMap();
            CreateMap<UserCred, User>()
                .ForMember(i => i.EmailAddress, dto =>
                dto.MapFrom(c => c.EmailAddress))
                .ReverseMap();
            CreateMap<UserDto, User>()
                .ForMember(i => i.EmailAddress, dto =>
                dto.MapFrom(c => c.EmailAddress))
                .ReverseMap();
        }
    }
}
