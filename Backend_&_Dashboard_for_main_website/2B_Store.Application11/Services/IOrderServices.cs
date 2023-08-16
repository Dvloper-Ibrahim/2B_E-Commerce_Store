using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public interface IOrderServices
    {
        Task<List<OrderDTO>> GetAllOrders();
        Task<OrderDTO> GetOrderById(int orderId);
        Task<List<OrderDTO>> GetOrdersByUserId(string userId);
        Task<OrderDTO> AddOrder(OrderDTO orderDTO);
        Task<OrderDTO> UpdateOrder(int orderId, OrderDTO orderDTO);
        Task DeleteOrder(int orderId);
        Task<ShippingDTO> AddShipping(ShippingDTO shippingDTO);
    }
}
