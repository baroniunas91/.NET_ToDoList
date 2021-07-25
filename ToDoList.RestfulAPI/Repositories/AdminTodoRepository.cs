using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using ToDoList.RestfulAPI.Data;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;

namespace ToDoList.RestfulAPI.Repositories
{
    public class AdminTodoRepository : IAdminTodoRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public AdminTodoRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<TodosGetDto> Get()
        {
            var todos = _context.Todos.OrderBy(x => x.Id).ToList();
            var todosList = new List<TodosGetDto>();
            foreach (var todo in todos)
            {
                var todoDto = _mapper.Map<TodosGetDto>(todo);
                todosList.Add(todoDto);
            }
            return todosList;
        }

        public void DeleteTodo(int id)
        {
            var todo = _context.Todos.First(i => i.Id == id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }
    }
}
