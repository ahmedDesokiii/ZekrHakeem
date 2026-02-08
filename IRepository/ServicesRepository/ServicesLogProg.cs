namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLogProg : IServicesRepositoryLog<LogProg>
    {
        private readonly ApplicationDbContext _context;

        public ServicesLogProg(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logProg = new LogProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    ProgId = Id
                };
                _context.LogProgs.Add(logProg);
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
                var logProg = new LogProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Pause,
                    Date = DateTime.Now,
                    UserId = UserId,
                    ProgId = Id
                };
                _context.LogProgs.Add(logProg);
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
                var logProg = new LogProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Resume,
                    Date = DateTime.Now,
                    UserId = UserId,
                    ProgId = Id
                };
                _context.LogProgs.Add(logProg);
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
                    _context.LogProgs.Remove(result);
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

        public LogProg FindBy(Guid Id)
        {
            try
            {
                return _context.LogProgs.Include(x => x.Prog).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LogProg> GetAll()
        {
            try
            {
                return _context.LogProgs.Include(x => x.Prog).OrderByDescending(x => x.Date).ToList();
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
                var logCategory = new LogProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    ProgId = Id
                };
                _context.LogProgs.Add(logCategory);
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
                var logCategory = new LogProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    ProgId = Id
                };
                _context.LogProgs.Add(logCategory);
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
