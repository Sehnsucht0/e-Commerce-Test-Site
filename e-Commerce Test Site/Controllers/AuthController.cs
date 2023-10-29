using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace e_Commerce_Test_Site.Controllers
{
    public class AuthController: Controller
    {
        private readonly StoreUserContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController (UserManager<User> userManager, SignInManager<User> signInManager, StoreUserContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        [Route("auth/register")]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View("Unavailable");
            //return View();
        }

        [HttpGet]
        [Route("auth/signin")]
        public IActionResult Signin(bool transfer)
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            TempData["previousUrl"] = Utility.GetPreviousUrlRelative(Request);
            if (transfer) TempData["Transfer"] = true;
            return View("Unavailable");
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register (RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var duplicates = await ValidateDuplicates(model);
                if (duplicates) return View(model);

                var user = new User { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (model.Email == "admin@admin.com") result = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, "admin@admin.com"));

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);  
                    var userData = new UserData { UserId = user.Id};       
                    _context.UserData.Add(userData);
                    _context.SaveChanges();
                    if (TempData["Transfer"] != null)
                    {
                        await Utility.TransferSessionCart(Request, userData.Id, _context);
                        return LocalRedirect("/cart");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }  
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SigninViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.PersistentCookie, false);
                if (result.Succeeded)
                {
                    string previousUrl = "/";
                    if (TempData.Peek("previousUrl") != null) previousUrl = TempData["previousUrl"]!.ToString()!;
                    return LocalRedirect(previousUrl);
                }
            }
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            if (user == null) return RedirectToAction("Index", "Home");
            return View(user);
        }

        [Authorize(Policy = "adminpolicy")]
        [HttpGet]
        public IActionResult AdminArea()
        {
            return View(new AdminAreaModel() { Context = _context });
        }

        [Authorize(Policy = "adminpolicy")]
        [HttpPost]
        public IActionResult AdminArea(AdminAreaModel model)
        {
            model.Context = _context;
            return View(model);
        }

        [Authorize(Policy = "adminpolicy")]
        [HttpPost]
        public async Task<IActionResult> AdminAreaCommand(AdminAreaCommand admincommand)
        {
            bool result = false;
            if (admincommand.Command == "delete") result = await AdminUtility.Delete(_userManager, admincommand.Id);
            if (admincommand.Command == "deleteorder") result = await AdminUtility.DeleteOrder(_context, admincommand.Id);
            if (result) return RedirectToAction("AdminArea");
            else return Content("Error");
        }


        public async Task<JsonResult> CheckEmail (string email)
        {
            if (Utility.GetPreviousUrlRelative(Request) != "/auth/register") return Json("Invalid request.");
            var check = await _userManager.FindByEmailAsync(email);
            if (check != null) return Json("Email already in use.");
            else return Json(true);
        }

        public async Task<JsonResult> CheckUsername(string username)
        {
            if (Utility.GetPreviousUrlRelative(Request) != "/auth/register") return Json("Invalid request.");
            var check = await _userManager.FindByNameAsync(username);
            if (check != null) return Json("Username already in use.");
            else return Json(true);
        }


        public async Task<bool> ValidateDuplicates (RegisterViewModel model)
        {
            User? check = await _userManager.FindByEmailAsync(model.Email!);
            if (check != null)  ModelState.AddModelError("Email", "Email already in use.");

            check = await _userManager.FindByNameAsync(model.Username!);
            if (check != null) ModelState.AddModelError("Username", "Username already in use.");

            return check != null;
        }
    }
}
