using System.Collections.Generic;
//using WebApplication1.Dtos.Skill;
//using WebApplication1.Dtos.Weapon;
using WebApplication1.Models;

namespace WebApplication1.Dtos.Product
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}