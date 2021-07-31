using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface IAdminTodoRepository
    {
        Task<List<TodosGetDto>> Get();
        Task DeleteTodo(int id);
    }
}
