using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace e_Commerce_Test_Site.Controllers
{
    public class OrderController : Controller
    {
        private StoreUserContext _context;
        private UserManager<User> _usermanager;
        private SignInManager<User> _signinmanager;
        public OrderController(StoreUserContext ctx, UserManager<User> usermgr, SignInManager<User> signinmgr)
        {
            _context = ctx;
            _usermanager = usermgr;
            _signinmanager = signinmgr;
        }
        [Authorize]
        [Route("[controller]")]
        public async Task<IActionResult> Index()
        {
            /*var tobedeleted = _context.Products.ToList();
            foreach (var item in tobedeleted) _context.Products.Remove(item);
            var newtobedeleted = _context.Orders.ToList();
            foreach (var item in newtobedeleted) _context.Orders.Remove(item);
            _context.SaveChanges();*/
            var user = await _context.Users.Where(a => a.UserName == User.Identity!.Name).Include("UserData").SingleOrDefaultAsync();
            if (user == null) return RedirectToAction("Index", "Home");
            
            var userData = user.UserData;
            if (!(_context.Entry(userData!).Collection("Orders").IsLoaded)) _context.Entry(userData!).Collection("Orders").Load();

            if (userData!.Orders == null) return View(new List<Order>());
            return View(userData.Orders.ToList());
            
        }

        [Authorize]
        [Route("[controller]/{id}")]
        [HttpGet]
        public IActionResult OrderDetail(int id)
        {
            var orderitems = _context.UserOrders.Where(a => a.OrderId == id).ToList();
            return View(orderitems);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(string username, decimal total, int count)
        {
            var currentUser = await _usermanager.FindByNameAsync(username);
            var userId = currentUser!.Id;
            var userData = await _context.UserData.FirstOrDefaultAsync(a => a.UserId == userId);
            int i = 0;

            var order = new Order() { Total = total, Count = count, UserData = userData };
            await _context.Orders.AddAsync(order);
            foreach (Product product in APIGetter.StoreCartOrder!)
            {
                var jsonproduct = JsonSerializer.Serialize(product);
                var tableproduct = JsonSerializer.Deserialize<Product>(jsonproduct);
                tableproduct!.IdAPI = product.Id;
                tableproduct.Id = 0;
                var productquantity = APIGetter.StoreCartQuantities![i++].Quantity;

                var previousProduct = await _context.Products.Where(a => a.IdAPI == product.Id).FirstOrDefaultAsync();

                if (previousProduct == null)
                {
                    await _context.Products.AddAsync(tableproduct);
                    await _context.UserOrders.AddAsync(new UserOrder() { Order = order, Product = tableproduct, Quantity = productquantity });
                }
                else await _context.UserOrders.AddAsync(new UserOrder() { Order = order, Product = previousProduct, Quantity = productquantity });
            }
            await _context.SaveChangesAsync();
            //var userorders = _context.UserOrders.Where(a => a.OrderId == order.Id).ToList();
            return RedirectToAction("Index");
        }
    }
}
