namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLogEpisodeProg : IServicesRepositoryLog<LogEpisodeProg>
    {
        private readonly ApplicationDbContext _context;

        public ServicesLogEpisodeProg(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Pause(Guid Id, Guid UserId)
        {
            try
            {
                var logEpisodeProg = new LogEpisodeProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Pause,
                    Date = DateTime.Now,
                    UserId = UserId,
                    EpisodeProgId = Id
                };
                _context.LogEpisodeProgs.Add(logEpisodeProg);
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
                var logEpisodeProg = new LogEpisodeProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Resume,
                    Date = DateTime.Now,
                    UserId = UserId,
                    EpisodeProgId = Id
                };
                _context.LogEpisodeProgs.Add(logEpisodeProg);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logEpisodeProg = new LogEpisodeProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    EpisodeProgId = Id
                };
                _context.LogEpisodeProgs.Add(logEpisodeProg);
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
                    _context.LogEpisodeProgs.Remove(result);
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

        public LogEpisodeProg FindBy(Guid Id)
        {
            try
            {
                return _context.LogEpisodeProgs.Include(x => x.EpisodeProg).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LogEpisodeProg> GetAll()
        {
            try
            {
                return _context.LogEpisodeProgs.Include(x => x.EpisodeProg).OrderByDescending(x => x.Date).ToList();
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
                var logEpisodeProg = new LogEpisodeProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    EpisodeProgId = Id
                };
                _context.LogEpisodeProgs.Add(logEpisodeProg);
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
                var logCategory = new LogEpisodeProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    EpisodeProgId = Id
                };
                _context.LogEpisodeProgs.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///////////////////
        /// Student Prog log services
        /// 

        public bool SaveStudentProg(Guid Id, Guid UserId)
        {
            try
            {
                var logStuProg = new LogStudentProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    StudentProgId = Id
                };
                _context.LogStudentProgs.Add(logStuProg);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateStudentProg(Guid Id, Guid UserId)
        {
            try
            {
                var logStuProg = new LogStudentProg
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    StudentProgId = Id
                };
                _context.LogStudentProgs.Add(logStuProg);
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
