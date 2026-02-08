using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class CentersController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesRepository<Center> _servicesCenter;
        private readonly IServicesRepositoryLog<LogCenter> _servicesCenterLog;
        private readonly IServicesCenter _servicesOfCenter;
        private readonly IServicesLocation _servicesOfLoction;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CentersController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, IServicesRepository<Center> servicesCenter, IServicesRepositoryLog<LogCenter> servicesCenterLog, IServicesCenter servicesOfCenter, IServicesLocation servicesOfLoction, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _servicesCenter = servicesCenter;
            _servicesCenterLog = servicesCenterLog;
            _servicesOfCenter = servicesOfCenter;
            _servicesOfLoction = servicesOfLoction;
            _userManager = userManager;
            _context = context;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #region Users Grouped
        //var allusers = _context.Users.Select(user => new UserViewModel()
        //{
        //    Id = user.Id,
        //    FullName = user.FullName,
        //    IdentityNum = user.IdentityNum,
        //    PhoneNumber = user.PhoneNumber,
        //    UserName = user.UserName,
        //    Email = user.Email,
        //    Roles = _userManager.GetRolesAsync(user).Result
        //}).ToList();

        ////Admins
        //var admins = allusers.Where(x => x.Roles.Select(role => role).Contains("Admin")).ToList();
        //var adminsVM = admins.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        ////General Supervisors Men
        //var supervisorsmen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Supervisor.ToString())).ToList();
        //var supervisorsmenVM = supervisorsmen.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        ////General Supervisors Women
        //var supervisorswomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorWomen.ToString())).ToList();
        //var supervisorswomenVM = supervisorswomen.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        ////Center Supervisors Men
        //var supervisorscentermen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorCenterMen.ToString())).ToList();
        //var supervisorscentermenVM = supervisorscentermen.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        ////Center Supervisors Women
        //var supervisorscenterwomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorCenterWomen.ToString())).ToList();
        //var supervisorscenterwomenVM = supervisorscenterwomen.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        ////Teacher Men
        //var teachersmen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString())).ToList();
        //var teachersmenVM = teachersmen.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        ////Teacher Women
        //var teacherswomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.TeacherWomen.ToString())).ToList();
        //var teacherswomenVM = teacherswomen.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        ////Parent
        //var parents = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Parent.ToString())).ToList();
        //var parentsVM = parents.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        #endregion

        #region List Of Centers [View]
        [Authorize(Permissions.Centers.View)]
        public IActionResult Centers()
        {
            //Users Grouped
            var allusers = _context.Users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                IdentityNum = user.IdentityNum,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToList();

            var supervisorscentermen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorCenterMen.ToString())).ToList();
            var supervisorscenterwomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorCenterWomen.ToString())).ToList();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MasterViewModel master = new MasterViewModel()
            {
                CenterVM = new()
                {
                    Centers = _servicesCenter.GetAll(),
                    LogCenters = _servicesCenterLog.GetAll(),
                    NewCenter = new Center(),
                    CentersForMen = _servicesOfCenter.GetCentersForMen(),
                    CentersForWomen = _servicesOfCenter.GetCentersForWomen(),
                    SupervisorsMenCenter = new SelectList(supervisorscentermen.OrderByDescending(x => x.Id).ToList(), "Id", "FullName"),
                    SupervisorsWomenCenter = new SelectList(supervisorscenterwomen.OrderByDescending(x => x.Id).ToList(), "Id", "FullName"),
                    CentersForSupervisorCenter = _servicesOfCenter.GetCentersWithSupervisor(userId),
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
        #endregion

        #region Pause | Resume Center [Pause & Resume]
        [Authorize(Permissions.Centers.Pause)]
        public IActionResult Pause(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfCenter.Pause(Id))
                return RedirectToAction("Centers", "Centers");
            return RedirectToAction("Centers", "Centers");

        }
        [Authorize(Permissions.Centers.Resume)]
        public IActionResult Resume(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfCenter.Resume(Id))
                return RedirectToAction("Centers", "Centers");
            return RedirectToAction("Centers", "Centers");

        }
        #endregion

        #region Delete Center [Delete & Delete Log]
        //Delete
        [Authorize(Permissions.Centers.Delete)]
        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesCenter.Delete(Id) && _servicesCenterLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction("Centers", "Centers");
            return RedirectToAction("Centers", "Centers");

        }
        //Delete Log
        [Authorize(Permissions.Centers.DeleteLog)]
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesCenterLog.DeleteLog(Id))
                return RedirectToAction("Centers", "Centers");
            return RedirectToAction("Centers", "Centers");
        }
        #endregion

        #region Add|Edit Center [Create & Update]
        [Authorize(Permissions.Centers.Create), Authorize(Permissions.Centers.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.CenterVM.NewCenter.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                    //Exist
                    if (_servicesCenter.FindBy(model.CenterVM.NewCenter.Name) != null)
                        SessionMsg(Helper.Error, "مركز مكرر !", "اسم المركز موجود من قبل");

                    else
                    {
                        if (_servicesCenter.Save(model.CenterVM.NewCenter)
                            && _servicesCenterLog.Save(model.CenterVM.NewCenter.Id, Guid.Parse(userId)))
                            SessionMsg(Helper.Success, "تم الإضافة !", "تم اضافة المركز بنجاح ");
                        else
                            SessionMsg(Helper.Error, "خطأ في الإضافة", "حدوث خطأ اثناء ادخال بعض البيانات !");
                    }
                }
                else
                { //Update
                    if (_servicesCenter.Save(model.CenterVM.NewCenter)
                            && _servicesCenterLog.Update(model.CenterVM.NewCenter.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, "تم التعديل", "تم تعديل بيانات المركز بنجاح !");
                    else
                        SessionMsg(Helper.Error, "مشكلة في التعديل", "! حدوث خطأ اثناء تعديل بعض البيانات");

                }
            }
            return RedirectToAction("Centers", "Centers");

        }
        #endregion

        #region Details Of Center [List Of Locations]
        [HttpGet]
        public IActionResult SelectedCenter(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MasterViewModel master = new()
            {
                CenterVM = new()
                {
                    NewCenter = _servicesOfCenter.GetById(id),
                    LocationsOfCenter = _servicesOfCenter.GetLocationsForCenter(id),
                    EpisodesOfCenter = _servicesOfCenter.GetEpisodesForCenter(id),
                    StudentsOfCenter = _servicesOfCenter.GetStudentsForCenter(id)
                },
                DemandVM = new()
                {
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                    NewDemand = new Demand(),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId)
                }
            };
            return View(master);
        }
        #endregion

        #region Details Of Every Location in Center [List Of Episodes]
        [HttpGet]
        public IActionResult SelectedLocationCenter(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MasterViewModel master = new()
            {
                LocationVM = new()
                {
                    NewLocation = _servicesOfLoction.GetById(id),
                    EpisodesOfLocation = _servicesOfLoction.GetEpisodesForLocation(id),
                    StudentsOfLocation = _servicesOfLoction.GetStudentsForLocation(id),
                },
                DemandVM = new()
                {
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                    NewDemand = new Demand(),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId)
                }
            };
            return View(master);
        }
        #endregion
    }
}
