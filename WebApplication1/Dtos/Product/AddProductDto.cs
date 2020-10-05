using WebApplication1.Models;

namespace WebApplication1.Dtos.Product
{
    public class AddProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}