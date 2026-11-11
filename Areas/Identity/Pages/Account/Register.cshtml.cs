#nullable disable
namespace ZekrHakeem_v11.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;
        private readonly IActivityLogger _activity;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext db,
            IActivityLogger activity)
        {
            _db = db;
            _activity = activity;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 

            [Required(ErrorMessage = "ادخل اسم المستخدم بشكل صحيح")]
            [MaxLength(200, ErrorMessage = "عدد حروف الإسم لا تتعدي 200 حرف ")]
            [MinLength(3, ErrorMessage = "عدد حروف الإسم لا تقل 3 حرف ")]
            public string FullName { get; set; } = string.Empty;
            [Required(ErrorMessage = "ادخل هوية المعلم بشكل صحيح")]
            [MaxLength(30, ErrorMessage = "عدد حروف هوية المستخدم لا تتعدي 30 خانات ")]
            [MinLength(8, ErrorMessage = "عدد حروف هوية المستخدم لا تقل 8 خانات ")]
            public string IdentityNum { get; set; } = string.Empty;

            [Phone(ErrorMessage = "برجاء ادخال رقم جوال صحيح")]
            [Required(ErrorMessage = " ادخل جوال المعلم بشكل صحيح ")]
            [MaxLength(30, ErrorMessage = "عدد حروف جوال المستخدم لا تتعدي 30 خانة ")]
            [MinLength(6, ErrorMessage = "عدد حروف جوال المستخدم لا تقل 6 خانات ")]
            public string PhoneNumber { get; set; }

            //[Required(ErrorMessage = " حدد جنس المعلم "), MaxLength(20)]

            public string Gender { get; set; }

            [Required(ErrorMessage = " ادخل الايميل بشكل صحيح ")]
            [EmailAddress(ErrorMessage = "الشكل المطلوب للايميل : [name@example.com]")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = " ادخل كلمة بشكل صحيح ")]
            [MinLength(6, ErrorMessage = "ادخل كلمة المرور حروف أو أرقام ")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$",
                ErrorMessage = "كلمة المرور  لا تقل عن 6 خانات تتكون من علي الأقل ( حرف كبير , حرف صغير , رقم , رمز مثل $ ) ")]

            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = " أعد كلمة بشكل صحيح ")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "إعادة كلمة المرور غير متطابف مع كلمة المرور")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (!ModelState.IsValid) return Page();

            var email = Input.Email.Trim();
            var identity = Input.IdentityNum.Trim();

            if (await _userManager.FindByEmailAsync(email) != null)
                ModelState.AddModelError("Input.Email", "البريد الإلكتروني مسجل من قبل — جرّب تسجيل الدخول أو استعادة كلمة المرور");
            if (await _db.Users.AnyAsync(u => u.IdentityNum == identity))
                ModelState.AddModelError("Input.IdentityNum", "رقم الهوية مسجل لحساب آخر");
            if (!ModelState.IsValid) return Page();

            var user = CreateUser();
            user.FullName = Input.FullName.Trim();
            user.IdentityNum = identity;
            user.PhoneNumber = Input.PhoneNumber?.Trim();
            user.Gender = Input.Gender ?? string.Empty;
            user.CreatedAt = DateTime.UtcNow;
            user.IsActive = true;

            await _userStore.SetUserNameAsync(user, await BuildUniqueUserNameAsync(email), CancellationToken.None);
            await _emailStore.SetEmailAsync(user, email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user, Input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, UserAdminService.TranslateIdentityError(error));
                return Page();
            }

            // المجموعة الافتراضية: معلم لبريد الجمعية، وإلا المجموعة المحددة كافتراضية في شاشة المجموعات
            var roleName = email.EndsWith("@quranumluj.org.sa", StringComparison.OrdinalIgnoreCase)
                ? Roles.Teacher.ToString()
                : await (from i in _db.RoleInfos
                         join r in _db.Roles on i.RoleId equals r.Id
                         where i.IsDefaultForRegistration
                         select r.Name).FirstOrDefaultAsync() ?? Roles.Parent.ToString();
            await _userManager.AddToRoleAsync(user, roleName);

            _logger.LogInformation("User created a new account with password.");
            await _activity.LogAsync(ActivityActions.Register, user.Id, roleName);

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null,
                values: new { area = "Identity", userId, code, returnUrl }, protocol: Request.Scheme);

            try
            {
                await _emailSender.SendEmailAsync(email, "تأكيد حسابك في الذكر الحكيم",
                    $"<div dir='rtl' style='font-family:Tahoma'>مرحبًا {HtmlEncoder.Default.Encode(user.FullName)}،<br/>لتفعيل حسابك اضغط على الرابط: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>تأكيد البريد الإلكتروني</a></div>");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to send confirmation email to {Email}", email);
            }

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
                return RedirectToPage("RegisterConfirmation", new { email, returnUrl });

            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }

        private async Task<string> BuildUniqueUserNameAsync(string email)
        {
            var local = new MailAddress(email).User;
            var baseName = new string(local.Where(c => char.IsAsciiLetterOrDigit(c) || c is '.' or '_' or '-').ToArray());
            if (baseName.Length < 3) baseName = "user" + baseName;
            var name = baseName;
            var i = 1;
            while (await _userManager.FindByNameAsync(name) != null) name = $"{baseName}{++i}";
            return name;
        }

        private ApplicationUser CreateUser()
        {
            try
            {

                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
        //private IUserEmailStore<ApplicationUser> GetIdentityNumStore()
        //{
        //    if (!_userManager.SupportsUserPhoneNumber)
        //    {
        //        throw new NotSupportedException("The default UI requires a user store with email support.");
        //    }
        //    return (IUserEmailStore<ApplicationUser>)_userStore;
        //}
    }
}
