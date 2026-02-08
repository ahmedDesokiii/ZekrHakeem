namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLogPlanDataData : IServicesRepositoryLog<LogPlanData>
    {
        private readonly ApplicationDbContext _context;

        public ServicesLogPlanDataData(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var LogPlanData = new LogPlanData
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    PlanDataId = Id
                };
                _context.LogPlansData.Add(LogPlanData);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Pause(Guid Id, Guid UserId)
        {
            try
            {
                var LogPlanData = new LogPlanData
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Pause,
                    Date = DateTime.Now,
                    UserId = UserId,
                    PlanDataId = Id
                };
                _context.LogPlansData.Add(LogPlanData);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Resume(Guid Id, Guid UserId)
        {
            try
            {
                var LogPlanData = new LogPlanData
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Resume,
                    Date = DateTime.Now,
                    UserId = UserId,
                    PlanDataId = Id
                };
                _context.LogPlansData.Add(LogPlanData);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteLog(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                if (!result.Equals(null))
                {
                    _context.LogPlansData.Remove(result);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LogPlanData FindBy(Guid Id)
        {
            try
            {
                return _context.LogPlansData.Include(x => x.PlanData).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LogPlanData> GetAll()
        {
            try
            {
                return _context.LogPlansData.Include(x => x.PlanData).OrderByDescending(x => x.Date).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Save(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogPlanData
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    PlanDataId = Id
                };
                _context.LogPlansData.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogPlanData
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    PlanDataId = Id
                };
                _context.LogPlansData.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
