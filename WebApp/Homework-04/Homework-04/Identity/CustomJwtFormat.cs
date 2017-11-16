using System;
using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Tokens;

using System.Text;

namespace Homework_04.Identity
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private static readonly byte[] _secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
        private readonly string _issuer;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            string audienceId = ConfigurationManager.AppSettings["audienceId"];

            string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["secret"];
            
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);
          
            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}