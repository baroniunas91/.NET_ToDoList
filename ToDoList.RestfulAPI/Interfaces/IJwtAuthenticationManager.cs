using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(UserCredDto userCred, List<User> users);
        string GenerateJwtToken(int userId);
        int? ValidateJwtToken(string token);
    }
}
