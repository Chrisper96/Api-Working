using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Dtos.Order;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly ShopContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IMapper mapper, ShopContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public async Task<ServiceResponse<List<GetOrderDto>>> AddOrder(AddOrderDto newOrder)
        {
            ServiceResponse<List<GetOrderDto>> serviceResponse = new ServiceResponse<List<GetOrderDto>>();
            Order order = _mapper.Map<Order>(newOrder);
            order.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Orders.Where(o => o.User.Id == GetUserId()).Select(o => _mapper.Map<GetOrderDto>(o))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> DeleteOrder(int id)
        {
            ServiceResponse<List<GetOrderDto>> serviceResponse = new ServiceResponse<List<GetOrderDto>>();
            try
            {
                Order order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.Id == id && o.User.Id == GetUserId());
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.Orders.Where(o => o.User.Id == GetUserId())
                        .Select(o => _mapper.Map<GetOrderDto>(o))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Order not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOrderDto>>> GetAllOrders()
        {
            ServiceResponse<List<GetOrderDto>> serviceResponse = new ServiceResponse<List<GetOrderDto>>();
            List<Order> dbOrders = 
                GetUserRole().Equals("Admin") ?
                await _context.Orders.Include(o => o.OrderLines).ThenInclude(ol => ol.Product).ToListAsync() :
                await _context.Orders.Include(o => o.OrderLines).ThenInclude(ol => ol.Product).Where(o => o.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = (dbOrders.Select(o => _mapper.Map<GetOrderDto>(o))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetOrderDto>> GetOrderById(int id)
        {
            ServiceResponse<GetOrderDto> serviceResponse = new ServiceResponse<GetOrderDto>();
            Order dbOrder = await _context.Orders
                .Include(o => o.OrderLines)
                .FirstOrDefaultAsync(o => o.Id == id && o.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetOrderDto>(dbOrder);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetOrderDto>> UpdateOrder(UpdateOrderDto updatedOrder)
        {
            ServiceResponse<GetOrderDto> serviceResponse = new ServiceResponse<GetOrderDto>();
            try
            {
                Order order = await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == updatedOrder.Id);
                if (order.User.Id == GetUserId())
                {
                    order.OrderDate = updatedOrder.OrderDate;

                    _context.Orders.Update(order);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetOrderDto>(order);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Order not found.";
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