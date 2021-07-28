using System.ComponentModel.DataAnnotations;

namespace ToDoList.RestfulAPI.Models
{
    public class ForgotPasswordEmail
    {
        [Required]
        [EmailAddress(ErrorMessage = "There is no such user")]
        public string EmailAddress { get; set; }
    }
}
