using System.ComponentModel.DataAnnotations;

namespace ToDoList.RestfulAPI.Dto
{
    public class ForgotPasswordEmailDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string EmailAddress { get; set; }
    }
}
