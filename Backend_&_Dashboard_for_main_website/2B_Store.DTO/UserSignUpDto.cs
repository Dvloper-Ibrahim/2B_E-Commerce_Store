using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.DTO
{
    public class UserSignUpDto
    {
        //public string Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This is a required field.")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This is a required field.")]
        public string LastName { get; set; }


        [Display(Name = "Userame")]
        public string UserName { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "This is a required field.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address (Ex: johndoe@domain.com).")]
        public string Email { get; set; }


        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please enter a valid number in this field.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Please enter a phone number of 11 digits.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "password must contain (capitals + smalls + numbers + symbols) characters.")]
        public string Password { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Please enter the same password correctly.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Sign Up For Newsletter")]
        public bool SignUpForNewsletter { get; set; }
    }
}
