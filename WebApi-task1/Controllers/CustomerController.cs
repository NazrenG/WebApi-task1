using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using WebApi_task1.Data;
using WebApi_task1.Dtos;
using WebApi_task1.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public CustomerController(ECommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<Controller>
        [HttpGet()]
        public IEnumerable<CustomerDto> Get()
        {
            var items = _context.Customers;

            var customers = items.Select(x => new CustomerDto

            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,

            });

            return customers;
        }
         

        // GET api/<Controller>/5 
        [HttpGet("{id}")]
        public ActionResult<CustomerDto> Get(int id)
        {
            var items = _context.Customers;

            var customers = items.Select(x => new CustomerDto

            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,

            });
            return customers.FirstOrDefault(c => c.Id == id);
        }


        // POST api/<Controller>
        [HttpPost()]
        public IActionResult Post([FromForm] CustomerExtendDto value)
        {
            if (value.Name != null && value.Surname != null)
            {
                var item = new Customer
                {
                    Name = value.Name,
                    Surname = value.Surname,
                    Orders = null
                };
                _context.Add(item);
                _context.SaveChanges();
                return Ok(value);
            }
            return BadRequest("Name and Surname cannot be empty");

        }


        // PUT api/<Controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerExtendDto value)
        {
            var item = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (item != null)
            {
                item.Surname = value.Surname;
                item.Name = value.Name;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }


        // DELETE api/<Controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                _context.Customers.Remove(item);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }
    }
}
