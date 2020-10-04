using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string OrderDate { get; set; }
        public List<OrderLine> OrderLine { get; set; }
        public User User { get; set; }
    }
}
