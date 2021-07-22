using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        public List<Todo> Get()
        {
            var todos = new List<Todo>();
            return todos;
        }
    }
}
