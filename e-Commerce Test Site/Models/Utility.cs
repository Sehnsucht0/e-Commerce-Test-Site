using Azure.Core;
using e_Commerce_Test_Site.Models.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;

namespace e_Commerce_Test_Site.Models
{
    public static class Utility
    {
        public static string GetPreviousUrlRelative (HttpRequest request)
        {
            string previousUrlAbsolute = request.Headers.Referer.ToString();
            string previousUrl = "/";
            string host = request.Host.Value;
            if (previousUrlAbsolute.Contains(host)) previousUrl = previousUrlAbsolute.Substring(previousUrlAbsolute.IndexOf(host) + host.Length);
            return previousUrl;
        }
        public static int GetCartCount (HttpContext httpContext, StoreUserContext _context)
        {
            bool isauthenticated = httpContext.User.Identity!.IsAuthenticated;
            int count = 0;
            if (isauthenticated)
            {
                var username = httpContext.User.Identity!.Name;
                User aspnetusers = _context.Users.Where(a => a.UserName == username).Single() ?? new User();

                if (!(_context.Entry(aspnetusers).Reference("UserData").IsLoaded)) _context.Entry(aspnetusers).Reference("UserData").Load();
                UserData userData = aspnetusers.UserData ?? new UserData();
                if (!(_context.Entry(userData).Collection("CartItems").IsLoaded)) _context.Entry(userData).Collection("CartItems").Load();
                List<CartItems> cartitems = userData.CartItems!.ToList() ?? new List<CartItems>();

                foreach (CartItems item in cartitems)
                {
                    count += item.Quantity;
                }
                return count;
            }
            else
            {
                string? cart = httpContext.Session.GetString("cart");
                List<SessionCartDTO> cartitems;

                if (cart != null) cartitems = JsonSerializer.Deserialize<List<SessionCartDTO>>(cart) ?? new List<SessionCartDTO>();
                else cartitems = new List<SessionCartDTO>();

                foreach (SessionCartDTO item in cartitems)
                {
                    count += item.Quantity;
                }
                return count;
            }
        }

        public static async Task TransferSessionCart (HttpRequest request, int userDataId, StoreUserContext context)
        {
           string? cart = request.HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<SessionCartDTO> SessionCart = JsonSerializer.Deserialize<List<SessionCartDTO>>(cart) ?? new List<SessionCartDTO>();
                foreach(SessionCartDTO item in SessionCart)
                {
                    CartItems cartItems = new CartItems() { UserDataId = userDataId, ProductId = item.ProductId, Quantity = item.Quantity };
                    await context.CartItems.AddAsync(cartItems);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
