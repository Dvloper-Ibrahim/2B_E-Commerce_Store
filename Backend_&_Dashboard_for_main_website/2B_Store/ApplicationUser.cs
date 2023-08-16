using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store
{
    public class ApplicationUser : IdentityUser
    {

        //  public string Address { get; set; }
        public string FirstName { get; set; }

        // public string FirstNameAR { get; set; }

        public string LastName { get; set; }
        // public string LastNameAR { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
