namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesReport : IServicesReport
    {
        private readonly ApplicationDbContext _context;

        public ServicesReport(ApplicationDbContext context)
        {
            _context = context;
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
        public List<Attendance> GetAllAttendance()
        {
            try
            {
                return _context.Attendances
                               .Include(x => x.Episode)
                               .Include(x => x.ApplicationUser)
                               .OrderByDescending(x => x.Att_In.Date)
                               .ThenBy(x => x.Status_AttIn)
                               .ThenBy(x => x.Status_AttOut)
                               .ThenBy(x => x.Att_In)
                               .ThenBy(x => x.Att_Out)
                               .ThenBy(x => x.Episode.Period)
                               .ThenBy(x => x.ApplicationUser.FullName)
                               .ThenBy(x => x.Episode.Name)
                              .Where(x => x.CurrentState > 0 && x.Episode.CurrentState > 0 && x.Episode.PauseState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Attendance> GetAllAttendanceMen()
        {
            try
            {
                return _context.Attendances
                               .Include(x => x.Episode)
                               .Include(x => x.Episode.EpLocation)
                               .Include(x => x.Episode.EpLocation.LoCenter)
                               .Include(x => x.ApplicationUser)
                               .OrderByDescending(x => x.Att_In.Date)
                               .ThenBy(x => x.Status_AttIn)
                               .ThenBy(x => x.Status_AttOut)
                               .ThenBy(x => x.Att_In)
                               .ThenBy(x => x.Att_Out)
                               .ThenBy(x => x.Episode.Period)
                               .ThenBy(x => x.ApplicationUser.FullName)
                               .ThenBy(x => x.Episode.Name)
                               .Where(x => x.CurrentState > 0 && x.Episode.CurrentState > 0 && x.Episode.PauseState > 0 && x.Episode.EpLocation.LoCenter.Type == "men".ToLower())
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Attendance> GetAllAttendanceWomen()
        {
            try
            {
                return _context.Attendances
                               .Include(x => x.Episode)
                               .Include(x => x.Episode.EpLocation)
                               .Include(x => x.Episode.EpLocation.LoCenter)
                               .Include(x => x.ApplicationUser)
                               .OrderByDescending(x => x.Att_In.Date)
                               .ThenBy(x => x.Status_AttIn)
                               .ThenBy(x => x.Status_AttOut)
                               .ThenBy(x => x.Att_In)
                               .ThenBy(x => x.Att_Out)
                               .ThenBy(x => x.Episode.Period)
                               .ThenBy(x => x.ApplicationUser.FullName)
                               .ThenBy(x => x.Episode.Name)
                               .Where(x => x.CurrentState > 0 && x.Episode.CurrentState > 0 && x.Episode.PauseState > 0 && x.Episode.EpLocation.LoCenter.Type == "women".ToLower())
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Attendance> GetTeacherAttendance(string teacherId)
        {
            try
            {
                var att = _context.Attendances
                               .Include(x => x.Episode)
                               .Include(x => x.ApplicationUser)
                               .OrderByDescending(x => x.Att_In.Date)
                               .ThenBy(x => x.Status_AttIn)
                               .ThenBy(x => x.Status_AttOut)
                               .ThenBy(x => x.Att_In)
                               .ThenBy(x => x.Att_Out)
                               .ThenBy(x => x.Episode.Period)
                               .ThenBy(x => x.ApplicationUser.FullName)
                               .ThenBy(x => x.Episode.Name)
                               .AsNoTracking()
                               .Where(x => x.CurrentState > 0 && x.Episode.CurrentState > 0 && x.Episode.PauseState > 0 && x.ApplicationUserId == teacherId)
                               .ToList();
                return att;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public List<Student> GetAllStudents()
        {
            try
            {
                var stus = _context.Students
                               .Include(x => x.Evaluations)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Nationality)
                               .Include(x => x.Demands)
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.HealthCondition)
                               .Include(x => x.LessonData)
                               .Include(x => x.PlanData)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .OrderBy(x => x.StEpisode.Name)
                               .ThenBy(x => x.PlanData.Num)
                               .ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0)
                               .ToList();

                return stus;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Student> GetAllStudentsMen()
        {
            try
            {
                var stus = _context.Students
                               .Include(x => x.Evaluations)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Nationality)
                               .Include(x => x.Demands)
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.HealthCondition)
                               .Include(x => x.LessonData)
                               .Include(x => x.PlanData)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .OrderBy(x => x.StEpisode.Name)
                               .ThenBy(x => x.PlanData.Num)
                               .ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocation.LoCenter.Type == "men")
                               .ToList();

                return stus;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Student> GetAllStudentsWomen()
        {
            try
            {
                var stus = _context.Students
                               .Include(x => x.Evaluations)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Nationality)
                               .Include(x => x.Demands)
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.HealthCondition)
                               .Include(x => x.LessonData)
                               .Include(x => x.PlanData)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .OrderBy(x => x.StEpisode.Name)
                               .ThenBy(x => x.PlanData.Num)
                               .ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocation.LoCenter.Type == "women")
                               .ToList();

                return stus;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Student> GetAllStudentsWithTeacher(string teacher)
        {
            try
            {
                var stus = _context.Students
                               .Include(x => x.Evaluations)
                               .Include(x => x.ApplicationUser)
                               .Include(x => x.Nationality)
                               .Include(x => x.Demands)
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.HealthCondition)
                               .Include(x => x.LessonData)
                               .Include(x => x.PlanData)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .OrderBy(x => x.StEpisode.Name)
                               .ThenBy(x => x.PlanData.Num)
                               .ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.StEpisode.ApplicationUserId == teacher)
                               .ToList();

                return stus;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ApplicationUser? GetById(string id)
        {
            var te = _context.Users
                               .Include(x => x.Attendances)
                               .Include(x => x.Students)
                               .Include(x => x.Episodes)
                               .OrderBy(x => x.FullName)
                               .AsNoTracking()
                               .SingleOrDefault(g => g.Id == id);
            return te;
        }
        public void CheckEmptyEvaluations(List<Student> Students)
        {
            List<Evaluation> result = new List<Evaluation>();

            Evaluation? emptyEval = new();
            foreach (var stu in Students)
            {

                emptyEval = _context.Evaluations.Include(x => x.EvStudent)
                                    .Where(x => x.CurrentState > 0 && x.EvStudentId == stu.Id)
                                    .OrderBy(x => x.Ev_Date)
                                    .FirstOrDefault();

                if (emptyEval == null)
                {
                    emptyEval = new() { Id = Guid.NewGuid(), CurrentState = (int)Helper.eCurrentState.Active, Ev_Date = DateTime.Now.Date, Ev_Attend = 0, Ev_Rabt = -1, Ev_Rev = -1, Ev_Save = -1, EvStudent = stu };
                    _context.Evaluations.Add(emptyEval);
                    _context.SaveChanges();
                }
            }
        }

        public List<Evaluation> GetListEvaluationOfStudent(Guid stuId)
        {
            var listEvalOfStu = _context.Evaluations.Include(x => x.EvStudent).Where(x => x.EvStudentId == stuId).ToList();

            return listEvalOfStu;
        }
        public Dictionary<Student, Evaluation> GetStudentFristEvaluate(List<Student> students, List<Evaluation> Evals)
        {
            try
            {
                Dictionary<Student, Evaluation> stuEvaluate = new();
                Evaluation? fristEval = new();
                foreach (var student in students)
                {
                    fristEval = GetListEvaluationOfStudent(student.Id)
                            .Where(x => x.Ev_SaveSurahNameFrom != null)
                            .OrderByDescending(x => x.Ev_Date)
                            .FirstOrDefault();

                    if (fristEval == null)
                    {
                        fristEval = GetListEvaluationOfStudent(student.Id)
                            .OrderByDescending(x => x.Ev_Date)
                            .FirstOrDefault();
                    }

                    stuEvaluate.Add(student, fristEval);
                }

                return stuEvaluate;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, Evaluation> GetStudentLastEvaluate(List<Student> students, List<Evaluation> Evals)
        {
            try
            {
                Dictionary<Student, Evaluation> stuEvaluate = new();

                foreach (var student in students)
                {
                    Evaluation? lastEval = new Evaluation();

                    lastEval = GetListEvaluationOfStudent(student.Id)
                            .Where(x => x.Ev_SaveSurahNameTo != null)
                            .OrderByDescending(x => x.Ev_Date)
                            .FirstOrDefault();

                    if (lastEval == null)
                    {
                        lastEval = GetListEvaluationOfStudent(student.Id)
                            .OrderByDescending(x => x.Ev_Date)
                            .FirstOrDefault();
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

        public Dictionary<Student, int> GetCountAttend_0(List<Student> students, List<Evaluation> Evals)
        {
            try
            {
                Dictionary<Student, int> stuAtt = new();

                foreach (var student in students)
                {
                    int attend = 0;

                    attend = Evals
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id && x.Ev_Attend == 0 && x.Notes != null)
                    .ToList().Count;

                    if (attend == null)
                    {
                        attend = 0;
                    }
                    stuAtt.Add(student, attend);
                }

                return stuAtt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, int> GetCountAttend_1(List<Student> students, List<Evaluation> Evals)
        {
            try
            {
                Dictionary<Student, int> stuAtt = new();

                foreach (var student in students)
                {
                    int attend = 0;

                    attend = Evals
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id && x.Ev_Attend == 1)
                    .ToList().Count;

                    if (attend == null)
                    {
                        attend = 0;
                    }
                    stuAtt.Add(student, attend);
                }

                return stuAtt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, int> GetCountAttend_2(List<Student> students, List<Evaluation> Evals)
        {
            try
            {
                Dictionary<Student, int> stuAtt = new();

                foreach (var student in students)
                {
                    int attend = 0;

                    attend = Evals
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id && x.Ev_Attend == 2)
                    .ToList().Count;

                    if (attend == null)
                    {
                        attend = 0;
                    }
                    stuAtt.Add(student, attend);
                }

                return stuAtt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, int> GetCountAttend_3(List<Student> students, List<Evaluation> Evals)
        {
            try
            {
                Dictionary<Student, int> stuAtt = new();

                foreach (var student in students)
                {
                    int attend = 0;

                    attend = Evals
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id && x.Ev_Attend == 3)
                    .ToList().Count;

                    if (attend == null)
                    {
                        attend = 0;
                    }
                    stuAtt.Add(student, attend);
                }

                return stuAtt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, int> GetCountAttend_4(List<Student> students, List<Evaluation> Evals)
        {
            try
            {
                Dictionary<Student, int> stuAtt = new();

                foreach (var student in students)
                {
                    int attend = 0;

                    attend = Evals
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id && x.Ev_Attend == 4)
                    .ToList().Count;

                    if (attend == null)
                    {
                        attend = 0;
                    }
                    stuAtt.Add(student, attend);
                }

                return stuAtt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<Student, int> GetCountAttend_5(List<Student> students, List<Evaluation> Evals)
        {
            try
            {
                Dictionary<Student, int> stuAtt = new();

                foreach (var student in students)
                {
                    int attend = -1;

                    attend = Evals
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id && x.Ev_Save == -1 && x.Notes == null)
                    .ToList().Count;


                    stuAtt.Add(student, attend);
                }

                return stuAtt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Dictionary<Student, float?> GetTotalPages_Save(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv)
        {
            var SavePages = new Dictionary<Student, float?>();
            foreach (var student in FristEv.Keys)
            {
                var fristEval = FristEv.Values
                    .OrderBy(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var lastEval = LastEv.Values
                    .OrderByDescending(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var verseFristPage = FindVerseById(fristEval.Ev_SaveSurahIdFrom, fristEval.Ev_SaveAyahIdFrom);
                var verseLastPage = FindVerseById(lastEval.Ev_SaveSurahIdTo, lastEval.Ev_SaveAyahIdTo);

                if ((fristEval.Ev_SaveSurahIdFrom == null || fristEval.Ev_SaveAyahIdFrom == null) && (lastEval.Ev_SaveSurahIdTo == null || lastEval.Ev_SaveAyahIdTo == null))
                    SavePages.Add(student, null);



                else if (student.SaveDirection == "ctp" && (fristEval.Ev_SaveSurahIdFrom == null || fristEval.Ev_SaveAyahIdFrom == null) && (lastEval.Ev_SaveSurahIdTo != null && lastEval.Ev_SaveAyahIdTo != null))
                    SavePages.Add(student, verseLastPage.Pages_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_SaveSurahIdFrom == null || fristEval.Ev_SaveAyahIdFrom == null) && (lastEval.Ev_SaveSurahIdTo != null && lastEval.Ev_SaveAyahIdTo != null))
                    SavePages.Add(student, verseLastPage.Pages_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_SaveSurahIdFrom != null && fristEval.Ev_SaveAyahIdFrom != null) && (lastEval.Ev_SaveSurahIdTo == null || lastEval.Ev_SaveAyahIdTo == null))
                    SavePages.Add(student, verseFristPage.Pages_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_SaveSurahIdFrom != null && fristEval.Ev_SaveAyahIdFrom != null) && (lastEval.Ev_SaveSurahIdTo == null || lastEval.Ev_SaveAyahIdTo == null))
                    SavePages.Add(student, verseFristPage.Pages_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_SaveSurahIdFrom != null && fristEval.Ev_SaveAyahIdFrom != null) && (lastEval.Ev_SaveSurahIdTo != null && lastEval.Ev_SaveAyahIdTo != null))
                    SavePages.Add(student, Math.Abs((float)verseLastPage.Pages_ctp - (float)verseFristPage.Pages_ctp));

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_SaveSurahIdFrom != null && fristEval.Ev_SaveAyahIdFrom != null) && (lastEval.Ev_SaveSurahIdTo != null && lastEval.Ev_SaveAyahIdTo != null))
                    SavePages.Add(student, Math.Abs((float)verseFristPage.Pages_ptc - (float)verseLastPage.Pages_ptc));

            }
            return SavePages;
        }
        public Dictionary<Student, float?> GetTotalPages_Rev(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv)
        {
            var RevPages = new Dictionary<Student, float?>();
            foreach (var student in FristEv.Keys)
            {
                var fristEval = FristEv.Values
                    .OrderBy(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var lastEval = LastEv.Values
                    .OrderByDescending(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var verseFristPage = FindVerseById(fristEval.Ev_RevSurahIdFrom, fristEval.Ev_RevAyahIdFrom);
                var verseLastPage = FindVerseById(lastEval.Ev_RevSurahIdTo, lastEval.Ev_RevAyahIdTo);

                if ((fristEval.Ev_RevSurahIdFrom == null || fristEval.Ev_RevAyahIdFrom == null) && (lastEval.Ev_RevSurahIdTo == null || lastEval.Ev_RevAyahIdTo == null))
                    RevPages.Add(student, null);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RevSurahIdFrom == null || fristEval.Ev_RevAyahIdFrom == null) && (lastEval.Ev_RevSurahIdTo != null && lastEval.Ev_RevAyahIdTo != null))
                    RevPages.Add(student, verseLastPage.Pages_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RevSurahIdFrom == null || fristEval.Ev_RevAyahIdFrom == null) && (lastEval.Ev_RevSurahIdTo != null && lastEval.Ev_RevAyahIdTo != null))
                    RevPages.Add(student, verseLastPage.Pages_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RevSurahIdFrom != null && fristEval.Ev_RevAyahIdFrom != null) && (lastEval.Ev_RevSurahIdTo == null || lastEval.Ev_RevAyahIdTo == null))
                    RevPages.Add(student, verseFristPage.Pages_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RevSurahIdFrom != null && fristEval.Ev_RevAyahIdFrom != null) && (lastEval.Ev_RevSurahIdTo == null || lastEval.Ev_RevAyahIdTo == null))
                    RevPages.Add(student, verseFristPage.Pages_ptc - 0);


                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RevSurahIdFrom != null && fristEval.Ev_RevAyahIdFrom != null) && (lastEval.Ev_RevSurahIdTo != null && lastEval.Ev_RevAyahIdTo != null))
                    RevPages.Add(student, Math.Abs((float)verseLastPage.Pages_ctp - (float)verseFristPage.Pages_ctp));

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RevSurahIdFrom != null && fristEval.Ev_RevAyahIdFrom != null) && (lastEval.Ev_RevSurahIdTo != null && lastEval.Ev_RevAyahIdTo != null))
                    RevPages.Add(student, Math.Abs((float)verseFristPage.Pages_ptc - (float)verseLastPage.Pages_ptc));


            }
            return RevPages;
        }
        public Dictionary<Student, float?> GetTotalPages_Rabt(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv)
        {
            var RabtPages = new Dictionary<Student, float?>();
            foreach (var student in FristEv.Keys)
            {
                var fristEval = FristEv.Values
                    .OrderBy(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var lastEval = LastEv.Values
                    .OrderByDescending(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var verseFristPage = FindVerseById(fristEval.Ev_RabtSurahIdFrom, fristEval.Ev_RabtAyahIdFrom);
                var verseLastPage = FindVerseById(lastEval.Ev_RabtSurahIdTo, lastEval.Ev_RabtAyahIdTo);

                if ((fristEval.Ev_RabtSurahIdFrom == null || fristEval.Ev_RabtAyahIdFrom == null) && (lastEval.Ev_RabtSurahIdTo == null || lastEval.Ev_RabtAyahIdTo == null))
                    RabtPages.Add(student, null);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RabtSurahIdFrom == null || fristEval.Ev_RabtAyahIdFrom == null) && (lastEval.Ev_RabtSurahIdTo != null && lastEval.Ev_RabtAyahIdTo != null))
                    RabtPages.Add(student, verseLastPage.Pages_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RabtSurahIdFrom == null || fristEval.Ev_RabtAyahIdFrom == null) && (lastEval.Ev_RabtSurahIdTo != null && lastEval.Ev_RabtAyahIdTo != null))
                    RabtPages.Add(student, verseLastPage.Pages_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RabtSurahIdFrom != null && fristEval.Ev_RabtAyahIdFrom != null) && (lastEval.Ev_RabtSurahIdTo == null || lastEval.Ev_RabtAyahIdTo == null))
                    RabtPages.Add(student, verseFristPage.Pages_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RabtSurahIdFrom != null && fristEval.Ev_RabtAyahIdFrom != null) && (lastEval.Ev_RabtSurahIdTo == null || lastEval.Ev_RabtAyahIdTo == null))
                    RabtPages.Add(student, verseFristPage.Pages_ptc - 0);


                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RabtSurahIdFrom != null && fristEval.Ev_RabtAyahIdFrom != null) && (lastEval.Ev_RabtSurahIdTo != null && lastEval.Ev_RabtAyahIdTo != null))
                    RabtPages.Add(student, Math.Abs((float)verseLastPage.Pages_ctp - (float)verseFristPage.Pages_ctp));

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RabtSurahIdFrom != null && fristEval.Ev_RabtAyahIdFrom != null) && (lastEval.Ev_RabtSurahIdTo != null && lastEval.Ev_RabtAyahIdTo != null))
                    RabtPages.Add(student, Math.Abs((float)verseFristPage.Pages_ptc - (float)verseLastPage.Pages_ptc));


            }
            return RabtPages;
        }

        public Dictionary<Student, float?> GetTotalSections_Save(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv)
        {
            var SaveSections = new Dictionary<Student, float?>();
            foreach (var student in FristEv.Keys)
            {
                var fristEval = FristEv.Values
                    .OrderBy(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var lastEval = LastEv.Values
                    .OrderByDescending(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var verseFristSec = FindVerseById(fristEval.Ev_SaveSurahIdFrom, fristEval.Ev_SaveAyahIdFrom);
                var verseLastSec = FindVerseById(lastEval.Ev_SaveSurahIdTo, lastEval.Ev_SaveAyahIdTo);

                if ((fristEval.Ev_SaveSurahIdFrom == null || fristEval.Ev_SaveAyahIdFrom == null) && (lastEval.Ev_SaveSurahIdTo == null || lastEval.Ev_SaveAyahIdTo == null))
                    SaveSections.Add(student, null);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_SaveSurahIdFrom == null || fristEval.Ev_SaveAyahIdFrom == null) && (lastEval.Ev_SaveSurahIdTo != null && lastEval.Ev_SaveAyahIdTo != null))
                    SaveSections.Add(student, verseLastSec.Sections_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_SaveSurahIdFrom == null || fristEval.Ev_SaveAyahIdFrom == null) && (lastEval.Ev_SaveSurahIdTo != null && lastEval.Ev_SaveAyahIdTo != null))
                    SaveSections.Add(student, verseLastSec.Sections_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_SaveSurahIdFrom != null && fristEval.Ev_SaveAyahIdFrom != null) && (lastEval.Ev_SaveSurahIdTo == null || lastEval.Ev_SaveAyahIdTo != null))
                    SaveSections.Add(student, verseFristSec.Sections_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_SaveSurahIdFrom != null || fristEval.Ev_SaveAyahIdFrom != null) && (lastEval.Ev_SaveSurahIdTo == null || lastEval.Ev_SaveAyahIdTo != null))
                    SaveSections.Add(student, verseFristSec.Sections_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_SaveSurahIdFrom != null && fristEval.Ev_SaveAyahIdFrom != null) && (lastEval.Ev_SaveSurahIdTo != null && lastEval.Ev_SaveAyahIdTo != null))
                    SaveSections.Add(student, Math.Abs((float)verseLastSec.Sections_ctp - (float)verseFristSec.Sections_ctp));

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_SaveSurahIdFrom != null && fristEval.Ev_SaveAyahIdFrom != null) && (lastEval.Ev_SaveSurahIdTo != null && lastEval.Ev_SaveAyahIdTo != null))
                    SaveSections.Add(student, Math.Abs((float)verseFristSec.Sections_ptc - (float)verseLastSec.Sections_ptc));


            }
            return SaveSections;
        }
        public Dictionary<Student, float?> GetTotalSections_Rev(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv)
        {
            var RevSections = new Dictionary<Student, float?>();
            foreach (var student in FristEv.Keys)
            {
                var fristEval = FristEv.Values
                    .OrderBy(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var lastEval = LastEv.Values
                    .OrderByDescending(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var verseFristSec = FindVerseById(fristEval.Ev_RevSurahIdFrom, fristEval.Ev_RevAyahIdFrom);
                var verseLastSec = FindVerseById(lastEval.Ev_RevSurahIdTo, lastEval.Ev_RevAyahIdTo);

                if ((fristEval.Ev_RevSurahIdFrom == null || fristEval.Ev_RevAyahIdFrom == null) && (lastEval.Ev_RevSurahIdTo == null || lastEval.Ev_RevAyahIdTo == null))
                    RevSections.Add(student, null);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RevSurahIdFrom == null || fristEval.Ev_RevAyahIdFrom == null) && (lastEval.Ev_RevSurahIdTo != null && lastEval.Ev_RevAyahIdTo != null))
                    RevSections.Add(student, verseLastSec.Sections_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RevSurahIdFrom == null || fristEval.Ev_RevAyahIdFrom == null) && (lastEval.Ev_RevSurahIdTo != null && lastEval.Ev_RevAyahIdTo != null))
                    RevSections.Add(student, verseLastSec.Sections_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RevSurahIdFrom != null && fristEval.Ev_RevAyahIdFrom != null) && (lastEval.Ev_RevSurahIdTo == null || lastEval.Ev_RevAyahIdTo == null))
                    RevSections.Add(student, verseFristSec.Sections_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RevSurahIdFrom != null && fristEval.Ev_RevAyahIdFrom != null) && (lastEval.Ev_RevSurahIdTo == null || lastEval.Ev_RevAyahIdTo == null))
                    RevSections.Add(student, verseFristSec.Sections_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RevSurahIdFrom != null && fristEval.Ev_RevAyahIdFrom != null) && (lastEval.Ev_RevSurahIdTo != null && lastEval.Ev_RevAyahIdTo != null))
                    RevSections.Add(student, Math.Abs((float)verseLastSec.Sections_ctp - (float)verseFristSec.Sections_ctp));

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RevSurahIdFrom != null && fristEval.Ev_RevAyahIdFrom != null) && (lastEval.Ev_RevSurahIdTo != null && lastEval.Ev_RevAyahIdTo != null))
                    RevSections.Add(student, Math.Abs((float)verseFristSec.Sections_ptc - (float)verseLastSec.Sections_ptc));
            }
            return RevSections;
        }
        public Dictionary<Student, float?> GetTotalSections_Rabt(Dictionary<Student, Evaluation> FristEv, Dictionary<Student, Evaluation> LastEv)
        {
            var RabtSections = new Dictionary<Student, float?>();
            foreach (var student in FristEv.Keys)
            {
                var fristEval = FristEv.Values
                    .OrderBy(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var lastEval = LastEv.Values
                    .OrderByDescending(x => x.Ev_Date)
                    .Where(x => x.CurrentState > 0 && x.EvStudentId == student.Id)
                    .FirstOrDefault();

                var verseFristSec = FindVerseById(fristEval.Ev_RabtSurahIdFrom, fristEval.Ev_RabtAyahIdFrom);
                var verseLastSec = FindVerseById(lastEval.Ev_RabtSurahIdTo, lastEval.Ev_RabtAyahIdTo);

                if ((fristEval.Ev_RabtSurahIdFrom == null || fristEval.Ev_RabtAyahIdFrom == null) && (lastEval.Ev_RabtSurahIdTo == null || lastEval.Ev_RabtAyahIdTo == null))
                    RabtSections.Add(student, null);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RabtSurahIdFrom == null || fristEval.Ev_RabtAyahIdFrom == null) && (lastEval.Ev_RabtSurahIdTo != null && lastEval.Ev_RabtAyahIdTo != null))
                    RabtSections.Add(student, verseLastSec.Sections_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RabtSurahIdFrom == null || fristEval.Ev_RabtAyahIdFrom == null) && (lastEval.Ev_RabtSurahIdTo != null && lastEval.Ev_RabtAyahIdTo != null))
                    RabtSections.Add(student, verseLastSec.Sections_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RabtSurahIdFrom != null && fristEval.Ev_RabtAyahIdFrom != null) && (lastEval.Ev_RabtSurahIdTo == null || lastEval.Ev_RabtAyahIdTo == null))
                    RabtSections.Add(student, verseFristSec.Sections_ctp - 0);

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RabtSurahIdFrom != null && fristEval.Ev_RabtAyahIdFrom != null) && (lastEval.Ev_RabtSurahIdTo == null || lastEval.Ev_RabtAyahIdTo == null))
                    RabtSections.Add(student, verseFristSec.Sections_ptc - 0);

                else if (student.SaveDirection == "ctp" && (fristEval.Ev_RabtSurahIdFrom != null && fristEval.Ev_RabtAyahIdFrom != null) && (lastEval.Ev_RabtSurahIdTo != null && lastEval.Ev_RabtAyahIdTo != null))
                    RabtSections.Add(student, Math.Abs((float)verseLastSec.Sections_ctp - (float)verseFristSec.Sections_ctp));

                else if (student.SaveDirection == "ptc" && (fristEval.Ev_RabtSurahIdFrom != null && fristEval.Ev_RabtAyahIdFrom != null) && (lastEval.Ev_RabtSurahIdTo != null && lastEval.Ev_RabtAyahIdTo != null))
                    RabtSections.Add(student, Math.Abs((float)verseFristSec.Sections_ptc - (float)verseLastSec.Sections_ptc));


            }
            return RabtSections;
        }


        // Last Lesson
        //public Dictionary<Student, Evaluation> GetStudentLastLesson(List<Student> students, List<Evaluation> Evals)
        //{
        //    try
        //    {
        //        Dictionary<Student, Evaluation> stuEvaluate = new();

        //        foreach (var student in students)
        //        {
        //            Evaluation? lastEval = new Evaluation();

        //            lastEval = GetListEvaluationOfStudent(student.Id)
        //                    .OrderByDescending(x => x.Ev_Date)
        //                    .FirstOrDefault();

        //            stuEvaluate.Add(student, lastEval);
        //        }

        //        return stuEvaluate;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}


    }
}
