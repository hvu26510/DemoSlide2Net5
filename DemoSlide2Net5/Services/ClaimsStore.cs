using System.Security.Claims;

namespace DemoSlide2Net5.Services
{
    public class ClaimsStore
    {
        public static List<Claim> GetAllClaim()
        {
            return new List<Claim>
            {
                new Claim("Create", "Create"),
                new Claim("Edit", "Edit"),
                new Claim("Delete", "Delete")
            };
        }
    }
}
