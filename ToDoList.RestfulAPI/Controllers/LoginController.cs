using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Data;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly MyDbContext _context;

        public LoginController(IJwtAuthenticationManager jwtAuthenticationManager, MyDbContext context)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
        }

        public IJwtAuthenticationManager JwtAuthenticationManager { get; }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "labas", "labas2" };
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var users = _context.Users.OrderBy(x => x.Id).ToList();
            var token = jwtAuthenticationManager.Authenticate(userCred.EmailAddress, userCred.Password, users);
            if(token == null) {
                return Unauthorized();
            };
            return Ok(token);
        }
    }
}
