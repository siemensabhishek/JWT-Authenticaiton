using CrudModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private CustomerEntities.CustomerEntities _entities;

        public CustomerController(CustomerEntities.CustomerEntities entities)
        {
            _entities = entities;
        }

        [HttpGet("")]
        public string Hello()
        {
            return "Hello from Customer Controller.";
        }

        [HttpGet("userids")]
        public string GetAllCustomers()
        {
            var ids = _entities.Customer.Select(c => c.Id).ToList();
            var sb = new StringBuilder();
            foreach (var id in ids)
            {
                sb.AppendLine(id.ToString());
            }
            return sb.ToString();
        }

        // login check

        [HttpGet("validCustomer/{id:int}/{password:int}")]
        public bool GetValidCustomer([FromRoute] int id, [FromRoute] int password)
        {
            var _password = _entities.CustPassword.FirstOrDefault(x => x.CustomerId == id);
            return password.ToString() == _password?.CPassword;
        }


        // Get Method to get all the deatils of the all Customers

        [HttpGet("customer_details")]

        public async Task<IActionResult> GetAllCustomer()
        {
            return Ok(await _entities.Customer.ToListAsync());

        }


        // get Customer by id Method

        [HttpGet("customer_by_id/{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            var customer = await _entities.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // Edit customer api get method
        [HttpGet("GetFullCustomerDetailById/{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var customer = await _entities.Customer.FindAsync(id);
            var address = await _entities.CustAddress.FindAsync(customer.AddressId);
            CustomerEditDetails customerEditDetails = new CustomerEditDetails();
            customerEditDetails.Address = new AddressDetails
            {
                AddressId = address.AddressId,
                Address = address.AddressText
            };
            customerEditDetails.Id = customer.Id;
            customerEditDetails.AddressId = customer.AddressId;
            customerEditDetails.FirstName = customer.FirstName;
            customerEditDetails.LastName = customer.LastName;


            if (customerEditDetails == null)
            {
                return NotFound();
            }
            return Ok(customerEditDetails);
        }





        [HttpPut("EditCustomerById/{id}")]
        public async Task<IActionResult> UpdateCustomerById([FromRoute] int id, CustomerEditDetails updateCustomerReq)
        {
            try
            {
                var customer = await _entities.Customer.FindAsync(id);
                var address = await _entities.CustAddress.FindAsync(customer.AddressId);

                customer.Id = updateCustomerReq.Id;
                customer.FirstName = updateCustomerReq.FirstName;
                customer.AddressId = updateCustomerReq.AddressId;
                address.AddressId = updateCustomerReq.AddressId;
                address.AddressText = updateCustomerReq.Address.Address;
                _entities.SaveChanges();
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return Ok("Failed");
            }
        }




        [HttpDelete("DeleteCustomer/{Id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int Id)
        {
            var customer = await _entities.Customer.FindAsync(Id);
            if (customer != null)
            {
                _entities.Remove(customer);
                await _entities.SaveChangesAsync();
                return Ok(customer);
            }
            return NotFound();

        }


    }
}
