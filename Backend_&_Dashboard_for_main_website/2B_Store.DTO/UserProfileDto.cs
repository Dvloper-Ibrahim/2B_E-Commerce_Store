using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2B_Store.DTO
{
    public class UserProfileDto
    {
        public string Id { get; set; }

        [Display(Name = "Userame")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This is a required field.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This is a required field.")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "This is a required field.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address (Ex: johndoe@domain.com).")]
        public string Email { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please enter a valid number in this field.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Please enter at least 11 characters.")]
        public string PhoneNumber { get; set; }

        //public List<AddressDto> Addresses { get; set; }
    }
}
