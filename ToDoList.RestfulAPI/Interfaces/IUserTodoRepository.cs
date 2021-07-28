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
        Task<List<TodosGetDto>> GetUserTodos(string loggedUser);
        Task AddUserTodo(TodoDto todoDto, string loggedUser);
        Task UpdateUserTodo(TodoDto todoDto, string loggedUser);
        Task DeleteUserTodo(int id, string loggedUser);
    }
}
