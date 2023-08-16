using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Contracts
{

    public interface IOrderRepository : IRepository<Order, int>
    {
       // Task<Order> GetOrderById(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
    }
}
