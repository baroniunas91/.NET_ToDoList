using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _config;
        private readonly ISendEmailService _sendEmailService;

        public LoginController(IJwtAuthenticationManager jwtAuthenticationManager, ILoginRepository loginRepository, IConfiguration configuration, ISendEmailService sendEmailService)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _loginRepository = loginRepository;
            _config = configuration;
            _sendEmailService = sendEmailService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
            var users = await _loginRepository.GetUsersDto();
            return Ok(users);
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserCredDto userCred)
        {
            var users = await _loginRepository.GetUsers();
            var token = _jwtAuthenticationManager.Authenticate(userCred, users);
            if (token == null)
            {
                return Unauthorized();
            };
            return Ok($"Bearer {token}");
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserCredDto userCred)
        {
            try
            {
                await _loginRepository.SignUpUser(userCred);
                return Ok("You have successfully signed up");
            }
            catch (Exception)
            {
                return BadRequest("User already exist!");
            }
        }

        [HttpPost("ForgotPasswordEmail")]
        public async Task<IActionResult> ForgotPasswordEmail([FromBody] ForgotPasswordEmailDto forgotPasswordEmail)
        {
            var users = await _loginRepository.GetUsers();
            var user = users.FirstOrDefault(x => x.EmailAddress == forgotPasswordEmail.EmailAddress);
            if (user != null)
            {
                var token = _jwtAuthenticationManager.GenerateJwtToken(user.Id);
                string url = $"{_config["AppUrl"]}/Login/ResetPassword?userId={user.Id}&token={token}";
                await _sendEmailService.Send(forgotPasswordEmail, url);
                return Ok($"Reset password link was sent to your email. Go check it! FOR TESTING PURPOSES: userId={user.Id} token={token}");
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
                    await _loginRepository.ResetPassword(userId, resetPasswordDto);
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
