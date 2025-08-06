using DemoSlide2Net5.Models;
using DemoSlide2Net5.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoSlide2Net5.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid) {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    Fullname = registerViewModel.FullName
                };
                
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded) { 
                    RedirectToAction("Index", "Home");
                }
                foreach (var e in result.Errors) {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
            }
            return View(registerViewModel);
        }

    }
}
