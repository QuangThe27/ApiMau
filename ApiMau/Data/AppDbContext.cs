using Microsoft.EntityFrameworkCore;
using ApiMau.Models;

namespace ApiMau.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<ApiMau.Models.User> Users { get; set; }
    }
}
