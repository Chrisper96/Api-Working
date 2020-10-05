using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Dtos.OrderLine;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services.OrderLineService
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IMapper _mapper;
        private readonly ShopContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderLineService(IMapper mapper, ShopContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public async Task<ServiceResponse<List<GetOrderLineDto>>> AddOrderLine(AddOrderLineDto newOrderLine)
        {
            ServiceResponse<List<GetOrderLineDto>> serviceResponse = new ServiceResponse<List<GetOrderLineDto>>();
            OrderLine orderLine = _mapper.Map<OrderLine>(newOrderLine);

            await _context.OrderLines.AddAsync(orderLine);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.OrderLines.Select(ol => _mapper.Map<GetOrderLineDto>(ol))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderLineDto>>> DeleteOrderLine(int id)
        {
            ServiceResponse<List<GetOrderLineDto>> serviceResponse = new ServiceResponse<List<GetOrderLineDto>>();
            try
            {
                OrderLine orderLine = await _context.OrderLines
                    .FirstOrDefaultAsync(ol => ol.Id == id && ol.Id == id);
                if (orderLine != null)
                {
                    _context.OrderLines.Remove(orderLine);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.OrderLines.Where(ol => ol.Id == id)
                        .Select(ol => _mapper.Map<GetOrderLineDto>(ol))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "OrderLine not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderLineDto>>> GetAllOrderLines()
        {
            ServiceResponse<List<GetOrderLineDto>> serviceResponse = new ServiceResponse<List<GetOrderLineDto>>();
            List<OrderLine> dbOrderLines = await _context.OrderLines.ToListAsync();
            serviceResponse.Data = (dbOrderLines.Select(ol => _mapper.Map<GetOrderLineDto>(ol))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetOrderLineDto>> GetOrderLineById(int id)
        {
            ServiceResponse<GetOrderLineDto> serviceResponse = new ServiceResponse<GetOrderLineDto>();
            OrderLine dbOrderLine = await _context.OrderLines
                .Include(ol => ol.Product)
                .FirstOrDefaultAsync(ol => ol.Id == id);
            serviceResponse.Data = _mapper.Map<GetOrderLineDto>(dbOrderLine);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetOrderLineDto>> UpdateOrderLine(UpdateOrderLineDto updatedOrderLine)
        {
            ServiceResponse<GetOrderLineDto> serviceResponse = new ServiceResponse<GetOrderLineDto>();
            try
            {
                OrderLine orderLine = await _context.OrderLines.FirstOrDefaultAsync(ol => ol.Id == updatedOrderLine.Id);
                if (orderLine.Id == updatedOrderLine.Id)
                {
                    orderLine.Quantity = updatedOrderLine.Quantity;
                    orderLine.Price = updatedOrderLine.Price;

                    _context.OrderLines.Update(orderLine);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetOrderLineDto>(orderLine);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "OrderLine not found.";
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