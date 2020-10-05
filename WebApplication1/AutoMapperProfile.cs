using System.Linq;
using AutoMapper;
using WebApplication1.Dtos.Order;
using WebApplication1.Dtos.OrderLine;
using WebApplication1.Dtos.Product;
// using WebApplication1.Dtos.Fight;
// using WebApplication1.Dtos.Skill;
// using WebApplication1.Dtos.Weapon;
using WebApplication1.Models;

namespace WebApplication1
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<OrderLine, GetOrderLineDto>();
            CreateMap<AddOrderLineDto, OrderLine>();
            CreateMap<Order, GetOrderDto>();
            // CreateMap<AddCharacterDto, Character>();
            // CreateMap<Weapon, GetWeaponDto>();
            // CreateMap<Skill, GetSkillDto>();
            // CreateMap<Character, HighscoreDto>();
        }
    }
}