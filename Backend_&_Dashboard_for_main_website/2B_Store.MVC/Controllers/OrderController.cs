using _2B_Store.Application11.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace _2B_Store.MVC.Controllers
{
    [Authorize(Roles = "Admin,Sup_Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public async Task<IActionResult> Index()
        {
            var allOrders = await _orderServices.GetAllOrders();
            return View(allOrders);
        }

        public async Task<IActionResult> UserOrders(string userId)
        {
            var userOrders = await _orderServices.GetOrdersByUserId(userId);
            return View(userOrders);
        }
    }
}
