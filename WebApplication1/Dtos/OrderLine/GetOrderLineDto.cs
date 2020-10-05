using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Dtos.Product;
using WebApplication1.Dtos.Order;

namespace WebApplication1.Dtos.OrderLine
{
    public class GetOrderLineDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public GetProductDto Product { get; set; }
    }
}