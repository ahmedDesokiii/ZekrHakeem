namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLogDemand : IServicesRepositoryLog<LogDemand>
    {
        private readonly ApplicationDbContext _context;


        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logDemand = new LogDemand
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    DemandId = Id
                };
                _context.LogDemands.Add(logDemand);
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
                var LogDemand = new LogDemand
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Pause,
                    Date = DateTime.Now,
                    UserId = UserId,
                    DemandId = Id
                };
                _context.LogDemands.Add(LogDemand);
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
                var LogDemand = new LogDemand
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Resume,
                    Date = DateTime.Now,
                    UserId = UserId,
                    DemandId = Id
                };
                _context.LogDemands.Add(LogDemand);
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
                    _context.LogDemands.Remove(result);
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

        public LogDemand FindBy(Guid Id)
        {
            try
            {

                return _context.LogDemands.Include(x => x.Demand).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LogDemand> GetAll()
        {
            try
            {
                return _context.LogDemands.Include(x => x.Demand).OrderByDescending(x => x.Date).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Save(Guid Id, Guid UserId)
        {
            //try
            //{
            //    var logCategory = new LogDemand
            //    {
            //        Id = Guid.NewGuid(),
            //        Action = Helper.ِAddRequestAsk,
            //        Date = DateTime.Now,
            //        UserId = UserId,
            //        DemandId = Id
            //    };
            //    _context.LogDemands.Add(logCategory);
            //    _context.SaveChanges();
            //    return true;
            //}
            //catch (Exception)
            //{}
            return true;

        }

        public bool Update(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogDemand
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    DemandId = Id
                };
                _context.LogDemands.Add(logCategory);
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
