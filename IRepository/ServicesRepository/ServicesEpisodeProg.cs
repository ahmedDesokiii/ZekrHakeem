using ExpressTimezone;
using Newtonsoft.Json;
using ZekrHakeem_v11.Dtos;

namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesEpisodeProg : IServicesRepository<EpisodeProg>, IServicesEpisodeProg
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;


        public ServicesEpisodeProg(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        #region All EpisodeProgs
        public List<EpisodeProg> GetAll()
        {
            try
            {
                return _context.EpisodeProgs
                               .Include(x => x.EpProgLocation)
                               .Include(x => x.EpProgLocation.LoCenter)
                               .Include(x => x.EpProgLocation.District)
                               .Include(x => x.EpProg)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Attendances)
                               .Include(x => x.StudentProgs)
                               .OrderByDescending(x => x.PauseState)
                               .ThenBy(x => x.AttendType)
                               .ThenBy(x => x.ApplicationUser.FullName)
                               .ThenBy(x => x.Period)

                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region EpisodeProgs Men
        public List<EpisodeProg> GetEpisodeProgsForMen()
        {
            try
            {
                return _context.EpisodeProgs
                            .Include(x => x.EpProgLocation)
                               .Include(x => x.EpProgLocation.LoCenter)
                               .Include(x => x.EpProgLocation.District)
                               .Include(x => x.EpProg)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Attendances)
                               .Include(x => x.StudentProgs)
                             .OrderByDescending(x => x.PauseState)
                             .ThenBy(x => x.AttendType)
                             .ThenBy(x => x.ApplicationUser.FullName)
                             .ThenBy(x => x.Period)
                                .Where(x => x.CurrentState > 0 && x.EpProgLocation.Type == "mosque")
                                .ToList();


            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region EpisodeProgs Women
        public List<EpisodeProg> GetEpisodeProgsForWomen()
        {
            try
            {
                return _context.EpisodeProgs
                              .Include(x => x.EpProgLocation)
                               .Include(x => x.EpProgLocation.LoCenter)
                               .Include(x => x.EpProgLocation.District)
                               .Include(x => x.EpProg)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Attendances)
                               .Include(x => x.StudentProgs)
                               .OrderByDescending(x => x.PauseState)
                               .ThenBy(x => x.AttendType)
                                .ThenBy(x => x.ApplicationUser.FullName)
                            .ThenBy(x => x.Period)
                               .Where(x => x.CurrentState > 0 && x.EpProgLocation.Type == "dar" && x.EpProgLocation.LoCenter.Type == "women")
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region EpisodeProgs With Supervisor
        public List<EpisodeProg> GetEpisodeProgsWithSupervisor(string userid)
        {
            try
            {
                return _context.EpisodeProgs
                            .Include(x => x.EpProgLocation)
                               .Include(x => x.EpProgLocation.LoCenter)
                               .Include(x => x.EpProgLocation.District)
                               .Include(x => x.EpProg)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Attendances)
                               .Include(x => x.StudentProgs)
                           .OrderByDescending(x => x.PauseState)
                            .ThenBy(x => x.AttendType)
                            .ThenBy(x => x.ApplicationUser.FullName)
                            .ThenBy(x => x.Period)
                               .Where(x => x.CurrentState > 0 && x.PauseState > 0 && x.EpProgLocation.LoCenter.ApplicationUserId == userid)
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region EpisodeProgs With Teacher
        public List<EpisodeProg> GetEpisodeProgsWithTeacher(string userid)
        {
            try
            {
                List<EpisodeProg> EpisodeProgsOfTeacher = new List<EpisodeProg>();
                EpisodeProgsOfTeacher = _context.EpisodeProgs
                                   .Include(x => x.EpProgLocation)
                               .Include(x => x.EpProgLocation.LoCenter)
                               .Include(x => x.EpProgLocation.District)
                               .Include(x => x.EpProg)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Attendances)
                               .Include(x => x.StudentProgs)
                                    .OrderBy(x => x.AttendType)
                                    .ThenBy(x => x.Period)
                                    .ThenBy(x => x.Name)
                                    .Where(x => x.CurrentState > 0 && x.PauseState > 0 && x.ApplicationUserId == userid)
                                    .ToList();

                return EpisodeProgsOfTeacher;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region EpisodeProgs With Prog
        public List<EpisodeProg> GetEpisodeProgsWithProg(Guid prid)
        {
            try
            {
                List<EpisodeProg> EpisodeProgsOfProg = new List<EpisodeProg>();
                EpisodeProgsOfProg = _context.EpisodeProgs
                                   .Include(x => x.EpProgLocation)
                               .Include(x => x.EpProgLocation.LoCenter)
                               .Include(x => x.EpProgLocation.District)
                               .Include(x => x.EpProg)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Attendances)
                               .Include(x => x.StudentProgs)
                                    .OrderBy(x => x.AttendType)
                                    .ThenBy(x => x.Period)
                                    .ThenBy(x => x.Name)
                                    .Where(x => x.CurrentState > 0 && x.PauseState > 0 && x.EpProgId == prid)
                                    .ToList();

                return EpisodeProgsOfProg;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Find EpisodeProgs By ...
        public EpisodeProg FindChecherEpisodeProgBy(Guid Id)
        {
            try
            {
                return _context.EpisodeProgs
                .Include(x => x.ApplicationUser)
                               .FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public EpisodeProg FindBy(Guid Id)
        {
            try
            {
                return _context.EpisodeProgs
                               .Include(x => x.ApplicationUser)
                               .FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public EpisodeProg FindBy(string Name)
        {
            try
            {
                return _context.EpisodeProgs
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
                var attend = _context.Attendances.FirstOrDefault(x => x.Att_In.Day.Equals(day) && x.Att_In.Month.Equals(month) && x.Att_In.Year.Equals(year) && x.CurrentState > 0 && x.ApplicationUserId.ToString().Equals(userApp) && x.Id != null && x.EpisodeProgId.Equals(epId));
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
        public StudentProg FindStudentProgById(Guid Id)
        {
            try
            {
                return _context.StudentProgs.Include(x => x.EpisodeProg).FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
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
        public EvaluationProg FindEvalById(Guid id)
        {
            try
            {
                return _context.EvaluationProgs.FirstOrDefault(x => x.Id.Equals(id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public EvaluationProg FindEvalByDate(Guid? stId)
        {
            try
            {
                return _context.EvaluationProgs.FirstOrDefault(x => x.Ev_Date.Date.Equals(DateTime.Now.Date) && x.EvStudentProgId.Equals(stId) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Evaluation FindBasicEvalByDate(Guid? stId)
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
        public LessonData FindLessonById(StudentProg stu)
        {
            try
            {
                return _context.LessonsData.Include(p => p.LevelData).Include(p => p.StudentProgs).FirstOrDefault(x => x.Id.Equals(stu.LessonDataId) && x.CurrentState > 0);
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

        #region Create & Update EpisodeProg
        public bool Save(EpisodeProg model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.PauseState = (int)Helper.ePauseState.Active;
                    model.IsAvilable = true;
                    model.EpProgLocation = null;

                    //if (!CheckDuplicate(model, GetEpisodeProgsWithTeacher(model.ApplicationUserId)))
                    //    return false;

                    _context.EpisodeProgs.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    result.AttendType = model.AttendType;
                    result.Status = model.Status;
                    result.Period = model.Period;
                    result.Description = model.Description;
                    result.EpProgLocationId = model.EpProgLocationId;
                    result.EpProgId = model.EpProgId;
                    result.CurrentState = (int)Helper.eCurrentState.Active;
                    result.PauseState = (int)Helper.ePauseState.Active;
                    result.ApplicationUserId = model.ApplicationUserId;
                    result.SaveDir = model.SaveDir;
                    result.StartSurah = model.StartSurah;
                    result.EndSurah = model.EndSurah;

                    //if (!CheckDuplicate(result, GetEpisodeProgsWithTeacher(model.ApplicationUserId)))
                    //    return false;

                    _context.EpisodeProgs.Update(result);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckDuplicate(EpisodeProg episode, List<EpisodeProg> EpisodeProgsWithTeacher)
        {
            if (episode.AttendType.Contains("حضوري"))
            {
                foreach (var ep in EpisodeProgsWithTeacher)
                {
                    if (episode.Id != ep.Id && episode.AttendType.Contains("حضوري") && episode.Period == ep.Period)
                        return false;
                }
            }
            return true;
        }


        #endregion

        #region Delete EpisodeProg
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.EpisodeProgs.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Pause | Resume EpisodeProg
        public bool Pause(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.PauseState = (int)Helper.ePauseState.Pause;
                result.IsAvilable = false;
                _context.EpisodeProgs.Update(result);
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

                _context.EpisodeProgs.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region EpisodeProg Details : [students , next lessons , previous lessons ]

        public EpisodeProg? GetById(Guid id)
        {
            var ep = _context.EpisodeProgs
                .Include(x => x.EpProgLocation)
                               .Include(x => x.EpProgLocation.LoCenter)
                               .Include(x => x.EpProgLocation.District)
                               .Include(x => x.EpProg)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Attendances)
                               .Include(x => x.StudentProgs)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
            return ep;
        }
        public void ResetAllEpisodeProgs()
        {
            var utcTime = DateTime.UtcNow;
            var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh");
            var result = FindChecherEpisodeProgBy(new Guid("00000000-0000-0000-1111-000000001111"));
            if (result.CheckDate != currentTime.Date)
            {
                foreach (var episode in _context.EpisodeProgs)
                {
                    episode.IsAvilable = true;
                    episode.Status = false;
                    episode.ManualFinished = false;
                    episode.CheckDate = currentTime.Date;

                    //update IsEvaluate For every student in episode
                    StudentProgViewModel svm = GetStudentProgsForEpisodeProg(episode.Id);
                    foreach (var stu in svm.StudentProgs)
                    {
                        stu.IsEvaluate = false;
                        if (stu.EpisodeProg.EpProg.Plan.Name.Contains("الخطة العامة"))
                        {

                        }
                        _context.StudentProgs.Update(stu);
                    }

                    _context.EpisodeProgs.Update(episode);
                }
            }
            _context.SaveChanges();
        }
        public void StartEpisodeProg(Guid id)
        {
            // active episode
            var result = FindBy(id);

            result.Status = true;
            result.IsAvilable = true;
            result.ManualFinished = false;

            // add attendance to teacher
            if (result.AttendType.Equals("حضوري"))
            {
                var utcTime = DateTime.UtcNow;
                var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh");
                var att = FindAttendByDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, result.ApplicationUserId, id);
                if (att == null)
                {
                    att = new Attendance();
                    att.Id = Guid.NewGuid();
                    att.CurrentState = (int)Helper.eCurrentState.Active;
                    att.Att_In = currentTime;
                    att.Att_Out = currentTime;
                    att.ApplicationUser = result.ApplicationUser;
                    att.EpisodeProgId = id;
                    _context.Attendances.Add(att);
                }
                //else
                //{
                //    att.Att_In = currentTime;
                //    att.Att_Out = currentTime;
                //    att.ApplicationUser = result.ApplicationUser;
                //    att.EpisodeProgId = id;
                //    _context.Attendances.Update(att);
                //}
            }
            _context.EpisodeProgs.Update(result);
            _context.SaveChanges();
        }
        public void ManualEndEpisodeProg(Guid id)
        {
            var utcTime = DateTime.UtcNow;
            var currentTime = utcTime.UTCToRegionalTime("Asia/Riyadh");
            var result = FindBy(id);

            result.Status = false;
            result.ManualFinished = true;
            result.IsAvilable = true;

            //update IsEvaluate For every student in episode
            StudentProgViewModel svm = GetStudentProgsForEpisodeProg(id);
            //foreach (var stu in svm.StudentProgs)
            //{
            //    stu.IsEvaluate = false;
            //    _context.Students.Update(stu);
            //}

            // Update attendance to teacher
            var att = FindAttendByDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, result.ApplicationUserId, id);
            if (att != null)
            {
                att.Att_Out = currentTime;
                _context.Attendances.Update(att);
            }

            _context.EpisodeProgs.Update(result);
            _context.SaveChanges();
        }

        public void AutoEndEpisodeProg(Guid id)
        {
            var result = FindBy(id);
            result.Status = false;
            result.ManualFinished = false;
            result.IsAvilable = true;
            _context.EpisodeProgs.Update(result);

            _context.SaveChanges();
        }
        public StudentProgViewModel GetStudentProgsForEpisodeProg(Guid epid)
        {
            try
            {
                var ep = GetById(epid);
                StudentProgViewModel stvm = new StudentProgViewModel();

                stvm.StudentProgs = _context.StudentProgs
                                    .Include(x => x.EpisodeProg)
                                    .Include(x => x.EpisodeProg.EpProg)
                                    .Include(x => x.EpisodeProg.EpProg.Plan)
                                    .Include(x => x.LessonData)
                                    .Include(x => x.Surah)
                                    .Include(x => x.EvaluationProgs)
                                    .Include(x => x.Nationality)
                                    .Where(x => x.CurrentState > 0 && x.StEpisodeProgId == epid)
                                    .OrderBy(x => x.Name)
                                    .ToList();

                stvm.StudentProgsForMen = stvm.StudentProgs;
                stvm.StudentProgsForWomen = stvm.StudentProgs;
                stvm.StudentProgsWithSupervisor = stvm.StudentProgs;
                stvm.StudentProgsWithTeacher = stvm.StudentProgs;

                stvm.Surahs_ctp = new SelectList(_context.Surahs.AsNoTracking().Where(x => x.Id <= ep.StartSurah && x.Id >= ep.EndSurah).OrderBy(x => x.Id).ToList(), "Id", "Name");
                stvm.Surahs_ptc = new SelectList(_context.Surahs.AsNoTracking().Where(x => x.Id <= ep.StartSurah && x.Id >= ep.EndSurah).OrderByDescending(x => x.Id).ToList(), "Id", "Name");


                return stvm;


            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<StudentProg, EvaluationProg> GetStudentProgLastEvaluate(List<StudentProg> students)
        {
            try
            {
                Dictionary<StudentProg, EvaluationProg> stuEvaluate = new();

                foreach (var student in students)
                {
                    EvaluationProg? lastEval = new EvaluationProg();

                    lastEval = _context.EvaluationProgs
                    .Include(x => x.EvStudentProg)
                    //.OrderByDescending(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentProgId == student.Id)
                    .AsNoTracking()
                    .SingleOrDefault(g => g.Ev_Date == (DateTime.Now.Date));

                    if (lastEval == null)
                    {
                        lastEval = new() { Id = Guid.NewGuid(), CurrentState = (int)Helper.eCurrentState.Active, Ev_Date = DateTime.Now.Date, Ev_Attend = 0, Ev_Rabt = -1, Ev_Rev = -1, Ev_Save = -1, EvStudentProg = student };
                        _context.EvaluationProgs.Add(lastEval);
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


        public bool AddEvaluatoinOnly(List<EvaluationProg> evaluations)
        {
            // Add Evaluations
            foreach (var eval in evaluations)
            {
                //if (eval == null) { return false; }
                var stu = FindStudentProgById((Guid)eval.EvStudentProgId);
                var result = FindEvalByDate(eval.EvStudentProgId);
                var basicEval = new Evaluation();

                if (result == null)
                {
                    eval.Id = Guid.NewGuid();
                    eval.CurrentState = (int)Helper.eCurrentState.Active;
                    eval.Ev_Date = DateTime.Now.Date;

                    if (eval.Ev_Save > -1)
                        stu.IsEvaluate = true;

                    if (eval.Notes == null)
                        eval.Notes = " ";

                    _context.Add(eval);
                    _context.Update(stu);
                }
                else
                {
                    //تقييم بدون قيمة
                    if (result.Ev_Save == -1 && stu.IsEvaluate == false)
                    {
                        result.Ev_Attend = eval.Ev_Attend;
                        result.Ev_Save = eval.Ev_Save;
                        result.Ev_Rev = eval.Ev_Rev;
                        result.Ev_Rabt = eval.Ev_Rabt;

                        result.Ev_RevSurahIdFrom = eval.Ev_RevSurahIdFrom;
                        result.Ev_RevAyahIdFrom = eval.Ev_RevAyahIdFrom;

                        result.Ev_RevSurahIdTo = eval.Ev_RevSurahIdTo;
                        result.Ev_RevAyahIdTo = eval.Ev_RevAyahIdTo;

                        result.Ev_SaveSurahIdTo = eval.Ev_SaveSurahIdTo;
                        result.Ev_SaveAyahIdTo = eval.Ev_SaveAyahIdTo;

                        if (eval.Notes == null)
                            eval.Notes = " ";
                        result.Notes = eval.Notes;

                        if (result.Ev_Save > -1)
                            stu.IsEvaluate = true;

                    }

                    // تعديل فيما تم تقييمه
                    else if (result.Ev_Save >= -1 && stu.IsEvaluate == true)
                    {
                        if (result.Ev_Attend != eval.Ev_Attend || result.Ev_Save != eval.Ev_Save || result.Ev_Rev != eval.Ev_Rev || result.Ev_Rabt != eval.Ev_Rabt
                            || result.Ev_RevSurahIdFrom != eval.Ev_RevSurahIdFrom || result.Ev_RevSurahIdTo != eval.Ev_RevSurahIdTo || result.Ev_SaveSurahIdTo != eval.Ev_SaveSurahIdTo
                            || result.Ev_RevAyahIdFrom != eval.Ev_RevAyahIdFrom || result.Ev_RevAyahIdTo != eval.Ev_RevAyahIdTo || result.Ev_SaveAyahIdTo != eval.Ev_SaveAyahIdTo)
                        {
                            result.Ev_Attend = eval.Ev_Attend;
                            result.Ev_Save = eval.Ev_Save;
                            result.Ev_Rabt = eval.Ev_Rabt;
                            result.Ev_Rev = eval.Ev_Rev;

                            result.Ev_RevSurahIdFrom = eval.Ev_RevSurahIdFrom;
                            result.Ev_RevAyahIdFrom = eval.Ev_RevAyahIdFrom;

                            result.Ev_RevSurahIdTo = eval.Ev_RevSurahIdTo;
                            result.Ev_RevAyahIdTo = eval.Ev_RevAyahIdTo;

                            result.Ev_SaveSurahIdTo = eval.Ev_SaveSurahIdTo;
                            result.Ev_SaveAyahIdTo = eval.Ev_SaveAyahIdTo;

                            if (eval.Notes == null)
                                eval.Notes = " ";
                            result.Notes = eval.Notes;
                        }
                    }
                    _context.Update(result);
                    _context.Update(stu);
                }
                // Set Data To Evaluation Entity When Student in
                //if (stu.outZH == false)
                //{
                //    basicEval.Id = eval.Id;
                //    basicEval.CurrentState = eval.CurrentState;
                //    basicEval.Ev_Date = eval.Ev_Date;
                //    basicEval.Ev_Attend = eval.Ev_Attend;
                //    basicEval.Ev_Save = eval.Ev_Save;
                //    basicEval.Ev_Rev = eval.Ev_Rev;
                //    basicEval.Ev_Rabt = eval.Ev_Rabt;
                //    basicEval.Notes = eval.Notes;
                //    basicEval.EvStudentId = eval.EvStudentProgId;

                //    _context.Add(basicEval);
                //}
            }

            _context.SaveChanges();
            return true;
        }
        public bool UpdateLessonsOfStudents(List<StudentProg> Students)
        {
            //Update Lesson Of Students
            foreach (var student in Students)
            {
                var result = FindStudentProgById(student.Id);
                result.EpisodeProg = FindBy((Guid)result.StEpisodeProgId);

                if (result != null)
                {

                    if (student.EpisodeProg.EpProg.Plan.Name.Equals("الخطة المفتوحة")) //  الخطة المفتوحة
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
                            if (student.PrevSaveVerseTo == maxVerse && result.EpisodeProg.SaveDir == "ctp")
                            {
                                resultSurah = FindSurahById((resultSurah.Id) + 1);

                                result.LessonData.Save_FromSorh = resultSurah.Name;
                                result.LessonData.Save_FromAyah = 1;
                            }
                            else if (student.PrevSaveVerseTo == maxVerse && result.EpisodeProg.SaveDir == "ptc")
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

                            _context.LessonsData.Update(resultLesson);
                        }
                    }
                    else if (student.EpisodeProg.EpProg.Plan.Name.Equals("الذكر الحكيم")) // الذكر الحكيم
                    {
                        if (student.newLessonState == true)
                            result.LessonDataId = (student.LessonDataId) + 1;
                    }

                    else if (student.EpisodeProg.EpProg.Plan.Name.Equals("الخطة العامة")) //  الخطة العامة
                    {
                        var resultLesson = FindLessonById(student);
                        if (student.newLessonState == true)
                        {
                            //مراجعة سابقة - من
                            //result.LessonData.PrevRev_FromSorh = student.PrevRevSurhFrom;
                            //result.LessonData.PrevRev_FromAyah = student.PrevRevVerseFrom;

                            //مراجعة سابقة - الي
                            //result.LessonData.PrevRev_ToSorh = student.PrevRevSurahTo;
                            //result.LessonData.PrevRev_ToAyah = student.PrevRevVerseTo;

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
                            if (student.PrevSaveVerseTo == maxVerse && result.EpisodeProg.SaveDir == "ctp")
                            {
                                resultSurah = FindSurahById((resultSurah.Id) + 1);

                                result.LessonData.Save_FromSorh = resultSurah.Name;
                                result.LessonData.Save_FromAyah = 1;
                            }
                            else if (student.PrevSaveVerseTo == maxVerse && result.EpisodeProg.SaveDir == "ptc")
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



                            _context.LessonsData.Update(resultLesson);
                        }
                    }

                }
                _context.StudentProgs.Update(result);
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
                pt.EndFajrTime = (Convert.ToDateTime(pt.FajrTime).AddHours(2)).ToString("HH:mm");

                pt.SunriseTime = result.data.timings.Sunrise;
                pt.SunriseTime = (Convert.ToDateTime(pt.SunriseTime).AddHours(0).AddMinutes(0).ToString("HH:mm"));
                pt.EndSunriseTime = (Convert.ToDateTime(pt.SunriseTime).AddHours(10)).AddMinutes(12).ToString("HH:mm");

                pt.ZohrTime = result.data.timings.Dhuhr;
                pt.ZohrTime = (Convert.ToDateTime(pt.ZohrTime).ToString("HH:mm"));
                pt.EndZohrTime = (Convert.ToDateTime(pt.ZohrTime).AddHours(2)).ToString("HH:mm");

                pt.AsrTime = result.data.timings.Asr;
                pt.AsrTime = (Convert.ToDateTime(pt.AsrTime).AddHours(0).ToString("HH:mm"));
                pt.EndAsrTime = (Convert.ToDateTime(pt.AsrTime).AddHours(2)).ToString("HH:mm");

                pt.MghrbTime = result.data.timings.Maghrib;
                pt.MghrbTime = (Convert.ToDateTime(pt.MghrbTime).AddHours(0).ToString("HH:mm"));
                pt.EndMghrbTime = (Convert.ToDateTime(pt.MghrbTime).AddHours(1).AddMinutes(30)).ToString("HH:mm");

                pt.IshaTime = result.data.timings.Isha;
                pt.IshaTime = (Convert.ToDateTime(pt.IshaTime).AddHours(0).ToString("HH:mm"));
                pt.EndIshaTime = (Convert.ToDateTime(pt.IshaTime).AddHours(2)).ToString("HH:mm");

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


        #region Teacher Attendance
        public Attendance FindAttendanceByEpisodeProgId(Guid epId)
        {
            try
            {
                return _context.Attendances
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.EpisodeProg)
                               .FirstOrDefault(x => x.EpisodeProgId.Equals(epId) && x.Att_In.Date == DateTime.Now.Date);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Teachers Data
        //GetTeachersOfEpisodeProgs()
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
                               .Include(x => x.EpisodeProgs)
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

        //public bool UpdateLocationState(EpisodeProgViewModel model)
        //{
        //    try
        //    {
        //        foreach (var episode in model.EpisodeProgsWithTeacher)
        //        {
        //            var result = FindBy(episode.Id);
        //            if (result != null)
        //            {

        //                result.Description = episode.Description;

        //                _context.EpisodeProgs.Update(result);
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


        //////////////////
        /// Student Prog Services
        /// 
        #region Find Student Prog

        public StudentProg FindStudentProgBy(Guid Id)
        {
            try
            {
                return _context.StudentProgs
                               .Include(x => x.EpisodeProg)
                               .FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public StudentProg FindStudentProgBy(string Name)
        {
            try
            {
                return _context.StudentProgs
                               .Include(x => x.EpisodeProg)
                               .FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Create & Update StudentProg
        public bool SaveStudentProg(StudentProg model)
        {
            try
            {
                var maxLesson = _context.LessonsData.Count();
                var result = FindStudentProgBy(model.Id);
                var usingEpProg = FindBy((Guid)model.StEpisodeProgId);
                //var stuBasic = FindStudentById(result.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.outZH = true;
                    model.IsEvaluate = false;
                    model.SurahId = usingEpProg.StartSurah;
                    var surahData = FindSurahById(model.SurahId);
                    model.PrevSaveSurhTo = surahData.Name;
                    model.PrevSaveVerseTo = 1;
                    model.LessonDataId = maxLesson + 1;
                    model.LessonData = new LessonData();
                    model.LessonData.Id = (int)model.LessonDataId;
                    model.LessonData.PrevSave_FromSorh = surahData.Name;
                    model.LessonData.PrevRev_FromSorh = surahData.Name;
                    model.LessonData.PrevSave_ToSorh = surahData.Name;
                    model.LessonData.PrevRev_ToSorh = surahData.Name;
                    model.LessonData.PrevSave_FromAyah = 1;
                    model.LessonData.PrevRev_FromAyah = 1;
                    model.LessonData.PrevSave_ToAyah = 1;
                    model.LessonData.PrevRev_FromAyah = 1;
                    model.LessonData.Save_FromSorh = surahData.Name;
                    model.LessonData.Save_FromAyah = 2;
                    model.LessonData.CurrentState = (int)Helper.eCurrentState.Active;

                    _context.StudentProgs.Add(model);
                }
                else
                {
                    if (result.outZH == true)
                    {
                        result.Name = model.Name;
                        result.ParentPhoneNumber = model.ParentPhoneNumber;
                        result.Gender = model.Gender;
                        result.Notes = model.Notes;
                        result.CurrentState = (int)Helper.eCurrentState.Active;
                        result.outZH = true;

                        result.SurahId = usingEpProg.StartSurah;
                        var surahData = FindSurahById(model.SurahId);
                        result.PrevSaveSurhTo = surahData.Name;
                        result.PrevSaveVerseTo = 1;
                        result.LessonDataId = model.LessonDataId;
                        //model.LessonData = new LessonData();
                        //model.LessonData.Id = (int)model.LessonDataId;
                        //model.LessonData.PrevSave_FromSorh = surahData.Name;
                        //model.LessonData.PrevRev_FromSorh = surahData.Name;
                        //model.LessonData.PrevSave_ToSorh = surahData.Name;
                        //model.LessonData.PrevRev_ToSorh = surahData.Name;
                        //model.LessonData.PrevSave_FromAyah = 1;
                        //model.LessonData.PrevRev_FromAyah = 1;
                        //model.LessonData.PrevSave_ToAyah = 1;
                        //model.LessonData.PrevRev_FromAyah = 1;
                        //model.LessonData.Save_FromSorh = surahData.Name;
                        //model.LessonData.Save_FromAyah = 2;
                        //model.LessonData.CurrentState = (int)Helper.eCurrentState.Active;

                        _context.StudentProgs.Update(result);
                    }
                    else
                    {
                        result.Name = model.Name;
                        result.ParentPhoneNumber = model.ParentPhoneNumber;
                        result.Gender = model.Gender;
                        result.Notes = model.Notes;
                        result.CurrentState = (int)Helper.eCurrentState.Active;
                        result.outZH = false;

                        //stuBasic.Id = model.Id;
                        //stuBasic.Name = model.Name;
                        //stuBasic.ParentPhoneNumber = model.ParentPhoneNumber;
                        //stuBasic.Gender = model.Gender;
                        //stuBasic.Notes = model.Notes;

                        //_context.Students.Update(stuBasic);
                        _context.StudentProgs.Update(result);
                    }

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

        #region Assigned Students to EpisodeProg
        public bool AssignedStudentsToEpisodeProg(EpisodeProgViewModel model)
        {
            try
            {
                //model.Students = GetStudentsForSelectedEpisodeProg();
                foreach (var student in model.StudentsVM.Students)
                {
                    var resultStu = FindStudentById(student.Id);


                    if (resultStu != null)
                    {
                        var stuProg = FindStudentProgBy(student.Id);
                        if (student.inProg == true)
                        {
                            if (stuProg == null)
                            {   // create
                                // غير مسجل بحلقات نوعية من قبل
                                stuProg = new StudentProg();
                                stuProg.Id = resultStu.Id;
                                stuProg.Name = resultStu.Name;
                                stuProg.ParentPhoneNumber = resultStu.ParentPhoneNumber;
                                stuProg.Gender = resultStu.Gender;
                                stuProg.NationalityId = resultStu.NationalityId;

                                stuProg.StEpisodeProgId = model.SelectedEpProgId;
                                stuProg.CurrentState = (int)Helper.eCurrentState.Active;
                                stuProg.outZH = false;

                                resultStu.inProg = student.inProg;
                                var resEpProg = FindBy(model.SelectedEpProgId);
                                resultStu.EpProgName = resEpProg.Name;

                                _context.StudentProgs.Add(stuProg);
                                _context.Students.Update(resultStu);

                            }
                            else
                            {   // update
                                //مسجل بحلقة نوعية مسبقا
                                stuProg.Id = resultStu.Id;
                                stuProg.Name = resultStu.Name;
                                stuProg.ParentPhoneNumber = resultStu.ParentPhoneNumber;
                                stuProg.Gender = resultStu.Gender;
                                stuProg.NationalityId = resultStu.NationalityId;
                                stuProg.StEpisodeProgId = model.SelectedEpProgId;
                                stuProg.CurrentState = (int)Helper.eCurrentState.Active;
                                stuProg.outZH = false;

                                resultStu.inProg = student.inProg;
                                var resEpProg = FindBy(model.SelectedEpProgId);
                                resultStu.EpProgName = resEpProg.Name;

                                _context.StudentProgs.Update(stuProg);
                                _context.Students.Update(resultStu);
                            }

                        }
                    }

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
    }
}
