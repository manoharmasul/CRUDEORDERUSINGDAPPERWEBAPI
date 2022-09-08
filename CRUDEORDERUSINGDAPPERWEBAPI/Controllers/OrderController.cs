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
            
          
                var result = await _orderRepo.GetAllOrderById(id);
            if(result==null)
            {
                return StatusCode(200, "Record Not Found");
            }
            else
                return Ok(result);
         
            
        }
        [HttpPost]
        public async Task<IActionResult> AddNewOrderr(Order order)
        {
            var result=await _orderRepo.AddNewOrder(order);
            if (result != 0)
                return StatusCode(200, "Recorde intseted successfully" +" ! " +result);
            else
                return StatusCode(500, "Something Is Worng");
           // return Ok(result);      
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrders(Order order)
        {
            var result = await _orderRepo.UpdateOrder(order);
            if (result != 0)
                return StatusCode(200, "Order Updated Successfully" + "  !  " + result);
            else
                return StatusCode(500, "Something is wrong");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result=await _orderRepo.DeleteOrder(id);
            if (result != 0)

                return StatusCode(200, "Order Deleted Successfully" + "  !  " + result);
            else
                return StatusCode(500,"Something is Wrong");
        }
    }
}
