using _2B_Store.Application.Services;
using _2B_Store.Application11.Services;
using _2B_Store.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2B_Store.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IOrderItemServices _orderItemServices;

        public OrderController(IOrderServices orderServices, IOrderItemServices orderItemServices)
        {
            _orderServices = orderServices;
            _orderItemServices = orderItemServices;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdOrder = await _orderServices.AddOrder(orderDTO);
                    //foreach (var item in orderDTO.OrderItems)
                    //{
                    //    await _orderItemServices.AddOrderItem(item);
                    //}
                    return Ok(createdOrder);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDTO orderDTO)
        {
            try
            {
                var updatedOrder = await _orderServices.UpdateOrder(id, orderDTO);
                return Ok(updatedOrder);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Order/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _orderServices.DeleteOrder(id);
                return Ok("Order deleted successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            var orders = await _orderServices.GetOrdersByUserId(userId);
            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderServices.GetOrderById(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        //[Route("Create")]
        [HttpPost("AddShipping")]
        public async Task<IActionResult> AddShipping(ShippingDTO shippingDTO)
        {
            //shippingDTO.ShippingMethod = "Shipping Van";
            //shippingDTO.Provider = "Aramix";
            //shippingDTO.TrackingNumber = Guid.NewGuid().ToString();
            try
            {
                if (ModelState.IsValid)
                {
                    var createdShipping = await _orderServices.AddShipping(shippingDTO);
                    return Ok(createdShipping);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
