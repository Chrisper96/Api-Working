using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Dtos.Order;
using WebApplication1.Models;

namespace WebApplication1.Services.OrderService
{
    public interface IOrderService
    {
         Task<ServiceResponse<List<GetOrderDto>>> GetAllOrders();
         Task<ServiceResponse<GetOrderDto>> GetOrderById(int id);
         Task<ServiceResponse<List<GetOrderDto>>> AddOrder(AddOrderDto newOrder);
         Task<ServiceResponse<GetOrderDto>> UpdateOrder(UpdateOrderDto updatedOrder);
         Task<ServiceResponse<List<GetOrderDto>>> DeleteOrder(int id);
    }
}