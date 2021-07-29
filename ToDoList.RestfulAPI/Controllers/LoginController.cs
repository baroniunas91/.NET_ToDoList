using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Data;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;
using ToDoList.RestfulAPI.Services;

namespace ToDoList.RestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public LoginController(IJwtAuthenticationManager jwtAuthenticationManager, MyDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
            _mapper = mapper;
            _config = configuration;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users.OrderBy(x => x.Id).ToListAsync();
            var usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                usersDto.Add(userDto);
            }
            return Ok(usersDto);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserCred userCred)
        {
            var users = await _context.Users.OrderBy(x => x.Id).ToListAsync();
            var token = _jwtAuthenticationManager.Authenticate(userCred, users);
            if (token == null)
            {
                return Unauthorized();
            };
            return Ok(token);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserCred userCred)
        {
            userCred.Password = BCrypt.Net.BCrypt.HashPassword(userCred.Password);
            var user = _mapper.Map<User>(userCred);
            user.Role = "user";
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("ForgotPasswordEmail")]
        public async Task<IActionResult> ForgotPasswordEmail([FromBody] ForgotPasswordEmailDto forgotPasswordEmail)
        {
            var users = await _context.Users.OrderBy(x => x.Id).ToListAsync();
            var user = users.FirstOrDefault(x => x.EmailAddress == forgotPasswordEmail.EmailAddress);
            if (user != null)
            {
                var token = _jwtAuthenticationManager.GenerateJwtToken(user.Id);
                string url = $"{_config["AppUrl"]}/Login/ResetPassword?userId={user.Id}&token={token}";
                await SendEmailService.Send(forgotPasswordEmail, url);
                return Ok("Reset Password link was sent to your email. Go check it!");
            }
            else
            {
                return BadRequest("The is no such user in our system!");
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto, [FromQuery] int userId, [FromQuery] string token)
        {
            int? userIdfromToken = _jwtAuthenticationManager.ValidateJwtToken(token);
            if(resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest("The password and confirmation password do not match!");
            }
            if(userIdfromToken != null && userIdfromToken == userId)
            {
                try
                {
                    var user = await _context.Users.FirstAsync(x => x.Id == userIdfromToken);
                    user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok("Password changed successfully!");
                }
                catch (Exception)
                {
                    return BadRequest("Sorry, we didn't change your password!");
                }
            } else
            {
                return BadRequest("Sorry, something went wrong!");
            }
            
        }
    }
}
