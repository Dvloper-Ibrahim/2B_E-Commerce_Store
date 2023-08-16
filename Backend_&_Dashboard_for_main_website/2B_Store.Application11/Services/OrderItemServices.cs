using _2B_Store.Application.Contracts;
using _2B_Store.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Services
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemServices(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderItemDTO>> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = await _orderItemRepository.FindAsync(oi => oi.OrderId == orderId);
            return _mapper.Map<List<OrderItemDTO>>(orderItems);
        }

        public async Task<OrderItemDTO> AddOrderItem(OrderItemDTO orderItemDTO)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDTO);
            orderItem = await _orderItemRepository.AddAsync(orderItem);
            //await _orderItemRepository.SaveChangesAsync();
            return _mapper.Map<OrderItemDTO>(orderItem);
        }

        public async Task<OrderItemDTO> UpdateOrderItem(int orderItemId, OrderItemDTO orderItemDTO)
        {
            var existingOrderItem = await _orderItemRepository.GetByIdAsync(orderItemId);
            if (existingOrderItem == null)
                throw new ArgumentException("Order item not found");

            _mapper.Map(orderItemDTO, existingOrderItem);
            existingOrderItem = await _orderItemRepository.UpdateAsync(existingOrderItem);
            //await _orderItemRepository.SaveChangesAsync();
            return _mapper.Map<OrderItemDTO>(existingOrderItem);
        }

        public async Task DeleteOrderItem(int orderItemId)
        {
            var existingOrderItem = await _orderItemRepository.GetByIdAsync(orderItemId);
            if (existingOrderItem == null)
                throw new ArgumentException("Order item not found");

            await _orderItemRepository.DeleteAsync(existingOrderItem);
            //await _orderItemRepository.SaveChangesAsync();
        }
    }
}
