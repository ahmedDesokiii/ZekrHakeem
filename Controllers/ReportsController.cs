using ClosedXML.Excel;
using System.Data;
using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IServicesReport _servicesOfReport;
        private readonly IServicesEpisode _servicesOfEpisode;

        public ReportsController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IServicesReport servicesOfReport, IServicesEpisode servicesOfEpisode)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _userManager = userManager;
            _context = context;
            _servicesOfReport = servicesOfReport;
            _servicesOfEpisode = servicesOfEpisode;
        }
        #region Attendance_Reports
        public IActionResult TeachersAttendance()
        {
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

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                ReportVM = new()
                {
                    AllAttendances = _servicesOfReport.GetAllAttendance(),
                    AllAttendancesMen = _servicesOfReport.GetAllAttendanceMen(),
                    AllAttendancesWomen = _servicesOfReport.GetAllAttendanceWomen(),

                    EpisodesMen = new SelectList(_context.Episodes
                                                     .AsNoTracking()
                                                     .Include(x => x.EpLocation)
                                                     .Include(x => x.ApplicationUser)
                                                     .Where(x => x.EpLocation.Type == "mosque" && x.AttendType == "حضوري" && x.CurrentState > 0)
                                                     .OrderBy(x => x.Name)
                                                     .ToList(), "Id", "Name"),

                    EpisodesWomen = new SelectList(_context.Episodes
                                                     .AsNoTracking()
                                                     .Include(x => x.EpLocation)
                                                     .Where(x => x.EpLocation.Type == "dar" && x.AttendType == "حضوري" && x.CurrentState > 0)
                                                     .OrderBy(x => x.AttendType)
                                                     .ThenBy(x => x.Name)
                                                     .ToList(), "Id", "Name"),

                    TeachersMen = new SelectList(allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString()))
                                                     .ToList(), "Id", "FullName"),
                    TeachersWomen = new SelectList(allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.TeacherWomen.ToString()))
                                                     .ToList(), "Id", "FullName"),


                },
                EpisodeVM = new() { PrayerTimesAPI = _servicesOfEpisode.GetPrayerTimesAPI() },
                DemandVM = new()
                {
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
        public IActionResult SingleTeacherAttendance(string Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                ReportVM = new()
                {
                    Teacher = _servicesOfReport.GetById(Id),
                    TeacherAttendances = _servicesOfReport.GetTeacherAttendance(Id),

                    EpisodesTeacher = new SelectList(_context.Episodes
                                                     .AsNoTracking()
                                                     .Include(x => x.EpLocation)
                                                     .Include(x => x.ApplicationUser)
                                                     .Where(x => x.ApplicationUserId == Id && x.AttendType == "حضوري" && x.CurrentState > 0)
                                                     .OrderBy(x => x.Name)
                                                     .ToList(), "Id", "Name"),
                    Plans = new SelectList(_context.PlansData
                .AsNoTracking()
                .Include(x => x.Students)
                .Include(x => x.LevelsData)
                .Where(x => x.CurrentState > 0)
                .OrderBy(x => x.Name)
                .ToList(), "Id", "Name"
                )
                },
                EpisodeVM = new() { PrayerTimesAPI = _servicesOfEpisode.GetPrayerTimesAPI() },
                DemandVM = new()
                {
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
        public IActionResult SingleTeacherAttendanceByTeacher()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                ReportVM = new()
                {
                    Teacher = _servicesOfReport.GetById(userId),
                    TeacherAttendances = _servicesOfReport.GetTeacherAttendance(userId),
                    EpisodesTeacher = new SelectList(_context.Episodes
                                                     .AsNoTracking()
                                                     .Include(x => x.EpLocation)
                                                     .Include(x => x.ApplicationUser)
                                                     .Where(x => x.ApplicationUserId == userId && x.AttendType == "حضوري" && x.CurrentState > 0)
                                                     .OrderBy(x => x.Name)
                                                     .ToList(), "Id", "Name"),
                    Plans = new SelectList(_context.PlansData
                .AsNoTracking()
                .Include(x => x.Students)
                .Include(x => x.LevelsData)
                .Where(x => x.CurrentState > 0)
                .OrderBy(x => x.Name)
                .ToList(), "Id", "Name"
                )
                },
                DemandVM = new()
                {
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

        #region StudentsEvaluations_Reports
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult StudentsEvaluations(DateTime start, DateTime end)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new();
            master.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            master.ReportVM = new();

            master.ReportVM.StartDate = start;
            master.ReportVM.EndDate = end;
            master.ReportVM.EpisodesMen = new SelectList(_context.Episodes
                                                    .AsNoTracking()
                                                    .Include(x => x.EpLocation)
                                                    .Include(x => x.ApplicationUser)
                                                    .Where(x => x.CurrentState > 0 && x.EpLocation.Type == "mosque" && x.AttendType == "حضوري")
                                                    .OrderBy(x => x.Name)
                                                    .ToList(), "Id", "Name");
            master.ReportVM.EpisodesWomen = new SelectList(_context.Episodes
                                                 .AsNoTracking()
                                                 .Include(x => x.EpLocation)
                                                 .Where(x => x.CurrentState > 0 && x.EpLocation.Type == "dar" && x.AttendType == "حضوري")
                                                 .OrderBy(x => x.AttendType)
                                                 .ThenBy(x => x.Name)
                                                 .ToList(), "Id", "Name");
            master.ReportVM.StudentsMen = new SelectList(_context.Students
                                                 .AsNoTracking()
                                                 .Include(x => x.StEpisode)
                                                 .Include(x => x.ApplicationUser)
                                                 .Include(x => x.Surah)
                                                 .Include(x => x.PlanData)
                                                 .Include(x => x.Verse)
                                                 .Include(x => x.Evaluations)
                                                 .Include(x => x.HealthCondition)
                                                 .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocation.Type == "mosque")
                                                 .OrderBy(x => x.StEpisode.Name).ThenBy(x => x.StEpisode.Period).ThenBy(x => x.Name)
                                                 .ToList(), "Id", "Name");
            master.ReportVM.Plans = new SelectList(_context.PlansData.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.ReportVM.Nationalities = new SelectList(_context.Nationalities.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.ReportVM.AllStudentsMen = _servicesOfReport.GetAllStudentsMen();
            master.ReportVM.Evaluations = _context.Evaluations.Include(x => x.EvStudent)
                            .Where(x => x.CurrentState > 0 && x.Ev_Date.Date >= start.Date && x.Ev_Date.Date <= end.Date)
                            .ToList();
            master.ReportVM.FristEvaluateOfStudents = _servicesOfReport.GetStudentFristEvaluate(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            master.ReportVM.LastEvaluateOfStudents = _servicesOfReport.GetStudentLastEvaluate(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            master.ReportVM.CountAttend_0 = _servicesOfReport.GetCountAttend_0(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            master.ReportVM.CountAttend_1 = _servicesOfReport.GetCountAttend_1(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            master.ReportVM.CountAttend_2 = _servicesOfReport.GetCountAttend_2(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            master.ReportVM.CountAttend_3 = _servicesOfReport.GetCountAttend_3(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            master.ReportVM.CountAttend_4 = _servicesOfReport.GetCountAttend_4(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);

            master.ReportVM.CountAttend_5 = _servicesOfReport.GetCountAttend_5(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);

            master.ReportVM.SavePages = _servicesOfReport.GetTotalPages_Save(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            master.ReportVM.RevPages = _servicesOfReport.GetTotalPages_Rev(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            master.ReportVM.RabtPages = _servicesOfReport.GetTotalPages_Rabt(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);

            master.ReportVM.SaveSections = _servicesOfReport.GetTotalSections_Save(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            master.ReportVM.RevSections = _servicesOfReport.GetTotalSections_Rev(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            master.ReportVM.RabtSections = _servicesOfReport.GetTotalSections_Rabt(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);

            return View("StudentsEvaluations", master);
        }
        public IActionResult StudentsEvaluations(MasterViewModel master)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = master;
            result.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            if (result.ReportVM.StartDate == null)
            {
                result.ReportVM.StartDate = new DateTime(2025, 01, 01);
                result.ReportVM.EndDate = DateTime.Now.Date;
            }
            else
            {
                result.ReportVM.StartDate = master.ReportVM.StartDate;
                result.ReportVM.EndDate = master.ReportVM.EndDate;
            }
            result.ReportVM.EpisodesMen = new SelectList(_context.Episodes
                                                         .AsNoTracking()
                                                         .Include(x => x.EpLocation)
                                                         .Include(x => x.ApplicationUser)
                                                         .Where(x => x.CurrentState > 0 && x.EpLocation.Type == "mosque" && x.AttendType == "حضوري")
                                                         .OrderBy(x => x.Name)
                                                         .ToList(), "Id", "Name");
            result.ReportVM.EpisodesWomen = new SelectList(_context.Episodes
                                                 .AsNoTracking()
                                                 .Include(x => x.EpLocation)
                                                 .Where(x => x.CurrentState > 0 && x.EpLocation.Type == "dar" && x.AttendType == "حضوري")
                                                 .OrderBy(x => x.AttendType)
                                                 .ThenBy(x => x.Name)
                                                 .ToList(), "Id", "Name");
            result.ReportVM.StudentsMen = new SelectList(_context.Students
                                                 .AsNoTracking()
                                                 .Include(x => x.StEpisode)
                                                 .Include(x => x.ApplicationUser)
                                                 .Include(x => x.Surah)
                                                 .Include(x => x.PlanData)
                                                 .Include(x => x.Verse)
                                                 .Include(x => x.Evaluations)
                                                 .Include(x => x.HealthCondition)
                                                 .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocation.Type == "mosque")
                                                 .OrderBy(x => x.StEpisode.Name).ThenBy(x => x.StEpisode.Period).ThenBy(x => x.Name)
                                                 .ToList(), "Id", "Name");

            result.ReportVM.Plans = new SelectList(_context.PlansData.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name");
            result.ReportVM.Nationalities = new SelectList(_context.Nationalities.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name");

            result.ReportVM.AllStudentsMen = _servicesOfReport.GetAllStudentsMen();
            result.ReportVM.AllStudents = _servicesOfReport.GetAllStudents();
            result.ReportVM.AllStudentsWomen = _servicesOfReport.GetAllStudentsWomen();
            _servicesOfReport.CheckEmptyEvaluations(result.ReportVM.AllStudentsMen); // check for every students have at least one evaluation



            result.ReportVM.Evaluations = _context.Evaluations.Include(x => x.EvStudent)
                                     .Where(x => x.CurrentState > 0 && x.Ev_Date.Date >= Convert.ToDateTime(result.ReportVM.StartDate).Date && x.Ev_Date.Date <= Convert.ToDateTime(result.ReportVM.EndDate).Date)
                                     .OrderBy(x => x.Ev_Date).ToList();


            result.ReportVM.FristEvaluateOfStudents = _servicesOfReport.GetStudentFristEvaluate(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            result.ReportVM.LastEvaluateOfStudents = _servicesOfReport.GetStudentLastEvaluate(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);

            result.ReportVM.CountAttend_0 = _servicesOfReport.GetCountAttend_0(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            result.ReportVM.CountAttend_1 = _servicesOfReport.GetCountAttend_1(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            result.ReportVM.CountAttend_2 = _servicesOfReport.GetCountAttend_2(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            result.ReportVM.CountAttend_3 = _servicesOfReport.GetCountAttend_3(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);
            result.ReportVM.CountAttend_4 = _servicesOfReport.GetCountAttend_4(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);

            result.ReportVM.CountAttend_5 = _servicesOfReport.GetCountAttend_5(master.ReportVM.AllStudentsMen, master.ReportVM.Evaluations);

            result.ReportVM.SavePages = _servicesOfReport.GetTotalPages_Save(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            result.ReportVM.RevPages = _servicesOfReport.GetTotalPages_Rev(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            result.ReportVM.RabtPages = _servicesOfReport.GetTotalPages_Rabt(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);

            result.ReportVM.SaveSections = _servicesOfReport.GetTotalSections_Save(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            result.ReportVM.RevSections = _servicesOfReport.GetTotalSections_Rev(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            result.ReportVM.RabtSections = _servicesOfReport.GetTotalSections_Rabt(master.ReportVM.FristEvaluateOfStudents, master.ReportVM.LastEvaluateOfStudents);
            return View(result);
        }
        #endregion

        #region LastStudentLessons_Reports
        public IActionResult StudentsLastLessons(MasterViewModel master)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = master;
            result.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            result.ReportVM = new()
            {
                AllStudentsMen = _servicesOfReport.GetAllStudentsMen(),
                AllStudents = _servicesOfReport.GetAllStudents(),
                AllStudentsWomen = _servicesOfReport.GetAllStudentsWomen(),
                AllStudentsWithTeacher = _servicesOfReport.GetAllStudentsWithTeacher(userId),

                Plans = new SelectList(_context.PlansData.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name"),
                Nationalities = new SelectList(_context.Nationalities.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name"),
            };
            result.ReportVM.NextLessonData_AllStudent = _servicesOfEpisode.GetStudentNextLesson(result.ReportVM.AllStudents);
            result.ReportVM.NextLessonData_AllStudentMen = _servicesOfEpisode.GetStudentNextLesson(result.ReportVM.AllStudentsMen);
            result.ReportVM.NextLessonData_AllStudentWomen = _servicesOfEpisode.GetStudentNextLesson(result.ReportVM.AllStudentsWomen);
            result.ReportVM.NextLessonData_AllStudentWithTeacher = _servicesOfEpisode.GetStudentNextLesson(result.ReportVM.AllStudentsWithTeacher);
            return View(result);
        }
        public IActionResult StudentsLastLessons_Teacher(MasterViewModel master)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = master;
            result.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            result.ReportVM = new()
            {
                AllStudentsMen = _servicesOfReport.GetAllStudentsMen(),
                AllStudents = _servicesOfReport.GetAllStudents(),
                AllStudentsWomen = _servicesOfReport.GetAllStudentsWomen(),
                AllStudentsWithTeacher = _servicesOfReport.GetAllStudentsWithTeacher(userId),

                Plans = new SelectList(_context.PlansData.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name"),
                Nationalities = new SelectList(_context.Nationalities.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name"),
            };
            result.ReportVM.NextLessonData_AllStudent = _servicesOfEpisode.GetStudentNextLesson(result.ReportVM.AllStudents);
            result.ReportVM.NextLessonData_AllStudentMen = _servicesOfEpisode.GetStudentNextLesson(result.ReportVM.AllStudentsMen);
            result.ReportVM.NextLessonData_AllStudentWomen = _servicesOfEpisode.GetStudentNextLesson(result.ReportVM.AllStudentsWomen);
            result.ReportVM.NextLessonData_AllStudentWithTeacher = _servicesOfEpisode.GetStudentNextLesson(result.ReportVM.AllStudentsWithTeacher);
            return View(result);
        }
        #endregion

        #region DailyEvaluation
        // Supervisor - Admin
        public IActionResult StudentsDailyEvaluations(MasterViewModel master)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = master;
            result.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            result.ReportVM = new()
            {
                AllStudentsMen = _servicesOfReport.GetAllStudentsMen(),
                AllStudents = _servicesOfReport.GetAllStudents(),
                AllStudentsWomen = _servicesOfReport.GetAllStudentsWomen(),
                AllStudentsWithTeacher = _servicesOfReport.GetAllStudentsWithTeacher(userId),

                Plans = new SelectList(_context.PlansData.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name"),
                Nationalities = new SelectList(_context.Nationalities.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name"),
                Evaluations = _context.Evaluations.Include(x => x.EvStudent)
                                     .Where(x => x.CurrentState > 0 && x.Ev_Date.Date == DateTime.Now.Date.AddDays(-1))
                                     .OrderByDescending(x => x.Ev_Date).ToList()

            };
            return View(result);
        }
        //Teacher
        public IActionResult StudentsDailyEvaluations_Teacher(MasterViewModel master)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = master;
            result.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };
            result.ReportVM = new()
            {
                AllStudentsMen = _servicesOfReport.GetAllStudentsMen(),
                AllStudents = _servicesOfReport.GetAllStudents(),
                AllStudentsWomen = _servicesOfReport.GetAllStudentsWomen(),
                AllStudentsWithTeacher = _servicesOfReport.GetAllStudentsWithTeacher(userId),

                Plans = new SelectList(_context.PlansData.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name"),
                Nationalities = new SelectList(_context.Nationalities.AsNoTracking().Where(x => x.CurrentState > 0).OrderBy(x => x.Id).ToList(), "Id", "Name"),
                Evaluations = _context.Evaluations.Include(x => x.EvStudent)
                                     .Where(x => x.CurrentState > 0 && x.Ev_Date.Date == DateTime.Now.Date.AddDays(-1))
                                     .OrderByDescending(x => x.Ev_Date).ToList()

            };
            return View(result);
        }
        #endregion

        #region Export to Excel [Daily Evaluations]
        public async Task<FileResult> ExportDailyEvaluationInExcel()
        {
            var evals = _context.Evaluations
                                    .Include(x => x.EvStudent).Include(x => x.EvStudent.Nationality)
                                    .Include(x => x.EvStudent.StEpisode).Include(x => x.EvStudent.PlanData)
                                    .Include(x => x.EvStudent.StEpisode.ApplicationUser)
                                     .Where(x => x.CurrentState > 0 && x.Ev_Date.Date == DateTime.Now.Date.AddDays(-1))
                                     .OrderByDescending(x => x.Ev_Date).ToList();
            var fileName = $"dailyEvaluations_{DateTime.Now.Date.AddDays(-1)}.xlsx";
            return GenerateExcel(fileName, evals);
        }
        private FileResult GenerateExcel(string fileName, IEnumerable<Evaluation> evals)
        {
            DataTable dt = new DataTable("Evaluations");
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("اليوم"),
                new DataColumn("التاريخ"),
                new DataColumn("الطالب"),
                new DataColumn("الجنسية"),
                new DataColumn("الخطة"),
                new DataColumn("الحلقة"),
                new DataColumn("المعلم"),
                new DataColumn("الحضور"),
                new DataColumn("الحفظ من"),
                new DataColumn("الحفظ الي"),
                new DataColumn("تقييم الحفظ"),
                new DataColumn("المراجعة من"),
                new DataColumn("المراجعة الي"),
                new DataColumn("تقييم المراجعة"),
                new DataColumn("الربط من"),
                new DataColumn("الربط الي"),
                new DataColumn("تقييم الربط"),

            });

            foreach (var eval in evals)
            {
                dt.Rows.Add(eval.Ev_Date.AddDays(-1).ToString("dddd"),
                            eval.Ev_Date.AddDays(-1).ToString("yyyy-MM-dd"),
                            eval.EvStudent.Name,
                            eval.EvStudent.Nationality.Name,
                            eval.EvStudent.PlanData.Name,
                            eval.EvStudent.StEpisode.Name,
                            eval.EvStudent.StEpisode.ApplicationUser.FullName,
                            eval.Ev_Attend,
                            eval.Ev_SaveSurahNameFrom + "(" + eval.Ev_SaveAyahIdFrom + ")",
                            eval.Ev_SaveSurahNameTo + "(" + eval.Ev_SaveAyahIdTo + ")",
                            eval.Ev_Save,
                            eval.Ev_RevSurahNameFrom + "(" + eval.Ev_RevAyahIdFrom + ")",
                            eval.Ev_RevSurahNameTo + "(" + eval.Ev_RevAyahIdTo + ")",
                            eval.Ev_Rev,
                            eval.Ev_RabtSurahNameFrom + "(" + eval.Ev_RabtAyahIdFrom + ")",
                            eval.Ev_RabtSurahNameTo + "(" + eval.Ev_RabtAyahIdTo + ")",
                            eval.Ev_Rabt
                            );
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Theme.ResolveThemeColor(XLThemeColor.Accent5);
                wb.RowHeight = 25.5;
                wb.ColumnWidth = 12.29;
                wb.RightToLeft = true;
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //wb.Style.Font;


                wb.Worksheets.Add(dt);


                using (MemoryStream stream = new MemoryStream())
                {

                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }

        }
        #endregion

        #region Export to Excel [Students Lessons]
        /*
        public async Task<FileResult> ExportStudentLessonInExcel()
        {
            /////////// Handel Admin - Supervisor - SupervisorWomen !!!!!!! 
            var evals = _context.Evaluations
                                    .Include(x => x.EvStudent).Include(x => x.EvStudent.Nationality)
                                    .Include(x => x.EvStudent.StEpisode).Include(x => x.EvStudent.PlanData)
                                    .Include(x => x.EvStudent.StEpisode.ApplicationUser)
                                    .Where(x => x.CurrentState > 0 && x.Ev_Date.Date == DateTime.Now.Date)
                                    .OrderByDescending(x => x.Ev_Date).ToList();

            var fileName = $"lessonsStudents_{DateTime.Now.Date}.xlsx";
            
            return GenerateLessonExcel(fileName, evals);
        }
        private FileResult GenerateLessonExcel(string fileName, IEnumerable<Student> Students, Dictionary<Student,LessonData> NextLesson)
        {
            DataTable dt = new DataTable("Evaluations");
            dt.Columns.AddRange(new DataColumn[] {
                new DataColumn("اليوم"),
                new DataColumn("التاريخ"),
                new DataColumn("الطالب"),
                new DataColumn("الجنسية"),
                new DataColumn("الخطة"),
                new DataColumn("الحلقة"),
                new DataColumn("المعلم"),
                new DataColumn("الحضور"),
                new DataColumn("الحفظ من"),
                new DataColumn("الحفظ الي"),
                new DataColumn("تقييم الحفظ"),
                new DataColumn("المراجعة من"),
                new DataColumn("المراجعة الي"),
                new DataColumn("تقييم المراجعة"),
                new DataColumn("الربط من"),
                new DataColumn("الربط الي"),
                new DataColumn("تقييم الربط"),

            });

            foreach (var eval in evals)
            {
                dt.Rows.Add(eval.Ev_Date.ToString("dddd"),
                            eval.Ev_Date.ToString("yyyy-MM-dd"),
                            eval.EvStudent.Name,
                            eval.EvStudent.Nationality.Name,
                            eval.EvStudent.PlanData.Name,
                            eval.EvStudent.StEpisode.Name,
                            eval.EvStudent.StEpisode.ApplicationUser.FullName,
                            eval.Ev_Attend,
                            eval.Ev_SaveSurahNameFrom + "(" + eval.Ev_SaveAyahIdFrom + ")",
                            eval.Ev_SaveSurahNameTo + "(" + eval.Ev_SaveAyahIdTo + ")",
                            eval.Ev_Save,
                            eval.Ev_RevSurahNameFrom + "(" + eval.Ev_RevAyahIdFrom + ")",
                            eval.Ev_RevSurahNameTo + "(" + eval.Ev_RevAyahIdTo + ")",
                            eval.Ev_Rev,
                            eval.Ev_RabtSurahNameFrom + "(" + eval.Ev_RabtAyahIdFrom + ")",
                            eval.Ev_RabtSurahNameTo + "(" + eval.Ev_RabtAyahIdTo + ")",
                            eval.Ev_Rabt
                            );
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Theme.ResolveThemeColor(XLThemeColor.Accent5);
                wb.RowHeight = 25.5;
                wb.ColumnWidth = 12.29;
                wb.RightToLeft = true;
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //wb.Style.Font;


                wb.Worksheets.Add(dt);


                using (MemoryStream stream = new MemoryStream())
                {

                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }

        }
        */
        #endregion
    }
}


/*
 SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

      for (int row = 0; row < content.GetLength(0); row++)
      {
          Row newRow = new Row();
          for (int col = 0; col < content.GetLength(1); col++)
          {
              Cell cell = new Cell();
              cell.DataType = CellValues.String;
              cell.CellValue = new CellValue(content[row, col]);
              newRow.AppendChild(cell);
          }
          sheetData.AppendChild(newRow);
      }
 */