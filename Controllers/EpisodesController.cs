using ExpressTimezone;
using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class EpisodesController : Controller
    {
        //public Guid selectEpisodeId { get; set; }
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly ApplicationDbContext _context;
        private readonly IServicesRepository<Location> _servicesLocation;
        private readonly IServicesRepository<Episode> _servicesEpisode;
        private readonly IServicesRepository<LessonData> _servicesLessonData;
        private readonly IServicesEpisode _servicesOfEpisode;
        private readonly IServicesLocation _servicesOfLocation;
        private readonly IServicesCenter _servicesOfCenter;
        private readonly IServicesStudent _servicesOfStudent;
        private readonly IServicesLessonData _servicesOfLessonData;
        private readonly IServicesRepositoryLog<LogEpisode> _servicesEpisodeLog;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public EpisodesController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, ApplicationDbContext context, IServicesRepository<Location> servicesLocation, IServicesRepository<Episode> servicesEpisode, IServicesRepository<LessonData> servicesLessonData, IServicesEpisode servicesOfEpisode, IServicesLocation servicesOfLocation, IServicesCenter servicesOfCenter, IServicesStudent servicesOfStudent, IServicesLessonData servicesOfLessonData, IServicesRepositoryLog<LogEpisode> servicesEpisodeLog, UserManager<ApplicationUser> userManager, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _context = context;
            _servicesLocation = servicesLocation;
            _servicesEpisode = servicesEpisode;
            _servicesLessonData = servicesLessonData;
            _servicesOfEpisode = servicesOfEpisode;
            _servicesOfLocation = servicesOfLocation;
            _servicesOfCenter = servicesOfCenter;
            _servicesOfStudent = servicesOfStudent;
            _servicesOfLessonData = servicesOfLessonData;
            _servicesEpisodeLog = servicesEpisodeLog;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #region List Of Episodes [View]
        [Authorize(Permissions.Episodes.View)]
        public IActionResult Episodes()
        {
            //ViewBag.SyncOrAsync = "Asynchronous";

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

            var fakeEp = _servicesEpisode.FindBy(new Guid("ad8a4ead-a5c8-4284-a22a-bf2c6659b9b2")); // حلقة الانتظار

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
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId)
            };
            master.EpisodeVM = new();
            master.EpisodeVM.LVM = new()
            {
                Locations = _servicesLocation.GetAll(),
                LocationsForMen = _servicesOfLocation.GetLocationsForMen(),
                LocationsForWomen = _servicesOfLocation.GetLocationsForWomen(),
                LocationsWithSupervisor = _servicesOfLocation.GetLocationsWithSupervisor(userId),
                CentersForMen = _servicesOfCenter.GetCentersForMen(),
                CentersForWomen = _servicesOfCenter.GetCentersForWomen(),
                CentersForSupervisorCenter = _servicesOfCenter.GetCentersWithSupervisor(userId)
            };

            master.EpisodeVM.NewEpisode = new Episode();
            master.EpisodeVM.Episodes = _servicesEpisode.GetAll();
            master.EpisodeVM.LogEpisodes = _servicesEpisodeLog.GetAll();
            master.EpisodeVM.EpisodesForMen = _servicesOfEpisode.GetEpisodesForMen();
            master.EpisodeVM.EpisodesForWomen = _servicesOfEpisode.GetEpisodesForWomen();
            master.EpisodeVM.EpisodesWithSupervisor = _servicesOfEpisode.GetEpisodesWithSupervisor(userId);
            master.EpisodeVM.EpisodesWithTeacher = _servicesOfEpisode.GetEpisodesWithTeacher(userId);


            master.EpisodeVM.TeachersMen = teachersmen;
            master.EpisodeVM.TeachersWomen = teacherswomen;
            master.EpisodeVM.PrayerTimesAPI = _servicesOfEpisode.GetPrayerTimesAPI();
            master.EpisodeVM.AvilableEpisodeTypes = new SelectList(_context.EpisodesTypes.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");
            master.EpisodeVM.EpisodeTypes = _servicesOfEpisode.GetEpisodeTypes();
            master.EpisodeVM.NewEpisodeType = new EpisodeType();
            master.EpisodeVM.EpisodeInTime = fakeEp;
            _servicesOfEpisode.ResetAllEpisodes();


            if (User.IsInRole(Roles.Teacher.ToString()) || User.IsInRole(Roles.TeacherWomen.ToString()))
            {
                foreach (var episode in master.EpisodeVM.EpisodesWithTeacher)
                {
                    var utcTime = DateTime.UtcNow;
                    var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh").TimeOfDay;

                    var epStartTime_Fajr = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.FajrTime).TimeOfDay;
                    var epEndTime_Fajr = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.EndFajrTime).TimeOfDay;
                    var epStartTime_Sunrise = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.SunriseTime).TimeOfDay;
                    var epEndTime_Sunrise = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.EndSunriseTime).TimeOfDay;
                    var epStartTime_Zohr = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.ZohrTime).TimeOfDay;
                    var epEndTime_Zohr = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.EndZohrTime).TimeOfDay;
                    var epStartTime_Asr = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.AsrTime).TimeOfDay;
                    var epEndTime_Asr = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.EndAsrTime).TimeOfDay;
                    var epStartTime_Mghrb = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.MghrbTime).TimeOfDay;
                    var epEndTime_Mghrb = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.EndMghrbTime).TimeOfDay;
                    var epStartTime_Isha = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.IshaTime).TimeOfDay;
                    var epEndTime_Isha = Convert.ToDateTime(master.EpisodeVM.PrayerTimesAPI.EndIshaTime).TimeOfDay;


                    if (episode.Period == null || episode.Period.Contains("عن بعد"))
                    { }
                    else
                    {
                        // check episode in time 
                        if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.الفجر.ToString()) && currentTime >= epStartTime_Fajr && currentTime <= epEndTime_Fajr)
                            master.EpisodeVM.EpisodeInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.الشروق.ToString()) && currentTime >= epStartTime_Sunrise && currentTime <= epEndTime_Sunrise)
                            master.EpisodeVM.EpisodeInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.الظهر.ToString()) && currentTime >= epStartTime_Zohr && currentTime <= epEndTime_Zohr)
                            master.EpisodeVM.EpisodeInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.العصر.ToString()) && currentTime >= epStartTime_Asr && currentTime <= epEndTime_Asr)
                            master.EpisodeVM.EpisodeInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.المغرب.ToString()) && currentTime >= epStartTime_Mghrb && currentTime <= epEndTime_Mghrb)
                            master.EpisodeVM.EpisodeInTime = episode;

                        else if (episode.IsAvilable && !episode.ManualFinished && episode.Period.Contains(PrayerTimesPeriods.العشاء.ToString()) && currentTime >= epStartTime_Isha && currentTime <= epEndTime_Isha)
                            master.EpisodeVM.EpisodeInTime = episode;

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

        #region Delete Episode [Delete & Delete Log & Delete Episode Type]
        [Authorize(Permissions.Episodes.Delete)]
        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesEpisode.Delete(Id) && _servicesEpisodeLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction("Episodes", "Episodes");
            return RedirectToAction("Episodes", "Episodes");

        }

        [Authorize(Permissions.Episodes.DeleteLog)]
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesEpisodeLog.DeleteLog(Id))
                return RedirectToAction("Episodes", "Episodes");
            return RedirectToAction("Episodes", "Episodes");
        }
        //[Authorize(Permissions.Episodes.Delete)]
        public IActionResult DeleteEpisodeType(Guid Id)
        {
            if (_servicesOfEpisode.DeleteEpisodeType(Id))
                return RedirectToAction("Episodes", "Episodes");

            return RedirectToAction("Episodes", "Episodes");
        }
        #endregion

        #region Add|Edit Episode [Create & Update]
        [Authorize(Permissions.Episodes.Create), Authorize(Permissions.Episodes.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.EpisodeVM.NewEpisode.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create

                    if (_servicesEpisode.FindBy(model.EpisodeVM.NewEpisode.Name) != null)
                        SessionMsg(Helper.Error, "حلقة مكررة !", "اسم الحلقة موجود من قبل");
                    else
                    {
                        if (Convert.ToBoolean(_servicesEpisode.Save(model.EpisodeVM.NewEpisode))
                             && _servicesEpisodeLog.Save(model.EpisodeVM.NewEpisode.Id, Guid.Parse(userId)))
                            SessionMsg(Helper.Success, "تم اضافة حلقة", "تم اضافة بيانات الحلقة بنجاح");
                        else
                            SessionMsg(Helper.Error, "خطأ في الاضافة", "حدث خطأ اثناء اضافة بيانات الحلقة بسبب ادخال بعض البيانات بطريقة خاطئة!");
                    }
                }
                else
                { //Update
                    if (_servicesEpisode.Save(model.EpisodeVM.NewEpisode)
                            && _servicesEpisodeLog.Update(model.EpisodeVM.NewEpisode.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, " تم تعديل الحلقة", "تم تعديل بيانات الحلقة بنجاح");
                    else
                        SessionMsg(Helper.Error, "خطأ في التعديل", "حدث خطأ اثناء تعديل بيانات الحلقة بسبب ادخال بعض البيانات بطريقة خاطئة!");

                }
            }
            return RedirectToAction("Episodes", "Episodes");
        }
        #endregion

        #region Details Of Episode 
        [HttpGet]
        public IActionResult SelectedEpisode(Guid id)
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
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId)
            };
            master.EpisodeVM = new();

            if (User.IsInRole(Roles.Teacher.ToString()) || User.IsInRole(Roles.TeacherWomen.ToString()))
            {
                _servicesOfEpisode.StartEpisode(id);
            }
            else if (!User.IsInRole(Roles.Teacher.ToString()) || !User.IsInRole(Roles.TeacherWomen.ToString()))
            {
                master.EpisodeVM.PrayerTimesAPI = _servicesOfEpisode.GetPrayerTimesAPI();
            }
            master.EpisodeVM.NewEpisode = _servicesOfEpisode.GetById(id);
            master.EpisodeVM.StudentsVM = _servicesOfEpisode.GetStudentsForEpisode(id);
            master.EpisodeVM.PrevLessonData = _servicesOfEpisode.GetStudentPrevLesson(master.EpisodeVM.StudentsVM.Students);
            master.EpisodeVM.NextLessonData = _servicesOfEpisode.GetStudentNextLesson(master.EpisodeVM.StudentsVM.Students);

            master.EpisodeVM.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
            master.EpisodeVM.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");

            master.EpisodeVM.AvilableEpisodeTypes = new SelectList(_context.EpisodesTypes.AsNoTracking().OrderBy(x => x.Name).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");
            master.EpisodeVM.TeacherAttendance = _servicesOfEpisode.FindAttendanceByEpisodeId(id);
            master.EpisodeVM.Verses = new List<Verse>();
            ViewBag.Verses = new SelectList(master.EpisodeVM.Verses, "Id", "Num");

            master.EpisodeVM.LastEvaluateOfStudents = _servicesOfEpisode.GetStudentLastEvaluate(master.EpisodeVM.StudentsVM.Students);


            return View(master);
        }
        public JsonResult GetVersesBySurah(int surahId)
        {
            var v = Json(_context.Verses.Where(p => p.SurahId == surahId).ToList());
            return v;
        }

        #endregion

        #region Add Student Evaluations & Update Lessons & WhatsApp Msg
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddEvaluatoinOnly(Guid id, MasterViewModel model)
        {
            //selectEpisodeId = id;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.DemandVM = new()
            {
                Demands = _servicesDemand.GetAll(),
                DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId),
            };

            if (!ModelState.IsValid)
            {
                //Add Evaluations : Done 
                _servicesOfEpisode.AddEvaluatoinOnly(model.EpisodeVM.Evaluations);
            }
            model.EpisodeVM.NewEpisode = _servicesOfEpisode.GetById(id);

            return RedirectToAction("SelectedEpisode", "Episodes", model.EpisodeVM.NewEpisode);

        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateLesson(Guid id, MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //Update Lesson : Done | WhatsApp Msg To Parents 
                if (_servicesOfEpisode.UpdateLessonsOfStudents(model.EpisodeVM.StudentsVM.Students))
                {
                    // ManualFinished
                    _servicesOfEpisode.ManualEndEpisode(id);

                    model.EpisodeVM.TeacherAttendance = _servicesOfEpisode.FindAttendanceByEpisodeId(id);
                    model.EpisodeVM.PrayerTimesAPI = _servicesOfEpisode.GetPrayerTimesAPI();

                    SessionMsg(Helper.Success, "تم التقييم !", "تم تقييم الطلاب بنجاح ! | ارسال رسائل اولياء الأمور !| تحديد درس جديد للطالب ! ");

                }
                else
                    SessionMsg(Helper.Error, "خطأ في التقييم", "لم يتم تقييم الطلاب لحدوث خطأ اثناء الارسال! ");
            }


            /*
             if (model.TeacherAttendance == null) { }
                        else if (model.TeacherAttendance.Att_In.TimeOfDay == model.TeacherAttendance.Att_Out.TimeOfDay)
                        {
                            // AutoFinished
                            _servicesOfEpisode.AutoEndEpisode(id);
                        }
             */

            try
            {
                // 1 → Get Students List Data
                //
                var lstStudents = _servicesOfEpisode.GetStudentsForEpisode(id);

                var failedMessages = new List<string>();
                var successMessages = new List<string>();

                var instanceId = _configuration["Ultramsg:InstanceId"];
                var token = _configuration["Ultramsg:Token"];
                var clients = lstStudents;

                // 2→ Check UltraMsg Connection Data [instance id | token]
                // 
                if (string.IsNullOrEmpty(instanceId) || string.IsNullOrEmpty(token))
                {
                    //return Json(new WhatsAppResponse
                    //{
                    //    Success = false,
                    //    Message = $"UltraMsg provider connection data [instanceid | token] "
                    //});
                }

                // 3→ Check Parent Phone of every student | Set Message Data
                // 
                foreach (var client in clients.Students)
                {
                    // check phone
                    if (string.IsNullOrEmpty(client.ParentPhoneNumber))
                    {
                        failedMessages.Add($"لم يتم الارسال الي الرقم {client.ParentPhoneNumber} الموجود في بيانات الطالب {client.Name}: برجاء مراجعة بياناته");
                        continue;
                    }

                    //set message data


                    string? apiUrl = $"https://api.ultramsg.com/{instanceId}/messages/image";
                    string? imageUrl = string.Empty;
                    string? captionText = string.Empty;

                    var formData = new List<KeyValuePair<string, string>>
                               {
                                   new KeyValuePair<string, string>("token", token),
                                   new KeyValuePair<string, string>("to", client.ParentPhoneNumber)
                               };
                    // Image
                    //imageUrl = "https://www.kanzalebda3.com/img/Parents2.jpg";
                    imageUrl = "https://zekrhakeem.premiumasp.net/images/whatsAppp.png";
                    formData.Add(new KeyValuePair<string, string>("image", imageUrl));

                    // Caption Text
                    captionText = BuildCaptionFromFields(client);
                    formData.Add(new KeyValuePair<string, string>("caption", captionText));

                    _logger.LogInformation($"إرسال صورة مع caption: {imageUrl}, النص: {"نص افتراضي"}");

                    using var httpClient = new HttpClient();

                    // convert form-urlencoded
                    var formContent = new FormUrlEncodedContent(formData);


                    _logger.LogInformation($"إرسال طلب إلى: {apiUrl}");
                    _logger.LogInformation($"البيانات المرسلة: {string.Join(", ", formData.Select(kv => $"{kv.Key}={kv.Value}"))}");


                    var response = await httpClient.PostAsync(apiUrl, formContent);


                    var responseContent = await response.Content.ReadAsStringAsync();


                    _logger.LogInformation($"استجابة API: Status={response.StatusCode}, Content={responseContent}");


                }
                //return Json(new WhatsAppResponse
                //{
                //    Success = true,
                //    Message = "تم إرسال الرسالة بنجاح",
                //});
            }
            catch (Exception ex)
            {
                // معالجة أي استثناءات غير متوقعة
                _logger.LogError(ex, "خطأ في إرسال رسالة WhatsApp");
                //return Json(new WhatsAppResponse
                //{
                //    Success = false,
                //    Message = $"حدث خطأ: {ex.Message}"
                //});
            }



            return RedirectToAction("Episodes", "Episodes");
        }

        //WhatsApp Msgs ??


        private string BuildCaptionFromFields(Student studentData)
        {
            var caption = "";
            // Attendance
            string att = "";
            string save = "";
            string rev = "";
            string rabt = "";
            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 0)
                att = "حاضر";
            else if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 1)
                att = "متأخر";
            else if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 2)
                att = "مستأذن";
            else if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 3)
                att = "غائب يإذن";
            else if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend == 4)
                att = "غائب";
            //Save
            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Save == -1)
            {
                save = "لم يقيم ";
            }
            else
            {
                save = $"{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Save} من  10";
            }
            //Rev
            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Rev == -1)
            {
                rev = "لم يقيم ";
            }
            else
            {
                rev = $"{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Rev} من  10 ";
            }
            //Rabt
            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Rabt == -1)
            {
                rabt = "لم يقيم ";
            }
            else
            {
                rabt = $"{studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Rabt} من  10 ";
            }



            if (studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_Attend < 3)
            {
                if (studentData.PlanData.Name == "الذكر الحكيم")
                {
                    caption =
                                    $"\t*السلام عليكم ورحمة الله وبركاته* \n" +
                                    $" أخي الكريم ولي أمر الطالب / {studentData.Name} \n\n" +
                                    $"\t📋 *بين يديك تقرير يوم* : \n " +
                                    $"\t\t {DateTime.Now.ToString("dddd")} , {DateTime.Now.ToString("dd/MM/yyyy")} \n\n " +

                                     $"\t\t  *الحلقة* : {studentData.StEpisode.Name} - \t[{studentData.StEpisode.Period}]\n\n" +


                                    $"\t\t\t\t  ۞ *بيانات الحضور* ۞  \n" +
                                    $"\t\t\t\t\t  {att}  \n\n" +

                                    $"\t\t\t\t  ۞ *بيانات الحفظ* ۞  \n" +
                                    $"\t\t\t\t - *مـن سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameFrom} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdFrom} \n" +
                                    $"\t\t\t\t - *إلي سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameTo} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdTo} \n" +
                                    $"\t\t\t\t 📿 *تقييم الحفظ*\t{save}\n\n" +

                                    $"\t\t\t\t  ۞ *بيانات المراجعة* ۞  \n" +
                                    $"\t\t\t\t - *مـن سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevSurahNameFrom} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevAyahIdFrom} \n" +
                                    $"\t\t\t\t - *إلي سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevSurahNameTo} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevAyahIdTo} \n" +
                                    $"\t\t\t\t 📿 *تقييم المراجعة*\t{rev}\n\n" +

                                    $"\t\t\t\t  ۞ *بيانات الربط* ۞  \n" +
                                    $"\t\t\t\t - *مـن سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RabtSurahNameFrom} \t *آية*   {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RabtAyahIdFrom} \n" +
                                    $"\t\t\t\t - *إلي سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RabtSurahNameTo} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RabtAyahIdTo} \n" +
                                    $"\t\t\t\t 📿 *تقييم الربط*\t{rabt}\n\n" +

                                    //$"\t\t للإطلاع علي كل ما يخص الطالب \n" +
                                    //$"\t\t يمكنك تسجيل الدخول \n" +
                                    //$"\t\t لتطبيق جمعية الذكر الحكيم \n" +
                                    //$"\t\t من خلال الرابط التالي \n" +
                                    //$"\t\t https://zekrhakeem.premiumasp.net/ \n\n";
                                    $"\t\t نتمني لكل الطلاب التوفيق الدائم  \n\n";
                }
                else if (studentData.PlanData.Name == "الخطة العامة")
                {
                    caption =
                                    $"\t*السلام عليكم ورحمة الله وبركاته* \n" +
                                    $" أخي الكريم ولي أمر الطالب / {studentData.Name} \n\n" +
                                    $"\t📋 *بين يديك تقرير يوم* : \n " +
                                    $"\t\t {DateTime.Now.ToString("dddd")} , {DateTime.Now.ToString("dd/MM/yyyy")} \n\n " +

                                    $"\t\t  *الحلقة* : {studentData.StEpisode.Name} - \t[{studentData.StEpisode.Period}]\n\n" +

                                    $"\t\t\t\t  ۞ *بيانات الحضور* ۞  \n" +
                                    $"\t\t  {att}  \n\n" +

                                    $"\t\t\t\t  ۞ *بيانات الحفظ* ۞  \n" +
                                    $"\t\t\t\t  - *مـن سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameFrom} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdFrom} \n" +
                                    $"\t\t\t\t  - *إلي سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameTo} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdTo} \n" +
                                    $"\t\t\t\t  📿 *تقييم الحفظ*\t{save}\n\n" +

                                   $"\t\t\t\t  ۞ *بيانات المراجعة* ۞  \n" +
                                    $"\t\t\t\t - *مـن سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevSurahNameFrom} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevAyahIdFrom} \n" +
                                    $"\t\t\t\t - *إلي سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevSurahNameTo} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_RevAyahIdTo} \n" +
                                    $"\t\t\t\t 📿 *تقييم المراجعة*\t{rev}\n\n" +

                                    //$"\t\t للإطلاع علي كل ما يخص الطالب \n" +
                                    //$"\t\t يمكنك تسجيل الدخول \n" +
                                    //$"\t\t لتطبيق جمعية الذكر الحكيم \n" +
                                    //$"\t\t من خلال الرابط التالي \n" +
                                    //$"\t\t https://zekrhakeem.premiumasp.net/ \n\n";
                                    $"\t\t نتمني لكل الطلاب التوفيق الدائم  \n\n";

                }
                else if (studentData.PlanData.Name == "الخطة المفتوحة")
                {
                    caption =
                                    $"\t*السلام عليكم ورحمة الله وبركاته* \n" +
                                    $" أخي الكريم ولي أمر الطالب / {studentData.Name} \n\n" +
                                    $"\t📋 *بين يديك تقرير يوم* : \n " +
                                    $"\t\t {DateTime.Now.ToString("dddd")} , {DateTime.Now.ToString("dd/MM/yyyy")} \n\n " +

                                    $"\t\t  *الحلقة* : {studentData.StEpisode.Name} - \t[{studentData.StEpisode.Period}]\n\n" +

                                    $"\t\t\t\t  ۞ *بيانات الحضور* ۞  \n" +
                                    $"\t\t\t\t\t  {att}  \n\n" +

                                    $"\t\t\t\t  ۞ *بيانات المقدار* ۞  \n" +
                                    $"\t\t\t\t - *مـن سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameFrom} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdFrom} \n" +
                                    $"\t\t\t\t - *إلي سورة* : {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveSurahNameTo} \t *آية*  {studentData.Evaluations.OrderByDescending(x => x.Ev_Date).First().Ev_SaveAyahIdTo} \n" +
                                    $"\t\t\t\t 📿 *تقييم المقدار*\t{save}\n\n" +

                                    //$"\t\t للإطلاع علي كل ما يخص الطالب \n" +
                                    //$"\t\t يمكنك تسجيل الدخول \n" +
                                    //$"\t\t لتطبيق جمعية الذكر الحكيم \n" +
                                    //$"\t\t من خلال الرابط التالي \n" +
                                    //$"\t\t https://zekrhakeem.premiumasp.net/ \n\n";
                                    $"\t\t نتمني لكل الطلاب التوفيق الدائم  \n\n";
                }
            }
            else
            {
                caption =
                                   $"\t*السلام عليكم ورحمة الله وبركاته* \n" +
                                    $" أخي الكريم ولي أمر الطالب / {studentData.Name} \n\n" +
                                    $"\t📋 *بين يديك تقرير يوم * : \n " +
                                    $"\t\t {DateTime.Now.ToString("dddd")} , {DateTime.Now.ToString("dd/MM/yyyy")} \n\n " +

                                   $"\t\t  *الحلقة* : {studentData.StEpisode.Name} - \t[{studentData.StEpisode.Period}]\n\n" +

                                    $"\t\t\t\t  ۞ *بيانات الحضور* ۞  \n" +
                                    $"\t\t\t\t\t  {att}  \n\n" +

                                    //$"\t\t للإطلاع علي كل ما يخص الطالب \n" +
                                    //$"\t\t يمكنك تسجيل الدخول \n" +
                                    //$"\t\t لتطبيق جمعية الذكر الحكيم \n" +
                                    //$"\t\t من خلال الرابط التالي \n" +
                                    //$"\t\t https://zekrhakeem.premiumasp.net/ \n\n";
                                    $"\t\t نتمني لكل الطلاب التوفيق الدائم  \n\n";

            }

            return caption;
        }
        #endregion



        #region Add|Edit EpisodeType [Create & Update]
        // [Authorize(Permissions.Episodes.Create), Authorize(Permissions.Episodes.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SaveEpisodeType(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.EpisodeVM.NewEpisodeType.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create

                    if (_servicesOfEpisode.FindByEpTypeName(model.EpisodeVM.NewEpisodeType.Name) != null)
                        SessionMsg(Helper.Error, "نوع حلقة مكرر !", "نوع الحلقة موجود من قبل");
                    else
                    {
                        if (_servicesOfEpisode.SaveEpisodeType(model.EpisodeVM.NewEpisodeType))
                            SessionMsg(Helper.Success, "اضافة نوع الحلقة !", "تم اضافة نوع الحلقة بنجاح");
                        else
                            SessionMsg(Helper.Error, "خطأ في الاضافة", "حدث خطأ اثناء اضافة نوع الحلقة بسبب بعض البيانات الخاطئة !");
                    }
                }
                else
                { //Update
                    //if (_servicesOfEpisode.SaveEpisodeType(model.NewEpisodeType)) { }
                    //SessionMsg(Helper.Success, "Episode Type Update", "Episode Type is Updated Successfully");
                    //else
                    //    SessionMsg(Helper.Error, "Episode Type Not Update", "Episode Type isn't Updated !");

                }
            }

            return RedirectToAction("Episodes", "Episodes");

        }

        #endregion

        #region Pause | Resume Episode [Pause & Resume]
        [Authorize(Permissions.Episodes.Pause)]
        public IActionResult Pause(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfEpisode.Pause(Id))
                return RedirectToAction("Episodes", "Episodes");
            return RedirectToAction("Episodes", "Episodes");
        }
        [Authorize(Permissions.Episodes.Resume)]
        public IActionResult Resume(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfEpisode.Resume(Id))
                return RedirectToAction("Episodes", "Episodes");
            return RedirectToAction("Episodes", "Episodes");
        }
        #endregion

        #region Update Location State For Valid Location | Reset All

        //[HttpPost]
        //public IActionResult UpdateLocationState(string locState)
        //{
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    model.EpisodesWithTeacher = _servicesOfEpisode.GetEpisodesWithTeacher(userId);

        //    _servicesOfEpisode.UpdateLocationState(model);
        //    return RedirectToAction(nameof(Episodes));
        //}

        #endregion
    }
}
















