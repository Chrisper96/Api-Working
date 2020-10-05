using WebApplication1.Models;

namespace WebApplication1.Dtos.OrderLine
{
    public class AddOrderLineDto
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}