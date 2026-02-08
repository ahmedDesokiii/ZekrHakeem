using ExpressTimezone;
using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class ProgsController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesCenter _servicesOfCenter;
        private readonly IServicesRepository<Student> _servicesStudent;
        private readonly IServicesRepository<StudentProg> _servicesStudentProg;
        private readonly IServicesRepository<Location> _servicesLocation;
        private readonly IServicesRepository<EpisodeProg> _servicesEpisodeProg;
        private readonly IServicesRepository<LessonData> _servicesLessonData;
        private readonly IServicesEpisodeProg _servicesOfEpisodeProg;
        private readonly IServicesLocation _servicesOfLocation;
        private readonly IServicesStudent _servicesOfStudent;
        private readonly IServicesStudentProg _servicesOfStudentProg;
        private readonly IServicesLessonData _servicesOfLessonData;
        private readonly IServicesRepositoryLog<LogEpisodeProg> _servicesEpisodeProgLog;
        private readonly IServicesRepository<Prog> _servicesProg;
        private readonly IServicesRepository<PlanData> _servicesPlanData;
        private readonly IServicesRepositoryLog<LogProg> _servicesProgLog;
        private readonly IServicesProg _servicesOfProg;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProgsController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, IServicesCenter servicesOfCenter, IServicesRepository<Student> servicesStudent, IServicesRepository<StudentProg> servicesStudentProg, IServicesRepository<Location> servicesLocation, IServicesRepository<EpisodeProg> servicesEpisodeProg, IServicesRepository<LessonData> servicesLessonData, IServicesEpisodeProg servicesOfEpisodeProg, IServicesLocation servicesOfLocation, IServicesStudent servicesOfStudent, IServicesStudentProg servicesOfStudentProg, IServicesLessonData servicesOfLessonData, IServicesRepositoryLog<LogEpisodeProg> servicesEpisodeProgLog, IServicesRepository<Prog> servicesProg, IServicesRepository<PlanData> servicesPlanData, IServicesRepositoryLog<LogProg> servicesProgLog, IServicesProg servicesOfProg, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _servicesOfCenter = servicesOfCenter;
            _servicesStudent = servicesStudent;
            _servicesStudentProg = servicesStudentProg;
            _servicesLocation = servicesLocation;
            _servicesEpisodeProg = servicesEpisodeProg;
            _servicesLessonData = servicesLessonData;
            _servicesOfEpisodeProg = servicesOfEpisodeProg;
            _servicesOfLocation = servicesOfLocation;
            _servicesOfStudent = servicesOfStudent;
            _servicesOfStudentProg = servicesOfStudentProg;
            _servicesOfLessonData = servicesOfLessonData;
            _servicesEpisodeProgLog = servicesEpisodeProgLog;
            _servicesProg = servicesProg;
            _servicesPlanData = servicesPlanData;
            _servicesProgLog = servicesProgLog;
            _servicesOfProg = servicesOfProg;
            _userManager = userManager;
            _context = context;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }
        //////////////////////
        /// Prog
        /// 

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

        ////Prog Supervisors Men
        //var supervisorscentermen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorProgMen.ToString())).ToList();
        //var supervisorscentermenVM = supervisorscentermen.Select(user => new UserViewModel { FullName = user.FullName }).ToList();

        ////Prog Supervisors Women
        //var supervisorscenterwomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorProgWomen.ToString())).ToList();
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

        #region List Of Progs [View]
        [Authorize(Permissions.Progs.View)]
        public IActionResult Progs()
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

            var supervisorsprogmen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorCenterMen.ToString())).ToList();
            var supervisorsprogwomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.SupervisorCenterWomen.ToString())).ToList();
            var teachersmen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString())).ToList();
            var teacherswomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.TeacherWomen.ToString())).ToList();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MasterViewModel master = new();
            master.DemandVM = new()
            {
                NewDemand = new Demand(),
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            master.EpProgVM = new();
            master.EpProgVM.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.EpProgVM.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");

            master.EpProgVM.Progs = new SelectList(_context.Progs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.EpProgVM.LVM = new LocationViewModel()
            {
                Locations = _servicesLocation.GetAll(),
                LocationsForMen = _servicesOfLocation.GetLocationsForMen(),
                LocationsForWomen = _servicesOfLocation.GetLocationsForWomen(),
                LocationsWithSupervisor = _servicesOfLocation.GetLocationsWithSupervisor(userId),
                CentersForMen = _servicesOfCenter.GetCentersForMen(),
                CentersForWomen = _servicesOfCenter.GetCentersForWomen(),
                CentersForSupervisorCenter = _servicesOfCenter.GetCentersWithSupervisor(userId)
            };
            master.EpProgVM.NewEpisodeProg = new EpisodeProg();
            master.EpProgVM.EpisodeProgs = _servicesEpisodeProg.GetAll();
            master.EpProgVM.LogEpisodeProgs = _servicesEpisodeProgLog.GetAll();
            master.EpProgVM.EpisodeProgsForMen = _servicesOfEpisodeProg.GetEpisodeProgsForMen();
            master.EpProgVM.EpisodeProgsForWomen = _servicesOfEpisodeProg.GetEpisodeProgsForWomen();
            master.EpProgVM.EpisodeProgsWithSupervisor = _servicesOfEpisodeProg.GetEpisodeProgsWithSupervisor(userId);
            master.EpProgVM.EpisodeProgsWithTeacher = _servicesOfEpisodeProg.GetEpisodeProgsWithTeacher(userId);
            master.EpProgVM.NewStudentProg = new StudentProg();
            master.EpProgVM.TeachersMen = teachersmen;
            master.EpProgVM.TeachersWomen = teacherswomen;

            master.EpProgVM.PrayerTimesAPI = _servicesOfEpisodeProg.GetPrayerTimesAPI();

            var fakeEp = _servicesEpisodeProg.FindBy(new Guid("ad8a4ead-a5c8-4284-a22a-bf2c6659b9b2")); // حلقة الانتظار
            master.EpProgVM.EpisodeProgInTime = fakeEp;

            master.ProgVM = new()
            {
                Progs = _servicesProg.GetAll(),
                LogProgs = _servicesProgLog.GetAll(),
                NewProg = new Prog(),
                ProgsForMen = _servicesOfProg.GetProgsForMen(),
                ProgsForWomen = _servicesOfProg.GetProgsForWomen(),
                PlansData = _servicesPlanData.GetAll(),
                SupervisorsMenProg = supervisorsprogmen,
                SupervisorsWomenProg = supervisorsprogwomen,
                SupervisorsMenProgList = new SelectList(supervisorsprogmen.OrderByDescending(x => x.Id).ToList(), "Id", "FullName"),
                SupervisorsWomenProgList = new SelectList(supervisorsprogwomen.OrderByDescending(x => x.Id).ToList(), "Id", "FullName"),
                ProgsForSupervisorProg = _servicesOfProg.GetProgsWithSupervisor(userId),
                EpVM = master.EpProgVM
            };
            return View(master);
        }
        #endregion

        #region Pause | Resume Prog [Pause & Resume]
        [Authorize(Permissions.Progs.Pause)]
        public IActionResult Pause(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfProg.Pause(Id))
                return RedirectToAction("Progs", "Progs");
            return RedirectToAction("Progs", "Progs");

        }
        [Authorize(Permissions.Progs.Resume)]
        public IActionResult Resume(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfProg.Resume(Id))
                return RedirectToAction("Progs", "Progs");
            return RedirectToAction("Progs", "Progs");

        }
        #endregion

        #region Delete Prog [Delete & Delete Log]
        //Delete
        [Authorize(Permissions.Progs.Delete)]
        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesProg.Delete(Id) /*&& _servicesProgLog.Delete(Id, Guid.Parse(userId))*/)
                return RedirectToAction("Progs", "Progs");
            return RedirectToAction("Progs", "Progs");

        }
        //Delete Log
        [Authorize(Permissions.Progs.DeleteLog)]
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesProgLog.DeleteLog(Id))
                return RedirectToAction("Progs", "Progs");
            return RedirectToAction("Progs", "Progs");
        }
        #endregion

        #region Add|Edit Prog [Create & Update]
        [Authorize(Permissions.Progs.Create), Authorize(Permissions.Progs.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.ProgVM.NewProg.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                    //Exist
                    if (_servicesProg.FindBy(model.ProgVM.NewProg.Name) != null)
                        SessionMsg(Helper.Error, "Exist !", "Prog is Exist");

                    else
                    {
                        if (_servicesProg.Save(model.ProgVM.NewProg)
                           /* && _servicesProgLog.Save(model.NewProg.Id, Guid.Parse(userId))*/)
                            SessionMsg(Helper.Success, "Done !", "Prog Added Successfully");
                        else
                            SessionMsg(Helper.Error, "Not Saved", "Prog Not Added !");
                    }
                }
                else
                { //Update
                    if (_servicesProg.Save(model.ProgVM.NewProg)
                           /* && _servicesProgLog.Update(model.NewProg.Id, Guid.Parse(userId))*/)
                        SessionMsg(Helper.Success, "Prog Update", "Prog is Updated Successfully");
                    else
                        SessionMsg(Helper.Error, "Prog Not Update", "Prog isn't Updated !");

                }
            }
            return RedirectToAction("Progs", "Progs");

        }
        #endregion

        #region Details Of Prog [List Of EpisodeProgs]
        [HttpGet]
        public IActionResult SelectedProg(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                ProgVM = new()
                {
                    NewProg = _servicesOfProg.GetById(id),
                    EpisodeProgsOfProg = _servicesOfEpisodeProg.GetEpisodeProgsWithProg(id)
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

        #region Details Of Every Location in Prog [List Of Episodes]
        //[HttpGet]
        //public IActionResult SelectedLocationProg(Guid id)
        //{
        //    LocationViewModel LVM = new();
        //    {
        //        LVM.NewLocation = _servicesOfLoction.GetById(id);
        //        LVM.EpisodesOfLocation = _servicesOfLoction.GetEpisodesForLocation(id);
        //        LVM.StudentsOfLocation = _servicesOfLoction.GetStudentsForLocation(id);
        //    }
        //    ;
        //    return View(LVM);
        //}
        #endregion


        ////////////////////////////////
        /// Episode Prog
        /// 


        #region List Of Episodes Prog [View]
        [Authorize(Permissions.ProgEpisodes.View)]
        public IActionResult EpisodeProgs()
        {
            ViewBag.SyncOrAsync = "Asynchronous";

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
            var teachersmen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString())).ToList();
            var teacherswomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.TeacherWomen.ToString())).ToList();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new();
            master.EpProgVM = new();
            master.DemandVM = new()
            {
                NewDemand = new Demand(),
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            master.EpProgVM.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.EpProgVM.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");
            master.EpProgVM.Progs = new SelectList(_context.Progs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.EpProgVM.LVM = new LocationViewModel()
            {
                Locations = _servicesLocation.GetAll(),
                LocationsForMen = _servicesOfLocation.GetLocationsForMen(),
                LocationsForWomen = _servicesOfLocation.GetLocationsForWomen(),
                LocationsWithSupervisor = _servicesOfLocation.GetLocationsWithSupervisor(userId),
                CentersForMen = _servicesOfCenter.GetCentersForMen(),
                CentersForWomen = _servicesOfCenter.GetCentersForWomen(),
                CentersForSupervisorCenter = _servicesOfCenter.GetCentersWithSupervisor(userId)
            };
            master.EpProgVM.NewEpisodeProg = new EpisodeProg();
            master.EpProgVM.EpisodeProgs = _servicesEpisodeProg.GetAll();
            master.EpProgVM.LogEpisodeProgs = _servicesEpisodeProgLog.GetAll();
            master.EpProgVM.EpisodeProgsForMen = _servicesOfEpisodeProg.GetEpisodeProgsForMen();
            master.EpProgVM.EpisodeProgsForWomen = _servicesOfEpisodeProg.GetEpisodeProgsForWomen();
            master.EpProgVM.EpisodeProgsWithSupervisor = _servicesOfEpisodeProg.GetEpisodeProgsWithSupervisor(userId);
            master.EpProgVM.EpisodeProgsWithTeacher = _servicesOfEpisodeProg.GetEpisodeProgsWithTeacher(userId);
            master.EpProgVM.NewStudentProg = new StudentProg();
            master.EpProgVM.TeachersMen = teachersmen;
            master.EpProgVM.TeachersWomen = teacherswomen;

            master.EpProgVM.PrayerTimesAPI = _servicesOfEpisodeProg.GetPrayerTimesAPI();

            var fakeEp = _servicesEpisodeProg.FindBy(new Guid("ad8a4ead-a5c8-4284-a22a-bf2c6659b9b2")); // حلقة الانتظار
            master.EpProgVM.EpisodeProgInTime = fakeEp;
            //master.EpProgVM.Verses = new List<Verse>();
            //ViewBag.Verses = new SelectList(master.EpProgVM.Verses, "Id", "Num");

            _servicesOfEpisodeProg.ResetAllEpisodeProgs();

            if (User.IsInRole(Roles.Teacher.ToString()) || User.IsInRole(Roles.TeacherWomen.ToString()))
            {
                foreach (var episode in master.EpProgVM.EpisodeProgsWithTeacher)
                {
                    var utcTime = DateTime.UtcNow;
                    var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh").TimeOfDay;

                    var epStartTime_Fajr = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.FajrTime).TimeOfDay;
                    var epEndTime_Fajr = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.EndFajrTime).TimeOfDay;
                    var epStartTime_Sunrise = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.SunriseTime).TimeOfDay;
                    var epEndTime_Sunrise = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.EndSunriseTime).TimeOfDay;
                    var epStartTime_Zohr = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.ZohrTime).TimeOfDay;
                    var epEndTime_Zohr = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.EndZohrTime).TimeOfDay;
                    var epStartTime_Asr = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.AsrTime).TimeOfDay;
                    var epEndTime_Asr = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.EndAsrTime).TimeOfDay;
                    var epStartTime_Mghrb = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.MghrbTime).TimeOfDay;
                    var epEndTime_Mghrb = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.EndMghrbTime).TimeOfDay;
                    var epStartTime_Isha = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.IshaTime).TimeOfDay;
                    var epEndTime_Isha = Convert.ToDateTime(master.EpProgVM.PrayerTimesAPI.EndIshaTime).TimeOfDay;


                    if (episode.Period == null)
                    { }
                    else
                    {
                        // check episode in time 
                        if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.الفجر.ToString()) && currentTime >= epStartTime_Fajr && currentTime <= epEndTime_Fajr)
                            master.EpProgVM.EpisodeProgInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.الشروق.ToString()) && currentTime >= epStartTime_Sunrise && currentTime <= epEndTime_Sunrise)
                            master.EpProgVM.EpisodeProgInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.الظهر.ToString()) && currentTime >= epStartTime_Zohr && currentTime <= epEndTime_Zohr)
                            master.EpProgVM.EpisodeProgInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.العصر.ToString()) && currentTime >= epStartTime_Asr && currentTime <= epEndTime_Asr)
                            master.EpProgVM.EpisodeProgInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.المغرب.ToString()) && currentTime >= epStartTime_Mghrb && currentTime <= epEndTime_Mghrb)
                            master.EpProgVM.EpisodeProgInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.العشاء.ToString()) && currentTime >= epStartTime_Isha && currentTime <= epEndTime_Isha)
                            master.EpProgVM.EpisodeProgInTime = episode;

                    }
                }
                //Response.Headers.Add("Refresh", "5");

            }
            else
            {
                Response.Headers.Add("Refresh", "120");
            }


            return View(master);
        }
        #endregion

        #region Delete EpisodeProg [Delete & Delete Log & Delete Episode Type]
        [Authorize(Permissions.ProgEpisodes.Delete)]
        public IActionResult DeleteEpisodeProg(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesEpisodeProg.Delete(Id) && _servicesEpisodeProgLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction("EpisodeProgs", "Progs");
            return RedirectToAction("EpisodeProgs", "Progs");

        }

        [Authorize(Permissions.Episodes.DeleteLog)]
        public IActionResult DeleteEpisodeProgLog(Guid Id)
        {
            if (_servicesEpisodeProgLog.DeleteLog(Id))
                return RedirectToAction("EpisodeProgs", "Progs");
            return RedirectToAction("EpisodeProgs", "Progs");
        }
        #endregion

        #region Add|Edit EpisodeProg [Create & Update]
        [Authorize(Permissions.ProgEpisodes.Create), Authorize(Permissions.ProgEpisodes.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SaveEpisodeProg(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.EpProgVM.NewEpisodeProg.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create

                    if (_servicesEpisodeProg.FindBy(model.EpProgVM.NewEpisodeProg.Name) != null)
                        SessionMsg(Helper.Error, "Exist !", "EpisodeProg is Exist");
                    else
                    {
                        if (Convert.ToBoolean(_servicesEpisodeProg.Save(model.EpProgVM.NewEpisodeProg))
                             && _servicesEpisodeProgLog.Save(model.EpProgVM.NewEpisodeProg.Id, Guid.Parse(userId)))
                            SessionMsg(Helper.Success, "Done !", "EpisodeProg Added Successfully");
                        else
                            SessionMsg(Helper.Error, "Not Saved", "EpisodeProg Not Added !");
                    }
                }
                else
                { //Update
                    if (_servicesEpisodeProg.Save(model.EpProgVM.NewEpisodeProg)
                            && _servicesEpisodeProgLog.Update(model.EpProgVM.NewEpisodeProg.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, "EpisodeProg Update", "EpisodeProg is Updated Successfully");
                    else
                        SessionMsg(Helper.Error, "EpisodeProg Not Update", "EpisodeProg isn't Updated !");

                }
            }
            return RedirectToAction("EpisodeProgs", "Progs");
        }
        #endregion

        #region Details Of EpisodeProg [Add Student Evaluations & Update Lessons & WhatsApp Msg]
        [HttpGet]
        public IActionResult SelectedEpisodeProg(Guid id)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new();
            master.DemandVM = new()
            {
                NewDemand = new Demand(),
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            master.EpProgVM = new();
            if (User.IsInRole(Roles.Teacher.ToString()) || User.IsInRole(Roles.TeacherWomen.ToString()))
            {
                _servicesOfEpisodeProg.StartEpisodeProg(id);
            }
            else if (!User.IsInRole(Roles.Teacher.ToString()) || !User.IsInRole(Roles.TeacherWomen.ToString()))
            {
                master.EpProgVM.PrayerTimesAPI = _servicesOfEpisodeProg.GetPrayerTimesAPI();
            }
            master.EpProgVM.NewEpisodeProg = _servicesOfEpisodeProg.GetById(id);
            master.EpProgVM.StudentProgsVM = _servicesOfEpisodeProg.GetStudentProgsForEpisodeProg(id);
            //master.EpProgVM.PrevLessonData = _servicesOfEpisodeProg.GetStudentPrevLesson(master.EpProgVM.StudentProgsVM.StudentProgs);
            //master.EpProgVM.NextLessonData = _servicesOfEpisodeProg.GetStudentNextLesson(master.EpProgVM.StudentsVM.Students);


            master.EpProgVM.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().Where(x => x.Id <= master.EpProgVM.NewEpisodeProg.StartSurah && x.Id >= master.EpProgVM.NewEpisodeProg.EndSurah).OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.EpProgVM.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().Where(x => x.Id <= master.EpProgVM.NewEpisodeProg.StartSurah && x.Id >= master.EpProgVM.NewEpisodeProg.EndSurah).OrderByDescending(x => x.Id).ToList(), "Id", "Name");



            master.EpProgVM.TeacherAttendance = _servicesOfEpisodeProg.FindAttendanceByEpisodeProgId(id);
            master.EpProgVM.Verses = new List<Verse>();
            ViewBag.Verses = new SelectList(master.EpProgVM.Verses, "Id", "Num");
            master.EpProgVM.LastEvaluateOfStudentProgs = _servicesOfEpisodeProg.GetStudentProgLastEvaluate(master.EpProgVM.StudentProgsVM.StudentProgs);
            return View(master);
        }
        public JsonResult GetVersesBySurah(int surahId)
        {
            var v = Json(_context.Verses.Where(p => p.SurahId == surahId).ToList());
            return v;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddEvaluatoinOnly(Guid id, EpisodeProgViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //Add Evaluations : Done 
                _servicesOfEpisodeProg.AddEvaluatoinOnly(model.EvaluationProgs);
            }
            var ep = _servicesOfEpisodeProg.GetById(id);
            return RedirectToAction("SelectedEpisodeProg", "Progs", ep);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult UpdateLesson(Guid id, EpisodeProgViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //Update Lesson : Done | WhatsApp Msg To Parents 
                if (_servicesOfEpisodeProg.UpdateLessonsOfStudents(model.StudentProgsVM.StudentProgs))
                {
                    // ManualFinished
                    _servicesOfEpisodeProg.ManualEndEpisodeProg(id);

                    model.TeacherAttendance = _servicesOfEpisodeProg.FindAttendanceByEpisodeProgId(id);
                    model.PrayerTimesAPI = _servicesOfEpisodeProg.GetPrayerTimesAPI();

                    SessionMsg(Helper.Success, "Done !", "Update Lesson and Finished EpisodeProg Successfully ! ");

                }
                else
                    SessionMsg(Helper.Error, "Not Finished", "Not Update Lesson and Not Finished EpisodeProg ");
            }



            return RedirectToAction("EpisodeProgs", "Progs");
        }

        #endregion

        #region Pause | Resume EpisodeProg [Pause & Resume]
        [Authorize(Permissions.ProgEpisodes.Pause)]
        public IActionResult PauseEpisodeProg(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfEpisodeProg.Pause(Id))
                return RedirectToAction("EpisodeProgs", "Progs");
            return RedirectToAction("EpisodeProgs", "Progs");
        }
        [Authorize(Permissions.ProgEpisodes.Resume)]
        public IActionResult ResumeEpisodeProg(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfEpisodeProg.Resume(Id))
                return RedirectToAction("EpisodeProgs", "Progs");
            return RedirectToAction("EpisodeProgs", "Progs");
        }
        #endregion





        //////////////////////////
        /// Student Prog
        ///  
        #region Students Of Episode Prog

        [Authorize(Permissions.Students.View)]
        public IActionResult StudentsWithEpisodeProg()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDate = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
            MasterViewModel master = new()
            {
                EpProgVM = new()
                {
                    EpisodeProgsList = new SelectList(_context.EpisodeProgs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name"),
                    EpisodeProgsForMenList = new SelectList(_context.EpisodeProgs.Where(x => x.EpProg.Type == "men").AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name"),
                    EpisodeProgsWithSupervisorList = new SelectList(_context.EpisodeProgs.Where(x => x.EpProg.ApplicationUserId == userId).AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name"),
                    EpisodeProgsWithTeacherList = new SelectList(_context.EpisodeProgs.Where(x => x.ApplicationUserId == userId).AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name"),
                    AvilableNationalities = new SelectList(_context.Nationalities.AsNoTracking().OrderBy(x => x.Name).Where(x => x.CurrentState > 0).ToList(), "Id", "Name"),
                    Students = _context.Students
                                   .Include(x => x.StEpisode)
                                   .Include(x => x.Nationality)
                                   .Include(x => x.LessonData)
                                   .Include(x => x.Verse)
                                   .Include(x => x.Surah)
                                   .Include(x => x.PlanData)
                                   .Include(x => x.Evaluations)
                                   .Include(x => x.LessonData.LevelData)
                                   .Include(x => x.LessonData.LevelData.PlanData)

                                    .OrderBy(x => x.Name)
                                   .Where(x => x.CurrentState > 0)
                                   .ToList(),

                    StudentsForMen = _context.Students
                                   .Include(x => x.StEpisode)
                                   .Include(x => x.Nationality)
                                   .Include(x => x.LessonData)
                                   .Include(x => x.Verse)
                                   .Include(x => x.Surah)
                                   .Include(x => x.PlanData)
                                   .Include(x => x.Evaluations)
                                   .Include(x => x.LessonData.LevelData)
                                   .Include(x => x.LessonData.LevelData.PlanData)

                                    .OrderBy(x => x.Name)
                                   .Where(x => x.CurrentState > 0)
                                   .ToList(),
                    StudentsForWomen = _context.Students
                                   .Include(x => x.StEpisode)
                                   .Include(x => x.Nationality)
                                   .Include(x => x.LessonData)
                                   .Include(x => x.Verse)
                                   .Include(x => x.Surah)
                                   .Include(x => x.PlanData)
                                   .Include(x => x.Evaluations)
                                   .Include(x => x.LessonData.LevelData)
                                   .Include(x => x.LessonData.LevelData.PlanData)

                                    .OrderBy(x => x.Name)
                                   .Where(x => x.CurrentState > 0)
                                   .ToList(),
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

        #region List Of EpisodeProgs of StudentProgs [View]
        public IActionResult EpisodeProgsOfStudentProgs()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDate = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
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
                StProgVM = new()
                {

                    StudentProgs = _servicesStudentProg.GetAll(),
                    StudentProgsForMen = _servicesOfStudentProg.GetStudentProgsForMen(),
                    StudentProgsForWomen = _servicesOfStudentProg.GetStudentProgsForWomen(),
                    StudentProgsWithSupervisor = _servicesOfStudentProg.GetStudentProgsWithSupervisor(userId),
                    StudentProgsWithTeacher = _servicesOfStudentProg.GetStudentProgsWithTeacher(userId),
                    StudentProgsWithParent = _servicesOfStudentProg.GetStudentProgsWithParent(userDate),
                    //svm.LogStudentProgs = _servicesStudentProgLog.GetAll();
                    NewStudentProg = new StudentProg(),
                    //svm.Centers = _servicesCenter.GetAll();

                    EVM = new EpisodeProgViewModel()
                    {
                        EpisodeProgs = _servicesEpisodeProg.GetAll(),
                        EpisodeProgsForMen = _servicesOfEpisodeProg.GetEpisodeProgsForMen(),
                        EpisodeProgsForWomen = _servicesOfEpisodeProg.GetEpisodeProgsForWomen(),
                        EpisodeProgsWithSupervisor = _servicesOfEpisodeProg.GetEpisodeProgsWithSupervisor(userId),
                        EpisodeProgsWithTeacher = _servicesOfEpisodeProg.GetEpisodeProgsWithTeacher(userId)
                    },
                    //svm.LevelsData = _servicesOfLevel.GetAllLevelsWithLessons();
                    PlansData = _servicesPlanData.GetAll(),
                    LessonsData = _servicesLessonData.GetAll(),
                    //svm.Surahs = _servicesOfStudentProg.GetAllSurahs();


                    Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name"),
                    Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name"),

                    Verses = new List<Verse>()
                }

            };
            ViewBag.Verses = new SelectList(master.StProgVM.Verses, "Id", "Num");
            return View(master);
        }
        #endregion

        #region Details Of EpisodeProg Of StudentProgs [Display EpisodeProg With StudentProgs]

        [HttpGet]
        public IActionResult SelectedStudentProgsOfEpisodeProg(Guid id)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDate = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
            MasterViewModel master = new();

            master.DemandVM = new()
            {
                NewDemand = new Demand(),
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            master.StProgVM = new();
            master.StProgVM = _servicesOfEpisodeProg.GetStudentProgsForEpisodeProg(id);
            master.StProgVM.SelectedEpisodeProg = _servicesOfEpisodeProg.GetById(id);

            master.StProgVM.StudentProgsWithParent = _servicesOfStudentProg.GetStudentProgsWithParent(userDate);
            //master.StProgVM.LogStudentProgs = _servicesStudentProgLog.GetAll();
            master.StProgVM.NewStudentProg = new StudentProg();
            //master.StProgVM.Centers = _servicesCenter.GetAll();

            master.StProgVM.EpisodeProgs = _servicesEpisodeProg.GetAll();
            master.StProgVM.EpisodeProgsForMen = _servicesOfEpisodeProg.GetEpisodeProgsForMen();
            master.StProgVM.EpisodeProgsForWomen = _servicesOfEpisodeProg.GetEpisodeProgsForWomen();
            master.StProgVM.EpisodeProgsWithSupervisor = _servicesOfEpisodeProg.GetEpisodeProgsWithSupervisor(userId);
            master.StProgVM.EpisodeProgsWithTeacher = _servicesOfEpisodeProg.GetEpisodeProgsWithTeacher(userId);

            //master.StProgVM.LevelsData = _servicesOfLevel.GetAllLevelsWithLessons();
            master.StProgVM.PlansData = _servicesPlanData.GetAll();
            master.StProgVM.LessonsData = _servicesLessonData.GetAll();
            //master.StProgVM.Surahs = _servicesOfStudentProg.GetAllSurahs();
            master.StProgVM.AvilableNationalities = new SelectList(_context.Nationalities.AsNoTracking().OrderBy(x => x.Name).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");

            master.StProgVM.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.StProgVM.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");


            master.StProgVM.Verses = new List<Verse>();


            ViewBag.Verses = new SelectList(master.StProgVM.Verses, "Id", "Num");

            return View(master);


        }

        #endregion

        #region Add|Edit EpisodeProg [Create & Update]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SaveStudentProg(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.StProgVM.NewStudentProg.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create

                    if (_servicesOfEpisodeProg.FindStudentProgBy(model.StProgVM.NewStudentProg.Name) != null)
                        SessionMsg(Helper.Error, "Exist !", "StudentProg is Exist");
                    else
                    {
                        if (Convert.ToBoolean(_servicesOfEpisodeProg.SaveStudentProg(model.StProgVM.NewStudentProg)))
                            SessionMsg(Helper.Success, "Done !", "StudentProg Added Successfully");
                        else
                            SessionMsg(Helper.Error, "Not Saved", "StudentProg Not Added !");
                    }
                }
                else
                { //Update
                    if (_servicesOfEpisodeProg.SaveStudentProg(model.StProgVM.NewStudentProg))
                    {
                        SessionMsg(Helper.Success, "StudentProg Update", "StudentProg is Updated Successfully");
                        return RedirectToAction("EpisodeProgsOfStudentProgs", "Progs");

                    }
                    else
                        SessionMsg(Helper.Error, "StudentProg Not Update", "StudentProg isn't Updated !");

                }
            }
            return RedirectToAction("StudentsWithEpisodeProg", "Progs");
        }
        #endregion

        #region Details Of StudentProg Out [List Evaluations & StudentProg info]
        [HttpGet]
        //[Authorize(Permissions.Students.ViewDemand)]
        public IActionResult SelectedStudentProg(Guid id)
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
                StProgVM = new()
                {
                    NewStudentProg = _servicesOfStudentProg.GetById(id),
                    //Evaluations = _servicesOfStudentProg.GetEvaluationsForStudentProg(id),
                    //Demands = _servicesOfStudent.GetDemandsForStudent(id),
                    //Centers = _servicesCenter.GetAll()
                }
            };
            return View(master);
        }
        #endregion

        #region Assigned Selected Students To EpisodeProg
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AssignedStudentsToEpProg(MasterViewModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.DemandVM = new()
            {
                NewDemand = new Demand(),
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            model.EpProgVM.Students = _context.Students
                                    .Include(x => x.StEpisode)
                                    .Include(x => x.Nationality)
                                    .Include(x => x.LessonData)
                                    .Include(x => x.Verse)
                                    .Include(x => x.Surah)
                                    .Include(x => x.PlanData)
                                    .Include(x => x.Evaluations)
                                    .Include(x => x.LessonData.LevelData)
                                    .Include(x => x.LessonData.LevelData.PlanData)

                                    .OrderBy(x => x.Name)
                                    .Where(x => x.CurrentState > 0)
                                    .ToList();

            model.EpProgVM.StudentsForMen = model.EpProgVM.Students;
            model.EpProgVM.StudentsForWomen = model.EpProgVM.Students;

            if (_servicesOfEpisodeProg.AssignedStudentsToEpisodeProg(model.EpProgVM))
                SessionMsg(Helper.Success, "StudentProg  Added", "StudentProg in zone is Added Successfully");
            else
                SessionMsg(Helper.Error, "StudentProg Not Added", "StudentProg in zone isn't Added !");

            return RedirectToAction("StudentsWithEpisodeProg", "Progs");
        }
        #endregion
    }
}
