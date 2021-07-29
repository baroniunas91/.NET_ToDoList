using System.Collections.Generic;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(UserCred userCred, List<User> users);
        string GenerateJwtToken(int userId);
        int? ValidateJwtToken(string token);
    }
}
