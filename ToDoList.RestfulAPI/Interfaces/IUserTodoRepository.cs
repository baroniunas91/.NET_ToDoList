using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface IUserTodoRepository
    {
        List<TodosGetDto> GetUserTodos(string loggedUser);
        void AddUserTodo(TodoDto todoDto, string loggedUser);
        void UpdateUserTodo(TodoDto todoDto, string loggedUser);
        void DeleteUserTodo(int id, string loggedUser);
    }
}
