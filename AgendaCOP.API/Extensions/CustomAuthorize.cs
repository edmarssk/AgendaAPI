using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgendaCOP.API.Extensions
{
    public class CustomAuthorize
    {

        public static bool ValidarClainUsuario(HttpContext httpContext, string claimName, string claimValue)
        {
            return httpContext.User.Identity.IsAuthenticated &&
                httpContext.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

    }

    public class ClaimsAuthotizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthotizeAttribute(string claimName, string claimValue) : base(typeof(RequesitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class RequesitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequesitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
            if (!CustomAuthorize.ValidarClainUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
