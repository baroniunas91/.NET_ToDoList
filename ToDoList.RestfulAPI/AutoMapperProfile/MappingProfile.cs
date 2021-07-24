using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
