using DocumentFormat.OpenXml.Spreadsheet;
using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RolesController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _roleManager = roleManager;
            _context = context;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #region List Of Roles [View]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                DemandVM = new()
                {
                    NewDemand = new Demand(),
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
                },
            //    RoleVM = await _roleManager.Roles.OrderBy(x => x.Name).ToListAsync()
            };

            // جلب كل المجموعات
            var roles = await _context.Roles.OrderBy(x=>x.Name).ToListAsync();
            master.RoleVM = roles;
            master.AddRoleVM = new RoleFormViewModel();
            return View(master);
        }
        #endregion

        #region Add|Edit Roles [Create & Update]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(MasterViewModel model)
       {

            if (await _roleManager.RoleExistsAsync(model.AddRoleVM.Name))
                SessionMsg(Helper.Error, "مجموعة مكررة!", "اسم المجموعة موجود من قبل !");

            else if (model.AddRoleVM.RoleId == null) // Create
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.AddRoleVM.Name.Trim()));

                if (result.Succeeded)// Succeeded 
                    SessionMsg(Helper.Success, "تم الاضافة !", "تم اضافة مجموعة المستخدم بنجاح !");
                else // Not Successeded
                    SessionMsg(Helper.Error, "خطأ", "حدث خطأ اثناء حفظ المعلومات !");
            }
            else // Update
            {
                var RoleUpdate = await _roleManager.FindByIdAsync(model.AddRoleVM.RoleId);
                RoleUpdate.Id = model.AddRoleVM.RoleId;
                RoleUpdate.Name = model.AddRoleVM.Name;
                var Result = await _roleManager.UpdateAsync(RoleUpdate);
                if (Result.Succeeded) // Succeeded
                    SessionMsg(Helper.Success, "تم التعديل", "تم تعديل بيانات المجموعة بنجاح !");
                else  // Not Successeded
                    SessionMsg(Helper.Error, "خطأ ", "حدث خأ اثناء تعديل الحلقة !");
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete Role [Delete]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);

            if (role == null)
                { return View("Error"); }

            if ((await _roleManager.DeleteAsync(role)).Succeeded)
                return RedirectToAction(nameof(Index));

            return RedirectToAction("Index");
        }
        #endregion

        #region Manage Permissions
        public async Task<IActionResult> ManagePermissions(string Id)
            {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = await _roleManager.FindByIdAsync(Id);

            if (role == null)
                return NotFound();

            var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
            var allClaims = Permissions.GenerateAllPermissions();
            var allPermissions = allClaims.Select(p => new CheckBoxViewModel { DisplayValue = p }).ToList();

            foreach (var permission in allPermissions)
            {
                if (roleClaims.Any(c => c == permission.DisplayValue))
                    permission.IsSelected = true;
            }

            MasterViewModel master = new()
            {
                PermissionVM = new()
                {
                    RoleId = Id,
                    RoleName = role.Name,
                    RoleCalims = allPermissions
                },
                DemandVM = new()
                {
                    NewDemand = new Demand(),
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
                }
            };


            return View(master);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePermissions(MasterViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.PermissionVM.RoleId);

            if (role == null)
                return NotFound();

            var roleClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in roleClaims)
                await _roleManager.RemoveClaimAsync(role, claim);

            var selectedClaims = model.PermissionVM.RoleCalims.Where(c => c.IsSelected).ToList();

            foreach (var claim in selectedClaims)
                await _roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayValue));

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
