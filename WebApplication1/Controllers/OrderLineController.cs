using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class OrderLineController : Controller
    {
        public OrderLineController(ShopContext context)
        {
            _context = context;
        }

        public ShopContext _context { get; }

        [HttpGet]
        //[EnableCors("AnotherPolicy")] //Route
        [Route("GetAll")]
        public IActionResult GetAllOrderLines()
        {
            return new JsonResult(_context.OrderLines);
        }

        [HttpGet("{Id}")]
        [EnableCors]
        public IActionResult GetProdut(int Id)
        {

            var orderLine = _context.OrderLines;

            var PTeamp = orderLine.FirstOrDefault(p => p.Id == Id);

            if (PTeamp == null)
            {
                return HttpNotFound();
            }

            return new JsonResult(PTeamp);

        }

        [HttpPost("post")]
        [EnableCors("AnotherPolicy")]
        public IActionResult PostOrderLine([FromBody] OrderLine orderLine)
        {
            
            using (var PostOrderLine = _context)
            {

                if (PostOrderLine != null)
                {
                    _context.OrderLines.Add(orderLine);
                    _context.SaveChanges();
                    return Ok("Added OrderLine");
                }
                else
                {
                    return NotFound("Not found");
                }
            }
        }

        [HttpDelete("{id}")]
        [EnableCors("AnotherPolicy")]
        public IActionResult DeleteOrderLine(int Id)
        {
            var DeleteOrderLines = _context.OrderLines.FirstOrDefault(p => p.Id == Id);
            if (DeleteOrderLines != null)
            {
                _context.OrderLines.Remove(DeleteOrderLines);
                _context.SaveChanges();
                return Ok("Removed OrderLine");
            }
            else
            {
                return NotFound("Not found");
            }
        }


        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
