using System.ComponentModel.DataAnnotations;

namespace ToDoList.RestfulAPI.Dto
{
    public class ResetPasswordDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Password must be 12 characters or more", MinimumLength = 12)]
        public string NewPassword { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Password must be 12 characters or more", MinimumLength = 12)]
        public string ConfirmPassword { get; set; }
    }
}
