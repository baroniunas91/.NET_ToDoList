using System.ComponentModel.DataAnnotations;

namespace ToDoList.RestfulAPI.Models
{
    public class UserCredDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Password must be 12 characters or more", MinimumLength = 12)]
        public string Password { get; set; }
    }
}
