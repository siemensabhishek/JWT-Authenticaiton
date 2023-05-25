using CustomerEntities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("CustPassword")]
    public class CustPasswordController : Controller
    {
        private CustomerEntities.CustomerEntities _entities;

        //public IActionResult Index()

        public CustPasswordController(CustomerEntities.CustomerEntities entities)
        {
            _entities = entities;
        }

        // To Check the controller

        [HttpGet("")]
        public string Get()
        {
            return "Hello From CustPassword Controller";
        }

        // Implementation of the get method

        [HttpGet("CustomerPassword")]
        public async Task<IActionResult> GetPassword()
        {
            return Ok(await _entities.CustPassword.ToListAsync());

        }

        // Implementation of the post Method

        [HttpPost("AddPassword")]
        public async Task<IActionResult> AddPassword(CustPassword _custPassword)
        {
            var Password = new CustPassword()
            {
                CustomerId = _custPassword.CustomerId,
                CPassword = _custPassword.CPassword,

            };
            await _entities.CustPassword.AddAsync(Password);
            await _entities.SaveChangesAsync();
            return Ok(Password);
        }

        // Implementation Of Put/Update Method

        [HttpPut("UpdatePassword/{CustomerId}")]
        public async Task<IActionResult> UpdatePassword([FromRoute] int CustomerId, CustPassword _updateCustPassword)
        {
            var password = await _entities.CustPassword.FindAsync(CustomerId);
            if (password != null)
            {
                password.CPassword = _updateCustPassword.CPassword;
                _entities.SaveChanges();
                return Ok(password);
            }
            return NotFound();
        }

        // Implementaion of Delete Method

        [HttpDelete("DeletePassword/{CustomerId}")]
        public async Task<IActionResult> RemovePassword([FromRoute] int CustomerId)
        {
            var password = await _entities.CustPassword.FindAsync(CustomerId);
            if (password != null)
            {
                _entities.Remove(password);
                await _entities.SaveChangesAsync();
                return Ok(password);

            }
            return NotFound();
        }



    }
}
