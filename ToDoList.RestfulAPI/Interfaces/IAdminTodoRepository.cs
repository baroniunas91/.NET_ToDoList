using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface IAdminTodoRepository
    {
        List<TodosGetDto> Get();
        void DeleteTodo(int id);
    }
}
