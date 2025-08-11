namespace DemoSlide2Net5.Models.ViewModel
{
    public class UserClaimsViewModel
    {
        public string UserID { get; set; }

        public List<UserClaim> Claims { get; set; } = new List<UserClaim>();
    }
}
