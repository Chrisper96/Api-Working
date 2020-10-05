using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Dtos.Order;
using WebApplication1.Models;
using WebApplication1.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //[Authorize(Roles = "Player,Admin")]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _orderService.GetOrderById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDto newOrder)
        {
            return Ok(await _orderService.AddOrder(newOrder));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto updatedOrder)
        {
            ServiceResponse<GetOrderDto> response = await _orderService.UpdateOrder(updatedOrder);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetOrderDto>> response = await _orderService.DeleteOrder(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}