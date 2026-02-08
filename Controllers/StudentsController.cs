using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class StudentsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IServicesRepository<Episode> _servicesEpisode;
        private readonly IServicesRepository<Center> _servicesCenter;
        private readonly IServicesRepository<LevelData> _servicesLevelData;
        private readonly IServicesRepository<PlanData> _servicesPlanData;
        private readonly IServicesPlanData _servicesOfPlan;
        private readonly IServicesLevelData _servicesOfLevel;
        private readonly IServicesRepository<LessonData> _servicesLessonData;
        private readonly IServicesStudent _servicesOfStudent;
        private readonly IServicesEpisode _servicesOfEpisode;
        private readonly IServicesRepository<Student> _servicesStudent;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesRepositoryLog<LogStudent> _servicesStudentLog;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsController(ApplicationDbContext context, IServicesRepository<Episode> servicesEpisode, IServicesRepository<Center> servicesCenter, IServicesRepository<LevelData> servicesLevelData, IServicesRepository<PlanData> servicesPlanData, IServicesPlanData servicesOfPlan, IServicesLevelData servicesOfLevel, IServicesRepository<LessonData> servicesLessonData, IServicesStudent servicesOfStudent, IServicesEpisode servicesOfEpisode, IServicesRepository<Student> servicesStudent, IServicesDemand servicesOfDemand, IServicesRepository<Demand> servicesDemand, IServicesRepositoryLog<LogStudent> servicesStudentLog, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _servicesEpisode = servicesEpisode;
            _servicesCenter = servicesCenter;
            _servicesLevelData = servicesLevelData;
            _servicesPlanData = servicesPlanData;
            _servicesOfPlan = servicesOfPlan;
            _servicesOfLevel = servicesOfLevel;
            _servicesLessonData = servicesLessonData;
            _servicesOfStudent = servicesOfStudent;
            _servicesOfEpisode = servicesOfEpisode;
            _servicesStudent = servicesStudent;
            _servicesOfDemand = servicesOfDemand;
            _servicesDemand = servicesDemand;
            _servicesStudentLog = servicesStudentLog;
            _userManager = userManager;
        }

        public JsonResult GetVersesBySurah(int surahId)
        {
            return Json(_context.Verses.Where(p => p.SurahId == surahId).ToList());
        }
        #region ListStudents
        [HttpGet]
        [Authorize(Permissions.Students.View)]
        public IActionResult Students()
        {
            ViewBag.SyncOrAsync = "Asynchronous";

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDate = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
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
            master.StudentVM = new();
            master.StudentVM.Students = _servicesStudent.GetAll();
            master.StudentVM.StudentsForMen = _servicesOfStudent.GetStudentsForMen();
            master.StudentVM.StudentsForWomen = _servicesOfStudent.GetStudentsForWomen();
            master.StudentVM.StudentsWithSupervisor = _servicesOfStudent.GetStudentsWithSupervisor(userId);
            master.StudentVM.StudentsWithTeacher = _servicesOfStudent.GetStudentsWithTeacher(userId);
            master.StudentVM.StudentsWithParent = _servicesOfStudent.GetStudentsWithParent(userDate);
            master.StudentVM.LogStudents = _servicesStudentLog.GetAll();
            master.StudentVM.NewStudent = new Student();
            master.StudentVM.EVM = new EpisodeViewModel()
            {
                Episodes = _servicesEpisode.GetAll(),
                EpisodesForMen = _servicesOfEpisode.GetEpisodesForMen(),
                EpisodesForWomen = _servicesOfEpisode.GetEpisodesForWomen(),
                EpisodesWithSupervisor = _servicesOfEpisode.GetEpisodesWithSupervisor(userId),
                EpisodesWithTeacher = _servicesOfEpisode.GetEpisodesWithTeacher(userId)
            };
            master.StudentVM.Centers = _servicesCenter.GetAll();
            master.StudentVM.Episodes = _servicesEpisode.GetAll();
            master.StudentVM.EpisodesForMen = _servicesOfEpisode.GetEpisodesForMen();
            master.StudentVM.EpisodesForWomen = _servicesOfEpisode.GetEpisodesForWomen();
            master.StudentVM.EpisodesWithSupervisor = _servicesOfEpisode.GetEpisodesWithSupervisor(userId);
            master.StudentVM.EpisodesWithTeacher = _servicesOfEpisode.GetEpisodesWithTeacher(userId);

            master.StudentVM.LevelsData = _servicesOfLevel.GetAllLevelsWithLessons();
            master.StudentVM.ListLevels = new SelectList(_context.LevelsData.AsNoTracking().Where(x => x.Num > 0 && x.Num < 8).OrderBy(x => x.Id).ToList(), "Id", "Name");

            master.StudentVM.PlansData = _servicesPlanData.GetAll();
            master.StudentVM.LessonsData = _servicesLessonData.GetAll();
            master.StudentVM.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.AvilableNationalities = new SelectList(_context.Nationalities.AsNoTracking().OrderBy(x => x.Name).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");
            master.StudentVM.AvilableHealthConditions = new SelectList(_context.HealthConditions.AsNoTracking().OrderBy(x => x.Num).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");
            master.StudentVM.Nationalities = _servicesOfStudent.GetNationalities();
            master.StudentVM.NewNationality = new Nationality();
            master.StudentVM.HealthConditions = _servicesOfStudent.GetHealth();
            master.StudentVM.NewHealth = new HealthCondition();
            master.StudentVM.Verses = new List<Verse>();
            master.StudentVM.EPisodes_List = new SelectList(_context.Episodes.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.EPisodesMen_List = new SelectList(_context.Episodes.AsNoTracking().Where(x => x.EpLocation.LoCenter.Type.Equals("men")).OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.EPisodesWomen_List = new SelectList(_context.Episodes.AsNoTracking().Where(x => x.EpLocation.LoCenter.Type.Equals("women")).OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.EPisodesSupervisor_List = new SelectList(_context.Episodes.AsNoTracking().Where(x => x.EpLocation.LoCenter.ApplicationUserId.Equals(userId)).OrderBy(x => x.Id).ToList(), "Id", "Name");


            ViewBag.Verses = new SelectList(master.StudentVM.Verses, "Id", "Num");
            return View(master);
        }
        #endregion

        #region List Of Episodes of Students [View]
        [Authorize(Permissions.Students.View)]
        public IActionResult EpisodesOfStudents()
        {
            ViewBag.SyncOrAsync = "Asynchronous";

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDate = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
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
            master.StudentVM = new();
            master.StudentVM.Students = _servicesStudent.GetAll();
            master.StudentVM.StudentsForMen = _servicesOfStudent.GetStudentsForMen();
            master.StudentVM.StudentsForWomen = _servicesOfStudent.GetStudentsForWomen();
            master.StudentVM.StudentsWithSupervisor = _servicesOfStudent.GetStudentsWithSupervisor(userId);
            master.StudentVM.StudentsWithTeacher = _servicesOfStudent.GetStudentsWithTeacher(userId);
            master.StudentVM.StudentsWithParent = _servicesOfStudent.GetStudentsWithParent(userDate);
            master.StudentVM.LogStudents = _servicesStudentLog.GetAll();
            master.StudentVM.NewStudent = new Student();

            master.StudentVM.Centers = _servicesCenter.GetAll();
            master.StudentVM.EVM = new EpisodeViewModel()
            {
                Episodes = _servicesEpisode.GetAll(),
                EpisodesForMen = _servicesOfEpisode.GetEpisodesForMen(),
                EpisodesForWomen = _servicesOfEpisode.GetEpisodesForWomen(),
                EpisodesWithSupervisor = _servicesOfEpisode.GetEpisodesWithSupervisor(userId),
                EpisodesWithTeacher = _servicesOfEpisode.GetEpisodesWithTeacher(userId)
            };
            master.StudentVM.LevelsData = _servicesOfLevel.GetAllLevelsWithLessons();
            master.StudentVM.ListLevels = new SelectList(_context.LevelsData.AsNoTracking().Where(x => x.Num > 0 && x.Num < 8).OrderBy(x => x.Num).ToList(), "Id", "Name");

            master.StudentVM.PlansData = _servicesPlanData.GetAll();
            master.StudentVM.LessonsData = _servicesLessonData.GetAll();
            master.StudentVM.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.AvilableNationalities = new SelectList(_context.Nationalities.AsNoTracking().OrderBy(x => x.Name).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");
            master.StudentVM.AvilableHealthConditions = new SelectList(_context.HealthConditions.AsNoTracking().OrderBy(x => x.Num).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");
            master.StudentVM.Nationalities = _servicesOfStudent.GetNationalities();
            master.StudentVM.NewNationality = new Nationality();
            master.StudentVM.HealthConditions = _servicesOfStudent.GetHealth();
            master.StudentVM.NewHealth = new HealthCondition();
            master.StudentVM.Verses = new List<Verse>();

            ViewBag.Verses = new SelectList(master.StudentVM.Verses, "Id", "Num");
            return View(master);
        }
        #endregion

        #region Details Of Episode Of Students [Display Episode With Students]

        [HttpGet]
        public IActionResult SelectedStudentsOfEpisode(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDate = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));

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
            master.StudentVM = new();

            master.StudentVM = _servicesOfEpisode.GetStudentsForEpisode(id);
            master.StudentVM.SelectedEpisode = _servicesOfEpisode.GetById(id);

            master.StudentVM.StudentsWithParent = _servicesOfStudent.GetStudentsWithParent(userDate);
            master.StudentVM.LogStudents = _servicesStudentLog.GetAll();
            master.StudentVM.NewStudent = new Student();
            master.StudentVM.Centers = _servicesCenter.GetAll();

            master.StudentVM.Episodes = _servicesEpisode.GetAll();
            master.StudentVM.EpisodesForMen = _servicesOfEpisode.GetEpisodesForMen();
            master.StudentVM.EpisodesForWomen = _servicesOfEpisode.GetEpisodesForWomen();
            master.StudentVM.EpisodesWithSupervisor = _servicesOfEpisode.GetEpisodesWithSupervisor(userId);
            master.StudentVM.EpisodesWithTeacher = _servicesOfEpisode.GetEpisodesWithTeacher(userId);

            master.StudentVM.LevelsData = _servicesOfLevel.GetAllLevelsWithLessons();
            master.StudentVM.PlansData = _servicesPlanData.GetAll();
            master.StudentVM.LessonsData = _servicesLessonData.GetAll();

            master.StudentVM.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");
            master.StudentVM.AvilableNationalities = new SelectList(_context.Nationalities.AsNoTracking().OrderBy(x => x.Name).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");
            master.StudentVM.AvilableHealthConditions = new SelectList(_context.HealthConditions.AsNoTracking().OrderBy(x => x.Num).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");

            master.StudentVM.Nationalities = _servicesOfStudent.GetNationalities();
            master.StudentVM.NewNationality = new Nationality();

            master.StudentVM.HealthConditions = _servicesOfStudent.GetHealth();
            master.StudentVM.NewHealth = new HealthCondition();

            master.StudentVM.Verses = new List<Verse>();
            ViewBag.Verses = new SelectList(master.StudentVM.Verses, "Id", "Num");

            return View(master);


        }
        #endregion

        #region Delete Student [Delete & Delete Log ]
        [Authorize(Permissions.Students.Delete)]
        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            //var ep = _servicesOfEpisode.GetById(Id);
            if (_servicesStudent.Delete(Id) && _servicesStudentLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction("EpisodesOfStudents", "Students");
            return RedirectToAction("EpisodesOfStudents", "Students");

        }

        [Authorize(Permissions.Students.DeleteLog)]
        public IActionResult DeleteLog(Guid Id)
        {
            var ep = _servicesOfEpisode.GetById(Id);
            if (_servicesStudentLog.DeleteLog(Id))
                return RedirectToAction("SelectedStudentsOfEpisode", "Students", ep);
            return RedirectToAction("SelectedStudentsOfEpisode", "Students", ep);
        }


        #endregion

        #region Add|Edit Student [Create & Update]
        [Authorize(Permissions.Students.Create), Authorize(Permissions.Students.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                if (model.StudentVM.NewStudent.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                    if (_servicesStudent.FindBy(model.StudentVM.NewStudent.Name) != null)
                        SessionMsg(Helper.Error, "Exist !", "Student is Exist");
                    else
                    {
                        if (_servicesStudent.Save(model.StudentVM.NewStudent)
                            && _servicesStudentLog.Save(model.StudentVM.NewStudent.Id, Guid.Parse(userId)))
                            SessionMsg(Helper.Success, " تم الاضافة ", "تم اضافة الطالب بنجاح");
                        else
                            SessionMsg(Helper.Error, "خطأ الاضافة", "حدث خطأ اثناء اضافة الطالب بسبب ادخال بعض البيانات بشكل خاطئ !");
                    }
                }
                else
                { //Update
                    if (_servicesStudent.Save(model.StudentVM.NewStudent)
                            && _servicesStudentLog.Update(model.StudentVM.NewStudent.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, "تم التعديل", "تم تعديل بيانات الطالب بنجاح");
                    else
                        SessionMsg(Helper.Error, "خطأ في التعديل", "حدث خطأ  اثناء تعديل بيانات الطالب بسبب ادخال بعض البيانات بشكل خاطئ !");
                }
            }
            if (_servicesOfStudent.WaitEpisodes(model.StudentVM.NewStudent))
                return RedirectToAction("EpisodesOfStudents", "Students");
            else
            {
                var ep = _servicesOfEpisode.GetById(model.StudentVM.NewStudent.StEpisodeId);
                return RedirectToAction("SelectedStudentsOfEpisode", "Students", ep);
            }

        }
        #endregion

        #region Assigned Students To Episode
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AssignedStudent(MasterViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
            {
                if (_servicesOfStudent.Assigned(model.StudentVM.NewStudent))
                {
                    SessionMsg(Helper.Success, "تم النقل !", "تم نقل الطالب الي الحلقة الجديدة بنجاح");
                    return RedirectToAction("Students", "Students");
                }
                else
                    SessionMsg(Helper.Error, "خطأ في النقل ", "حدث خطأ اثناء نقل الطالب بسبب ادخال خطأ في بعض البيانات");
            }
            return RedirectToAction("Students", "Students");

        }
        #endregion

        #region Details Of Student [List Evaluations & Student info]
        [HttpGet]
        //[Authorize(Permissions.Students.ViewDemand)]
        public IActionResult SelectedStudent(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDate = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
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
            master.StudentVM = new()
            {
                NewStudent = _servicesOfStudent.GetById(id),
                Evaluations = _servicesOfStudent.GetEvaluationsForStudent(id),
                Demands = _servicesOfStudent.GetDemandsForStudent(id),
                Centers = _servicesCenter.GetAll()
            };
            return View(master);
        }
        #endregion

        #region Details Of Student Demands 
        [HttpGet]
        //[Authorize(Permissions.Students.ViewDemand)]
        public IActionResult Demands(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDate = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
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
            master.StudentVM = new()
            {
                NewStudent = _servicesOfStudent.GetById(id),
                //Demands = _servicesOfStudent.GetDemandsForStudent(id),

            };
            return View(master);
        }
        #endregion

        #region Add|Edit|Delete Nationality [Create & Update & Delete]
        // [Authorize(Permissions.Episodes.Create), Authorize(Permissions.Episodes.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SaveNationality(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.StudentVM.NewNationality.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create

                    if (_servicesOfStudent.FindByNationalityName(model.StudentVM.NewNationality.Name) != null)
                        SessionMsg(Helper.Error, "جنسية مكررة !", "الجنسية موجودة في القائمة من قبل");
                    else
                    {
                        if (_servicesOfStudent.SaveNationality(model.StudentVM.NewNationality))
                            SessionMsg(Helper.Success, "تم الاضافة !", "تم اضافة الجنسية الي القائمة بنجاح");
                        else
                            SessionMsg(Helper.Error, "خطأ في الاضافة", "خطأ اثناء اضافة الجنسية بسبب ادخال خاطئ للبيانات");
                    }
                }
                else
                { //Update
                    //if (_servicesOfEpisode.SaveEpisodeType(model.NewEpisodeType)) { }
                    ////SessionMsg(Helper.Success, "Episode Type Update", "Episode Type is Updated Successfully");
                    //else
                    //    SessionMsg(Helper.Error, "Episode Type Not Update", "Episode Type isn't Updated !");

                }
            }

            return RedirectToAction("EpisodesOfStudents", "Students");

        }

        public IActionResult DeleteNationality(Guid Id)
        {
            if (_servicesOfStudent.DeletNationality(Id))
                return RedirectToAction("EpisodesOfStudents", "Students");
            return RedirectToAction("EpisodesOfStudents", "Students");
        }
        #endregion

        #region Add|Edit|Delete Health Conditions [Create & Update & Delete]
        // [Authorize(Permissions.Episodes.Create), Authorize(Permissions.Episodes.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SaveHealthCondition(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.StudentVM.NewHealth.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create

                    if (_servicesOfStudent.FindByHealthName(model.StudentVM.NewHealth.Name) != null)
                        SessionMsg(Helper.Error, "حالة صحية مكررة !", "الحالة الصحية موجودة في القائمة من قبل");
                    else
                    {
                        if (_servicesOfStudent.SaveHealth(model.StudentVM.NewHealth))
                            SessionMsg(Helper.Success, "تم الاضافة !", "تم اضافة الحالة الصحية الي القائمة بنجاح !");
                        else
                            SessionMsg(Helper.Error, "خطأ في الاضافة", "حدث خطأ اثناء اضافة الحالة الصحية بسبب ادخال خاطئ للبيانات");
                    }
                }
                else
                { //Update
                    //if (_servicesOfEpisode.SaveEpisodeType(model.NewEpisodeType)) { }
                    ////SessionMsg(Helper.Success, "Episode Type Update", "Episode Type is Updated Successfully");
                    //else
                    //    SessionMsg(Helper.Error, "Episode Type Not Update", "Episode Type isn't Updated !");

                }
            }

            return RedirectToAction("EpisodesOfStudents", "Students");

        }

        public IActionResult DeleteHealthCondition(Guid Id)
        {
            if (_servicesOfStudent.DeleteHealth(Id))
                return RedirectToAction("EpisodesOfStudents", "Students");
            return RedirectToAction("EpisodesOfStudents", "Students");
        }
        #endregion

        #region Add New Student By Request With Parent
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddNewStudentParentRequest(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (model.StudentVM.NewStudent.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                  ////Exist
                  //if (_servicesDemand.FindBy(model.NewDemand.Request_Type) != null)
                  //    SessionMsg(Helper.Error, "Exist !", "Demand is Exist");

                    //else
                    //{

                    //}
                    if (_servicesOfStudent.AddNewStudentRequest(model.StudentVM.NewStudent))
                        SessionMsg(Helper.Success, "تم الاضافة  !", "تم اضافة طلب لاضافة الطالب بنجاح");
                    else
                        SessionMsg(Helper.Error, "خطأ في الاضافة", "حدث خطأ في الاضافة اثناء قبول الطلب");
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

        #endregion
        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }
    }

}
