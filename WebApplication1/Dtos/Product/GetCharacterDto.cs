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
        /*
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto Weapon { get; set; }
        public List<GetSkillDto> Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }*/
    }
}