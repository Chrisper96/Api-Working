using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Dtos.OrderLine;
using WebApplication1.Models;
using WebApplication1.Services.OrderLineService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //[Authorize(Roles = "Player,Admin")]
    [ApiController]
    [Route("[controller]")]
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineService _orderLineService;

        public OrderLineController(IOrderLineService OrderLineService)
        {
            _orderLineService = OrderLineService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orderLineService.GetAllOrderLines());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _orderLineService.GetOrderLineById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderLine(AddOrderLineDto newOrderLine)
        {
            return Ok(await _orderLineService.AddOrderLine(newOrderLine));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderLine(UpdateOrderLineDto updateOrderLineDto)
        {
            ServiceResponse<GetOrderLineDto> response = await _orderLineService.UpdateOrderLine(updateOrderLineDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetOrderLineDto>> response = await _orderLineService.DeleteOrderLine(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}