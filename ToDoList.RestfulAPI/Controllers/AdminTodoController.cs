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
    [Authorize(Roles = "admin")]
    public class AdminTodoController : ControllerBase
    {
        private readonly IAdminTodoRepository _adminTodoRepository;

        public AdminTodoController(IAdminTodoRepository adminTodoRepository)
        {
            _adminTodoRepository = adminTodoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_adminTodoRepository.Get());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _adminTodoRepository.DeleteTodo(id);
            return Ok();
        }
    }
}
