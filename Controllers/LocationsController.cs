using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class LocationsController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly ApplicationDbContext _context;
        private readonly IServicesRepository<Location> _servicesLocation;
        private readonly IServicesRepository<Center> _servicesCenter;
        private readonly IServicesRepository<District> _servicesDistrict;
        private readonly IServicesRepositoryLog<LogLocation> _servicesLocationLog;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServicesLocation _servicesOfLocation;
        private readonly IServicesCenter _servicesOfCenter;

        public LocationsController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, ApplicationDbContext context, IServicesRepository<Location> servicesLocation, IServicesRepository<Center> servicesCenter, IServicesRepository<District> servicesDistrict, IServicesRepositoryLog<LogLocation> servicesLocationLog, UserManager<ApplicationUser> userManager, IServicesLocation servicesOfLocation, IServicesCenter servicesOfCenter)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _context = context;
            _servicesLocation = servicesLocation;
            _servicesCenter = servicesCenter;
            _servicesDistrict = servicesDistrict;
            _servicesLocationLog = servicesLocationLog;
            _userManager = userManager;
            _servicesOfLocation = servicesOfLocation;
            _servicesOfCenter = servicesOfCenter;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #region List Of Locations [View]
        [Authorize(Permissions.Locations.View)]
        public IActionResult Locations()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                LocationVM = new()
                {
                    Locations = _servicesLocation.GetAll(),
                    LogLocations = _servicesLocationLog.GetAll(),
                    LocationsForMen = _servicesOfLocation.GetLocationsForMen(),
                    LocationsForWomen = _servicesOfLocation.GetLocationsForWomen(),
                    LocationsWithSupervisor = _servicesOfLocation.GetLocationsWithSupervisor(userId),
                    Districts = _servicesDistrict.GetAll(),
                    CVM = new CenterViewModel()
                    {
                        Centers = _servicesCenter.GetAll(),
                        CentersForMen = _servicesOfCenter.GetCentersForMen(),
                        CentersForWomen = _servicesOfCenter.GetCentersForWomen(),
                        CentersForSupervisorCenter = _servicesOfCenter.GetCentersWithSupervisor(userId)
                    },
                    NewLocation = new Location(),

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

        #region Pause | Resume Location [Pause & Resume]
        [Authorize(Permissions.Locations.Pause)]
        public IActionResult Pause(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfLocation.Pause(Id))
                return RedirectToAction("Locations", "Locations");
            return RedirectToAction("Locations", "Locations");

        }
        [Authorize(Permissions.Locations.Resume)]
        public IActionResult Resume(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfLocation.Resume(Id))
                return RedirectToAction("Locations", "Locations");
            return RedirectToAction("Locations", "Locations");

        }

        #endregion

        #region Delete Location [Delete & Delete Log]
        [Authorize(Permissions.Locations.Delete)]
        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesLocation.Delete(Id) && _servicesLocationLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction("Locations", "Locations");
            return RedirectToAction("Locations", "Locations");

        }
        [Authorize(Permissions.Locations.DeleteLog)]
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesLocationLog.DeleteLog(Id))
                return RedirectToAction("Locations", "Locations");
            return RedirectToAction("Locations", "Locations");
        }
        #endregion

        #region Add|Edit Location [Create & Update]
        [Authorize(Permissions.Locations.Create), Authorize(Permissions.Locations.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.LocationVM.NewLocation.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                    //Exist
                    if (_servicesLocation.FindBy(model.LocationVM.NewLocation.Name) != null)
                        SessionMsg(Helper.Error, "مسجد مكرر !", "اسم المسجد موجود من قبل");
                    else
                    {
                        if (_servicesLocation.Save(model.LocationVM.NewLocation) && _servicesLocationLog.Save(model.LocationVM.NewLocation.Id, Guid.Parse(userId)))
                            SessionMsg(Helper.Success, "تم الاضافة !", "تم اضافة بيانات المسجد بنجاح");
                        else
                            SessionMsg(Helper.Error, "خطأ في الاضافة", "حدث خطأ اثناء عملية الاضافة بسبب خطأ في ادخال البيانات");
                    }
                }
                else
                { //Update
                    if (_servicesLocation.Save(model.LocationVM.NewLocation)
                            && _servicesLocationLog.Update(model.LocationVM.NewLocation.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, "تم التعديل", "تم تعديل بيانات المسجد بنجاح");
                    else
                        SessionMsg(Helper.Error, "خطأ في التعديل", "حدث خطأ في عملية التعديل بسبب خطأ في ادخال بعض البيانات");

                }
            }
            return RedirectToAction("Locations", "Locations");

        }
        #endregion

        #region Details Of Every Location  [List Of Episodes]
        [HttpGet]
        public IActionResult SelectedLocation(Guid id)
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
                LocationVM = new()
                {
                    NewLocation = _servicesOfLocation.GetById(id),
                    EpisodesOfLocation = _servicesOfLocation.GetEpisodesForLocation(id),
                    StudentsOfLocation = _servicesOfLocation.GetStudentsForLocation(id),
                }
            };

            return View(master);
        }
        #endregion
    }
}
