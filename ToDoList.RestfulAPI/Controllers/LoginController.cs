﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public LoginController(IJwtAuthenticationManager jwtAuthenticationManager, MyDbContext context, IMapper mapper)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
            _mapper = mapper;
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
            var token = _jwtAuthenticationManager.Authenticate(userCred.EmailAddress, userCred.Password, users);
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

        [HttpPost("forgot-password-email")]
        public async Task<IActionResult> ForgotPasswordEmail([FromBody] ForgotPasswordEmail forgotPasswordEmail)
        {
            var users = await _context.Users.OrderBy(x => x.Id).ToListAsync();
            var user = users.FirstOrDefault(x => x.EmailAddress == forgotPasswordEmail.EmailAddress);
            if (user != null)
            {
                await SendEmailService.Send(forgotPasswordEmail);
                return Ok();
            }
            else
            {
                return BadRequest("The is no such user in our system!");
            }
        }
    }
}
