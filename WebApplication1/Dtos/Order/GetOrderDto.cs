using System.Collections.Generic;
using WebApplication1.Dtos.OrderLine;
using WebApplication1.Dtos.User;
using WebApplication1.Models;

namespace WebApplication1.Dtos.Order
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public List<GetOrderLineDto> OrderLines { get; set; }
    }
}