using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Dtos.OrderLine;

namespace WebApplication1.Dtos.Order
{
    public class AddOrderDto
    {
        public string OrderDate { get; set; }
        public int UserId { get; set; }
    }
}