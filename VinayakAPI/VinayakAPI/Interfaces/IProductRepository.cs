using Microsoft.AspNetCore.Mvc;
using VinayakAPI.Models;

namespace VinayakAPI.Interfaces
{
    public interface IProductRepository
    {
        //IEnumerable<Product> GetAll();
        ////Task<IActionResult> GetAll();
        //Product GetById(int id);
        //void Add(Product product);
        //void Save();

        //Task<IEnumerable<Product>> GetAllProducts();
        //Task<Product> GetProductById(int id);
        //Task<Product> AddProduct(Product product);
        //Task UpdateProduct(Product product);
        //Task DeleteProduct(int id);

        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Guid id);

    }
}
