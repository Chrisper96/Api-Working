using WebApplication1.Models;

namespace WebApplication1.Dtos.OrderLine
{
    public class UpdateOrderLineDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}