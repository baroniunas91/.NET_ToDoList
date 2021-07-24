using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "user")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public List<TodosGetDto> Get()
        {
            return _todoRepository.Get();
        }

        [HttpPut]
        public void Put(TodoDto todoDto)
        {
            _todoRepository.UpdateTodo(todoDto);
        }

        [HttpPost]
        public void Post(TodoDto todoDto)
        {
            string loggedUserEmail = User.FindFirst(ClaimTypes.Name)?.Value;
            _todoRepository.AddTodo(todoDto, loggedUserEmail);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _todoRepository.DeleteTodo(id);
        }
    }
}
