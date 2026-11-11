#nullable disable

namespace ZekrHakeem_v11.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IActivityLogger _activity;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                          IActivityLogger activity, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _activity = activity;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "ادخل اسم المستخدم أو البريد الإلكتروني")]
            public string Email { get; set; }

            [Required(ErrorMessage = "ادخل كلمة المرور")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "تذكرني")]
            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true && string.IsNullOrEmpty(ErrorMessage))
                return LocalRedirect(Url.Content("~/"));

            if (!string.IsNullOrEmpty(ErrorMessage))
                ModelState.AddModelError(string.Empty, ErrorMessage);

            returnUrl ??= Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;

            if (!ModelState.IsValid) return Page();

            // الدخول باسم المستخدم أو البريد الإلكتروني
            var login = Input.Email.Trim();
            var user = await _userManager.FindByNameAsync(login)
                       ?? (login.Contains('@') ? await _userManager.FindByEmailAsync(login) : null);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "اسم المستخدم أو كلمة المرور غير صحيحة");
                return Page();
            }

            if (!user.IsActive)
            {
                await _activity.LogAsync(ActivityActions.LoginFailed, user.Id, "محاولة دخول لحساب موقوف");
                ModelState.AddModelError(string.Empty, "هذا الحساب موقوف. تواصل مع إدارة الجمعية لإعادة تفعيله.");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                user.LastLoginAt = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
                await _activity.LogAsync(ActivityActions.Login, user.Id);
                _logger.LogInformation("User {User} logged in.", user.UserName);
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            if (result.IsLockedOut)
            {
                await _activity.LogAsync(ActivityActions.LoginFailed, user.Id, "تم قفل الحساب مؤقتًا بسبب محاولات متكررة");
                return RedirectToPage("./Lockout");
            }
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "يجب تأكيد البريد الإلكتروني أولاً. راجع بريدك أو اطلب إعادة إرسال رسالة التأكيد.");
                return Page();
            }

            await _activity.LogAsync(ActivityActions.LoginFailed, user.Id, "كلمة مرور غير صحيحة");
            var left = _userManager.Options.Lockout.MaxFailedAccessAttempts - await _userManager.GetAccessFailedCountAsync(user);
            ModelState.AddModelError(string.Empty, left is > 0 and <= 3
                ? $"اسم المستخدم أو كلمة المرور غير صحيحة — متبقي {left} محاولات قبل القفل المؤقت"
                : "اسم المستخدم أو كلمة المرور غير صحيحة");
            return Page();
        }
    }
}
