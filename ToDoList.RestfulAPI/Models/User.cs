using System.Collections.Generic;

namespace ToDoList.RestfulAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<Todo> Todos { get; set; }
    }
}
