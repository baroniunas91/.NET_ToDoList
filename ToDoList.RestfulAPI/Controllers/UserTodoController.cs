using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
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
        public async Task<IActionResult> Get()
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(await _userTodoRepository.GetUserTodos(_loggedUser));
        }

        [HttpPost]
        public async Task<IActionResult> Post(TodoDto todoDto)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            await _userTodoRepository.AddUserTodo(todoDto, _loggedUser);
            return Ok("Successfully added.");
        }

        [HttpPut]
        public async Task <IActionResult> Put(TodoDto todoDto)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            try
            {
                await _userTodoRepository.UpdateUserTodo(todoDto, _loggedUser);
                return Ok("Successfully updated.");
            }
            catch (Exception)
            {
                return BadRequest("The is no such todo in your todos list!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _loggedUser = User.FindFirst(ClaimTypes.Name)?.Value;
            try
            {
                await _userTodoRepository.DeleteUserTodo(id, _loggedUser);
                return Ok("Successfully deleted.");
            }
            catch(Exception)
            {
                return BadRequest("The is no such todo in your todos list!");
            }
        }
    }
}
