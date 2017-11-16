using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Homework_04.Providers;
using Homework_04.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Homework_04.Identity;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace Homework_04
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["issuer"]),
                AuthenticationType = DefaultAuthenticationTypes.ExternalBearer
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["issuer"];
            string audienceId = ConfigurationManager.AppSettings["audienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
