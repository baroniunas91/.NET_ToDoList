using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                _adminTodoRepository.DeleteTodo(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("The is no such todo in todos list!");
            }
        }
    }
}
