using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Data;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public TodoRepository(MyDbContext context, IMapper mapper)
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

        public void AddTodo(TodoDto todoDto, string loggedUserEmail)
        {
            var user = _context.Users.First(x => x.EmailAddress == loggedUserEmail);
            var todo = _mapper.Map<Todo>(todoDto);
            todo.User = user;
            _context.Add(todo);
            _context.SaveChanges();
        }

        public void UpdateTodo(TodoDto todoDto)
        {
            bool isInTodosList = _context.Todos.Any(x => x.Id == todoDto.Id);

            if (isInTodosList)
            {
                var todo = _mapper.Map<Todo>(todoDto);
                _context.Todos.Update(todo);
                _context.SaveChanges();
            } else
            {
                throw new Exception($"The is no such todo!");
            }
        }

        public void DeleteTodo(int id)
        {
            var todo = _context.Todos.First(i => i.Id == id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }
    }
}
