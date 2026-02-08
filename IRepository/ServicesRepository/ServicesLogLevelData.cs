namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLogLevelData : IServicesRepositoryLog<LogLevelData>
    {
        private readonly ApplicationDbContext _context;

        public ServicesLogLevelData(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logLevel = new LogLevelData
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    LevelDataId = Id
                };
                _context.LogLevelsData.Add(logLevel);
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
                    _context.LogLevelsData.Remove(result);
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

        public LogLevelData FindBy(Guid Id)
        {
            try
            {
                return _context.LogLevelsData.Include(x => x.LevelData).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LogLevelData> GetAll()
        {
            try
            {
                return _context.LogLevelsData.Include(x => x.LevelData).OrderByDescending(x => x.Date).ToList();
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
                var logLevel = new LogLevelData
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    LevelDataId = Id
                };
                _context.LogLevelsData.Add(logLevel);
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
                var logLevel = new LogLevelData
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    LevelDataId = Id
                };
                _context.LogLevelsData.Add(logLevel);
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
