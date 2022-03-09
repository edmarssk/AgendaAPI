using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.ViewModel
{
    public class UserTokenVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimsVM> Claims { get; set; }
    }

    public class LoginResponseVM
    {
        public string AccessToken { get; set; }
        public double ExpireIn { get; set; }
        public UserTokenVM UserToken { get; set; }
    }

    public class ClaimsVM
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

}
