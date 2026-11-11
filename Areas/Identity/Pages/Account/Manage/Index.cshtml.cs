#nullable disable

namespace ZekrHakeem_v11.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private const long MaxPictureBytes = 2 * 1024 * 1024;
        private static readonly string[] AllowedTypes = { "image/jpeg", "image/png", "image/webp", "image/gif" };

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly IActivityLogger _activity;

        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                          ApplicationDbContext db, IActivityLogger activity)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _activity = activity;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public bool HasPicture { get; set; }
        public IList<string> RoleNames { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public IFormFile Picture { get; set; }

        [BindProperty]
        public bool RemovePicture { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "الاسم الكامل مطلوب"), MaxLength(200), MinLength(3, ErrorMessage = "3 حروف على الأقل")]
            public string FullName { get; set; } = string.Empty;
            [Required(ErrorMessage = "رقم الهوية مطلوب"), MaxLength(30)]
            public string IdentityNum { get; set; } = string.Empty;
            [Phone(ErrorMessage = "رقم جوال غير صحيح")]
            public string PhoneNumber { get; set; }
            [MaxLength(20)]
            public string Gender { get; set; } = string.Empty;
            [MaxLength(50)]
            public string Nationality { get; set; } = string.Empty;
            [MaxLength(300)]
            public string Adress { get; set; } = string.Empty;
            [MaxLength(1000)]
            public string Notes { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Username = user.UserName;
            Email = user.Email;
            UserId = user.Id;
            HasPicture = user.ProfilePicture != null;
            CreatedAt = user.CreatedAt;
            var roles = await _userManager.GetRolesAsync(user);
            RoleNames = await (from r in _db.Roles
                               join i in _db.RoleInfos on r.Id equals i.RoleId into gi
                               from i in gi.DefaultIfEmpty()
                               where roles.Contains(r.Name)
                               select i != null && i.NameAr != null ? i.NameAr : r.Name).ToListAsync();
            Input ??= new InputModel
            {
                FullName = user.FullName,
                IdentityNum = user.IdentityNum,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Nationality = user.Nationality,
                Adress = user.Adress,
                Notes = user.Notes
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var identity = Input.IdentityNum?.Trim();
            if (!string.IsNullOrEmpty(identity) && await _db.Users.AnyAsync(u => u.Id != user.Id && u.IdentityNum == identity))
                ModelState.AddModelError("Input.IdentityNum", "رقم الهوية مسجل لحساب آخر");

            if (Picture != null)
            {
                if (Picture.Length > MaxPictureBytes)
                    ModelState.AddModelError(nameof(Picture), "حجم الصورة يجب ألا يتجاوز 2 ميجابايت");
                else if (!AllowedTypes.Contains(Picture.ContentType?.ToLowerInvariant()))
                    ModelState.AddModelError(nameof(Picture), "الصيغ المسموحة: JPG, PNG, WEBP, GIF");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FullName = Input.FullName.Trim();
            user.IdentityNum = identity;
            user.PhoneNumber = Input.PhoneNumber?.Trim();
            user.Gender = Input.Gender ?? string.Empty;
            user.Nationality = Input.Nationality;
            user.Adress = Input.Adress;
            user.Notes = Input.Notes;

            if (RemovePicture) user.ProfilePicture = null;
            if (Picture != null)
            {
                using var ms = new MemoryStream();
                await Picture.CopyToAsync(ms);
                user.ProfilePicture = ms.ToArray();
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError(string.Empty, UserAdminService.TranslateIdentityError(e));
                await LoadAsync(user);
                return Page();
            }

            await _activity.LogAsync(ActivityActions.ProfileUpdated, user.Id);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "تم حفظ بياناتك بنجاح";
            return RedirectToPage();
        }
    }
}
