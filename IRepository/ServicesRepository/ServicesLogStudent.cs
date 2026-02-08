namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLogStudent : IServicesRepositoryLog<LogStudent>
    {
        private readonly ApplicationDbContext _context;

        public ServicesLogStudent(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogStudent
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    StudentId = Id
                };
                _context.LogStudents.Add(logCategory);
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
                    _context.LogStudents.Remove(result);
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

        public LogStudent FindBy(Guid Id)
        {
            try
            {
                return _context.LogStudents.Include(x => x.Student).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LogStudent> GetAll()
        {
            try
            {
                return _context.LogStudents.Include(x => x.Student).OrderByDescending(x => x.Date).ToList();
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
                var logCategory = new LogStudent
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    StudentId = Id
                };
                _context.LogStudents.Add(logCategory);
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
                var logStudent = new LogStudent
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    StudentId = Id
                };
                _context.LogStudents.Add(logStudent);
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
