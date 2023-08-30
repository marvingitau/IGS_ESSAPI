using Microsoft.AspNetCore.Identity;
using RPFBE.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Auth
{
    public class ResetPasswordTokenProvider : TotpSecurityStampBasedTokenProvider<ApplicationUser>
    {
        public const string ProviderKey = "ResetPassword";

        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(false);
        }
    }
}
