using DemoSlide2Net5.Models;
using DemoSlide2Net5.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DemoSlide2Net5.Services;

namespace DemoSlide2Net5.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController (UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }


        [HttpGet]

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) {
                return NotFound();
            }

            var model = new EditUserViewModel()
            {
                FullName = user.Fullname,
                Email = user.Email,
                Id = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid) {
                user.UserName = model.Email;
                user.Email = model.Email;
                user.Fullname = model.FullName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = _userManager.DeleteAsync(user);
            if (result.IsCompletedSuccessfully)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userID)
        {
            //kiem tra user
            var user = await _userManager.FindByIdAsync(userID);

            if (user == null) { return NotFound(); }

            //lay danh sach claim cua user
            var userClaims = await _userManager.GetClaimsAsync(user);

            //tao view model
            UserClaimsViewModel model = new UserClaimsViewModel
            {
                UserID = user.Id,
                Claims = ClaimsStore.GetAllClaim().Select(claim => new UserClaim
                {
                    ClaimType = claim.Type,
                    IsSelected = userClaims.Any(c => c.Type == claim.Type)
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            //kiem tra user
            var user = await _userManager.FindByIdAsync(model.UserID);

            if (user == null) { return NotFound(); }

            //loai bo cac claim
            var existClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in existClaims) {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            //Them moi theo model nhan vao

            var SelectedClaim = model.Claims
                .Where(c => c.IsSelected)
                .Select(c => new Claim(c.ClaimType, c.ClaimType));

            foreach (var claim in SelectedClaim)
            {
                await _userManager.AddClaimAsync(user, claim);
            }

            return RedirectToAction("Index");
        }

    }
}
