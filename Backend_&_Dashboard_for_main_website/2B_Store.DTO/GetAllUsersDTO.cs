using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _2B_Store.DTO
{
    public class GetAllUsersDTO
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Userame")]
        public string UserName { get; set; }


        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Sign Up For Newsletter")]
        public bool SignUpForNewsletter { get; set; }
    }
}
