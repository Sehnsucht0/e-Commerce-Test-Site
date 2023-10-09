using Microsoft.AspNetCore.Identity;

namespace e_Commerce_Test_Site.Models
{
    public static class AdminUtility
    {
        public static async Task<bool> Delete (UserManager<User> usermanager, string UserId)
        {
            var user = await usermanager.FindByIdAsync(UserId);
            if (user != null)
            {
                var result = await usermanager.DeleteAsync(user);
                if (result.Succeeded) return true;
            }
            return false;
        }
        public static async Task<bool> DeleteOrder (StoreUserContext context, string Id)
        {
            int orderId = Convert.ToInt32(Id);
            var order = context.Orders.Find(orderId);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}