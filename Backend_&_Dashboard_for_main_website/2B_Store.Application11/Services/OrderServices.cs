using _2B_Store.Application.Contracts;
using _2B_Store.Application11.Services;
using _2B_Store.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Services
{
    public class OrderServices : IOrderServices

    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IShippingRepository _shippingRepository;

        public OrderServices(IOrderRepository orderRepository, IMapper mapper,
            IOrderItemRepository orderItemRepository, IShippingRepository shippingRepository,
                        IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
            _shippingRepository = shippingRepository;
        }

        public async Task<List<OrderDTO>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            order.OrderItems = (await _orderItemRepository.GetAllAsync()).Where(oi =>
                oi.OrderId == orderId).ToList();
            //var myOrderItems = orderItems.Where(oi =>
            //    oi.OrderId == orderId).ToList();
            //order.OrderItems = orderItems;
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<List<OrderDTO>> GetOrdersByUserId(string userId)
        {
            var orders = (await _orderRepository.GetOrdersByUserId(userId)).ToList();
            foreach (var order in orders)
            {
                order.OrderItems = (await _orderItemRepository.GetAllAsync()).Where(oi
                    => oi.OrderId == order.Id).ToList();
            }
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> AddOrder(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            //var orderItems = _mapper.Map<List<OrderItem>>(orderDTO.OrderItems);
            order = await _orderRepository.AddAsync(order);
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                product.Stock -= item.Quantity;
                await _productRepository.UpdateAsync(product);
            }
            return _mapper.Map<OrderDTO>(order);
            //order.OrderItems = orderItems;
            //await _orderRepository.SaveChangesAsync();
        }

        public async Task<OrderDTO> UpdateOrder(int orderId, OrderDTO orderDTO)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(orderId);
            if (existingOrder == null)
                throw new ArgumentException("Order not found");

            _mapper.Map(orderDTO, existingOrder);
            existingOrder = await _orderRepository.UpdateAsync(existingOrder);
            //await _orderRepository.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(existingOrder);
        }

        public async Task DeleteOrder(int orderId)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(orderId);
            //var existingOrder = GetOrderById(orderId);
            if (existingOrder == null)
                throw new ArgumentException("Order not found");

            existingOrder.OrderItems = (await _orderItemRepository.GetAllAsync())
                .Where(oi => oi.OrderId == orderId).ToList();
            List<OrderItem> myOrderItems = existingOrder.OrderItems.ToList();

            //var myOrderItems = orderItems.Where(oi =>
            //    oi.OrderId == orderId).ToList();
            //existingOrder.OrderItems = orderItems.ToList();
            //var theOrder = _mapper.Map<Order>(existingOrder);

            foreach (var item in myOrderItems)
            {
                await _orderItemRepository.DeleteAsync(item);
            }
            await _orderRepository.DeleteAsync(existingOrder);
            //await _orderRepository.SaveChangesAsync();
        }

        public async Task<ShippingDTO> AddShipping(ShippingDTO shippingDTO)
        {
            shippingDTO.ShippingMethod = "Shipping Van";
            shippingDTO.Provider = "Aramix";
            shippingDTO.TrackingNumber = Guid.NewGuid().ToString();
            var shipping = _mapper.Map<Shipping>(shippingDTO);
            shipping = await _shippingRepository.AddAsync(shipping);
            return _mapper.Map<ShippingDTO>(shipping);
        }
    }
}
