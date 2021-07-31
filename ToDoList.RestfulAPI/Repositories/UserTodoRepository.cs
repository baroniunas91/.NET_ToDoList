using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Data;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Repositories
{
    public class UserTodoRepository : IUserTodoRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public UserTodoRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TodosGetDto>> GetUserTodos(string loggedUser)
        {
            var todos = await _context.Todos.OrderBy(x => x.Id).Where(x => x.User.EmailAddress == loggedUser).ToListAsync();
            var todosList = new List<TodosGetDto>();
            foreach (var todo in todos)
            {
                var todoDto = _mapper.Map<TodosGetDto>(todo);
                todosList.Add(todoDto);
            }
            return todosList;
        }

        public async Task AddUserTodo(TodoDto todoDto, string loggedUser)
        {
            var user = await _context.Users.FirstAsync(x => x.EmailAddress == loggedUser);
            var todo = _mapper.Map<Todo>(todoDto);
            todo.User = user;
            _context.Add(todo);
            _context.SaveChanges();
        }

        public async Task UpdateUserTodo(TodoDto todoDto, string loggedUser)
        {
            bool isItInUserTodosList = _context.Todos.Where(x => x.User.EmailAddress == loggedUser).Any(x => x.Id == todoDto.Id);
            
            if (isItInUserTodosList)
            {
                var todo = _mapper.Map<Todo>(todoDto);
                var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == loggedUser);
                todo.UserId = user.Id;
                todo.User = user;
                _context.Todos.Update(todo);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The is no such todo in your todos list!");
            }
        }

        public async Task DeleteUserTodo(int id, string loggedUser)
        {
            bool isItInUserTodosList = _context.Todos.Where(x => x.User.EmailAddress == loggedUser).Any(x => x.Id == id);
            if (isItInUserTodosList)
            {
                var todo = await _context.Todos.FirstAsync(i => i.Id == id);
                _context.Todos.Remove(todo);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The is no such todo in your todos list!");
            }
        }
    }
}
