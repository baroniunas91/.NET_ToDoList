using System.ComponentModel.DataAnnotations;

namespace ToDoList.RestfulAPI.Dto
{
    public class ForgotPasswordEmailDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "There is no such user")]
        public string EmailAddress { get; set; }
    }
}
