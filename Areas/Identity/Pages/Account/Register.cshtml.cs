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

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
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
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.FullName = Input.FullName;
                user.IdentityNum = Input.IdentityNum;
                user.PhoneNumber = Input.PhoneNumber;
                user.Gender = Input.Gender;

                // take user name from Email
                await _userStore.SetUserNameAsync(user, new MailAddress(Input.Email).User, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    //default role of user
                    if (Input.Email.Contains("@quranumluj.org.sa"))
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Teacher.ToString());
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Parent.ToString());
                    }

                    _logger.LogInformation("User created a new account with password.");


                    // await _emailSender.SendEmailAsync(Input.Email, "Account Created", "Congratulations your account has been Successfully created");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);


                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"فضلا لتأكيد الدخول للتطبيق رجاء الرابط :  <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اضغط هنا لتأكيد الدخول!</a>.");


                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: true);
                        //_userManager.Options.SignIn.RequireConfirmedEmail = true;
                        return RedirectToPage("تم تسجيل الدخول بنجاح", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
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
