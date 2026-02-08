using ExpressTimezone;
using Newtonsoft.Json;
using ZekrHakeem_v11.Dtos;

namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesEpisode : IServicesRepository<Episode>, IServicesEpisode
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;


        public ServicesEpisode(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        #region All Episodes
        public List<Episode> GetAll()
        {
            try
            {
                return _context.Episodes
                               .Include(x => x.EpLocation)
                               .Include(x => x.EpLocation.LoCenter)
                               .Include(x => x.EpLocation.District)
                               .Include(x => x.EpisodeType)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Students)

                             .OrderByDescending(x => x.PauseState)
                               .ThenBy(x => x.AttendType)
                               .ThenBy(x => x.ApplicationUser.FullName)

                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Episodes Men
        public List<Episode> GetEpisodesForMen()
        {
            try
            {
                return _context.Episodes
                             .Include(x => x.EpLocation)
                             .Include(x => x.EpLocation.LoCenter)
                             .Include(x => x.EpLocation.District)
                             .Include(x => x.EpisodeType)
                             .Include(x => x.ApplicationUser)
                             .Include(x => x.Students)

                            .OrderByDescending(x => x.PauseState)
                               .ThenBy(x => x.AttendType)
                               .ThenBy(x => x.ApplicationUser.FullName)



                                .Where(x => x.CurrentState > 0 && x.EpLocation.Type == "mosque")
                                .ToList();


            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Episodes Women
        public List<Episode> GetEpisodesForWomen()
        {
            try
            {
                return _context.Episodes
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.EpLocation)
                               .Include(x => x.EpLocation.LoCenter)
                               .Include(x => x.EpLocation.District)
                               .Include(x => x.EpisodeType)
                               .Include(x => x.Students)

                             .OrderByDescending(x => x.PauseState)
                               .ThenBy(x => x.AttendType)
                               .ThenBy(x => x.ApplicationUser.FullName)


                               .Where(x => x.CurrentState > 0 && x.EpLocation.Type == "dar" && x.EpLocation.LoCenter.Type == "women")
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion


        #region Episodes With Supervisor
        public List<Episode> GetEpisodesWithSupervisor(string userid)
        {
            try
            {
                return _context.Episodes
                            .Include(x => x.EpLocation)
                            .Include(x => x.EpLocation.LoCenter)
                            .Include(x => x.EpLocation.District)
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.Students)
                            .Include(x => x.EpisodeType)

                            .OrderByDescending(x => x.PauseState)
                               .ThenBy(x => x.AttendType)
                               .ThenBy(x => x.ApplicationUser.FullName)

                               .Where(x => x.CurrentState > 0 && x.PauseState > 0 && x.EpLocation.LoCenter.ApplicationUserId == userid)
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Episodes With Teacher
        public List<Episode> GetEpisodesWithTeacher(string userid)
        {
            try
            {
                List<Episode> EpisodesOfTeacher = new List<Episode>();
                EpisodesOfTeacher = _context.Episodes
                                    .Include(x => x.EpLocation)
                                    .Include(x => x.EpLocation.LoCenter)
                                    .Include(x => x.EpLocation.District)
                                    .Include(x => x.ApplicationUser)
                                    .Include(x => x.Students)
                                    .Include(x => x.EpisodeType)

                                    .OrderByDescending(x => x.PauseState)
                               .ThenBy(x => x.AttendType)
                               .ThenBy(x => x.ApplicationUser.FullName)



                                    .Where(x => x.CurrentState > 0 && x.PauseState > 0 && x.ApplicationUserId == userid)
                                    .ToList();

                return EpisodesOfTeacher;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Find Episodes By ...
        public Episode FindChecherEpisodeBy(Guid Id)
        {
            try
            {
                return _context.Episodes
                .Include(x => x.ApplicationUser)
                               .FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Episode FindBy(Guid Id)
        {
            try
            {
                return _context.Episodes
                               .Include(x => x.ApplicationUser)
                               .FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Episode FindBy(string Name)
        {
            try
            {
                return _context.Episodes
                    .Include(x => x.ApplicationUser).FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Attendance FindAttendByDay(int day, int month, int year, string userApp, Guid epId)
        {
            try
            {
                var attend = _context.Attendances.FirstOrDefault(x => x.Att_In.Day.Equals(day) && x.Att_In.Month.Equals(month) && x.Att_In.Year.Equals(year) && x.CurrentState > 0 && x.ApplicationUserId.ToString().Equals(userApp) && x.Id != null && x.EpisodeId.Equals(epId));
                if (attend == null)
                    return null;
                else
                    return attend;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Attendance FindAttendById(Guid attendId)
        {
            try
            {
                var attend = _context.Attendances.FirstOrDefault(x => x.Id.Equals(attendId));
                if (attend == null)
                    return null;
                else
                    return attend;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Student FindStudentById(Guid Id)
        {
            try
            {
                return _context.Students.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Evaluation FindEvalById(Guid id)
        {
            try
            {
                return _context.Evaluations.FirstOrDefault(x => x.Id.Equals(id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Evaluation FindEvalByDate(Guid? stId)
        {
            try
            {
                return _context.Evaluations.FirstOrDefault(x => x.Ev_Date.Date.Equals(DateTime.Now.Date) && x.EvStudentId.Equals(stId) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public LessonData FindLessonById(Student stu)
        {
            try
            {
                return _context.LessonsData.Include(p => p.LevelData).Include(p => p.Students).FirstOrDefault(x => x.Id.Equals(stu.LessonDataId) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Verse FindVerseById(int? Id)
        {
            try
            {
                return _context.Verses.Include(t => t.Surah).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Surah FindSurahById(int? Id)
        {
            try
            {
                return _context.Surahs.FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Verse FindVerseById(int? surahId, int? verseNum)
        {
            try
            {
                return _context.Verses.FirstOrDefault(x => x.SurahId.Equals(surahId) && x.Num == verseNum && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Surah FindSurahByName(string? surahName)
        {
            try
            {
                return _context.Surahs.FirstOrDefault(x => x.Name.Equals(surahName) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int? GetMaxVerse(int? surahId)
        {
            try
            {
                return _context.Verses.Where(x => x.SurahId == surahId).Max(x => x.Num);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Create & Update Episode
        public bool Save(Episode model)
        {
            try
            {
                var utcTime = DateTime.UtcNow;
                var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh");
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.PauseState = (int)Helper.ePauseState.Active;
                    model.IsAvilable = true;
                    model.CheckDate = currentTime.Date;
                    model.EpLocation = null;


                    if (!CheckDuplicate(model, GetEpisodesWithTeacher(model.ApplicationUserId)))
                        return false;

                    result = model;
                    result.Period = model.Period;
                    result.EpisodeTypeId = model.EpisodeTypeId;
                    result.EpLocation = null;

                    _context.Episodes.Add(result);
                }
                else
                {
                    result.Name = model.Name;
                    result.EpisodeTypeId = model.EpisodeTypeId;
                    result.AttendType = model.AttendType;
                    result.LimitStudents = model.LimitStudents;
                    result.Status = model.Status;
                    result.Period = model.Period;
                    result.Description = model.Description;
                    result.EpLocationId = model.EpLocationId;
                    result.CurrentState = (int)Helper.eCurrentState.Active;
                    result.PauseState = (int)Helper.ePauseState.Active;
                    result.ApplicationUserId = model.ApplicationUserId;

                    if (!CheckDuplicate(result, GetEpisodesWithTeacher(model.ApplicationUserId)))
                        return false;

                    _context.Episodes.Update(result);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckDuplicate(Episode episode, List<Episode> EpisodesWithTeacher)
        {
            int count = 0;
            foreach (var ep in EpisodesWithTeacher)
            {
                if (episode.Id != ep.Id && episode.AttendType.Contains("حضوري") && episode.Period == ep.Period)
                    count++;
            }
            if (count > 0)
                return false;
            else
                return true;

        }


        #endregion

        #region Delete Episode
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.Episodes.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Pause | Resume Episode
        public bool Pause(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.PauseState = (int)Helper.ePauseState.Pause;
                result.IsAvilable = false;
                _context.Episodes.Update(result);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Resume(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.PauseState = (int)Helper.ePauseState.Active;
                result.IsAvilable = true;

                _context.Episodes.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Episode Details : [students , next lessons , previous lessons ]

        public Episode? GetById(Guid id)
        {
            var ep = _context.Episodes
                .Include(t => t.EpLocation)
                .Include(t => t.EpisodeType)
                .Include(st => st.Students)
                .Include(st => st.Attendances)
                .Include(st => st.ApplicationUser)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
            return ep;
        }
        public void ResetAllEpisodes()
        {
            var utcTime = DateTime.UtcNow;
            var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh");
            var result = FindChecherEpisodeBy(new Guid("00000000-0000-0000-1111-000000001111"));
            if (result.CheckDate.Value.Date != currentTime.Date)
            {
                foreach (var episode in _context.Episodes)
                {
                    episode.IsAvilable = true;
                    episode.Status = false;
                    episode.ManualFinished = false;
                    episode.CheckDate = currentTime.Date;

                    //update IsEvaluate For every student in episode
                    StudentViewModel svm = GetStudentsForEpisode(episode.Id);
                    foreach (var stu in svm.Students)
                    {
                        stu.IsEvaluate = false;
                        _context.Students.Update(stu);
                    }
                    var att = FindAttendByDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, episode.ApplicationUserId, episode.Id);
                    //EveryEpisode AttendStatus
                    if (att == null)
                    {
                        att = new Attendance();
                        att.Id = Guid.NewGuid();
                        att.CurrentState = (int)Helper.eCurrentState.Active;
                        att.Att_In = currentTime;
                        att.Att_Out = currentTime;
                        att.ApplicationUser = episode.ApplicationUser;
                        att.EpisodeId = episode.Id;
                        if (episode.AttendType == "حضوري")
                        {
                            att.Status_AttIn = "غائب";
                            att.Status_AttOut = "غائب";
                        }
                        else
                        {
                            att.Status_AttIn = "عن بعد";
                            att.Status_AttOut = "عن بعد";
                        }
                        _context.Attendances.Add(att);
                        //foreach (var attendance in _context.Attendances.Where(x => x.Att_In.Date == DateTime.Now.Date))

                    }
                    _context.Episodes.Update(episode);
                }
            }
            _context.SaveChanges();
        }
        public void StartEpisode(Guid epid)
        {

            // active episode
            var result = FindBy(epid);

            result.IsAvilable = true;
            result.ManualFinished = false;
            var pt = new EpisodeViewModel();
            pt.PrayerTimesAPI = GetPrayerTimesAPI();
            // add attendance to teacher

            var utcTime = DateTime.UtcNow;
            var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh");

            #region API StartTime & EndTime
            var epStartTime_Fajr = Convert.ToDateTime(pt.PrayerTimesAPI.FajrTime).ToString("hh:mm tt");
            var epEndTime_Fajr = Convert.ToDateTime(pt.PrayerTimesAPI.EndFajrTime).ToString("hh:mm tt");

            var epStartTime_Sunrise = Convert.ToDateTime(pt.PrayerTimesAPI.SunriseTime).ToString("hh:mm tt");
            var epEndTime_Sunrise = Convert.ToDateTime(pt.PrayerTimesAPI.EndSunriseTime).ToString("hh:mm tt");

            var epStartTime_Zohr = Convert.ToDateTime(pt.PrayerTimesAPI.ZohrTime).ToString("hh:mm tt");
            var epEndTime_Zohr = Convert.ToDateTime(pt.PrayerTimesAPI.EndZohrTime).ToString("hh:mm tt");

            var epStartTime_Asr = Convert.ToDateTime(pt.PrayerTimesAPI.AsrTime).ToString("hh:mm tt");
            var epEndTime_Asr = Convert.ToDateTime(pt.PrayerTimesAPI.EndAsrTime).ToString("hh:mm tt");

            var epStartTime_Mghrb = Convert.ToDateTime(pt.PrayerTimesAPI.MghrbTime).ToString("hh:mm tt");
            var epEndTime_Mghrb = Convert.ToDateTime(pt.PrayerTimesAPI.EndMghrbTime).ToString("hh:mm tt");

            var epStartTime_Isha = Convert.ToDateTime(pt.PrayerTimesAPI.IshaTime).ToString("hh:mm tt");
            var epEndTime_Isha = Convert.ToDateTime(pt.PrayerTimesAPI.EndIshaTime).ToString("hh:mm tt");

            #endregion

            var att = FindAttendByDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, result.ApplicationUserId, epid);
            // att = (att is null) ? new Attendance() : att;
            if (att is null)
            {
                att = new Attendance();
                att.Id = Guid.NewGuid();


                att.CurrentState = (int)Helper.eCurrentState.Active;
                att.Att_In = currentTime;
                att.Att_Out = currentTime;
                att.ApplicationUser = result.ApplicationUser;
                att.EpisodeId = epid;
                att.Status_AttIn = " ";
                //att.Status_AttOut = " ";

                //Status Att In
                if (result.AttendType == "حضوري")
                {
                    #region حاضر
                    if (result.Period.Contains(PrayerTimesPeriods.الفجر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Fajr).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Fajr).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.الشروق.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Sunrise).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Sunrise).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.الظهر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Zohr).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Zohr).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.العصر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Asr).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Asr).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.المغرب.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Mghrb).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Mghrb).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.العشاء.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Isha).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Isha).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    #endregion

                    #region  تأخير
                    else if (result.Period.Contains(PrayerTimesPeriods.الفجر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Fajr).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.الشروق.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Sunrise).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.الظهر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Zohr).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.العصر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Asr).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.المغرب.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Mghrb).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.العشاء.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Isha).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    #endregion
                }
                else if (result.AttendType == "عن بعد")
                {
                    att.Status_AttIn = "حاضر";
                }

                _context.Attendances.Add(att);
            }

            else if (result.Status == false && att != null && result.ManualFinished == false)
            {

                att.CurrentState = (int)Helper.eCurrentState.Active;
                att.Att_In = currentTime;
                att.Att_Out = currentTime;
                att.ApplicationUser = result.ApplicationUser;
                att.EpisodeId = epid;
                att.Status_AttIn = " ";
                //att.Status_AttOut = " ";

                //Status Att In
                if (result.AttendType == "حضوري")
                {
                    #region حاضر
                    if (result.Period.Contains(PrayerTimesPeriods.الفجر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Fajr).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Fajr).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.الشروق.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Sunrise).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Sunrise).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.الظهر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Zohr).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Zohr).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.العصر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Asr).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Asr).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.المغرب.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Mghrb).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Mghrb).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    else if (result.Period.Contains(PrayerTimesPeriods.العشاء.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Isha).AddMinutes(0) && att.Att_In <= Convert.ToDateTime(epStartTime_Isha).AddMinutes(45))
                        att.Status_AttIn = "حاضر";
                    #endregion

                    #region  تأخير
                    else if (result.Period.Contains(PrayerTimesPeriods.الفجر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Fajr).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.الشروق.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Sunrise).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.الظهر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Zohr).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.العصر.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Asr).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.المغرب.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Mghrb).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    else if (result.Period.Contains(PrayerTimesPeriods.العشاء.ToString()) && att.Att_In >= Convert.ToDateTime(epStartTime_Isha).AddMinutes(45) && att.Att_In <= Convert.ToDateTime(epEndTime_Asr))
                        att.Status_AttIn = "تأخير";
                    #endregion
                }
                else if (result.AttendType == "عن بعد")
                {
                    att.Status_AttIn = "حاضر";
                }
                _context.Attendances.Update(att);
            }
            result.Status = true;

            _context.Episodes.Update(result);
            _context.SaveChanges();
        }
        public void ManualEndEpisode(Guid id)
        {
            var utcTime = DateTime.UtcNow;
            var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh");
            var result = FindBy(id);

            result.Status = false;
            result.ManualFinished = true;
            result.IsAvilable = true;
            var pt = new EpisodeViewModel();
            pt.PrayerTimesAPI = GetPrayerTimesAPI();
            // add attendance to teacher

            #region API StartTime & EndTime
            var epStartTime_Fajr = Convert.ToDateTime(pt.PrayerTimesAPI.FajrTime).ToString("hh:mm tt");
            var epEndTime_Fajr = Convert.ToDateTime(pt.PrayerTimesAPI.EndFajrTime).ToString("hh:mm tt");

            var epStartTime_Sunrise = Convert.ToDateTime(pt.PrayerTimesAPI.SunriseTime).ToString("hh:mm tt");
            var epEndTime_Sunrise = Convert.ToDateTime(pt.PrayerTimesAPI.EndSunriseTime).ToString("hh:mm tt");

            var epStartTime_Zohr = Convert.ToDateTime(pt.PrayerTimesAPI.ZohrTime).ToString("hh:mm tt");
            var epEndTime_Zohr = Convert.ToDateTime(pt.PrayerTimesAPI.EndZohrTime).ToString("hh:mm tt");

            var epStartTime_Asr = Convert.ToDateTime(pt.PrayerTimesAPI.AsrTime).ToString("hh:mm tt");
            var epEndTime_Asr = Convert.ToDateTime(pt.PrayerTimesAPI.EndAsrTime).ToString("hh:mm tt");

            var epStartTime_Mghrb = Convert.ToDateTime(pt.PrayerTimesAPI.MghrbTime).ToString("hh:mm tt");
            var epEndTime_Mghrb = Convert.ToDateTime(pt.PrayerTimesAPI.EndMghrbTime).ToString("hh:mm tt");

            var epStartTime_Isha = Convert.ToDateTime(pt.PrayerTimesAPI.IshaTime).ToString("hh:mm tt");
            var epEndTime_Isha = Convert.ToDateTime(pt.PrayerTimesAPI.EndIshaTime).ToString("hh:mm tt");
            #endregion

            //update IsEvaluate For every student in episode
            StudentViewModel svm = GetStudentsForEpisode(id);
            foreach (var stu in svm.Students)
            {
                stu.IsEvaluate = false;
                _context.Students.Update(stu);
            }

            // Update attendance to teacher
            var att = FindAttendByDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, result.ApplicationUserId, id);
            att.Att_Out = currentTime;

            if (att != null && result.AttendType == "حضوري")
            {
                #region  انصراف مبكر
                if (result.Period.Contains(PrayerTimesPeriods.الفجر.ToString()) && att.Att_Out <= Convert.ToDateTime(epEndTime_Fajr).AddMinutes(-25) && att.Att_Out != att.Att_In)
                    att.Status_AttOut = "انصراف مبكر";
                else if (result.Period.Contains(PrayerTimesPeriods.الشروق.ToString()) && att.Att_Out <= Convert.ToDateTime(epEndTime_Sunrise).AddMinutes(-25) && att.Att_Out != att.Att_In)
                    att.Status_AttOut = "انصراف مبكر";
                else if (result.Period.Contains(PrayerTimesPeriods.الظهر.ToString()) && att.Att_Out <= Convert.ToDateTime(epEndTime_Zohr).AddMinutes(-25) && att.Att_Out != att.Att_In)
                    att.Status_AttOut = "انصراف مبكر";
                else if (result.Period.Contains(PrayerTimesPeriods.العصر.ToString()) && att.Att_Out <= Convert.ToDateTime(epEndTime_Asr).AddMinutes(-25) && att.Att_Out != att.Att_In)
                    att.Status_AttOut = "انصراف مبكر";
                else if (result.Period.Contains(PrayerTimesPeriods.المغرب.ToString()) && att.Att_Out <= Convert.ToDateTime(epEndTime_Mghrb).AddMinutes(-25) && att.Att_Out != att.Att_In)
                    att.Status_AttOut = "انصراف مبكر";
                else if (result.Period.Contains(PrayerTimesPeriods.العشاء.ToString()) && att.Att_Out <= Convert.ToDateTime(epEndTime_Isha).AddMinutes(-25) && att.Att_Out != att.Att_In)
                    att.Status_AttOut = "انصراف مبكر";
                #endregion
                #region  خروج   
                if (result.Period.Contains(PrayerTimesPeriods.الفجر.ToString()) && att.Att_Out > Convert.ToDateTime(epEndTime_Fajr).AddMinutes(-25) && att.Att_Out <= Convert.ToDateTime(epEndTime_Fajr))
                    att.Status_AttOut = "خروج";
                else if (result.Period.Contains(PrayerTimesPeriods.الشروق.ToString()) && att.Att_Out > Convert.ToDateTime(epEndTime_Fajr).AddMinutes(-25) && att.Att_Out <= Convert.ToDateTime(epEndTime_Fajr))
                    att.Status_AttOut = "خروج";
                else if (result.Period.Contains(PrayerTimesPeriods.الظهر.ToString()) && att.Att_Out > Convert.ToDateTime(epEndTime_Fajr).AddMinutes(-25) && att.Att_Out <= Convert.ToDateTime(epEndTime_Fajr))
                    att.Status_AttOut = "خروج";
                else if (result.Period.Contains(PrayerTimesPeriods.العصر.ToString()) && att.Att_Out > Convert.ToDateTime(epEndTime_Fajr).AddMinutes(-25) && att.Att_Out <= Convert.ToDateTime(epEndTime_Fajr))
                    att.Status_AttOut = "خروج";
                else if (result.Period.Contains(PrayerTimesPeriods.المغرب.ToString()) && att.Att_Out > Convert.ToDateTime(epEndTime_Fajr).AddMinutes(-25) && att.Att_Out <= Convert.ToDateTime(epEndTime_Fajr))
                    att.Status_AttOut = "خروج";
                else if (result.Period.Contains(PrayerTimesPeriods.العشاء.ToString()) && att.Att_Out > Convert.ToDateTime(epEndTime_Fajr).AddMinutes(-25) && att.Att_Out <= Convert.ToDateTime(epEndTime_Fajr))
                    att.Status_AttOut = "خروج";
                #endregion   
            }
            else if (att != null && result.AttendType == "عن بعد")
            {
                att.Status_AttOut = "خروج";
            }
            _context.Attendances.Update(att);

            _context.Episodes.Update(result);
            _context.SaveChanges();
        }
        public void AutoEndEpisode(Guid id)
        {
            var result = FindBy(id);
            result.Status = false;
            result.ManualFinished = false;
            result.IsAvilable = true;
            _context.Episodes.Update(result);

            _context.SaveChanges();
        }
        public StudentViewModel GetStudentsForEpisode(Guid epid)
        {
            try
            {
                StudentViewModel stvm = new StudentViewModel();
                stvm.Students = _context.Students
                                    .Include(x => x.StEpisode)
                                    .Include(x => x.Nationality)
                                    .Include(x => x.LessonData)
                                    .Include(x => x.Verse)
                                    .Include(x => x.Surah)
                                    .Include(x => x.PlanData)
                                    .Include(x => x.Evaluations)
                                    .Include(x => x.LessonData.LevelData)
                                    .Include(x => x.LessonData.LevelData.PlanData)

                                    .OrderBy(x => x.PlanDataId)
                                    .ThenBy(x => x.Name)
                                    .Where(x => x.CurrentState > 0 && x.StEpisodeId == epid)
                                    .ToList();

                stvm.StudentsForMen = stvm.StudentsForMen;
                stvm.StudentsForWomen = stvm.StudentsForWomen;
                stvm.StudentsWithSupervisor = stvm.StudentsWithSupervisor;
                stvm.StudentsWithTeacher = stvm.StudentsWithTeacher;

                stvm.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().OrderBy(x => x.Id).ToList(), "Id", "Name");
                stvm.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().OrderByDescending(x => x.Id).ToList(), "Id", "Name");

                stvm.AvilableNationalities = new SelectList(_context.Nationalities.AsNoTracking().OrderBy(x => x.Name).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");
                stvm.AvilableHealthConditions = new SelectList(_context.HealthConditions.AsNoTracking().OrderBy(x => x.Num).Where(x => x.CurrentState > 0).ToList(), "Id", "Name");


                return stvm;


            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, LessonData> GetStudentPrevLesson(List<Student> students)
        {
            try
            {
                Dictionary<Student, LessonData> stOfPrevLesson = new();
                LessonData? PrevLesson = new();
                foreach (var student in students)
                {
                    PrevLesson = _context.LessonsData
                                        .Include(x => x.Students)
                                        .Include(x => x.LevelData)
                                        .Include(x => x.LevelData.PlanData)
                                        .OrderBy(x => x.Id)
                                        .Where(x => x.CurrentState > 0 && x.Id == (student.LessonDataId) - 1)
                                        .AsNoTracking()
                                        .SingleOrDefault(g => g.Id == (student.LessonDataId) - 1);
                    stOfPrevLesson.Add(student, PrevLesson);
                }
                return stOfPrevLesson;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, LessonData> GetStudentNextLesson(List<Student> students)
        {
            try
            {
                Dictionary<Student, LessonData> stOfNextLesson = new();
                LessonData? nextLesson = new();


                foreach (var student in students)
                {
                    nextLesson = _context.LessonsData
                                        .Include(x => x.Students)
                                        .Include(x => x.LevelData)
                                        .Include(x => x.LevelData.PlanData)
                                        .OrderBy(x => x.Id)
                                        .Where(x => x.CurrentState > 0 && x.Id == (student.LessonDataId) + 1)
                                        .AsNoTracking()
                                        .SingleOrDefault(g => g.Id == (student.LessonDataId) + 1);
                    stOfNextLesson.Add(student, nextLesson);
                }
                return stOfNextLesson;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, Evaluation> GetStudentLastEvaluate(List<Student> students)
        {
            try
            {
                Dictionary<Student, Evaluation> stuEvaluate = new();

                foreach (var student in students)
                {
                    Evaluation? lastEval = new Evaluation();

                    lastEval = _context.Evaluations
                    .Include(x => x.EvStudent)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id && x.Ev_Date == (DateTime.Now.Date))
                    .FirstOrDefault();

                    if (lastEval == null)
                    {
                        lastEval = new() { Id = Guid.NewGuid(), CurrentState = (int)Helper.eCurrentState.Active, Ev_Date = DateTime.Now.Date, Ev_Attend = 0, Ev_Rabt = -1, Ev_Rev = -1, Ev_Save = -1, EvStudent = student };
                        _context.Evaluations.Add(lastEval);
                        _context.SaveChanges();
                    }
                    stuEvaluate.Add(student, lastEval);
                }

                return stuEvaluate;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Add Evaluations & Update student lesson For Selected 

        public bool AddEvaluatoinOnly(List<Evaluation> evaluations)
        {
            Surah sura = new Surah();

            // Add Evaluations
            foreach (var eval in evaluations)
            {

                var stu = FindStudentById((Guid)eval.EvStudentId);
                var result = FindEvalByDate(eval.EvStudentId);
                if (result == null)
                {
                    //create new evaluate
                    eval.Id = Guid.NewGuid();
                    eval.CurrentState = (int)Helper.eCurrentState.Active;
                    eval.Ev_Date = DateTime.Now.Date;

                    if (result.Ev_Save > -1)
                        stu.IsEvaluate = true;

                    if (eval.Notes == null)
                        eval.Notes = " ";

                    _context.Add(eval);
                    _context.Update(stu);

                }
                else
                {
                    if (stu.IsEvaluate == false)
                    {
                        #region Case
                        result.Ev_Attend = eval.Ev_Attend;
                        result.Ev_Save = eval.Ev_Save;
                        result.Ev_Rev = eval.Ev_Rev;
                        result.Ev_Rabt = eval.Ev_Rabt;

                        //RevFrom
                        result.Ev_RevSurahIdFrom = eval.Ev_RevSurahIdFrom;
                        result.Ev_RevAyahIdFrom = eval.Ev_RevAyahIdFrom;
                        //RevTo
                        result.Ev_RevSurahIdTo = eval.Ev_RevSurahIdTo;
                        result.Ev_RevAyahIdTo = eval.Ev_RevAyahIdTo;
                        //SaveFrom
                        result.Ev_SaveSurahIdFrom = eval.Ev_SaveSurahIdFrom;
                        result.Ev_SaveAyahIdFrom = eval.Ev_SaveAyahIdFrom;
                        //SaveTo
                        result.Ev_SaveSurahIdTo = eval.Ev_SaveSurahIdTo;
                        result.Ev_SaveAyahIdTo = eval.Ev_SaveAyahIdTo;
                        //RabtFrom
                        result.Ev_RabtSurahIdFrom = eval.Ev_RabtSurahIdFrom;
                        result.Ev_RabtAyahIdFrom = eval.Ev_RabtAyahIdFrom;
                        //RabtTo
                        result.Ev_RabtSurahIdTo = eval.Ev_RabtSurahIdTo;
                        result.Ev_RabtAyahIdTo = eval.Ev_RabtAyahIdTo;
                        //Notes
                        if (eval.Notes == null)
                            eval.Notes = " ";
                        result.Notes = eval.Notes;
                        ////////////////////
                        //RevFromName
                        sura = FindSurahById(eval.Ev_RevSurahIdFrom);
                        if (sura == null)
                            result.Ev_RevSurahNameFrom = null;
                        else
                            result.Ev_RevSurahNameFrom = sura.Name;
                        //RevToName
                        sura = FindSurahById(eval.Ev_RevSurahIdTo);
                        if (sura == null)
                            result.Ev_RevSurahNameTo = null;
                        else
                            result.Ev_RevSurahNameTo = sura.Name;
                        //SaveFromName
                        sura = FindSurahById(eval.Ev_SaveSurahIdFrom);
                        if (sura == null)
                            result.Ev_SaveSurahNameFrom = null;
                        else
                            result.Ev_SaveSurahNameFrom = sura.Name;
                        //SaveToName
                        sura = FindSurahById(eval.Ev_SaveSurahIdTo);
                        if (sura == null)
                            result.Ev_SaveSurahNameTo = null;
                        else
                            result.Ev_SaveSurahNameTo = sura.Name;
                        //RabtFromName
                        sura = FindSurahById(eval.Ev_RabtSurahIdFrom);
                        if (sura == null)
                            result.Ev_RabtSurahNameFrom = null;
                        else
                            result.Ev_RabtSurahNameFrom = sura.Name;
                        //RabtToName
                        sura = FindSurahById(eval.Ev_RabtSurahIdTo);
                        if (sura == null)
                            result.Ev_RabtSurahNameTo = null;
                        else
                            result.Ev_RabtSurahNameTo = sura.Name;
                        #endregion

                        if (eval.Ev_Save > -1)
                        {
                            stu.IsEvaluate = true;
                            _context.Update(stu);
                        }
                        _context.Update(result);
                    }
                    else
                    {
                        if (result.Ev_Attend != eval.Ev_Attend || result.Ev_Save != eval.Ev_Save
                        || result.Ev_Rev != eval.Ev_Rev || result.Ev_Rabt != eval.Ev_Rabt
                        || result.Ev_RevSurahIdFrom != eval.Ev_RevSurahIdFrom
                        || result.Ev_RevAyahIdFrom != eval.Ev_RevAyahIdFrom
                        || result.Ev_RevSurahIdTo != eval.Ev_RevSurahIdTo
                        || result.Ev_RevAyahIdTo != eval.Ev_RevAyahIdTo
                        || result.Ev_SaveSurahIdFrom != eval.Ev_SaveSurahIdFrom
                        || result.Ev_SaveAyahIdFrom != eval.Ev_SaveAyahIdFrom
                        || result.Ev_SaveSurahIdTo != eval.Ev_SaveSurahIdTo
                        || result.Ev_SaveAyahIdTo != eval.Ev_SaveAyahIdTo
                        || result.Ev_RabtSurahIdFrom != eval.Ev_RabtSurahIdFrom
                        || result.Ev_RabtAyahIdFrom != eval.Ev_RabtAyahIdFrom
                        || result.Ev_RabtSurahIdTo != eval.Ev_RabtSurahIdTo
                        || result.Ev_RabtAyahIdTo != eval.Ev_RabtAyahIdTo
                        )
                        {
                            #region Case
                            result.Ev_Attend = eval.Ev_Attend;
                            result.Ev_Save = eval.Ev_Save;
                            result.Ev_Rev = eval.Ev_Rev;
                            result.Ev_Rabt = eval.Ev_Rabt;

                            //RevFrom
                            result.Ev_RevSurahIdFrom = eval.Ev_RevSurahIdFrom;
                            result.Ev_RevAyahIdFrom = eval.Ev_RevAyahIdFrom;
                            //RevTo
                            result.Ev_RevSurahIdTo = eval.Ev_RevSurahIdTo;
                            result.Ev_RevAyahIdTo = eval.Ev_RevAyahIdTo;
                            //SaveFrom
                            result.Ev_SaveSurahIdFrom = eval.Ev_SaveSurahIdFrom;
                            result.Ev_SaveAyahIdFrom = eval.Ev_SaveAyahIdFrom;
                            //SaveTo
                            result.Ev_SaveSurahIdTo = eval.Ev_SaveSurahIdTo;
                            result.Ev_SaveAyahIdTo = eval.Ev_SaveAyahIdTo;
                            //RabtFrom
                            result.Ev_RabtSurahIdFrom = eval.Ev_RabtSurahIdFrom;
                            result.Ev_RabtAyahIdFrom = eval.Ev_RabtAyahIdFrom;
                            //RabtTo
                            result.Ev_RabtSurahIdTo = eval.Ev_RabtSurahIdTo;
                            result.Ev_RabtAyahIdTo = eval.Ev_RabtAyahIdTo;
                            //Notes
                            if (eval.Notes == null)
                                eval.Notes = " ";
                            result.Notes = eval.Notes;
                            ////////////////////
                            //RevFromName
                            sura = FindSurahById(eval.Ev_RevSurahIdFrom);
                            if (sura == null)
                                result.Ev_RevSurahNameFrom = null;
                            else
                                result.Ev_RevSurahNameFrom = sura.Name;
                            //RevToName
                            sura = FindSurahById(eval.Ev_RevSurahIdTo);
                            if (sura == null)
                                result.Ev_RevSurahNameTo = null;
                            else
                                result.Ev_RevSurahNameTo = sura.Name;
                            //SaveFromName
                            sura = FindSurahById(eval.Ev_SaveSurahIdFrom);
                            if (sura == null)
                                result.Ev_SaveSurahNameFrom = null;
                            else
                                result.Ev_SaveSurahNameFrom = sura.Name;
                            //SaveToName
                            sura = FindSurahById(eval.Ev_SaveSurahIdTo);
                            if (sura == null)
                                result.Ev_SaveSurahNameTo = null;
                            else
                                result.Ev_SaveSurahNameTo = sura.Name;
                            //RabtFromName
                            sura = FindSurahById(eval.Ev_RabtSurahIdFrom);
                            if (sura == null)
                                result.Ev_RabtSurahNameFrom = null;
                            else
                                result.Ev_RabtSurahNameFrom = sura.Name;
                            //RabtToName
                            sura = FindSurahById(eval.Ev_RabtSurahIdTo);
                            if (sura == null)
                                result.Ev_RabtSurahNameTo = null;
                            else
                                result.Ev_RabtSurahNameTo = sura.Name;
                            #endregion
                            _context.Update(result);
                        }
                    }
                }
            }
            _context.SaveChanges();
            return true;
        }
        public bool UpdateLessonsOfStudents(List<Student> Students)
        {
            //Update Lesson Of Students
            foreach (var student in Students)
            {
                var result = FindStudentById(student.Id);

                if (result != null)
                {
                    if (student.PlanData.Name.Equals("الخطة العامة")) //  الخطة العامة
                    {
                        var resultLesson = FindLessonById(student);
                        if (student.newLessonState == true)
                        {
                            //مراجعة سابقة - من
                            result.LessonData.PrevRev_FromSorh = student.PrevRevSurhFrom;
                            result.LessonData.PrevRev_FromAyah = student.PrevRevVerseFrom;

                            //مراجعة سابقة - الي
                            result.LessonData.PrevRev_ToSorh = student.PrevRevSurahTo;
                            result.LessonData.PrevRev_ToAyah = student.PrevRevVerseTo;

                            //حفظ سابق - من
                            result.LessonData.PrevSave_FromSorh = result.LessonData.Save_FromSorh;
                            result.LessonData.PrevSave_FromAyah = result.LessonData.Save_FromAyah;

                            //حفظ سابق - الي
                            result.LessonData.PrevSave_ToSorh = student.PrevSaveSurhTo;
                            result.LessonData.PrevSave_ToAyah = student.PrevSaveVerseTo;

                            //حفظ حالي -من
                            // في حالة لو اخر آية ف السورة نبدأ ف السورة اللي بعدها والاية 1
                            var resultSurah = FindSurahByName(student.PrevSaveSurhTo);

                            var maxVerse = GetMaxVerse(resultSurah.Id);
                            if (student.PrevSaveVerseTo == maxVerse && result.SaveDirection == "ctp")
                            {
                                resultSurah = FindSurahById((resultSurah.Id) + 1);

                                result.LessonData.Save_FromSorh = resultSurah.Name;
                                result.LessonData.Save_FromSurah = resultSurah.Id;
                                result.LessonData.Save_FromAyah = 1;
                            }
                            else if (student.PrevSaveVerseTo == maxVerse && result.SaveDirection == "ptc")
                            {
                                resultSurah = FindSurahById((resultSurah.Id) - 1);

                                result.LessonData.Save_FromSorh = resultSurah.Name;
                                result.LessonData.Save_FromSurah = resultSurah.Id;
                                result.LessonData.Save_FromAyah = 1;
                            }
                            else
                            {
                                resultSurah = FindSurahByName(student.PrevSaveSurhTo);
                                result.LessonData.Save_FromSorh = student.PrevSaveSurhTo;
                                result.LessonData.Save_FromSurah = resultSurah.Id;
                                result.LessonData.Save_FromAyah = student.PrevSaveVerseTo + 1;
                            }
                            result.Surah = resultSurah;



                            _context.LessonsData.Update(resultLesson);
                        }
                    }
                    else if (student.PlanData.Name.Equals("الخطة المفتوحة")) // الخطة المفتوحة
                    {
                        var resultLesson = FindLessonById(student);
                        if (student.newLessonState == true)
                        {
                            //حفظ سابق - من
                            result.LessonData.PrevSave_FromSorh = result.LessonData.Save_FromSorh;
                            result.LessonData.PrevSave_FromAyah = result.LessonData.Save_FromAyah;

                            //حفظ سابق - الي
                            result.LessonData.PrevSave_ToSorh = student.PrevSaveSurhTo;
                            result.LessonData.PrevSave_ToAyah = student.PrevSaveVerseTo;

                            //حفظ حالي -من
                            // في حالة لو اخر آية ف السورة نبدأ ف السورة اللي بعدها والاية 1
                            var resultSurah = FindSurahByName(student.PrevSaveSurhTo);

                            var maxVerse = GetMaxVerse(resultSurah.Id);
                            if (student.PrevSaveVerseTo == maxVerse && result.SaveDirection == "ctp")
                            {
                                resultSurah = FindSurahById((resultSurah.Id) + 1);

                                result.LessonData.Save_FromSorh = resultSurah.Name;
                                result.LessonData.Save_FromAyah = 1;
                            }
                            else if (student.PrevSaveVerseTo == maxVerse && result.SaveDirection == "ptc")
                            {
                                resultSurah = FindSurahById((resultSurah.Id) - 1);

                                result.LessonData.Save_FromSorh = resultSurah.Name;
                                result.LessonData.Save_FromAyah = 1;
                            }
                            else
                            {
                                result.LessonData.Save_FromSorh = student.PrevSaveSurhTo;
                                result.LessonData.Save_FromAyah = student.PrevSaveVerseTo + 1;
                            }
                            result.Surah = resultSurah;

                            ////مراجعة سابقة - من
                            //result.LessonData.PrevRev_FromSorh = student.PrevRevSurhFrom;
                            //result.LessonData.PrevRev_FromAyah = student.PrevRevVerseFrom;

                            ////مراجعة سابقة - الي
                            ////prob
                            //result.LessonData.PrevRev_ToSorh = student.PrevRevSurahTo;
                            //result.LessonData.PrevRev_ToAyah = student.PrevRevVerseTo;

                            _context.LessonsData.Update(resultLesson);
                        }
                    }
                    else if (student.PlanData.Name.Equals("الذكر الحكيم")) // الذكر الحكيم
                    {
                        if (student.newLessonState == true)
                            result.LessonDataId = (student.LessonDataId) + 1;
                    }
                }
                Task.Delay(5);
                _context.Students.Update(result);
            }
            _context.SaveChanges();
            return true;
        }

        #endregion

        #region All PrayerTimes With API
        public PrayerTimingsDto GetPrayerTimesAPI()
        {
            try
            {
                PrayerTimingsDto pt = new PrayerTimingsDto();

                HttpClient httpClient = new HttpClient();
                string? endPoint = "https://api.aladhan.com/timingsByCity";
                string? city = "umluj";
                string? country = "SA";
                string? patameters = $"?city={city}&country={country}";
                var uri = endPoint + patameters;

                HttpResponseMessage httpResponseMessage = httpClient.GetAsync(uri).Result;

                var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                dynamic result = JsonConvert.DeserializeObject(response);

                pt.FajrTime = result.data.timings.Fajr;
                pt.FajrTime = (Convert.ToDateTime(pt.FajrTime).AddHours(0).ToString("HH:mm"));
                pt.EndFajrTime = (Convert.ToDateTime(pt.FajrTime).AddHours(2)).AddMinutes(15).ToString("HH:mm");

                pt.SunriseTime = result.data.timings.Sunrise;
                pt.SunriseTime = (Convert.ToDateTime(pt.SunriseTime).AddHours(1).AddMinutes(0).ToString("HH:mm"));
                pt.EndSunriseTime = (Convert.ToDateTime(pt.SunriseTime).AddHours(5)).AddMinutes(0).ToString("HH:mm");

                pt.ZohrTime = result.data.timings.Dhuhr;
                pt.ZohrTime = (Convert.ToDateTime(pt.ZohrTime).ToString("HH:mm"));
                pt.EndZohrTime = (Convert.ToDateTime(pt.ZohrTime).AddMinutes(15).AddHours(2)).ToString("HH:mm");

                pt.AsrTime = result.data.timings.Asr;
                pt.AsrTime = (Convert.ToDateTime(pt.AsrTime).AddHours(0).ToString("HH:mm"));
                pt.EndAsrTime = (Convert.ToDateTime(pt.AsrTime).AddMinutes(15).AddHours(2)).ToString("HH:mm");

                pt.MghrbTime = result.data.timings.Maghrib;
                pt.MghrbTime = (Convert.ToDateTime(pt.MghrbTime).AddHours(0).ToString("HH:mm"));
                pt.EndMghrbTime = (Convert.ToDateTime(pt.MghrbTime).AddHours(1).AddMinutes(45)).ToString("HH:mm");

                pt.IshaTime = result.data.timings.Isha;
                pt.IshaTime = (Convert.ToDateTime(pt.IshaTime).AddHours(0).ToString("HH:mm"));
                pt.EndIshaTime = (Convert.ToDateTime(pt.IshaTime).AddHours(2).AddMinutes(15)).ToString("HH:mm");

                pt.DateOfDay_Hijri = result.data.date.hijri.date;
                pt.DateOfDay_Gregorian = result.data.date.gregorian.date;

                return pt;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        #endregion

        #region All Episode Types
        public List<EpisodeType> GetEpisodeTypes()
        {
            try
            {
                return _context.EpisodesTypes
                               .Include(l => l.Episodes)
                               .OrderBy(x => x.Name)
                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Create & Update Episode Type

        public EpisodeType FindByEpTypeId(Guid Id)
        {
            try
            {
                return _context.EpisodesTypes.FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public EpisodeType FindByEpTypeName(string Name)
        {
            try
            {
                return _context.EpisodesTypes.FirstOrDefault(x => x.Name.Equals(Name));
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);
            using var stream = File.Create(path);
            cover.CopyToAsync(stream);

            return coverName;
        }
        public bool SaveEpisodeType(EpisodeType model)
        {
            EpisodeViewModel epvm = new();
            try
            {
                //var epPic = SaveCover(epvm.EpTypeImage);
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();

                    //model.EpisodePicture = epPic;
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.EpisodesTypes.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    //var hasNewPic = epvm.EpTypeImage is not null;
                    //var oldPic = model.EpisodePicture;
                    //if (hasNewPic)
                    //{
                    //    model.EpisodePicture = SaveCover(epvm.EpTypeImage!);
                    //}
                    //var effectedRows = _context.SaveChanges();
                    //if (effectedRows > 0)
                    //{
                    //    if (hasNewPic)
                    //    {
                    //        var pic = Path.Combine(_imagesPath, oldPic);
                    //        File.Delete(pic);
                    //    }
                    //}
                    //else
                    //{
                    //    var cover = Path.Combine(_imagesPath, model.EpisodePicture);
                    //    File.Delete(cover);
                    //}

                    //result.EpisodePicture = model.EpisodePicture;
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.Episodes.Update(result);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Delete Episode Type
        public bool DeleteEpisodeType(Guid Id)
        {
            try
            {
                var result = FindByEpTypeId(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.EpisodesTypes.Update(result);
                // _context.EpisodesTypes.Remove(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Teacher Attendance
        public Attendance FindAttendanceByEpisodeId(Guid epId)
        {
            try
            {
                return _context.Attendances
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Episode)
                               .FirstOrDefault(x => x.EpisodeId.Equals(epId) && x.Att_In.Date == DateTime.Now.Date);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Teachers Data
        //GetTeachersOfEpisodes()
        public List<ApplicationUser> GetAllTeachers()
        {

            var allusers = _context.Users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                IdentityNum = user.IdentityNum,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Email = user.Email,
            }).ToList();
            var teachersmen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString())).ToList();
            var teacherswomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.TeacherWomen.ToString())).ToList();

            try
            {
                return _context.Users
                               .Include(x => x.Episodes)
                               .Include(x => x.Attendances)
                               .OrderBy(x => x.Nationality)
                               .ThenBy(x => x.FullName)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        #endregion

        #region Location State

        //public bool UpdateLocationState(EpisodeViewModel model)
        //{
        //    try
        //    {
        //        foreach (var episode in model.EpisodesWithTeacher)
        //        {
        //            var result = FindBy(episode.Id);
        //            if (result != null)
        //            {

        //                result.Description = episode.Description;

        //                _context.Episodes.Update(result);
        //            }
        //        }
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }
}
