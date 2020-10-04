using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Dtos.Product;
using WebApplication1.Models;

namespace WebApplication1.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();
        Task<ServiceResponse<GetProductDto>> GetProductById(int id);
        Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct);
        Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct);
        Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id);
    }
}