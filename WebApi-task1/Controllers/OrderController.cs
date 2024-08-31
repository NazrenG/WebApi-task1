using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_task1.Data;
using WebApi_task1.Dtos;
using WebApi_task1.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class OrderController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public OrderController(ECommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<OrderController>

        [HttpGet()]
        public IEnumerable<OrderDto> Get()
        {
            var items = _context.Orders;
            var orders = items.Select(p => new OrderDto
            {
                Id = p.Id,
                OrderDate = p.OrderDate,
                ProductId = p.ProductId,
                CustomerId = p.CustomerId,
            });
            return orders;
        }
        // GET api/<OrderController>/5

        [HttpGet("{id}")]
        public ActionResult<OrderDto> Get(int id)
        {
            var items = _context.Orders;
            var orders = items.Select(p => new OrderDto
            {
                Id = p.Id,
                OrderDate = p.OrderDate,
                ProductId = p.ProductId,
                CustomerId = p.CustomerId,
            });
            return orders.FirstOrDefault(c => c.Id == id);
        }


        // POST api/<OrderController>
        [HttpPost()]
        public IActionResult Post([FromForm] OrderExtendDto value)
        {
            if (value.CustomerId > 0 && value.ProductId > 0)
            {
                var item = new Order
                {
                    OrderDate = DateTime.Now,
                    ProductId = value.ProductId,
                    CustomerId = value.CustomerId,
                    Customer = null,
                    Product = null,

                };
                _context.Add(item);
                _context.SaveChanges();
                return Ok(value);
            }
            return BadRequest("CustomerId and ProductId must be non - zero");

        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderExtendDto value)
        {
            var item = _context.Orders.First(c => c.Id == id);
            if (item != null)
            {
                item.ProductId = value.ProductId;
                item.CustomerId = value.CustomerId;
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/<OrderController>/5


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Orders.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                _context.Orders.Remove(item);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }
    }
}
