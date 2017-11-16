using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Homework_04.Models;

namespace Homework_04.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = context.OwinContext.Get<ApplicationDbContext>().Users.FirstOrDefault(u => u.UserName == context.UserName);
            if (!context.OwinContext.Get<ApplicationUserManager>().CheckPassword(user, context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var ticket = new AuthenticationTicket(SetClaimsIdentityAsync(context, user), null);
            context.Validated(ticket);

            return Task.FromResult<object>(user);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        private static  ClaimsIdentity SetClaimsIdentityAsync(OAuthGrantResourceOwnerCredentialsContext context, IdentityUser user)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
       
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));
            identity.AddClaim(new Claim("sub", context.UserName));

            var userRoles = context.OwinContext.Get<ApplicationUserManager>().GetRoles(user.Id);
            foreach (var role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }



            return identity;
        }
    }
}