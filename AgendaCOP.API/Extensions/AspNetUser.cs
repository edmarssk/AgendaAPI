using AgendaCOP.Business.Interfaces.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AgendaCOP.API.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor acessor)
        {
            _accessor = acessor;
        }
        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }
        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetEmail() : String.Empty;
        }
       
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

    }

    public static class ClaimsExtensionsIdentity
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if(claimsPrincipal == null)
            {
                throw new ArgumentException(nameof(claimsPrincipal));
            }

            var claim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentException(nameof(claimsPrincipal));
            }

            var claim = claimsPrincipal.FindFirst(ClaimTypes.Email);

            return claim?.Value;
        }
    }
}
