using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Services
{
    public interface IOrderItemServices
    {
        Task<List<OrderItemDTO>> GetOrderItemsByOrderId(int orderId);
        Task<OrderItemDTO> AddOrderItem(OrderItemDTO orderItemDTO);
        Task<OrderItemDTO> UpdateOrderItem(int orderItemId, OrderItemDTO orderItemDTO);
        Task DeleteOrderItem(int orderItemId);

    }
}
