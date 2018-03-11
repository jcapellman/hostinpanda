using System;
using System.Linq;
using System.Security.Claims;

namespace hostinpanda.web.Common
{
    public class HostinUser : ClaimsPrincipal
    {
        public HostinUser(ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
        }

        public int? ID
        {
            get
            {

                var userClaim = Claims.FirstOrDefault();

                if (userClaim == null)
                {
                    return null;
                }

                return (int?)Convert.ToInt32(userClaim.Value);
            }
        }
    }
}