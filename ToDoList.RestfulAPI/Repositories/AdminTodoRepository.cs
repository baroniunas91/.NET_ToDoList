using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<TodosGetDto>> Get()
        {
            var todos = await _context.Todos.OrderBy(x => x.Id).ToListAsync();
            var todosList = new List<TodosGetDto>();
            foreach (var todo in todos)
            {
                var todoDto = _mapper.Map<TodosGetDto>(todo);
                todosList.Add(todoDto);
            }
            return todosList;
        }

        public async Task DeleteTodo(int id)
        {
            var todo = await _context.Todos.FirstAsync(i => i.Id == id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }
    }
}
