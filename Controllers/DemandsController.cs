using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class DemandsController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesRepositoryLog<LogDemand> _servicesDemandLog;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DemandsController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, IServicesRepositoryLog<LogDemand> servicesDemandLog, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _servicesDemandLog = servicesDemandLog;
            _userManager = userManager;
            _context = context;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #region List Of Demands [View]
        [Authorize(Permissions.Demands.View)]
        public IActionResult Demands()
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

                }
            };
            return View(master);
        }
        #endregion

        #region Delete Demand [Delete & Delete Log]
        //Delete
        [Authorize(Permissions.Demands.Delete)]
        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesDemand.Delete(Id) && _servicesDemandLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction("Demands", "Demands");
            return RedirectToAction("Demands", "Demands");

        }
        //Delete Log
        [Authorize(Permissions.Demands.DeleteLog)]
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesDemandLog.DeleteLog(Id))
                return RedirectToAction("Demands", "Demands");
            return RedirectToAction("Demands", "Demands");
        }
        #endregion

        #region Add|Edit Demand [Create & Update]
        [Authorize(Permissions.Demands.Create), Authorize(Permissions.Demands.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.DemandVM.NewDemand.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                    //Exist
                    if (_servicesDemand.FindBy(model.DemandVM.NewDemand.Request_Type) != null)
                        SessionMsg(Helper.Error, "Exist !", "Demand is Exist");

                    else
                    {
                        if (_servicesDemand.Save(model.DemandVM.NewDemand)
                            && _servicesDemandLog.Save(model.DemandVM.NewDemand.Id, Guid.Parse(userId)))
                            SessionMsg(Helper.Success, "Done !", "Demand Added Successfully");
                        else
                            SessionMsg(Helper.Error, "Not Saved", "Demand Not Added !");
                    }
                }
                else
                { //Update
                    if (_servicesDemand.Save(model.DemandVM.NewDemand)
                            && _servicesDemandLog.Update(model.DemandVM.NewDemand.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, "Demand Update", "Demand is Updated Successfully");
                    else
                        SessionMsg(Helper.Error, "Demand Not Update", "Demand isn't Updated !");

                }
            }
            return RedirectToAction("Demands", "Demands");

        }

        //طلب اذن
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddAskRequest(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (model.DemandVM.NewDemand.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                  ////Exist
                  //if (_servicesDemand.FindBy(model.NewDemand.Request_Type) != null)
                  //    SessionMsg(Helper.Error, "Exist !", "Demand is Exist");

                    //else
                    //{

                    //}
                    if (_servicesOfDemand.AddAskRequest(model.DemandVM.NewDemand))
                        SessionMsg(Helper.Success, "Done !", "Demand Added Successfully");
                    else
                        SessionMsg(Helper.Error, "Not Saved", "Demand Not Added !");
                }
                //else
                //{ //Update
                //    if (_servicesDemand.Save(model.NewDemand)
                //            && _servicesDemandLog.Update(model.NewDemand.Id, Guid.Parse(userId)))
                //        SessionMsg(Helper.Success, "Demand Update", "Demand is Updated Successfully");
                //    else
                //        SessionMsg(Helper.Error, "Demand Not Update", "Demand isn't Updated !");

                //}
            }
            if (User.IsInRole(Roles.Parent.ToString()))
                return RedirectToAction("EpisodesOfStudents", "Students");
            else
                return RedirectToAction("Demands", "Demands");


        }

        //طلب شكوي
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddComplaintRequest(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (model.DemandVM.NewDemand.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                  ////Exist
                  //if (_servicesDemand.FindBy(model.NewDemand.Request_Type) != null)
                  //    SessionMsg(Helper.Error, "Exist !", "Demand is Exist");

                    //else
                    //{

                    //}
                    if (_servicesOfDemand.AddComplaintRequest(model.DemandVM.NewDemand))
                        SessionMsg(Helper.Success, "Done !", "Demand Added Successfully");
                    else
                        SessionMsg(Helper.Error, "Not Saved", "Demand Not Added !");
                }
                //else
                //{ //Update
                //    if (_servicesDemand.Save(model.NewDemand)
                //            && _servicesDemandLog.Update(model.NewDemand.Id, Guid.Parse(userId)))
                //        SessionMsg(Helper.Success, "Demand Update", "Demand is Updated Successfully");
                //    else
                //        SessionMsg(Helper.Error, "Demand Not Update", "Demand isn't Updated !");

                //}
            }
            if (User.IsInRole(Roles.Parent.ToString()))
                return RedirectToAction("EpisodesOfStudents", "Students");
            else
                return RedirectToAction("Demands", "Demands");


        }

        //طلب تعديل بيانات
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddRequestEditPersonal(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (model.DemandVM.NewDemand.Id == Guid.Parse(Guid.Empty.ToString()))
                {
                    if (_servicesOfDemand.AddEditPersonalRequest(model.DemandVM.NewDemand))
                        SessionMsg(Helper.Success, "Done !", "Demand Added Successfully");
                    else
                        SessionMsg(Helper.Error, "Not Saved", "Demand Not Added !");
                }
            }
            if (User.IsInRole(Roles.Parent.ToString()))
                return RedirectToAction("EpisodesOfStudents", "Students");
            else
                return RedirectToAction("Demands", "Demands");
        }
        // طلب اجازة
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddHolidayRequest(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (model.DemandVM.NewDemand.Id == Guid.Parse(Guid.Empty.ToString()))
                {
                    if (_servicesOfDemand.AddHolidayRequest(model.DemandVM.NewDemand))

                        SessionMsg(Helper.Success, "Done !", "Demand Added Successfully");
                    else
                        SessionMsg(Helper.Error, "Not Saved", "Demand Not Added !");
                }
            }
            if (User.IsInRole(Roles.Parent.ToString()))
                return RedirectToAction("EpisodesOfStudents", "Students");
            else
                return RedirectToAction("Demands", "Demands");


        }

        // طلب نقل
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddTransferRequest(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (model.DemandVM.NewDemand.Id == Guid.Parse(Guid.Empty.ToString()))
                {
                    if (_servicesOfDemand.AddTransferRequest(model.DemandVM.NewDemand)
                            && _servicesDemandLog.Save(model.DemandVM.NewDemand.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, "Done !", "Demand Added Successfully");
                    else
                        SessionMsg(Helper.Error, "Not Saved", "Demand Not Added !");
                }
            }
            if (User.IsInRole(Roles.Parent.ToString()))
                return RedirectToAction("EpisodesOfStudents", "Students");
            else
                return RedirectToAction("Demands", "Demands");


        }
        #endregion

        #region Details Of Demands [List Of Levels]
        [HttpGet]
        public IActionResult SelectedDemand(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new();
            master.DemandVM = new();

            master.DemandVM.NewDemand = _servicesOfDemand.GetById(id);
            _servicesOfDemand.OpenDemand(master.DemandVM.NewDemand); // notification open and access request

            master.DemandVM.Demands = _servicesDemand.GetAll();
            master.DemandVM.DemandsForMen = _servicesOfDemand.GetDemandsForMen();
            master.DemandVM.DemandsForWomen = _servicesOfDemand.GetDemandsForWomen();
            master.DemandVM.DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId);
            master.DemandVM.DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify();
            master.DemandVM.DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify();
            master.DemandVM.DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId);
            master.DemandVM.SelectStudentDemands = _servicesOfDemand.GetStudentDemands(master.DemandVM.NewDemand.StudentId);


            return View(master);
        }
        #endregion

        #region Accept Request New Student
        //
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AcceptNewStudentRequest(Guid id)
        {
            if (_servicesOfDemand.AcceptedNewStudent(id))
                SessionMsg(Helper.Success, "Done !", "Student Added Successfully");
            else
                SessionMsg(Helper.Error, "Not Saved", "Student Not Added !");

            return RedirectToAction("Demands", "Demands");

        }
        #endregion

    }
}
