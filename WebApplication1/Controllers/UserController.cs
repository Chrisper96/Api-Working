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
    public class UserController : Controller
    {
        public UserController(ShopContext context)
        {
            _context = context;
        }

        public ShopContext _context { get; }

        [HttpGet]
        //[EnableCors("AnotherPolicy")] //Route
        [Route("GetAll")]
        public IActionResult GetAllUsers()
        {
            return new JsonResult(_context.Users);
        }

        [HttpGet("{Id}")]
        //[EnableCors]
        public IActionResult GetUser(int Id)
        {

            var user = _context.Users;

            var PTeamp = user.FirstOrDefault(p => p.Id == Id);

            if (PTeamp == null)
            {
                return HttpNotFound();
            }

            return new JsonResult(PTeamp);

        }

        [HttpPost("post")]
        //[EnableCors("AnotherPolicy")]
        public IActionResult PostUser([FromBody] User user)
        {
            
            using (var Postuser = _context)
            {

                if (Postuser != null)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return Ok("Added user");
                }
                else
                {
                    return NotFound("Not found");
                }
            }
        }

        [HttpDelete("{id}")]
        //[EnableCors("AnotherPolicy")]
        public IActionResult DeleteUser(int Id)
        {
            var DeleteUsers = _context.Users.FirstOrDefault(p => p.Id == Id);
            if (DeleteUsers != null)
            {
                _context.Users.Remove(DeleteUsers);
                _context.SaveChanges();
                return Ok("Removed user");
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
