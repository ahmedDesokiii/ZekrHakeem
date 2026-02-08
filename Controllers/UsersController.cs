using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UsersController(IServicesDemand servicesOfDemand, IServicesRepository<Demand> servicesDemand, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _servicesOfDemand = servicesOfDemand;
            _servicesDemand = servicesDemand;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        #region All Users
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new();
            master.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                NewDemand = new Demand(),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            master.UserVM = new List<UserViewModel>();
            master.UserVM = _context.Users
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
            Thread.Sleep(1000);
            return View(master);
        }
        #endregion

        #region User Process [Add & Update]
        public async Task<IActionResult> AddUser(string userId)
        {
            var roles = await _roleManager.Roles.Select(r => new CheckBoxViewModel { RoleId = r.Id, DisplayValue = r.Name }).ToListAsync();
            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new();
            master.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(usrId),
                NewDemand = new Demand(),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(usrId),
            };
            master.AddUserVM = new AddUserViewModel
            {
                Roles = roles
            };

            return View(master);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(MasterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!model.AddUserVM.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "please , select at least one role");
                return View(model);
            }

            if (await _userManager.FindByNameAsync(model.AddUserVM.IdentityNum) != null)
            {
                ModelState.AddModelError("IdentityNum", "this Identity Number already exists");
                return View(model);
            }

            if (await _userManager.FindByEmailAsync(model.AddUserVM.Email) != null)
            {
                ModelState.AddModelError("Email", "this email already exists");
                return View(model);
            }

            if (await _userManager.FindByNameAsync(model.AddUserVM.UserName) != null)
            {
                ModelState.AddModelError("UserName", "this User Name already exists");
                return View(model);
            }

            var user = new ApplicationUser()
            {
                FullName = model.AddUserVM.FullName,
                IdentityNum = model.AddUserVM.IdentityNum,
                PhoneNumber = model.AddUserVM.PhoneNumber,
                UserName = model.AddUserVM.UserName,
                Email = model.AddUserVM.Email
            };
            var result = await _userManager.CreateAsync(user, model.AddUserVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }
            await _userManager.AddToRolesAsync(user, model.AddUserVM.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (user == null)
                return NotFound();

            MasterViewModel master = new();
            master.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(usrId),
                NewDemand = new Demand(),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(usrId),
            };
            master.ProfileFormVM = new ProfileFormViewModel
            {
                Id = userId,
                FullName = user.FullName,
                IdentityNum = user.IdentityNum,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(master);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(MasterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.ProfileFormVM.Id);

            if (user == null)
                return NotFound();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.ProfileFormVM.Email);

            if (userWithSameEmail != null && userWithSameEmail.Id != model.ProfileFormVM.Id)
            {
                ModelState.AddModelError("Email", "This email already assigned to another user");
                return View(model);
            }
            var userWithSameIdentityNum = await _userManager.FindByEmailAsync(model.ProfileFormVM.IdentityNum);

            if (userWithSameIdentityNum != null && userWithSameIdentityNum.Id != model.ProfileFormVM.Id)
            {
                ModelState.AddModelError("IdentityNum", "This identity number already assigned to another user");
                return View(model);
            }
            var userWithSameUserName = await _userManager.FindByNameAsync(model.ProfileFormVM.UserName);

            if (userWithSameUserName != null && userWithSameUserName.Id != model.ProfileFormVM.Id)
            {
                ModelState.AddModelError("UserName", "This username already assigned to another user");
                return View(model);
            }

            user.FullName = model.ProfileFormVM.FullName;
            user.IdentityNum = model.ProfileFormVM.IdentityNum;
            user.PhoneNumber = model.ProfileFormVM.PhoneNumber;
            user.Email = model.ProfileFormVM.Email;
            user.UserName = model.ProfileFormVM.UserName;

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

            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new();
            master.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(usrId),
                NewDemand = new Demand(),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(usrId),
            };
            master.UserRolesVM = new UserRolesViewModel
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

            return View(master);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(MasterViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserRolesVM.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.UserRolesVM.Roles)
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
        public async Task<IActionResult> UpdateRoles(MasterViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserRolesVM.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, model.UserRolesVM.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));

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