using WebApplication1.Models;

namespace WebApplication1.Dtos.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}