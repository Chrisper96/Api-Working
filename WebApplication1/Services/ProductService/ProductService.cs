using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Dtos.Product;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ShopContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IMapper mapper, ShopContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            Product product = _mapper.Map<Product>(newProduct);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Products.Select(p => _mapper.Map<GetProductDto>(p))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                Product product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.Products.Select(p => _mapper.Map<GetProductDto>(p))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Product not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            List<Product> dbCharacters = await _context.Products.ToListAsync();
            serviceResponse.Data = (dbCharacters.Select(p => _mapper.Map<GetProductDto>(p))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            Product dbCharacter = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.Id == id);
            serviceResponse.Data = _mapper.Map<GetProductDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            try
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
                if (product.Id == updatedProduct.Id)
                {
                    product.Title = updatedProduct.Title;
                    product.Description = updatedProduct.Description;
                    product.Price = updatedProduct.Price;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetProductDto>(product);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Product not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}