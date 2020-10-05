using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Dtos.OrderLine;
using WebApplication1.Models;

namespace WebApplication1.Services.OrderLineService
{
    public interface IOrderLineService
    {
         Task<ServiceResponse<List<GetOrderLineDto>>> GetAllOrderLines();
         Task<ServiceResponse<GetOrderLineDto>> GetOrderLineById(int id);
         Task<ServiceResponse<List<GetOrderLineDto>>> AddOrderLine(AddOrderLineDto newOrderLine);
         Task<ServiceResponse<GetOrderLineDto>> UpdateOrderLine(UpdateOrderLineDto updatedOrderLine);
         Task<ServiceResponse<List<GetOrderLineDto>>> DeleteOrderLine(int id);
    }
}