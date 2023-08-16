using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Contracts
{
    public interface IUserRepository : IRepository<ApplicationUser, string>
    {

        //Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> CheckforUser(string email, string password);
    }
}
