using Microsoft.AspNetCore.Mvc;
using VinayakAPI.Interfaces;
using VinayakAPI.Models;

namespace VinayakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;//Proparty of interfcae

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            await _productRepository.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, Product product)
        {
            product.Id = id;

            await _productRepository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productRepository.DeleteProduct(id);
            return NoContent();
        }


        //// GET api/products
        //[HttpGet]
        //public ActionResult<IEnumerable<Product>> Get()
        //{
        //    return Ok(_productRepository.GetAll());
        //}

        //// GET api/products/{id}
        //[HttpGet("{id}")]
        //public ActionResult<Product> Get(int id)
        //{
        //    var product = _productRepository.GetById(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

        //// POST api/products
        //[HttpPost]
        //public ActionResult<Product> Post([FromBody] Product product)
        //{
        //    if (product == null)
        //    {
        //        return BadRequest();
        //    }

        //    _productRepository.Add(product);
        //    _productRepository.Save();

        //    //return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        //    return Ok(product);
        //}
    
    
    }
}
