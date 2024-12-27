using VinayakAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Numerics;
using System.Reflection;

namespace VinayakAPI.Data
{
    public class mainAPIDbContext: DbContext
    {
        public mainAPIDbContext(DbContextOptions options) : base(options) { }


        // Products Models
        public DbSet<Product> Products { get; set; }

        public DbSet<UserRegistration> UserRegistration { get; set; }

        public DbSet<LoginModel> LoginModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginModel>().HasNoKey();
        }


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


        // Login
        public async Task<List<LoginModel>> GetAllLogin()
        {
            return await LoginModel.FromSqlRaw("EXEC GetLoginData").ToListAsync();
        }


        // User Registration forms.

        // Method to get a product by Id
        //public async Task<UserRegistration> GetUserByIdAsync(Guid id)
        //{
        //    var param = new SqlParameter("@Id", id);
        //    var result = await Products.FromSqlRaw("EXEC GetProductById @Id", param).ToListAsync();
        //    return result.FirstOrDefault();
        //}

        // Method to insert a product
        public async Task<int> InsertUserProductAsync(UserRegistration user)
        {
            var parameters = new[]
            {
            new SqlParameter("@firstName", user.firstName),
                new SqlParameter("@lastName", user.lastName),
                new SqlParameter("@email", user.email),
                new SqlParameter ("@phone", user.phone),
                new SqlParameter("@gender", user.@gender),
                new SqlParameter ("@location", user.location),
                new SqlParameter ("@password", user.password),
                new SqlParameter ("@confirmPassword", user.confirmPassword)

            };
            return await Database.ExecuteSqlRawAsync("EXEC InsertUserRegistration @firstName, @lastName, @email,@phone, @gender, @location, @password , @confirmPassword", parameters);
        }



        // Method to insert a User Registration
        //public async Task<int> InsertUserRegisterAsync(UserRegistration userRegistration)
        //{
        //    var parameters = new[]
        //    {
        //        new SqlParameter("@Username", userRegistration.Username),
        //        new SqlParameter ("@Password", userRegistration.Password),
        //        new SqlParameter("@Phone",userRegistration.Phone),
        //        new SqlParameter ("@Email",userRegistration.Email),
        //        new SqlParameter ("@Gender",userRegistration.Gender)

        //    };
        //    return await Database.ExecuteSqlRawAsync("EXEC InsertProduct @Name, @Price, @Quantity,@Salary, @Phone, @Department, @Email , @Education", parameters);
        //}


    }
}
