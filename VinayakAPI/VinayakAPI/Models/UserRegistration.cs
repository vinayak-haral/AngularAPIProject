namespace VinayakAPI.Models
{
    public class UserRegistration
    {
        public Guid Id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public long phone { get; set; }
        public string? gender { get; set; }
        public string? location { get; set; }
        public string? password { get; set; }
        public string? confirmPassword { get; set; }
    }
}
