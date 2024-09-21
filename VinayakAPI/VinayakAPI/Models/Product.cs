namespace VinayakAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public decimal Price { get; set; }
        public long Salary { get; set; }
        public string Department { get; set; }
        public int Quantity { get; set; }
        public string Education { get; set; }
        // public Product1? Education { get; set; }
    }


    //public class Product1
    //{
    //    public List<int> Education { get; set; }
    //    public Guid Id { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //}
   }
