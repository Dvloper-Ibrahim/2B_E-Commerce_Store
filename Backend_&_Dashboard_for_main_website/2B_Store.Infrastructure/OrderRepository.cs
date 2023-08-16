using _2B_Store.Application.Contracts;
using _2B_Store.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Infrastructure
{
    public class OrderRepository : Repository<Order , int> , IOrderRepository
    {

        public OrderRepository(StoreContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            return await _Dbset.Where(o => o.UserId == userId).ToListAsync();
        }
    }
}
