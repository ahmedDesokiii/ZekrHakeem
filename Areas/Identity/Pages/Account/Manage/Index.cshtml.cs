// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

namespace ZekrHakeem_v11.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required, MaxLength(200)]
            public string FullName { get; set; } = string.Empty;
            [Required, MaxLength(200)]
            public string IdentityNum { get; set; } = string.Empty;
            [Phone, Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Required, MaxLength(20)]
            public string Gender { get; set; } = string.Empty;
            [Required, MaxLength(50)]
            public string Nationality { get; set; } = string.Empty;
            public string Adress { get; set; } = string.Empty;
            public byte[] ProfilePicture { get; set; }
            public string Notes { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FullName = user.FullName,
                IdentityNum = user.IdentityNum,
                PhoneNumber = phoneNumber,
                Gender = user.Gender,
                //Nationality = user.Nationality.Name,
                Nationality = user.Nationality,
                Adress = user.Adress,
                Notes = user.Notes,
                ProfilePicture = user.ProfilePicture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var fullName = user.FullName;
            var identityNumber = user.IdentityNum;
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var gender = user.Gender;
            var nationality = user.Nationality;
            var adress = user.Adress;
            var notes = user.Notes;
            var profilepicture = user.ProfilePicture;

            if (Input.FullName != fullName)
            {
                user.FullName = Input.FullName;
                await _signInManager.UserManager.UpdateAsync(user);
            }
            if (Input.IdentityNum != identityNumber)
            {
                user.IdentityNum = Input.IdentityNum;
                await _signInManager.UserManager.UpdateAsync(user);
            }
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.Gender != gender)
            {
                user.Gender = Input.Gender;
                await _signInManager.UserManager.UpdateAsync(user);
            }
            if (Input.Nationality != nationality)
            {
                user.Nationality = Input.Nationality;
                await _signInManager.UserManager.UpdateAsync(user);
            }
            if (Input.Adress != adress)
            {
                user.Adress = Input.Adress;
                await _signInManager.UserManager.UpdateAsync(user);
            }
            if (Input.Notes != notes)
            {
                user.Notes = Input.Notes;
                await _signInManager.UserManager.UpdateAsync(user);
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                //check file size and extension

                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }

                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
