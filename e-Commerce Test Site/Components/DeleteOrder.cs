using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce_Test_Site.Components
{
    public class DeleteOrder: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string command, string UserId, StoreUserContext context)
        {
            AdminOrder adminOrder = new AdminOrder();
            var userdata = await context.UserData.Where(a => a.UserId == UserId).SingleAsync();
            if (!context.Entry(userdata).Collection("Orders").IsLoaded) context.Entry(userdata).Collection("Orders").Load();
            if (userdata.Orders != null)
            {
                foreach (Order order in userdata.Orders)
                {
                    adminOrder.OrderIds.Add(order.Id);
                }
            }
            adminOrder.Command = command;
            return View(adminOrder);
        }
    }
}
