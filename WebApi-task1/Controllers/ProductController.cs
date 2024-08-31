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
    public class ProductController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public ProductController(ECommerceDbContext context)
        {
            _context = context;
        }

        // GET: api/<ProductController>
        [HttpGet()]
        public IEnumerable<ProductDto> Get()
        {
            var items = _context.Products;
            var products = items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Discount = p.Discount,
            });
            return products;
        }
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get(int id)
        {
            var items = _context.Products;
            var products = items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Discount = p.Discount,
            });
            return products.FirstOrDefault(c => c.Id == id);
        }

        // POST api/<ProductController>
        [HttpPost()]
        public IActionResult Post([FromForm] ProductExtendDto value)
        {
            if (value.Price > 0 && value.Name != null && value.Discount >= 0)
            {
                var item = new Product
                {
                    Name = value.Name,
                    Price = value.Price,
                    Discount = value.Discount,
                    Orders = null

                };
                _context.Add(item);
                _context.SaveChanges();
                return Ok(value);
            }
            return BadRequest("Name cannot be empty and Price,Discount must be greater than zero");

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductExtendDto value)
        {
            var item = _context.Products.First(c => c.Id == id);
            if (item != null)
            {
                item.Name = value.Name;
                item.Price = value.Price;
                item.Discount = value.Discount;
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/<ProductController>/5

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Products.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                _context.Products.Remove(item);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }


        [HttpGet("ExpensiveProduct")]
        public ProductDto GetHigher()
        {
            var items = _context.Products.ToList();
            decimal max = 0;
            ProductDto product = null;  
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].Price > max)
                {
                    max = items[i].Price;
                    product = new ProductDto
                    {
                        Id = items[i].Id,
                        Name = items[i].Name,
                        Price = items[i].Price,
                        Discount = items[i].Discount
                    };
                }
            }

            return product;

        }

        [HttpGet("HighDiscountProduct")]
        public ProductDto GetHigherDiscounts()
        {
            var items = _context.Products.ToList();
            decimal max = 0;
            ProductDto product = null;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Discount > max)
                {
                    max = items[i].Discount;
                    product = new ProductDto
                    {
                        Id = items[i].Id,
                        Name = items[i].Name,
                        Price = items[i].Price,
                        Discount = items[i].Discount
                    };
                }
            }

            return product;

        }
    }
}
