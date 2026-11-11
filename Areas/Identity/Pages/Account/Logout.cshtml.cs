#nullable disable
namespace ZekrHakeem_v11.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IActivityLogger _activity;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IActivityLogger activity)
        {
            _signInManager = signInManager;
            _logger = logger;
            _activity = activity;
        }


        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (uid != null) await _activity.LogAsync(ActivityActions.Logout, uid);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
