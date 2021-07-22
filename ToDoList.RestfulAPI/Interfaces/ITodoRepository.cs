using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface ITodoRepository
    {
        List<Todo> Get();
        void AddTodo(Todo todo);
    }
}
