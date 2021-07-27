using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.RestfulAPI.Models
{
    public class ForgotPasswordEmail
    {
        [Required]
        [EmailAddress(ErrorMessage = "There is no such user")]
        public string EmailAddress { get; set; }
    }
}
