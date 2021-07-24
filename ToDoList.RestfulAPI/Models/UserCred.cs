using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.RestfulAPI.Models
{
    public class UserCred
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "password must be 12 characters or more", MinimumLength = 12)]
        public string Password { get; set; }
    }
}
