using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.Claims;
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

        public void SessionMsg(string type, string title, string message)
        {
            TempData["MsgType"] = type;
            TempData["MsgTitle"] = title;
            TempData["Msg"] = message;
        }

        #region Index / All Users
        public async Task<IActionResult> Index()
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

            // جلب كل المستخدمين
            var users = await _context.Users.ToListAsync();

            var userVMs = new List<UserViewModel>();

            foreach (var u in users)
            {
                // جلب كل الأدوار للمستخدم الحالي
                var roles = await _userManager.GetRolesAsync(u);

                // ترتيب الأدوار أبجدياً
                var sortedRoles = roles.OrderBy(r => r).ToList();

                userVMs.Add(new UserViewModel
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    IdentityNum = u.IdentityNum,
                    PhoneNumber = u.PhoneNumber,
                    UserName = u.UserName,
                    Email = u.Email,
                    Roles = sortedRoles
                });
            }

            // ترتيب المستخدمين حسب أول دور عندهم (أبجدياً)
            var orderedUsers = userVMs
                .OrderBy(vm => vm.Roles.FirstOrDefault() ?? string.Empty)  // لو ما عندوش دور، اعتبره فارغ
                .ThenBy(vm => vm.FullName) // ترتيب ثانوي حسب الاسم الكامل
                .ToList();
            master.UserVM = orderedUsers;

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
                        Gender = user.Gender,
                        Roles = roles
                    }
             

                };

                return PartialView("_UserModalPartial", model);
            }
        #endregion

        #region Save User [Add / Edit] Post Action
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Save(MasterViewModel model)
            {
                // ===========================
                // Create
                // ===========================
                if (model.AddUserVM.Id == null)
                {
                    var existUser = await _userManager.FindByNameAsync(model.AddUserVM.Email);

                    if (existUser != null)
                    {
                        SessionMsg(Helper.Error, "مستخدم مكرر", "البريد الإلكتروني مستخدم من قبل");
                        return RedirectToAction("Index");
                    }

                    var user = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.AddUserVM.UserName,
                        Email = model.AddUserVM.Email,
                        FullName = model.AddUserVM.FullName,
                        PhoneNumber = model.AddUserVM.PhoneNumber,
                        IdentityNum = model.AddUserVM.IdentityNum,
                        Gender = model.AddUserVM.Gender
                    };

                    var result = await _userManager.CreateAsync(user, model.AddUserVM.Password);

                    if (result.Succeeded)
                    {
                        ////  نخلي الايميل confirmed
                        //user.EmailConfirmed = true;
                        //await _userManager.UpdateAsync(user);


                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        await _userManager.ConfirmEmailAsync(user, token);

                        // Roles
                        var selectedRoles = model.AddUserVM.Roles
                            .Where(r => r.IsSelected)
                            .Select(r => r.DisplayValue);

                        await _userManager.AddToRolesAsync(user, selectedRoles);

                    TempData["MsgType"] = "success";
                    TempData["MsgTitle"] = "تم الإضافة";
                    TempData["Msg"] = "تم إضافة المستخدم بنجاح";

                }
                else
                    {
                        SessionMsg(Helper.Error, "خطأ", string.Join(",", result.Errors.Select(e => e.Description)));
                        foreach (var error in result.Errors)
                            ModelState.AddModelError("", error.Description);

                        model.IsUserModalOpen = true;  // 👈 خليه يفضل مفتوح
                        return View("Users", model);
                    }
                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }

                // ===========================
                // Update
                // ===========================
                else
                {
                    var user = await _userManager.FindByIdAsync(model.AddUserVM.Id);

                    if (user == null)
                    {
                        SessionMsg(Helper.Error, "خطأ", "المستخدم غير موجود");
                        return RedirectToAction("Index");
                    }

                    user.FullName = model.AddUserVM.FullName;
                    user.PhoneNumber = model.AddUserVM.PhoneNumber;
                    user.Email = model.AddUserVM.Email;
                    user.UserName = model.AddUserVM.UserName;
                    user.IdentityNum = model.AddUserVM.IdentityNum;
                    user.Gender = model.AddUserVM.Gender;
                    var updateResult = await _userManager.UpdateAsync(user);

                    if (updateResult.Succeeded)
                    {
                        // Update Roles
                        var currentRoles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);

                        var selectedRoles = model.AddUserVM.Roles
                            .Where(r => r.IsSelected)
                            .Select(r => r.DisplayValue);

                        await _userManager.AddToRolesAsync(user, selectedRoles);
                    TempData["MsgType"] = "success";
                    TempData["MsgTitle"] = "تم التعديل";
                    TempData["Msg"] = "تم تعديل بيانات المستخدم بنجاح";

                }
                else
                {
                    SessionMsg(Helper.Error, "خطأ", "حدث خطأ أثناء التعديل");
                }
                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }
        }
        #endregion

        #region Helpers Reload Roles
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
            if (user == null) return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();
            string usrId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MasterViewModel master = new()
            {
                DemandVM = new()
                {
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(usrId),
                    NewDemand = new Demand(),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(usrId),
                }
            };

            var rolesList = new List<CheckBoxViewModel>();
            foreach (var role in roles)
            {
                bool isSelected = await _userManager.IsInRoleAsync(user, role.Name);
                rolesList.Add(new CheckBoxViewModel
                {
                    RoleId = role.Id,
                    DisplayValue = role.Name,
                    IsSelected = isSelected
                });
            }

            master.UserRolesVM = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Roles = rolesList
            };

            return PartialView("_ManageRolesModal", master);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(MasterViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserRolesVM.UserId);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.UserRolesVM.Roles)
            {
                if (userRoles.Contains(role.DisplayValue) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.DisplayValue);

                if (!userRoles.Contains(role.DisplayValue) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.DisplayValue);
            }

            return Json(new
            {
                success = true,
                message = "تم تحديث الصلاحيات بنجاح",
                userId = model.UserRolesVM.UserId,
                updatedRoles = model.UserRolesVM.Roles
                         .Where(r => r.IsSelected)
                         .Select(r => r.DisplayValue)
                         .ToList()
            });

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