using Microsoft.AspNetCore.Mvc;
using VinayakAPI.Interfaces;
using VinayakAPI.Models;

namespace VinayakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _productRepository;//Proparty of interfcae

        public ProductsController(IProductRepository productRepository,ILogger<ProductsController> logger)
        {
            _logger = logger;
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
            _logger.LogInformation("API gets called");
            _logger.LogTrace("Get Method started at {time}", DateTime.Now);
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _logger.LogTrace("Get Method finished at {time}", DateTime.Now);

            return Ok(product);


        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            _logger.LogTrace("CreateProduct started at {Time}", DateTime.Now);

            try
            {
                _logger.LogTrace("Processing in SomeMethod...");
                await _productRepository.AddProduct(product);
                var dataToReturn =  CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

              _logger.LogTrace("SomeMethod completed successfully at {Time}", DateTime.Now);

                return dataToReturn;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeMethod at {Time}", DateTime.Now);
                throw;
            }
           
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
