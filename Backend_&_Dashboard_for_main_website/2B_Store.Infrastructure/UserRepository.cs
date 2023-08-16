using _2B_Store.Application.Contracts;
using _2B_Store.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Infrastructure
{
    public class UserRepository : Repository<ApplicationUser, string>, IUserRepository
    {
        private readonly StoreContext _context;

        public UserRepository(StoreContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _Dbset.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser> CheckforUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }


        //public async Task<ApplicationUser> CheckforUser(string email, string password)
        //{
        //    return await _Dbset.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        //}



        //  private readonly UserManager<User> _userManager;

        //  StoreContext _dbContext;
        //public UserRepository(StoreContext dbContext, UserManager<User> userManager) : base(dbContext) 
        //{
        //   // _userManager = userManager;

        //   // _dbContext = dbContext;
        //}

        //public async Task<User> GetUserByEmail(string email)
        //{
        //    return await _Dbset.FirstOrDefaultAsync(u => u.Email == email);
        //}

        //public async Task<User> CheckforUser(string email, string password)
        //{
        //    User user = await GetUserByEmail(email);
        //    if (user != null && await _userManager.CheckPasswordAsync(user, password))
        //    {
        //        // Password is correct, return the user
        //        return user;
        //    }

        //    // User not found or password is incorrect, return null
        //    return null;
        //}



        //public async Task<User> GetUserById(int userId)
        //{
        //    return await GetByIdAsync(userId);
        //}

        //public async Task<User> GetUserByUsername(string username)
        //{
        //    return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        //}
    }
}
