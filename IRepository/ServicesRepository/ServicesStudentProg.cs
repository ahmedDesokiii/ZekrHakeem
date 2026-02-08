

namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesStudentProg : IServicesRepository<StudentProg>, IServicesStudentProg
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public ServicesStudentProg(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        #region All StudentProgs
        public List<StudentProg> GetAll()
        {
            try
            {
                return _context.StudentProgs
                               .Include(st => st.EpisodeProg)
                               .Include(x => x.Nationality)
                               .Where(x => x.CurrentState > 0)
                               .OrderBy(x => x.Name)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  StudentProgs Men
        public List<StudentProg> GetStudentProgsForMen()
        {
            try
            {
                return _context.StudentProgs
                               .Include(st => st.EpisodeProg)
                               .Include(x => x.Nationality)
                               .Where(x => x.CurrentState > 0 && x.EpisodeProg.EpProgLocation.LoCenter.Type == "men")
                               .OrderBy(x => x.Name)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  StudentProgs Women
        public List<StudentProg> GetStudentProgsForWomen()
        {
            try
            {
                return _context.StudentProgs
                                .Include(st => st.EpisodeProg)
                                .Include(x => x.Nationality)
                               .Where(x => x.CurrentState > 0 && x.EpisodeProg.EpProgLocation.LoCenter.Type == "women")
                               .OrderBy(x => x.Name)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  StudentProgs With Supervisor
        public List<StudentProg> GetStudentProgsWithSupervisor(string userid)
        {
            try
            {
                return _context.StudentProgs
                            .Include(st => st.EpisodeProg)
                            .Include(x => x.Nationality)
                            .Where(x => x.CurrentState > 0 && x.EpisodeProg.EpProgLocation.LoCenter.ApplicationUserId == userid)
                            .OrderBy(x => x.Name)
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  StudentProgs With Teacher
        public List<StudentProg> GetStudentProgsWithTeacher(string userid)
        {
            try
            {
                return _context.StudentProgs
                            .Include(st => st.EpisodeProg)
                            .Include(x => x.Nationality)
                            .Where(x => x.CurrentState > 0 && x.EpisodeProg.ApplicationUserId == userid)
                            .OrderBy(x => x.Name)
                            .ToList();


            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  StudentProgs With Parent
        public List<StudentProg> GetStudentProgsWithParent(ApplicationUser user)
        {
            try
            {
                return _context.StudentProgs
                           .Include(st => st.EpisodeProg)
                           .Include(x => x.Nationality)
                        .Where(x => x.CurrentState > 0 && x.ParentPhoneNumber == user.PhoneNumber)
                        .OrderBy(x => x.Name)
                           .ToList();

            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Find student by ...
        public StudentProg FindBy(Guid Id)
        {
            try
            {
                return _context.StudentProgs.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public StudentProg FindBy(string Name)
        {
            try
            {
                return _context.StudentProgs.FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool FindByLimit(int episodeLimit, int episodeStudentProgs)
        {
            try
            {
                if (episodeLimit > episodeStudentProgs)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public LessonData FindLessonById(int? Id)
        {
            try
            {
                return _context.LessonsData.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
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
                return _context.Surahs.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Surah FindSurahById(string? Name)
        {
            try
            {
                return _context.Surahs.FirstOrDefault(x => x.Name.Equals(Name) && x.CurrentState > 0);
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
        public Verse FindVerseById(string? surahId, int? verseNum)
        {
            try
            {
                return _context.Verses.FirstOrDefault(x => x.Surah.Name.Equals(surahId) && x.Num == verseNum && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Create & Update StudentProg
        public bool Save(StudentProg model)
        {
            //try
            //{
            //    var maxLesson = _context.LessonsData.Count();
            //    var result = FindBy(model.Id);

            //    var resultLesson = FindLessonById(model.LessonDataId);


            //    if (result == null) // create new student
            //    {
            //        model.Id = Guid.NewGuid();
            //        model.CurrentState = (int)Helper.eCurrentState.Active;

            //        if (model.PlanDataId == Guid.Parse("5b35a4de-5100-4f4b-b065-527f04ae1472")) // الخطة العامة
            //        {
            //            resultLesson = FindLessonById(model.LessonDataId);
            //            if (resultLesson == null)
            //            {
            //                resultLesson = new LessonData() { Id = maxLesson + 1 };
            //                resultLesson.LevelDataId = Guid.Parse("00000000-0000-0000-0000-000000000101");
            //                resultLesson.CurrentState = (int)Helper.eCurrentState.Active;
            //            }
            //            var resultSurah = FindSurahById(model.SurahId);
            //            var resultVerse = FindVerseById(model.SurahId, model.Verse.Num);
            //            model.VerseId = resultVerse.Id;
            //            model.Verse = resultVerse;
            //            model.SurahId = resultSurah.Id;
            //            model.Surah = resultSurah;

            //            //Save
            //            resultLesson.Save_FromSorh = resultSurah.Name;
            //            resultLesson.Save_FromAyah = model.Verse.Num + 1;
            //            resultLesson.Save_ToSorh = resultSurah.Name;
            //            resultLesson.Save_ToAyah = resultVerse.Num + 1;
            //            resultLesson.Save_Satr = 0;
            //            //Rabt
            //            resultLesson.Rabt_FromSorh = resultSurah.Name;
            //            resultLesson.Rabt_FromAyah = resultVerse.Num;
            //            resultLesson.Rabt_ToSorh = resultSurah.Name;
            //            resultLesson.Rabt_ToAyah = resultVerse.Num;
            //            resultLesson.Rabt_Satr = 0;
            //            //Rev
            //            resultLesson.Rev_FromSorh = resultSurah.Name;
            //            resultLesson.Rev_FromAyah = resultVerse.Num;
            //            resultLesson.Rev_ToSorh = resultSurah.Name;
            //            resultLesson.Rev_ToAyah = resultVerse.Num;
            //            resultLesson.Rev_Satr = 0;
            //            //PrevSave
            //            resultLesson.PrevSave_FromSorh = resultSurah.Name;
            //            resultLesson.PrevSave_FromAyah = resultVerse.Num;
            //            resultLesson.PrevSave_ToSorh = resultSurah.Name;
            //            resultLesson.PrevSave_ToAyah = resultVerse.Num;
            //            //PrevRev
            //            resultLesson.PrevRev_FromSorh = resultSurah.Name;
            //            resultLesson.PrevRev_FromAyah = resultVerse.Num;
            //            resultLesson.PrevRev_ToSorh = resultSurah.Name;
            //            resultLesson.PrevRev_ToAyah = resultVerse.Num;

            //            _context.LessonsData.Add(resultLesson);
            //            model.LessonDataId = resultLesson.Id;
            //        }
            //        else if (model.PlanDataId == Guid.Parse("934f4260-39d3-41ce-afb2-e4ae4f8474eb")) // الخطة المفتوحة
            //        {
            //            resultLesson = FindLessonById(model.LessonDataId);
            //            if (resultLesson == null)
            //            {
            //                resultLesson = new LessonData() { Id = maxLesson + 1 };
            //                resultLesson.LevelDataId = Guid.Parse("00000000-0000-0000-0000-000000000201");
            //                resultLesson.CurrentState = (int)Helper.eCurrentState.Active;
            //            }
            //            var resultSurah = FindSurahById(model.SurahId);
            //            var resultVerse = FindVerseById(model.SurahId, model.Verse.Num);
            //            model.VerseId = resultVerse.Id;
            //            model.Verse = resultVerse;
            //            model.SurahId = resultSurah.Id;
            //            model.Surah = resultSurah;

            //            //Save
            //            resultLesson.Save_FromSorh = resultSurah.Name;
            //            resultLesson.Save_FromAyah = model.Verse.Num + 1;
            //            resultLesson.Save_ToSorh = resultSurah.Name;
            //            resultLesson.Save_ToAyah = resultVerse.Num + 1;
            //            resultLesson.Save_Satr = 0;
            //            //Rabt
            //            resultLesson.Rabt_FromSorh = resultSurah.Name;
            //            resultLesson.Rabt_FromAyah = resultVerse.Num;
            //            resultLesson.Rabt_ToSorh = resultSurah.Name;
            //            resultLesson.Rabt_ToAyah = resultVerse.Num;
            //            resultLesson.Rabt_Satr = 0;
            //            //Rev
            //            resultLesson.Rev_FromSorh = resultSurah.Name;
            //            resultLesson.Rev_FromAyah = resultVerse.Num;
            //            resultLesson.Rev_ToSorh = resultSurah.Name;
            //            resultLesson.Rev_ToAyah = resultVerse.Num;
            //            resultLesson.Rev_Satr = 0;
            //            //PrevSave
            //            resultLesson.PrevSave_FromSorh = resultSurah.Name;
            //            resultLesson.PrevSave_FromAyah = resultVerse.Num;
            //            resultLesson.PrevSave_ToSorh = resultSurah.Name;
            //            resultLesson.PrevSave_ToAyah = resultVerse.Num;
            //            //PrevRev
            //            resultLesson.PrevRev_FromSorh = resultSurah.Name;
            //            resultLesson.PrevRev_FromAyah = resultVerse.Num;
            //            resultLesson.PrevRev_ToSorh = resultSurah.Name;
            //            resultLesson.PrevRev_ToAyah = resultVerse.Num;

            //            _context.LessonsData.Add(resultLesson);
            //            model.LessonDataId = resultLesson.Id;
            //        }
            //        else
            //        {
            //            resultLesson = FindLessonById(model.LessonDataId);

            //            model.Verse = FindVerseById(resultLesson.Save_FromSorh, resultLesson.Save_FromAyah);
            //            model.VerseId = model.Verse.Id;
            //            model.Surah = FindSurahById(resultLesson.Save_FromSorh);
            //            model.SurahId = model.Surah.Id;
            //        }
            //        _context.StudentProgs.Add(model);
            //    }
            //    else //update student data
            //    {
            //        if (model.PlanDataId == Guid.Parse("5b35a4de-5100-4f4b-b065-527f04ae1472")) // الخطة العامة
            //        {
            //            //resultLesson = FindLessonById(result.LessonDataId);

            //            if (resultLesson == null)
            //            {
            //                resultLesson = new LessonData() { Id = maxLesson + 1 };
            //                resultLesson.LevelDataId = Guid.Parse("00000000-0000-0000-0000-000000000101");
            //                resultLesson.CurrentState = (int)Helper.eCurrentState.Active;
            //            }

            //            var resultSurah = FindSurahById(model.SurahId);
            //            var resultVerse = FindVerseById(model.SurahId, model.Verse.Num);
            //            model.VerseId = resultVerse.Id;
            //            model.Verse = resultVerse;
            //            model.SurahId = resultSurah.Id;
            //            model.Surah = resultSurah;

            //            //Save
            //            resultLesson.Save_FromSorh = resultSurah.Name;
            //            resultLesson.Save_FromAyah = model.Verse.Num + 1;
            //            resultLesson.Save_ToSorh = resultSurah.Name;
            //            resultLesson.Save_ToAyah = resultVerse.Num + 1;
            //            resultLesson.Save_Satr = 0;
            //            //Rabt
            //            resultLesson.Rabt_FromSorh = resultSurah.Name;
            //            resultLesson.Rabt_FromAyah = resultVerse.Num;
            //            resultLesson.Rabt_ToSorh = resultSurah.Name;
            //            resultLesson.Rabt_ToAyah = resultVerse.Num;
            //            resultLesson.Rabt_Satr = 0;
            //            //Rev
            //            resultLesson.Rev_FromSorh = resultSurah.Name;
            //            resultLesson.Rev_FromAyah = resultVerse.Num;
            //            resultLesson.Rev_ToSorh = resultSurah.Name;
            //            resultLesson.Rev_ToAyah = resultVerse.Num;
            //            resultLesson.Rev_Satr = 0;
            //            //PrevSave
            //            resultLesson.PrevSave_FromSorh = resultSurah.Name;
            //            resultLesson.PrevSave_FromAyah = resultVerse.Num;
            //            resultLesson.PrevSave_ToSorh = resultSurah.Name;
            //            resultLesson.PrevSave_ToAyah = resultVerse.Num;
            //            //PrevRev
            //            resultLesson.PrevRev_FromSorh = resultSurah.Name;
            //            resultLesson.PrevRev_FromAyah = resultVerse.Num;
            //            resultLesson.PrevRev_ToSorh = resultSurah.Name;
            //            resultLesson.PrevRev_ToAyah = resultVerse.Num;
            //            _context.LessonsData.Add(resultLesson);
            //            result.LessonDataId = resultLesson.Id;

            //        }
            //        else if (model.PlanDataId == Guid.Parse("934f4260-39d3-41ce-afb2-e4ae4f8474eb")) // الخطة المفتوحة
            //        {
            //            //resultLesson = FindLessonById(result.LessonDataId);

            //            if (resultLesson == null)
            //            {
            //                resultLesson = new LessonData() { Id = maxLesson + 1 };
            //                resultLesson.LevelDataId = Guid.Parse("00000000-0000-0000-0000-000000000201");
            //                resultLesson.CurrentState = (int)Helper.eCurrentState.Active;
            //            }

            //            var resultSurah = FindSurahById(model.SurahId);
            //            var resultVerse = FindVerseById(model.SurahId, model.Verse.Num);
            //            model.VerseId = resultVerse.Id;
            //            model.Verse = resultVerse;
            //            model.SurahId = resultSurah.Id;
            //            model.Surah = resultSurah;

            //            //Save
            //            resultLesson.Save_FromSorh = resultSurah.Name;
            //            resultLesson.Save_FromAyah = model.Verse.Num + 1;
            //            resultLesson.Save_ToSorh = resultSurah.Name;
            //            resultLesson.Save_ToAyah = resultVerse.Num + 1;
            //            resultLesson.Save_Satr = 0;
            //            //Rabt
            //            resultLesson.Rabt_FromSorh = resultSurah.Name;
            //            resultLesson.Rabt_FromAyah = resultVerse.Num;
            //            resultLesson.Rabt_ToSorh = resultSurah.Name;
            //            resultLesson.Rabt_ToAyah = resultVerse.Num;
            //            resultLesson.Rabt_Satr = 0;
            //            //Rev
            //            resultLesson.Rev_FromSorh = resultSurah.Name;
            //            resultLesson.Rev_FromAyah = resultVerse.Num;
            //            resultLesson.Rev_ToSorh = resultSurah.Name;
            //            resultLesson.Rev_ToAyah = resultVerse.Num;
            //            resultLesson.Rev_Satr = 0;
            //            //PrevSave
            //            resultLesson.PrevSave_FromSorh = resultSurah.Name;
            //            resultLesson.PrevSave_FromAyah = resultVerse.Num;
            //            resultLesson.PrevSave_ToSorh = resultSurah.Name;
            //            resultLesson.PrevSave_ToAyah = resultVerse.Num;
            //            //PrevRev
            //            resultLesson.PrevRev_FromSorh = resultSurah.Name;
            //            resultLesson.PrevRev_FromAyah = resultVerse.Num;
            //            resultLesson.PrevRev_ToSorh = resultSurah.Name;
            //            resultLesson.PrevRev_ToAyah = resultVerse.Num;
            //            _context.LessonsData.Add(resultLesson);
            //            result.LessonDataId = resultLesson.Id;

            //        }
            //        else // الذكر الحكيم
            //        {
            //            resultLesson = FindLessonById(model.LessonDataId);
            //            model.Verse = FindVerseById(resultLesson.Save_FromSorh, resultLesson.Save_FromAyah);
            //            model.VerseId = model.Verse.Id;
            //            model.Surah = FindSurahById(resultLesson.Save_FromSorh);
            //            model.SurahId = model.Surah.Id;
            //            result.LessonDataId = model.LessonDataId;
            //        }

            //        result.Name = model.Name;
            //        result.IdentityNumber = model.IdentityNumber;
            //        result.Gender = model.Gender;
            //        result.Adress = model.Adress;
            //        result.Notes = model.Notes;
            //        result.StEpisodeProgId = model.StEpisodeProgId;
            //        result.CurrentState = (int)Helper.eCurrentState.Active;
            //        result.newLessonState = false;
            //        result.ApplicationUserId = null;
            //        result.Category = model.Category;
            //        result.NationalityId = model.NationalityId;
            //        result.HealthConditionId = model.HealthConditionId;
            //        result.BirthDate = model.BirthDate; // ??
            //        result.EducationType = model.EducationType;
            //        result.ParentIdentityNumber = model.ParentIdentityNumber;
            //        result.ParentPhoneNumber = model.ParentPhoneNumber;
            //        result.ProfilePicture = model.ProfilePicture;
            //        result.VerseId = model.VerseId;
            //        result.SurahId = model.SurahId;
            //        result.PlanDataId = model.PlanDataId;
            //        result.SaveDirection = model.SaveDirection;//??

            //        _context.StudentProgs.Update(result);
            //    }
            //    _context.SaveChanges();
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return false;
        }
        #endregion

        #region Delete StudentProg
        public bool Delete(Guid Id)
        {
            try
            {
                var isDeleted = false;
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.StudentProgs.Update(result);

                var effectedRows = _context.SaveChanges();

                return isDeleted;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region StudentProg Details & Evaluations
        public StudentProg? GetById(Guid id)
        {
            return _context.StudentProgs
                //.Include(t => t.Evaluations)
                //.Include(t => t.Surah)
                //.Include(t => t.Demands)
                .Include(st => st.EpisodeProg)
                .Include(st => st.EpisodeProg.EpProgLocation)
                .Include(x => x.EpisodeProg.EpProgLocation.LoCenter)
                //.Include(st => st.LessonData)
                //.Include(st => st.LessonData.LevelData)
                //.Include(st => st.LessonData.LevelData.PlanData)
                .Include(st => st.Nationality)
                //.Include(st => st.HealthCondition)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }
        //public List<Evaluation> GetEvaluationsForStudentProg(Guid stId)
        //{
        //    try
        //    {
        //        List<Evaluation> evaluations = new();
        //        evaluations = _context.Evaluations
        //                            .Include(x => x.EvStudentProg)
        //                            .OrderByDescending(x => x.Ev_Date)
        //                            .Where(x => x.CurrentState > 0 && x.EvStudentProgId == stId)
        //                            .ToList();

        //        return evaluations;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public List<Demand> GetDemandsForStudentProg(Guid stid)
        //{
        //    try
        //    {
        //        List<Demand> demands = new();

        //        demands = _context.Demands
        //                            .Include(x => x.StudentProg)
        //                            .OrderBy(x => x.Request_Date)
        //                            .Where(x => x.StudentProgId == stid)
        //                            .ToList();
        //        return demands;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        #endregion



    }
}
