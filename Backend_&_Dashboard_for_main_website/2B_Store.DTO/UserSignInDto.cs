using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.DTO
{
    public class UserSignInDto
    {
        //[Required(ErrorMessage = "This is a required field.")]
        //[EmailAddress(ErrorMessage = "Please enter a valid email address (Ex: johndoe@domain.com).")]
        //public string Email { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
