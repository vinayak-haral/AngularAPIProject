using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VinayakAPI.Data;
using VinayakAPI.Interfaces;
using VinayakAPI.Models;

namespace VinayakAPI.Repository
{
    //Repository design pattern
    public class ProductRepository : IProductRepository
    {
        // Instance of DbContex Class
        private readonly mainAPIDbContext _products;

        private readonly ILogger<ProductRepository> _logger;


        public ProductRepository(mainAPIDbContext product,ILogger<ProductRepository> logger)
        {
            _products = product;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Product data called");
                _logger.LogTrace("Checks first {Time}", DateTime.Now);
                // Below we can write the our repository logics
                var dataToReturn = await _products.GetAllProductsAsync();
                _logger.LogTrace("Checks second trace {Time}", DateTime.Now);

                return dataToReturn;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred in somemethods at {time}", DateTime.Now);
                throw;
            }
          
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _products.GetProductByIdAsync(id);
        }

        public async Task AddProduct(Product product)
        {
            await _products.InsertProductAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _products.UpdateProductAsync(product);
        }
        public async Task DeleteProduct(Guid id)
        {
            await _products.DeleteProductAsync(id);
        }

        //public async Task<IEnumerable<Product>> GetAllProducts()
        //{
        //    return await _products.Products.ToListAsync();
        //}

        //public async Task<Product> GetProductById(int id)
        //{
        //    return await _products.Products.FindAsync(id);
        //}

        //public async Task<Product> AddProduct(Product product)
        //{
        //    _products.Products.Add(product);
        //    await _products.SaveChangesAsync();
        //    return product;
        //}

        //public async Task UpdateProduct(Product product)
        //{
        //    _products.Entry(product).State = EntityState.Modified;
        //    await _products.SaveChangesAsync();
        //}

        //public async Task DeleteProduct(int id)
        //{
        //    var product = await _products.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _products.Products.Remove(product);
        //        await _products.SaveChangesAsync();
        //    }
        //}

        public void Save()
        {
            // In a real implementation, this would persist changes to the database
        } 



    }


}
