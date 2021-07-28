using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Interfaces;

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
        public async Task<IActionResult> Get()
        {
            return Ok(await _adminTodoRepository.Get());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _adminTodoRepository.DeleteTodo(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("The is no such todo in todos list!");
            }
        }
    }
}
