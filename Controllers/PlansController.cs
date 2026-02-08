using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class PlansController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesRepository<PlanData> _servicesPlan;
        private readonly IServicesRepositoryLog<LogPlanData> _servicesPlanLog;
        private readonly IServicesPlanData _servicesOfPlan;
        private readonly IServicesLevelData _servicesOfLevelData;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PlansController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, IServicesRepository<PlanData> servicesPlan, IServicesRepositoryLog<LogPlanData> servicesPlanLog, IServicesPlanData servicesOfPlan, IServicesLevelData servicesOfLevelData, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _servicesPlan = servicesPlan;
            _servicesPlanLog = servicesPlanLog;
            _servicesOfPlan = servicesOfPlan;
            _servicesOfLevelData = servicesOfLevelData;
            _userManager = userManager;
            _context = context;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #region List Of Plans [View]
        [Authorize(Permissions.Plans.View)]
        public IActionResult Plans()
        {
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                MasterViewModel master = new()
                {
                    PlanVM = new()
                    {
                        Plans = _servicesPlan.GetAll(),
                        LogPlans = _servicesPlanLog.GetAll(),
                        NewPlan = new PlanData(),
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
        }
        #endregion

        #region Delete Plan [Delete & Delete Log]
        //Delete
        [Authorize(Permissions.Plans.Delete)]
        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesPlan.Delete(Id) && _servicesPlanLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction("Plans", "Plans");
            return RedirectToAction("Plans", "Plans");

        }
        //Delete Log
        [Authorize(Permissions.Plans.DeleteLog)]
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesPlanLog.DeleteLog(Id))
                return RedirectToAction("Plans", "Plans");
            return RedirectToAction("Plans", "Plans");
        }
        #endregion

        #region Add|Edit Plan [Create & Update]
        [Authorize(Permissions.Plans.Create), Authorize(Permissions.Plans.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.PlanVM.NewPlan.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                    //Exist
                    if (_servicesPlan.FindBy(model.PlanVM.NewPlan.Name) != null)
                        SessionMsg(Helper.Error, "Exist !", "Plan is Exist");

                    else
                    {
                        if (_servicesPlan.Save(model.PlanVM.NewPlan)
                            && _servicesPlanLog.Save(model.PlanVM.NewPlan.Id, Guid.Parse(userId)))
                            SessionMsg(Helper.Success, "Done !", "Plan Added Successfully");
                        else
                            SessionMsg(Helper.Error, "Not Saved", "Plan Not Added !");
                    }
                }
                else
                { //Update
                    if (_servicesPlan.Save(model.PlanVM.NewPlan)
                            && _servicesPlanLog.Update(model.PlanVM.NewPlan.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, "Plan Update", "Plan is Updated Successfully");
                    else
                        SessionMsg(Helper.Error, "Plan Not Update", "Plan isn't Updated !");

                }
            }
            return RedirectToAction("Plans", "Plans");

        }
        #endregion

        #region Details Of Plans [List Of Levels]
        [HttpGet]
        public IActionResult SelectedPlan(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                PlanVM = new()
                {
                    NewPlan = _servicesOfPlan.GetById(id),
                    Levels = _servicesOfPlan.GetLevelsForPlan(id),
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

        #region Details Of Every Lesson in Plan [List Of Lessons]
        [HttpGet]
        public IActionResult SelectedLevelPlan(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                LevelVM = new()
                {
                    NewLevel = _servicesOfLevelData.GetById(id),
                    LessonsOfLevel = _servicesOfLevelData.GetLessonsForLevelData(id),
                    StudentsOfLevel = _servicesOfLevelData.GetStudentsForLevelData(id),
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
    }
}
