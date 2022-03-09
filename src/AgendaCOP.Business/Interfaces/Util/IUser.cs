using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
namespace AgendaCOP.Business.Interfaces.Util
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();

    }
}
