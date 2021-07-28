using System.Collections.Generic;
using ToDoList.RestfulAPI.Models;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password, List<User> users);
    }
}
