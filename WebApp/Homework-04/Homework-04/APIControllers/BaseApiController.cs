using Homework_04.Identity;
using Homework_04.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PushBots.NET;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace Homework_04.Controllers
{
    public class BaseApiController : ApiController
    {

        private readonly PushBotsClient _pushBotsClient;
        private const string AppId = "5a0c68d49b823ae75c8b4568"; 
        private const string Secret = "2ea297eee9933ab16e23bbc28d1cf2c1"; 

        public BaseApiController()
        {
            _pushBotsClient = new PushBotsClient(AppId, Secret);
        }

        protected PushBotsClient PushBots_Client
        {
            get
            {
                return _pushBotsClient;
            }
        }
        private ApplicationUser _member;

       

        public string UserIdentityId
        {
            get
            {
                var user = UserManager.FindByName(User.Identity.Name);
                return user.Id;
            }
        }

        public ApplicationUser UserRecord
        {
            get
            {
                if (_member != null)
                {
                    return _member;
                }
                _member = UserManager.FindByEmail(Thread.CurrentPrincipal.Identity.Name);
                return _member;
            }
            set { _member = value; }
        }
        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ModelFactory _modelFactory;
        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.UserManager);
                }
                return _modelFactory;
            }
        }

        #region Helpers

        public IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

       public IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        public class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        public static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
