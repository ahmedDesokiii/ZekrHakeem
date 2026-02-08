

namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesPlanData : IServicesRepository<PlanData>, IServicesPlanData
    {
        private readonly ApplicationDbContext _context;

        public ServicesPlanData(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.PlansData.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public PlanData FindBy(Guid Id)
        {
            try
            {
                return _context.PlansData.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public PlanData FindBy(string Name)
        {
            try
            {
                return _context.PlansData.FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<PlanData> GetAll()
        {
            try
            {
                return _context.PlansData
                               .Include(x => x.LevelsData)
                               .Include(x => x.Students)
                               .OrderBy(x => x.Num)
                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public PlanData? GetById(Guid id)
        {
            return _context.PlansData
               .AsNoTracking()
               .SingleOrDefault(g => g.Id == id);
        }
        public bool Save(PlanData model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.PlansData.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    result.Description = model.Description;
                    result.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.PlansData.Update(result);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LevelViewModel GetLevelsForPlan(Guid planid)
        {
            try
            {
                StudentViewModel svm = new();

                LevelViewModel levelViewModel = new LevelViewModel()
                {
                    Levels = _context.LevelsData
                                        .Include(x => x.PlanData)
                                        .Include(x => x.LessonsData)
                                        .OrderBy(x => x.Num)
                                        .Where(x => x.CurrentState > 0 && x.PlanDataId == planid)
                                        .ToList()
                };
                foreach (var level in levelViewModel.Levels)
                {
                    svm.Students = _context.Students
                                       .Include(x => x.StEpisode)
                                       .Include(x => x.ApplicationUser)
                                       .Include(x => x.Demands)
                                       .Include(x => x.Evaluations)
                                       .Include(x => x.LessonData)
                                       .OrderBy(x => x.Name)
                                       .Where(x => x.CurrentState > 0 && x.LessonData.LevelDataId == level.Id)
                                       .ToList();
                    level.Description = svm.Students.Count().ToString();

                }

                return levelViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LessonViewModel GetLessonsForPlan(Guid planid)
        {
            try
            {
                StudentViewModel svm = new();
                LessonViewModel lessonViewModel = new LessonViewModel()
                {
                    LessonsData = _context.LessonsData
                                        .Include(x => x.LevelData)
                                        .Include(x => x.Students)
                                        .OrderBy(x => x.Id)
                                        .Where(x => x.CurrentState > 0 && x.LevelData.PlanDataId == planid)
                                        .ToList()
                };
                foreach (var lesson in lessonViewModel.LessonsData)
                {
                    svm.Students = _context.Students
                                       .Include(x => x.StEpisode)
                                       .Include(x => x.LessonData)
                                       .Include(x => x.LessonData.LevelData)
                                       .Include(x => x.LessonData.LevelData.PlanData)
                                       .Include(x => x.ApplicationUser)
                                       .Include(x => x.Demands)
                                       .Include(x => x.Evaluations)
                                       .OrderBy(x => x.Name)
                                       .Where(x => x.CurrentState > 0 && x.LessonDataId == lesson.Id)
                                       .ToList();
                    //lesson.Description = svm.Students.Count().ToString();

                }

                return lessonViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
