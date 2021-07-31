using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface ILoginRepository
    {
        Task<List<UserDto>> GetUsersDto();
        Task<List<User>> GetUsers();
        Task SignUpUser(UserCredDto userCred);
        Task ResetPassword(int userIdfromToken, ResetPasswordDto resetPasswordDto);
    }
}
