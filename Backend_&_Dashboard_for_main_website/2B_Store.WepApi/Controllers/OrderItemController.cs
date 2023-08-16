using _2B_Store.Application.Services;
using _2B_Store.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2B_Store.WepApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class OrderItemController : ControllerBase
    //{
    //    private readonly IOrderItemServices _orderItemServices;

    //    public OrderItemController(IOrderItemServices orderItemServices)
    //    {
    //        _orderItemServices = orderItemServices;
    //    }


    //    [HttpGet("order/{orderId}")]
    //    public async Task<IActionResult> GetOrderItemsByOrderId(int orderId)
    //    {
    //        var orderItems = await _orderItemServices.GetOrderItemsByOrderId(orderId);
    //        return Ok(orderItems);
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> AddOrderItem(OrderItemDTO orderItemDTO)
    //    {
    //        try
    //        {
    //            var createdOrderItem = await _orderItemServices.AddOrderItem(orderItemDTO);
    //            return Ok(createdOrderItem); 
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }



    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> UpdateOrderItem(int id, OrderItemDTO orderItemDTO)
    //    {
    //        try
    //        {
    //            var updatedOrderItem = await _orderItemServices.UpdateOrderItem(id, orderItemDTO);
    //            return Ok(updatedOrderItem);
    //        }
    //        catch (ArgumentException ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }

       
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteOrderItem(int id)
    //    {
    //        try
    //        {
    //            await _orderItemServices.DeleteOrderItem(id);
    //            return NoContent();
    //        }
    //        catch (ArgumentException ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }



    //}
}
