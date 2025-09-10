namespace ApiMau.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; } // Unique
        public required string Password { get; set; }
        public required string Role { get; set; } // Role ["user", "admin"]
    }
}
