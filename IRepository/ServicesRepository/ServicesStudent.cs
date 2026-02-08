

namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesStudent : IServicesRepository<Student>, IServicesStudent
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public ServicesStudent(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        #region All Students
        public List<Student> GetAll()
        {
            try
            {
                return _context.Students
                               .Include(x => x.Demands)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.Nationality)
                               .Include(st => st.StEpisode.EpLocation)
                               .Include(x => x.StEpisode.EpLocation.LoCenter)
                               .Include(x => x.Evaluations)
                               .Include(x => x.LessonData)
                               .Include(x => x.LessonData.LevelData)
                               .Include(x => x.LessonData.LevelData.PlanData)
                               .OrderBy(x => x.PlanDataId)
                               .ThenBy(x => x.StEpisode.Name).ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  Students Men
        public List<Student> GetStudentsForMen()
        {
            try
            {
                return _context.Students
                               .Include(x => x.Demands)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .Include(x => x.Nationality)
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.Evaluations)
                               .Include(x => x.LessonData)
                               .Include(x => x.LessonData.LevelData)
                               .Include(x => x.LessonData.LevelData.PlanData)
                                .OrderBy(x => x.PlanDataId)
                               .ThenBy(x => x.StEpisode.Name).ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocation.LoCenter.Type == "men")
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  Students Women
        public List<Student> GetStudentsForWomen()
        {
            try
            {
                return _context.Students
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .Include(x => x.Demands)
                               .Include(x => x.Nationality)
                               .Include(x => x.Evaluations)
                               .Include(x => x.LessonData)
                               .Include(x => x.LessonData.LevelData)
                               .Include(x => x.LessonData.LevelData.PlanData)
                                .OrderBy(x => x.PlanDataId)
                               .ThenBy(x => x.StEpisode.Name).ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocation.LoCenter.Type == "women")
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  Students With Supervisor
        public List<Student> GetStudentsWithSupervisor(string userid)
        {
            try
            {
                return _context.Students
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .Include(x => x.Demands)
                               .Include(x => x.Nationality)
                               .Include(x => x.Evaluations)
                               .Include(x => x.LessonData)
                               .Include(x => x.LessonData.LevelData)
                               .Include(x => x.LessonData.LevelData.PlanData)
                                .OrderBy(x => x.PlanDataId)
                               .ThenBy(x => x.StEpisode.Name).ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocation.LoCenter.ApplicationUserId == userid)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  Students With Teacher
        public List<Student> GetStudentsWithTeacher(string userid)
        {
            try
            {
                return _context.Students
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .Include(x => x.Demands)
                               .Include(x => x.Nationality)
                               .Include(x => x.Evaluations)
                               .Include(x => x.LessonData)
                               .Include(x => x.LessonData.LevelData)
                               .Include(x => x.LessonData.LevelData.PlanData)
                                .OrderBy(x => x.PlanDataId)
                               .ThenBy(x => x.StEpisode.Name).ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.StEpisode.ApplicationUserId == userid)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region  Students With Parent
        public List<Student> GetStudentsWithParent(ApplicationUser user)
        {
            try
            {
                return _context.Students
                               .Include(x => x.StEpisode)
                               .Include(x => x.StEpisode.ApplicationUser)
                               .Include(x => x.Surah)
                               .Include(x => x.Verse)
                               .Include(x => x.Demands)
                               .Include(x => x.Nationality)
                               .Include(x => x.Evaluations)
                               .Include(x => x.LessonData)
                               .Include(x => x.LessonData.LevelData)
                               .Include(x => x.LessonData.LevelData.PlanData)
                               .OrderBy(x => x.PlanDataId)
                               .ThenBy(x => x.StEpisode.Name).ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.ParentIdentityNumber == user.IdentityNum)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Find student by ...
        public Student FindBy(Guid Id)
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
        public Student FindBy(string Name)
        {
            try
            {
                return _context.Students.FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Episode FindEpisodetBy(Guid epId) => _context.Episodes.FirstOrDefault(x => x.Id.Equals(epId));

        public bool FindByLimit(int episodeLimit, int episodeStudents)
        {
            try
            {
                if (episodeLimit > episodeStudents)
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
        #region Assigned Student To Episode
        public bool Assigned(Student model)
        {
            try
            {
                var result = FindBy(model.Id);
                result.StEpisodeId = model.StEpisodeId;
                _context.Students.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Create & Update Student
        public bool WaitEpisodes(Student model)
        {
            if (model.StEpisodeId == Guid.Parse("ad8a4ead-a5c8-4284-a22a-bf2c6659b9b2"))
            { // حلقة انتظار رجال 
                return true;
            }
            else if (model.StEpisodeId == Guid.Parse("f0f94024-cf59-4fc1-b800-a37511e97ac1"))
            { // حلقة انتظار نساء 
                return true;
            }
            else { return false; }
        }
        public bool Save(Student model)
        {
            try
            {
                var maxLesson = _context.LessonsData.Count();
                var result = FindBy(model.Id);
                var resultLesson = FindLessonById(model.LessonDataId);
                if (result == null) // create new student
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;

                    if (model.PlanDataId == Guid.Parse("5b35a4de-5100-4f4b-b065-527f04ae1472")) // الخطة العامة
                    {
                        resultLesson = FindLessonById(model.LessonDataId);
                        if (resultLesson == null)
                        {
                            resultLesson = new LessonData() { Id = maxLesson + 1 };
                            resultLesson.LevelDataId = Guid.Parse("00000000-0000-0000-0000-000000000101");
                            resultLesson.CurrentState = (int)Helper.eCurrentState.Active;
                        }
                        var resultSurah = FindSurahById(model.SurahId);
                        var resultVerse = FindVerseById(model.SurahId, model.Verse.Num);
                        model.VerseId = resultVerse.Id;
                        model.Verse = resultVerse;
                        model.SurahId = resultSurah.Id;
                        model.Surah = resultSurah;

                        //Save
                        resultLesson.Save_FromSorh = resultSurah.Name;
                        resultLesson.Save_FromAyah = model.Verse.Num + 1;
                        resultLesson.Save_ToSorh = resultSurah.Name;
                        resultLesson.Save_ToAyah = resultVerse.Num + 1;
                        resultLesson.Save_Satr = 0;
                        //Rabt
                        resultLesson.Rabt_FromSorh = resultSurah.Name;
                        resultLesson.Rabt_FromAyah = resultVerse.Num;
                        resultLesson.Rabt_ToSorh = resultSurah.Name;
                        resultLesson.Rabt_ToAyah = resultVerse.Num;
                        resultLesson.Rabt_Satr = 0;
                        //Rev
                        resultLesson.Rev_FromSorh = resultSurah.Name;
                        resultLesson.Rev_FromAyah = resultVerse.Num;
                        resultLesson.Rev_ToSorh = resultSurah.Name;
                        resultLesson.Rev_ToAyah = resultVerse.Num;
                        resultLesson.Rev_Satr = 0;
                        //PrevSave
                        resultLesson.PrevSave_FromSorh = resultSurah.Name;
                        resultLesson.PrevSave_FromAyah = resultVerse.Num;
                        resultLesson.PrevSave_ToSorh = resultSurah.Name;
                        resultLesson.PrevSave_ToAyah = resultVerse.Num;
                        //PrevRev
                        resultLesson.PrevRev_FromSorh = resultSurah.Name;
                        resultLesson.PrevRev_FromAyah = resultVerse.Num;
                        resultLesson.PrevRev_ToSorh = resultSurah.Name;
                        resultLesson.PrevRev_ToAyah = resultVerse.Num;

                        _context.LessonsData.Add(resultLesson);
                        model.LessonDataId = resultLesson.Id;
                    }
                    else if (model.PlanDataId == Guid.Parse("934f4260-39d3-41ce-afb2-e4ae4f8474eb")) // الخطة المفتوحة
                    {
                        resultLesson = FindLessonById(model.LessonDataId);
                        if (resultLesson == null)
                        {
                            resultLesson = new LessonData() { Id = maxLesson + 1 };
                            resultLesson.LevelDataId = Guid.Parse("00000000-0000-0000-0000-000000000201");
                            resultLesson.CurrentState = (int)Helper.eCurrentState.Active;
                        }
                        var resultSurah = FindSurahById(model.SurahId);
                        var resultVerse = FindVerseById(model.SurahId, model.Verse.Num);
                        model.VerseId = resultVerse.Id;
                        model.Verse = resultVerse;
                        model.SurahId = resultSurah.Id;
                        model.Surah = resultSurah;

                        //Save
                        resultLesson.Save_FromSorh = resultSurah.Name;
                        resultLesson.Save_FromAyah = model.Verse.Num + 1;
                        resultLesson.Save_ToSorh = resultSurah.Name;
                        resultLesson.Save_ToAyah = resultVerse.Num + 1;
                        resultLesson.Save_Satr = 0;
                        //Rabt
                        resultLesson.Rabt_FromSorh = resultSurah.Name;
                        resultLesson.Rabt_FromAyah = resultVerse.Num;
                        resultLesson.Rabt_ToSorh = resultSurah.Name;
                        resultLesson.Rabt_ToAyah = resultVerse.Num;
                        resultLesson.Rabt_Satr = 0;
                        //Rev
                        resultLesson.Rev_FromSorh = resultSurah.Name;
                        resultLesson.Rev_FromAyah = resultVerse.Num;
                        resultLesson.Rev_ToSorh = resultSurah.Name;
                        resultLesson.Rev_ToAyah = resultVerse.Num;
                        resultLesson.Rev_Satr = 0;
                        //PrevSave
                        resultLesson.PrevSave_FromSorh = resultSurah.Name;
                        resultLesson.PrevSave_FromAyah = resultVerse.Num;
                        resultLesson.PrevSave_ToSorh = resultSurah.Name;
                        resultLesson.PrevSave_ToAyah = resultVerse.Num;
                        //PrevRev
                        resultLesson.PrevRev_FromSorh = resultSurah.Name;
                        resultLesson.PrevRev_FromAyah = resultVerse.Num;
                        resultLesson.PrevRev_ToSorh = resultSurah.Name;
                        resultLesson.PrevRev_ToAyah = resultVerse.Num;

                        _context.LessonsData.Add(resultLesson);
                        model.LessonDataId = resultLesson.Id;
                    }
                    else
                    {
                        resultLesson = FindLessonById(model.LessonDataId);

                        model.Verse = FindVerseById(resultLesson.Save_FromSorh, resultLesson.Save_FromAyah);
                        model.VerseId = model.Verse.Id;
                        model.Surah = FindSurahById(resultLesson.Save_FromSorh);
                        model.SurahId = model.Surah.Id;
                    }
                    _context.Students.Add(model);
                }
                else //update student data
                {
                    if (model.PlanDataId == Guid.Parse("5b35a4de-5100-4f4b-b065-527f04ae1472")) // الخطة العامة
                    {
                        var resultSurah = FindSurahById(model.SurahId);
                        var resultVerse = FindVerseById(model.SurahId, model.Verse.Num);
                        model.VerseId = resultVerse.Id;
                        model.Verse = resultVerse;
                        model.SurahId = resultSurah.Id;
                        model.Surah = resultSurah;
                        if (resultLesson == null)
                        {
                            resultLesson = new LessonData() { Id = maxLesson + 1 };
                            resultLesson.LevelDataId = Guid.Parse("00000000-0000-0000-0000-000000000101");
                            resultLesson.CurrentState = (int)Helper.eCurrentState.Active;
                            //Save
                            resultLesson.Save_FromSorh = resultSurah.Name;
                            resultLesson.Save_FromAyah = model.Verse.Num + 1;
                            resultLesson.Save_ToSorh = resultSurah.Name;
                            resultLesson.Save_ToAyah = resultVerse.Num + 1;
                            resultLesson.Save_Satr = 0;
                            //Rabt
                            resultLesson.Rabt_FromSorh = resultSurah.Name;
                            resultLesson.Rabt_FromAyah = resultVerse.Num;
                            resultLesson.Rabt_ToSorh = resultSurah.Name;
                            resultLesson.Rabt_ToAyah = resultVerse.Num;
                            resultLesson.Rabt_Satr = 0;
                            //Rev
                            resultLesson.Rev_FromSorh = resultSurah.Name;
                            resultLesson.Rev_FromAyah = resultVerse.Num;
                            resultLesson.Rev_ToSorh = resultSurah.Name;
                            resultLesson.Rev_ToAyah = resultVerse.Num;
                            resultLesson.Rev_Satr = 0;
                            //PrevSave
                            resultLesson.PrevSave_FromSorh = resultSurah.Name;
                            resultLesson.PrevSave_FromAyah = resultVerse.Num;
                            resultLesson.PrevSave_ToSorh = resultSurah.Name;
                            resultLesson.PrevSave_ToAyah = resultVerse.Num;
                            //PrevRev
                            resultLesson.PrevRev_FromSorh = resultSurah.Name;
                            resultLesson.PrevRev_FromAyah = resultVerse.Num;
                            resultLesson.PrevRev_ToSorh = resultSurah.Name;
                            resultLesson.PrevRev_ToAyah = resultVerse.Num;
                            _context.LessonsData.Add(resultLesson);
                        }
                        else
                        {
                            //Save
                            resultLesson.Save_FromSorh = resultSurah.Name;
                            resultLesson.Save_FromAyah = model.Verse.Num + 1;
                            resultLesson.Save_ToSorh = resultSurah.Name;
                            resultLesson.Save_ToAyah = resultVerse.Num + 1;
                            resultLesson.Save_Satr = 0;
                            //Rabt
                            resultLesson.Rabt_FromSorh = resultSurah.Name;
                            resultLesson.Rabt_FromAyah = resultVerse.Num;
                            resultLesson.Rabt_ToSorh = resultSurah.Name;
                            resultLesson.Rabt_ToAyah = resultVerse.Num;
                            resultLesson.Rabt_Satr = 0;
                            //Rev
                            resultLesson.Rev_FromSorh = resultSurah.Name;
                            resultLesson.Rev_FromAyah = resultVerse.Num;
                            resultLesson.Rev_ToSorh = resultSurah.Name;
                            resultLesson.Rev_ToAyah = resultVerse.Num;
                            resultLesson.Rev_Satr = 0;
                            //PrevSave
                            resultLesson.PrevSave_FromSorh = resultSurah.Name;
                            resultLesson.PrevSave_FromAyah = resultVerse.Num;
                            resultLesson.PrevSave_ToSorh = resultSurah.Name;
                            resultLesson.PrevSave_ToAyah = resultVerse.Num;
                            //PrevRev
                            resultLesson.PrevRev_FromSorh = resultSurah.Name;
                            resultLesson.PrevRev_FromAyah = resultVerse.Num;
                            resultLesson.PrevRev_ToSorh = resultSurah.Name;
                            resultLesson.PrevRev_ToAyah = resultVerse.Num;
                            _context.LessonsData.Update(resultLesson);
                        }
                        result.LessonDataId = resultLesson.Id;
                    }
                    else if (model.PlanDataId == Guid.Parse("934f4260-39d3-41ce-afb2-e4ae4f8474eb")) // الخطة المفتوحة
                    {
                        //resultLesson = FindLessonById(result.LessonDataId);

                        if (resultLesson == null)
                        {
                            resultLesson = new LessonData() { Id = maxLesson + 1 };
                            resultLesson.LevelDataId = Guid.Parse("00000000-0000-0000-0000-000000000201");
                            resultLesson.CurrentState = (int)Helper.eCurrentState.Active;
                        }

                        var resultSurah = FindSurahById(model.SurahId);
                        var resultVerse = FindVerseById(model.SurahId, model.Verse.Num);
                        model.VerseId = resultVerse.Id;
                        model.Verse = resultVerse;
                        model.SurahId = resultSurah.Id;
                        model.Surah = resultSurah;

                        //Save
                        resultLesson.Save_FromSorh = resultSurah.Name;
                        resultLesson.Save_FromAyah = model.Verse.Num + 1;
                        resultLesson.Save_ToSorh = resultSurah.Name;
                        resultLesson.Save_ToAyah = resultVerse.Num + 1;
                        resultLesson.Save_Satr = 0;
                        //Rabt
                        resultLesson.Rabt_FromSorh = resultSurah.Name;
                        resultLesson.Rabt_FromAyah = resultVerse.Num;
                        resultLesson.Rabt_ToSorh = resultSurah.Name;
                        resultLesson.Rabt_ToAyah = resultVerse.Num;
                        resultLesson.Rabt_Satr = 0;
                        //Rev
                        resultLesson.Rev_FromSorh = resultSurah.Name;
                        resultLesson.Rev_FromAyah = resultVerse.Num;
                        resultLesson.Rev_ToSorh = resultSurah.Name;
                        resultLesson.Rev_ToAyah = resultVerse.Num;
                        resultLesson.Rev_Satr = 0;
                        //PrevSave
                        resultLesson.PrevSave_FromSorh = resultSurah.Name;
                        resultLesson.PrevSave_FromAyah = resultVerse.Num;
                        resultLesson.PrevSave_ToSorh = resultSurah.Name;
                        resultLesson.PrevSave_ToAyah = resultVerse.Num;
                        //PrevRev
                        resultLesson.PrevRev_FromSorh = resultSurah.Name;
                        resultLesson.PrevRev_FromAyah = resultVerse.Num;
                        resultLesson.PrevRev_ToSorh = resultSurah.Name;
                        resultLesson.PrevRev_ToAyah = resultVerse.Num;
                        _context.LessonsData.Add(resultLesson);
                        result.LessonDataId = resultLesson.Id;

                    }
                    else // الذكر الحكيم
                    {
                        resultLesson = FindLessonById(model.LessonDataId);
                        model.Verse = FindVerseById(resultLesson.Save_FromSorh, resultLesson.Save_FromAyah);
                        model.VerseId = model.Verse.Id;
                        model.Surah = FindSurahById(resultLesson.Save_FromSorh);
                        model.SurahId = model.Surah.Id;
                        result.LessonDataId = model.LessonDataId;
                    }

                    result.Name = model.Name;
                    result.IdentityNumber = model.IdentityNumber;
                    result.Gender = model.Gender;
                    result.Adress = model.Adress;
                    result.Notes = model.Notes;
                    result.StEpisodeId = model.StEpisodeId;
                    result.CurrentState = (int)Helper.eCurrentState.Active;
                    result.newLessonState = false;
                    //result.ApplicationUserId = null;
                    result.Category = model.Category;
                    result.NationalityId = model.NationalityId;
                    result.HealthConditionId = model.HealthConditionId;
                    result.BirthDate = model.BirthDate; // ??
                    result.EducationType = model.EducationType;
                    result.ParentIdentityNumber = model.ParentIdentityNumber;
                    result.ParentPhoneNumber = model.ParentPhoneNumber;
                    result.ProfilePicture = model.ProfilePicture;
                    result.VerseId = model.VerseId;
                    result.SurahId = model.SurahId;
                    result.PlanDataId = model.PlanDataId;
                    result.SaveDirection = model.SaveDirection;//??

                    _context.Students.Update(result);
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

        #region Delete Student
        public bool Delete(Guid Id)
        {
            try
            {
                //var isDeleted = false;
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.Students.Update(result);
                _context.SaveChanges();
                return true;

                //var effectedRows = _context.SaveChanges();
                //if (effectedRows > 0)
                //{
                //    isDeleted = true;

                //    var pic = Path.Combine(_imagesPath, result.ProfilePicture);
                //    File.Delete(pic);
                //}
                //return isDeleted;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Student Details & Evaluations
        public Student? GetById(Guid id)
        {
            return _context.Students
                .Include(t => t.Evaluations)
                .Include(t => t.Surah)
                .Include(t => t.Demands)
                .Include(st => st.StEpisode)
                .Include(st => st.StEpisode.EpLocation)
                .Include(x => x.StEpisode.EpLocation.LoCenter)
                .Include(st => st.LessonData)
                .Include(st => st.LessonData.LevelData)
                .Include(st => st.LessonData.LevelData.PlanData)
                .Include(st => st.Nationality)
                .Include(st => st.HealthCondition)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }
        public List<Evaluation> GetEvaluationsForStudent(Guid stId)
        {
            try
            {
                List<Evaluation> evaluations = new();
                evaluations = _context.Evaluations
                                    .Include(x => x.EvStudent)
                                    .OrderByDescending(x => x.Ev_Date)
                                    .Where(x => x.CurrentState > 0 && x.EvStudentId == stId)
                                    .ToList();

                return evaluations;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Demand> GetDemandsForStudent(Guid stid)
        {
            try
            {
                List<Demand> demands = new();

                demands = _context.Demands
                                    .Include(x => x.Student)
                                    .Include(x => x.ApplicationUser)
                                    .OrderBy(x => x.Request_Date)
                                    .Where(x => x.StudentId == stid)
                                    .ToList();
                return demands;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region All Nationalities
        public List<Nationality> GetNationalities()
        {
            try
            {
                return _context.Nationalities
                               .Include(l => l.Students)
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
        #region Create & Update Nationality
        public Nationality FindByNationalityId(Guid Id)
        {
            try
            {
                return _context.Nationalities.FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Nationality FindByNationalityName(string Name)
        {
            try
            {
                return _context.Nationalities.FirstOrDefault(x => x.Name.Equals(Name));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool SaveNationality(Nationality model)
        {
            try
            {
                var result = FindByNationalityId(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.Nationalities.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.Nationalities.Update(result);
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
        #region Delete Nationality
        public bool DeletNationality(Guid Id)
        {
            try
            {
                var result = FindByNationalityId(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.Nationalities.Update(result);
                // _context.Nationalities.Remove(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region All Health Conditions
        public List<HealthCondition> GetHealth()
        {
            try
            {
                return _context.HealthConditions
                               .Include(l => l.Students)
                               .OrderBy(x => x.Num).ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region Create & Update Health
        public HealthCondition FindByHealthId(Guid Id)
        {
            try
            {
                return _context.HealthConditions.FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public HealthCondition FindByHealthName(string Name)
        {
            try
            {
                return _context.HealthConditions.FirstOrDefault(x => x.Name.Equals(Name));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool SaveHealth(HealthCondition model)
        {
            try
            {
                var result = FindByHealthId(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.HealthConditions.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.HealthConditions.Update(result);
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
        #region Delete Health Condition
        public bool DeleteHealth(Guid Id)
        {
            try
            {
                var result = FindByHealthId(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.HealthConditions.Update(result);
                //_context.HealthConditions.Remove(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion

        #region SelectedLevel
        public LevelData? GetSelectedLevel()
        {
            return _context.LevelsData
                .Include(st => st.LessonsData)
                .Include(st => st.PlanData)
            .AsNoTracking()
                .SingleOrDefault();
        }
        #endregion

        #region AddNew Student Request Parent
        // طلب اضافة طالب جديد
        public bool AddNewStudentRequest(Student model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null) // create new student
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Delete;
                    model.PlanDataId = Guid.Parse("64944ffb-f494-4141-b93c-e8ec32194a8b"); // الذكر الحكيم
                    model.LessonDataId = 1;
                    model.VerseId = 6231;
                    model.SurahId = 114;
                    model.newLessonState = false;
                    model.ApplicationUserId = null;
                    model.SaveDirection = "ptc";
                    model.EpProgName = null;

                    //Demand
                    var demand = new Demand();
                    demand.Id = Guid.NewGuid();
                    demand.Request_Type = "طلب إضافة طالب جديد";
                    demand.Request_Reason = "إضافة طالب جديد";
                    demand.Request_Date = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    demand.CurrentState = (int)Helper.eCurrentState.Active;
                    demand.StartHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    demand.EndHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    demand.IsAccepted = false;
                    demand.IsOpened = false;
                    demand.Student = model;

                    demand.NewEpisode = FindEpisodetBy(model.StEpisodeId).Name;
                    demand.OldEpisode = demand.NewEpisode;
                    demand.NotificationText = $"طلب إضافة طالب جديد - {model.Name}";
                    demand.StName = model.Name;
                    demand.StIdentityNum = model.IdentityNumber;
                    demand.StParPhoneNum = model.ParentPhoneNumber;
                    demand.StAdress = model.Adress;
                    demand.StNotes = model.Notes;

                    _context.Students.Add(model);
                    _context.Demands.Add(demand);
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
