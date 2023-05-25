using CustomerEntities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("CustomerAddress")]
    public class CustAddressController : Controller
    {
        private CustomerEntities.CustomerEntities _entities;

        public CustAddressController(CustomerEntities.CustomerEntities entities)
        {
            _entities = entities;
        }

        [HttpGet("hello")]
        public string Get()
        {
            return "Hello From CustomerAddressController";
        }
        // Implemenation of get method
        [HttpGet("getAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            return Ok(await _entities.CustAddress.ToListAsync());
        }


        [HttpPost("AddAddress")]
        public async Task<IActionResult> AddAddress(AddAddress addAddress)
        {
            var CustAddress = new CustAddress()
            {
                AddressId = addAddress.AddressId,
                AddressText = addAddress.AddressText,

            };
            await _entities.CustAddress.AddAsync(CustAddress);
            await _entities.SaveChangesAsync();
            return Ok(CustAddress);

        }

        // Implemntation of Update Method
        [HttpPut("UpdateAddress/{AddressId}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int AddressId, CustAddress updateAddress)
        {
            var _CustAddress = await _entities.CustAddress.FindAsync(AddressId);
            if (_CustAddress != null)
            {
                _CustAddress.AddressText = updateAddress.AddressText;
                _CustAddress.AddressId = updateAddress.AddressId;
                _entities.SaveChanges();
                return Ok(_CustAddress);
            }
            return NotFound();
        }

        // Implementation fo Delete Method
        [HttpDelete("DeleteAddress/{AddressId}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int AddressId)
        {
            var address = await _entities.CustAddress.FindAsync(AddressId);
            if (address != null)
            {
                _entities.Remove(address);
                await _entities.SaveChangesAsync();
                return Ok(address);
            }
            return NotFound();
        }

    }
}
