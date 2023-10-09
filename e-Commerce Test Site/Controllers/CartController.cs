using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using e_Commerce_Test_Site.Models.DTOs;

namespace e_Commerce_Test_Site.Controllers
{
    public class CartController : Controller
    {
        private StoreUserContext _context;
        private UserManager<User> _usermanager;
        private SignInManager<User> _signinmanager;
        public CartController(StoreUserContext ctx, UserManager<User> usermgr,SignInManager<User> signinmgr)
        {
            _context = ctx;
            _usermanager = usermgr;
            _signinmanager = signinmgr;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToCartViewModel model, bool cartTransfer = false)
        {
            if (ModelState.IsValid)
            {
               
                if (_signinmanager.IsSignedIn(User))
                {
                    var username = User.Identity!.Name;
                    var currentUser = await _usermanager.FindByNameAsync(username!);
                    var userId = currentUser!.Id;

                    UserData? userData = await _context.UserData.FirstOrDefaultAsync(a =>  a.UserId == userId);
                    var userDataId = userData!.Id;

                    var previousCartItem = await _context.CartItems.FirstOrDefaultAsync(a => a.UserDataId == userDataId && a.ProductId == model.ProductId);
                    if (previousCartItem != null)
                    {
                        previousCartItem.Quantity = model.Quantity;
                        _context.CartItems.Update(previousCartItem);
                    }
                    else
                    {
                        CartItems cartItems = new CartItems() { UserDataId = userDataId, ProductId = model.ProductId, Quantity = model.Quantity };
                        await _context.CartItems.AddAsync(cartItems);
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    string? cart = Request.HttpContext.Session.GetString("cart");
                    List<SessionCartDTO> SessionCart;

                    if (cart != null) SessionCart = JsonSerializer.Deserialize<List<SessionCartDTO>>(cart) ?? new List<SessionCartDTO>();
                    else SessionCart = new List<SessionCartDTO>();

                    SessionCartDTO? previousItem = SessionCart.Find(item => item.ProductId == model.ProductId);
                    if (previousItem != null)
                    {
                        int index = SessionCart.IndexOf(previousItem);
                        previousItem.Quantity = model.Quantity;

                        SessionCart.RemoveAt(index);
                        SessionCart.Insert(index, previousItem);
                        Request.HttpContext.Session.Remove("cart");
                    }
                    else
                    {
                        SessionCartDTO AddCartItem = new SessionCartDTO() { ProductId = model.ProductId, Quantity = model.Quantity };
                        SessionCart.Add(AddCartItem);
                    }
                   Request.HttpContext.Session.SetString("cart", JsonSerializer.Serialize(SessionCart));
                }
            }
            if (cartTransfer) return LocalRedirect("/cart");
            return LocalRedirect(Utility.GetPreviousUrlRelative(Request));
        }

        [HttpPost]
        public async Task<IActionResult> Remove (RemoveFromCartDTO model)
        {
            if (model.IsAuthenticated)
            {
                var username = User.Identity!.Name;
                var currentUser = await _usermanager.FindByNameAsync(username!);
                var userId = currentUser!.Id;

                UserData? userData = await _context.UserData.FirstOrDefaultAsync(a => a.UserId == userId);
                var userDataId = userData!.Id;
                var CartItem = await _context.CartItems.FirstAsync(a => a.UserDataId == userDataId && a.ProductId == model.ProductId);
                _context.CartItems.Remove(CartItem);
                _context.SaveChanges();
            }
            else
            {
                string? cart = Request.HttpContext.Session.GetString("cart");
                List<SessionCartDTO> SessionCart = JsonSerializer.Deserialize<List<SessionCartDTO>>(cart!) ?? new List<SessionCartDTO>();
                SessionCartDTO? Item = SessionCart.Find(item => item.ProductId == model.ProductId);
                SessionCart.Remove(Item!);
                Request.HttpContext.Session.Remove("cart");
                Request.HttpContext.Session.SetString("cart", JsonSerializer.Serialize(SessionCart));
            }
            return LocalRedirect(Utility.GetPreviousUrlRelative(Request));
        }
    }
}
