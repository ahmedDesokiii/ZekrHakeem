namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLogLocation : IServicesRepositoryLog<LogLocation>
    {
        private readonly ApplicationDbContext _context;

        public ServicesLogLocation(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogLocation
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    LocationId = Id
                };
                _context.LogLocations.Add(logCategory);
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
                var logCategory = new LogLocation
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Pause,
                    Date = DateTime.Now,
                    UserId = UserId,
                    LocationId = Id
                };
                _context.LogLocations.Add(logCategory);
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
                var logCategory = new LogLocation
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Resume,
                    Date = DateTime.Now,
                    UserId = UserId,
                    LocationId = Id
                };
                _context.LogLocations.Add(logCategory);
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
                    _context.LogLocations.Remove(result);
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

        public LogLocation FindBy(Guid Id)
        {
            try
            {
                return _context.LogLocations.Include(x => x.Location).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LogLocation> GetAll()
        {
            try
            {
                return _context.LogLocations.Include(x => x.Location).OrderByDescending(x => x.Date).ToList();
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
                var logLocation = new LogLocation
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    LocationId = Id
                };
                _context.LogLocations.Add(logLocation);
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
                var logCategory = new LogLocation
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    LocationId = Id
                };
                _context.LogLocations.Add(logCategory);
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
