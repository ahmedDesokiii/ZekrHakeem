using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, RoleManager<IdentityRole> roleManager)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _roleManager = roleManager;
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
                RoleVM = await _roleManager.Roles.OrderBy(x => x.Name).ToListAsync()
            };

            return View(master);
        }
        #endregion

        #region Add|Edit Roles [Create & Update]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleFormViewModel model)
        {

            if (await _roleManager.RoleExistsAsync(model.Name))
            {
                SessionMsg(Helper.Error, "Exist Role !", "Role is exists !");
                return View("Index", await _roleManager.Roles.OrderBy(x => x.Name).ToListAsync());
            }

            if (!ModelState.IsValid)
            {
                if (model.RoleId == null)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));

                    if (result.Succeeded)// Succeeded 
                        SessionMsg(Helper.Success, "Done !", "Role Added Successfully");
                    else // Not Successeded
                        SessionMsg(Helper.Error, "Not Saved", "Role Not Added !");
                }
                return View("Index", await _roleManager.Roles.OrderBy(x => x.Name).ToListAsync());
            }
            else
            {
                var RoleUpdate = await _roleManager.FindByIdAsync(model.RoleId);
                RoleUpdate.Id = model.RoleId;
                RoleUpdate.Name = model.Name;
                var Result = await _roleManager.UpdateAsync(RoleUpdate);
                if (Result.Succeeded) // Succeeded
                    SessionMsg(Helper.Success, "Role Update", "Role is Updated Successfully");
                else  // Not Successeded
                    SessionMsg(Helper.Error, "Role Not Update", "Role isn't Updated !");
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
        public async Task<IActionResult> ManagePermissions(string roleId)
            {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = await _roleManager.FindByIdAsync(roleId);

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
                    RoleId = roleId,
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
