using Microsoft.AspNetCore.Authentication;

namespace ZekrHakeem_v11.Services.Security
{
    /// <summary>يمنع الحسابات الموقوفة (IsActive = false) من تسجيل الدخول</summary>
    public class AppSignInManager : SignInManager<ApplicationUser>
    {
        public AppSignInManager(UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<bool> CanSignInAsync(ApplicationUser user)
        {
            if (!user.IsActive) return false;
            return await base.CanSignInAsync(user);
        }

        public override async Task<ApplicationUser?> ValidateSecurityStampAsync(ClaimsPrincipal? principal)
        {
            var user = await base.ValidateSecurityStampAsync(principal);
            return user != null && user.IsActive ? user : null;
        }
    }
}
