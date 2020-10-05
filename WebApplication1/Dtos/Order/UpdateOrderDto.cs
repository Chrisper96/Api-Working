using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Dtos.OrderLine;
using WebApplication1.Dtos.User;

namespace WebApplication1.Dtos.Order
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
    }
}