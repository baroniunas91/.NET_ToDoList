using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Data;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Interfaces;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public LoginRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetUsersDto()
        {
            var users = await _context.Users.OrderBy(x => x.Id).ToListAsync();
            var usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                usersDto.Add(userDto);
            }
            return usersDto;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.OrderBy(x => x.Id).ToListAsync();
            return users;
        }

        public async Task SignUpUser(UserCredDto userCred)
        {
            bool isAlreadyExists = _context.Users.Any(x => x.EmailAddress == userCred.EmailAddress);
            if (!isAlreadyExists)
            {
                userCred.Password = BCrypt.Net.BCrypt.HashPassword(userCred.Password);
                var user = _mapper.Map<User>(userCred);
                user.Role = "user";
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task ResetPassword(int userIdfromToken, ResetPasswordDto resetPasswordDto)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == userIdfromToken);
            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
