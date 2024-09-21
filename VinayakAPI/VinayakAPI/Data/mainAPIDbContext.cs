using VinayakAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace VinayakAPI.Data
{
    public class mainAPIDbContext: DbContext
    {
        public mainAPIDbContext(DbContextOptions options) : base(options) { }


        // Products Models
        public DbSet<Product> Products { get; set; }

        public DbSet<UserRegistration> UserRegistration { get; set; }

        public DbSet<LoginModel> LoginModel { get; set; }

        // Method to get all products using  Method to execute stored procedure
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await Products.FromSqlRaw("EXEC GetAllProducts").ToListAsync();
        }

        // Method to get a product by Id
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var param = new SqlParameter("@Id", id);
            var result = await Products.FromSqlRaw("EXEC GetProductById @Id", param).ToListAsync();
            return result.FirstOrDefault();
        }

        // Method to insert a product
        public async Task<int> InsertProductAsync(Product product)
        {
            var parameters = new[]
            {
                new SqlParameter("@Name", product.Name),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Quantity", product.Quantity),
                new SqlParameter ("@Salary",product.Salary),
                new SqlParameter ("@Phone", product.Phone),
                new SqlParameter("@Department",product.Department),
                new SqlParameter ("@Email",product.Email),
                new SqlParameter ("@Education",product.Education)

            };
            return await Database.ExecuteSqlRawAsync("EXEC InsertProduct @Name, @Price, @Quantity,@Salary, @Phone, @Department, @Email , @Education", parameters);
        }

        // Method to update a product
        public async Task<int> UpdateProductAsync(Product product)
        {
            var parameters = new[] 
            {
                new SqlParameter("@Id", product.Id),
                new SqlParameter("@Name", product.Name),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@Quantity", product.Quantity),
                new SqlParameter ("@Salary",product.Salary),
                new SqlParameter ("@Phone", product.Phone),
                new SqlParameter("@Department",product.Department),
                new SqlParameter ("@Email",product.Email),
                new SqlParameter("@Education",product.Education)
            };
            return await Database.ExecuteSqlRawAsync("EXEC UpdateProduct @Id, @Name, @Price, @Quantity, @Salary, @Phone, @Department, @Email, @Education", parameters);
        }

        // Method to delete a product
        public async Task<int> DeleteProductAsync(Guid id)
        {
            var param = new SqlParameter("@Id", id);
            return await Database.ExecuteSqlRawAsync("EXEC DeleteProduct @Id", param);
        }



    }
}
