using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public List<Todo> Get()
        {
            return _todoRepository.Get();
        }

        [HttpPost]
        public void Post(Todo todo)
        {
            _todoRepository.AddTodo(todo);
        }
    }
}
