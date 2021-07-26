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
        public IActionResult Get()
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(_userTodoRepository.GetUserTodos(_loggedUser));
        }

        [HttpPost]
        public IActionResult Post(TodoDto todoDto)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            _userTodoRepository.AddUserTodo(todoDto, _loggedUser);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(TodoDto todoDto)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            try
            {
                _userTodoRepository.UpdateUserTodo(todoDto, _loggedUser);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("The is no such todo in your todos list!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            try
            {
                _userTodoRepository.DeleteUserTodo(id, _loggedUser);
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest("The is no such todo in your todos list!");
            }
        }

    }
}
