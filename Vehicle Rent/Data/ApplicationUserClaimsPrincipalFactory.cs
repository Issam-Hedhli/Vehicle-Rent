using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe.Terminal;
using System.Security.Claims;
using Vehicle_Rent.Models;

namespace Vehicle_Rent.Data
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User , IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserName", user.UserName ?? ""));
            identity.AddClaim(new Claim("Image", user.Image ?? ""));
            identity.AddClaim(new Claim("Id", user.Id ?? ""));
            return identity;
        }
    }
}

