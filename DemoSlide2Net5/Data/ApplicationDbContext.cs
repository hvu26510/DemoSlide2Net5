using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DemoSlide2Net5.Models;
using Microsoft.EntityFrameworkCore;
namespace DemoSlide2Net5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
