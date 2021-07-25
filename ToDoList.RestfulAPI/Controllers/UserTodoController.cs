using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;

namespace ToDoList.RestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "user")]
    public class UserTodoController : ControllerBase
    {
        private readonly IUserTodoRepository _userTodoRepository;
        private string _loggedUser;

        public UserTodoController(IUserTodoRepository userTodoRepository)
        {
            _userTodoRepository = userTodoRepository;
        }

        [HttpGet]
        public List<TodosGetDto> Get()
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            return _userTodoRepository.GetUserTodos(_loggedUser);
        }

        [HttpPost]
        public void Post(TodoDto todoDto)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            _userTodoRepository.AddUserTodo(todoDto, _loggedUser);
        }

        [HttpPut]
        public void Put(TodoDto todoDto)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            _userTodoRepository.UpdateUserTodo(todoDto, _loggedUser);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            _userTodoRepository.DeleteUserTodo(id, _loggedUser);
        }

    }
}
