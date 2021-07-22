using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Data;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly MyDbContext _context;

        public TodoRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Todo> Get()
        {
            var todos = _context.Todos.OrderBy(x => x.Id).ToList();
            return todos;
        }

        public void AddTodo(Todo todo)
        {
            _context.Add(todo);
            _context.SaveChanges();
        }
    }
}
