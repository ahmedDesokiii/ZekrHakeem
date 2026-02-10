using ZekrHakeem_v11.IRepository.ServicesRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public UsersController(IServicesDemand servicesOfDemand,
                               IServicesRepository<Demand> servicesDemand,
                               UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               SignInManager<ApplicationUser> signInManager,
                               ApplicationDbContext context)
        {
            _servicesOfDemand = servicesOfDemand;
            _servicesDemand = servicesDemand;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        #region Index / All Users
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                DemandVM = new DemandViewModel
                {
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                    NewDemand = new Demand(),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
                }
            };

            master.UserVM = _context.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    IdentityNum = u.IdentityNum,
                    PhoneNumber = u.PhoneNumber,
                    UserName = u.UserName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToList();

            Thread.Sleep(1000); // لو تحب تشيلها لزيادة السرعة
            return View(master);
        }
        #endregion

        #region Add User
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            var roles = await _roleManager.Roles
                .Select(r => new CheckBoxViewModel
                {
                    RoleId = r.Id,
                    DisplayValue = r.Name
                }).ToListAsync();

            var model = new MasterViewModel
            {
                AddUserVM = new AddUserViewModel
                {
                    Roles = roles
                }
            };

            return PartialView("_UserModalPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await ReloadRoles(model);
                return PartialView("_UserModalPartial", model);
            }

            if (!model.AddUserVM.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("AddUserVM.Roles", "Please select at least one role.");
                await ReloadRoles(model);
                return PartialView("_UserModalPartial", model);
            }

            if (await _userManager.FindByEmailAsync(model.AddUserVM.Email) != null)
            {
                ModelState.AddModelError("AddUserVM.Email", "Email already exists.");
                await ReloadRoles(model);
                return PartialView("_UserModalPartial", model);
            }

            if (await _userManager.FindByNameAsync(model.AddUserVM.UserName) != null)
            {
                ModelState.AddModelError("AddUserVM.UserName", "Username already exists.");
                await ReloadRoles(model);
                return PartialView("_UserModalPartial", model);
            }

            var user = new ApplicationUser
            {
                FullName = model.AddUserVM.FullName,
                IdentityNum = model.AddUserVM.IdentityNum,
                PhoneNumber = model.AddUserVM.PhoneNumber,
                Email = model.AddUserVM.Email,
                UserName = model.AddUserVM.UserName
            };

            var result = await _userManager.CreateAsync(user, model.AddUserVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                await ReloadRoles(model);
                return PartialView("_UserModalPartial", model);
            }

            await _userManager.AddToRolesAsync(user,
                model.AddUserVM.Roles.Where(r => r.IsSelected)
                                     .Select(r => r.DisplayValue));

            _context.Add(user);
            // Response for AJAX
            return Json(new { success = true });
        }
        #endregion

        #region Edit User
        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles
                .Select(r => new CheckBoxViewModel
                {
                    RoleId = r.Id,
                    DisplayValue = r.Name,
                    IsSelected = userRoles.Contains(r.Name)
                }).ToListAsync();

            var model = new MasterViewModel
            {
                AddUserVM = new AddUserViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    IdentityNum = user.IdentityNum,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles
                }
            };

            return PartialView("_UserModalPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(MasterViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_UserModalPartial", model);

            var user = await _userManager.FindByIdAsync(model.AddUserVM.Id);
            if (user == null) return NotFound();

            // Validation uniqueness
            if (await _userManager.Users.AnyAsync(u => u.Email == model.AddUserVM.Email && u.Id != user.Id))
            {
                ModelState.AddModelError("AddUserVM.Email", "Email already exists.");
                return PartialView("_UserModalPartial", model);
            }

            if (await _userManager.Users.AnyAsync(u => u.UserName == model.AddUserVM.UserName && u.Id != user.Id))
            {
                ModelState.AddModelError("AddUserVM.UserName", "Username already exists.");
                return PartialView("_UserModalPartial", model);
            }

            // Update user
            user.FullName = model.AddUserVM.FullName;
            user.IdentityNum = model.AddUserVM.IdentityNum;
            user.PhoneNumber = model.AddUserVM.PhoneNumber;
            user.Email = model.AddUserVM.Email;
            user.UserName = model.AddUserVM.UserName;

            await _userManager.UpdateAsync(user);

            // Update roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRolesAsync(user,
                model.AddUserVM.Roles.Where(r => r.IsSelected).Select(r => r.DisplayValue));

            return Json(new { success = true });
        }
        #endregion

        #region Helpers
        private async Task ReloadRoles(MasterViewModel model)
        {
            model.AddUserVM.Roles = await _roleManager.Roles
                .Select(r => new CheckBoxViewModel
                {
                    RoleId = r.Id,
                    DisplayValue = r.Name,
                    IsSelected = model.AddUserVM.Roles.Any(x => x.DisplayValue == r.Name && x.IsSelected)
                }).ToListAsync();
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