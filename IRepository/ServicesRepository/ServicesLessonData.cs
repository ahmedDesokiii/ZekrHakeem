namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLessonData : IServicesRepository<LessonData>, IServicesLessonData
    {
        private readonly ApplicationDbContext _context;
        public ServicesLessonData(ApplicationDbContext context)
        {
            _context = context;
        }
        #region All Lessons
        public List<LessonData> GetAll()
        {
            try
            {
                var maxLesson = _context.LessonsData.Count();
                return _context.LessonsData
                               .Include(x => x.LevelData)
                               //.Include(x => x.LevelData.PlanData)
                               .OrderBy(x => x.Id)
                               .Where(x => x.CurrentState > 0 && x.Id > 0 && x.Id < maxLesson - 1)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lessons With Selected Plan !!

        #endregion

        #region Find Lesson By ...
        public LessonData FindBy(Guid Id)
        {
            throw new NotImplementedException();
        }
        public LessonData FindBy(string Name)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Create & Update Lesson
        public bool Save(LessonData model)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Delete Lesson
        public bool Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
