using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Movies.Web.Controllers
{
    public class BaseController : Controller
    {
        public HttpCookie CreateGuidCookie()
        {
            Guid guid = Guid.NewGuid();

            HttpCookie cookie = new HttpCookie("GuidToken");
            cookie.Value = guid.ToString();
            cookie.Expires = DateTime.Now.AddDays(30);

            return cookie;
        }

        public string CreateAccessToken()
        {
            string cookie = Request.Cookies["GuidToken"]?.Value?.ToString();

            var tokenExpiration = TimeSpan.FromDays(1);
            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim("userGuidId", cookie));
            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };
            var ticket = new AuthenticationTicket(identity, props);
            var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
            JObject tokenResponse = new JObject(
                new JProperty("access_token", accessToken),
                new JProperty("token_type", "bearer"));

            return tokenResponse["access_token"].ToString();
        }
    }
}