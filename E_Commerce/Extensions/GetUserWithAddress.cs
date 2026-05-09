using E_Commerece.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Extensions
{
    public static class GetUserWithAddress
    {
        public static AppUser GetUserAddress(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            
            var currentUser= userManager.Users.Include(U=>U.Email).FirstOrDefault(U=>U.Email==email);
            return currentUser;

        }
    }
}
