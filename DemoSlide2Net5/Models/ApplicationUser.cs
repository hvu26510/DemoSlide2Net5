using Microsoft.AspNetCore.Identity;
namespace DemoSlide2Net5.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Fullname { get; set; }
    }
}
