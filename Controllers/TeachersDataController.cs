namespace ZekrHakeem_v11.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeachersDataController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public TeachersDataController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        #region All Users
        public IActionResult Index()
        {

            var users = _context.Users
                .Select(user => new UserViewModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    IdentityNum = user.IdentityNum,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = _userManager.GetRolesAsync(user).Result,
                })
                .ToList();
            var teachersMen = users.Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString())).ToList();
            var teachersWomen = users.Where(x => x.Roles.Select(role => role).Contains(Roles.TeacherWomen.ToString())).ToList();
            var teachers = teachersMen;
            teachers.AddRange(teachersWomen);
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TeacherViewModel tvm = new()
            {
                // AllTeachers = _servicesOfTeacher.GetAllTeachers(),
                AllTeachers = teachers
                //TeachersMen = teachersMen,
                //TeachersWomen = teachersWomen

            };


            //Thread.Sleep(1000);
            return View(users);
        }
        #endregion
















        #region User Process [Add & Update]
        public async Task<IActionResult> AddUser(string userId)
        {
            var roles = await _roleManager.Roles.Select(r => new CheckBoxViewModel { RoleId = r.Id, DisplayValue = r.Name }).ToListAsync();

            var viewModel = new AddUserViewModel
            {
                Roles = roles
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "please , select at least one role");
                return View(model);
            }

            if (await _userManager.FindByNameAsync(model.IdentityNum) != null)
            {
                ModelState.AddModelError("IdentityNum", "this Identity Number already exists");
                return View(model);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "this email already exists");
                return View(model);
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "this User Name already exists");
                return View(model);
            }

            var user = new ApplicationUser()
            {
                FullName = model.FullName,
                IdentityNum = model.IdentityNum,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }
            await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();



            var viewModel = new ProfileFormViewModel
            {
                Id = userId,
                FullName = user.FullName,
                IdentityNum = user.IdentityNum,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(ProfileFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail != null && userWithSameEmail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "This email already assigned to another user");
                return View(model);
            }
            var userWithSameIdentityNum = await _userManager.FindByEmailAsync(model.IdentityNum);

            if (userWithSameIdentityNum != null && userWithSameIdentityNum.Id != model.Id)
            {
                ModelState.AddModelError("IdentityNum", "This identity number already assigned to another user");
                return View(model);
            }
            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);

            if (userWithSameUserName != null && userWithSameUserName.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "This username already assigned to another user");
                return View(model);
            }

            user.FullName = model.FullName;
            user.IdentityNum = model.IdentityNum;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.UserName = model.UserName;

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Roles Process [Manage & Update]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new CheckBoxViewModel
                {
                    RoleId = role.Id,
                    DisplayValue = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.DisplayValue) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.DisplayValue);

                if (!userRoles.Any(r => r == role.DisplayValue) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.DisplayValue);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete User
        //public IActionResult Delete(Guid Id)
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (_servicesUser.Delete(Id))
        //        return RedirectToAction(nameof(ApplicationUsers));
        //    return RedirectToAction(nameof(ApplicationUsers));

        //}
        #endregion
    }
}