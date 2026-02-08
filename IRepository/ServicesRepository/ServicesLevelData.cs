namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLevelData : IServicesRepository<LevelData>, IServicesLevelData
    {
        private readonly ApplicationDbContext _context;
        public ServicesLevelData(ApplicationDbContext context)
        {
            _context = context;
        }

        #region All LevelDatas
        public List<LevelData> GetAll()
        {
            try
            {
                return _context.LevelsData
                               .Include(x => x.PlanData)
                               //.Include(x => x.LessonsData)
                               .OrderBy(x => x.Num)
                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public LevelViewModel GetAllLevelsWithLessons()
        {
            try
            {
                LevelViewModel lvm = new LevelViewModel();
                lvm.Levels = _context.LevelsData
                               .Include(x => x.PlanData)
                               //.Include(x => x.LessonsData)
                               .OrderBy(x => x.Num)
                               .Where(x => x.CurrentState > 0)
                               .ToList();

                foreach (var level in lvm.Levels)
                {
                    level.LessonsData = _context.LessonsData
                                .Include(x => x.Students)
                                .OrderBy(x => x.Id)
                                .Where(x => x.CurrentState > 0 && x.LevelDataId == level.Id && x.Id > 0)
                                .ToList();
                }

                return lvm;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Find LevelData by ...
        public LevelData FindBy(Guid Id)
        {
            try
            {
                return _context.LevelsData.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public LevelData FindBy(string Name)
        {
            try
            {
                return _context.LevelsData.FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Create & Update LevelData
        public bool Save(LevelData model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.LevelsData.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    result.Description = model.Description;
                    //result.LePlanId = model.LePlanId;
                    result.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.LevelsData.Update(result);
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

        #region Delete
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.LevelsData.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion

        #region Level Details

        public LevelData? GetById(Guid id)
        {
            return _context.LevelsData
               .Include(st => st.LessonsData)
               .Include(st => st.PlanData)
               .AsNoTracking()
               .SingleOrDefault(g => g.Id == id);
        }

        public LessonViewModel GetLessonsForLevelData(Guid levelDataid)
        {
            try
            {
                LessonViewModel lessonViewModel = new LessonViewModel()
                {
                    LessonsData = _context.LessonsData
                                        .Include(x => x.LevelData)
                                        .Include(x => x.LevelData.PlanData)
                                        .Include(x => x.Students)
                                        .OrderBy(x => x.Id)
                                        .Where(x => x.CurrentState > 0 && x.LevelDataId == levelDataid)
                                        .ToList()
                };

                return lessonViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public StudentViewModel GetStudentsForLevelData(Guid levelDataid)
        {
            try
            {
                StudentViewModel studentViewModel = new StudentViewModel()
                {
                    Students = _context.Students
                                        .Include(x => x.StEpisode)
                                        .Include(x => x.ApplicationUser)
                                        .Include(x => x.Demands)
                                        .Include(x => x.Nationality)
                                        .Include(x => x.Evaluations)
                                        .Include(x => x.LessonData)
                                        .Include(x => x.LessonData.LevelData)
                                        .Include(x => x.LessonData.LevelData.PlanData)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.LessonData.LevelDataId == levelDataid)
                                        .ToList()
                };

                return studentViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

    }
}
