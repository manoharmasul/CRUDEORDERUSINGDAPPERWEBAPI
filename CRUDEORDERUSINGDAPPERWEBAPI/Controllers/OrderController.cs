using CRUDEORDERUSINGDAPPERWEBAPI.Model;
using CRUDEORDERUSINGDAPPERWEBAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDEORDERUSINGDAPPERWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
           var result=await _orderRepo.GetAllOrders();

           return Ok(result);
        }
        [HttpGet("/id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
           var result=await _orderRepo.GetAllOrderById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewOrderr(Order order)
        {
            var result=await _orderRepo.AddNewOrder(order);
            return Ok(result);      
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrders(Order order)
        {
            var result = await _orderRepo.UpdateOrder(order);
            return Ok(result);
        }
    }
}
