namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesProg : IServicesRepository<Prog>, IServicesProg
    {
        private readonly ApplicationDbContext _context;
        public ServicesProg(ApplicationDbContext context)
        {
            _context = context;
        }

        #region AllProgs
        // Get All Progs | admin role
        public List<Prog> GetAll()
        {
            try
            {
                var centers = _context.Progs
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.EpisodeProgs)
                            .OrderBy(x => x.Type)
                            .OrderBy(x => x.Kind)
                            .ThenBy(x => x.Name)
                            .Where(x => x.CurrentState > 0)
                            .ToList();
                return centers;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Progs For Men
        public List<Prog> GetProgsForMen()
        {
            try
            {
                return _context.Progs
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.EpisodeProgs)
                            .OrderBy(x => x.Type)
                            .OrderBy(x => x.Kind)
                            .ThenBy(x => x.Name)
                            .Where(x => x.CurrentState > 0 && x.Type == "men".ToLower())
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Progs For Women
        public List<Prog> GetProgsForWomen()
        {
            try
            {
                return _context.Progs
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.EpisodeProgs)
                             .OrderBy(x => x.Type)
                             .OrderBy(x => x.Kind)
                            .ThenBy(x => x.Name)
                            .Where(x => x.CurrentState > 0 && x.Type == "women".ToLower())
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        #endregion

        #region Progs with supervisor
        public List<Prog> GetProgsWithSupervisor(string userid)
        {
            try
            {
                return _context.Progs
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.EpisodeProgs)
                            .OrderBy(x => x.Name)
                            .Where(x => x.CurrentState > 0 && x.PauseState > 0 && x.ApplicationUserId == userid)
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Find center by ...
        //FindProgBy => Id | Name | Type
        public Prog FindBy(Guid Id) => _context.Progs.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
        public Prog FindBy(string Name) => _context.Progs.FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);

        #endregion

        #region Create & Edit Prog
        // Add | Update Prog Without User
        public bool Save(Prog model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.PauseState = (int)Helper.eCurrentState.Active;
                    _context.Progs.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    result.Type = model.Type;
                    result.StartProg = model.StartProg;
                    result.EndProg = model.EndProg;
                    result.PlanId = model.PlanId;
                    result.Kind = model.Kind;
                    result.Description = model.Description;
                    result.ApplicationUserId = model.ApplicationUserId;
                    result.CurrentState = (int)Helper.eCurrentState.Active;
                    result.PauseState = (int)Helper.eCurrentState.Active;
                    _context.Progs.Update(result);
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

        #region Delete Prog
        // Delete Prog Without User
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.Progs.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Pause | Resume Prog
        // Delete Prog Without User
        public bool Pause(Guid Id)
        {
            try
            {
                EpisodeProgViewModel model = new()
                {
                    EpisodeProgs = _context.EpisodeProgs
                                        .Include(x => x.StudentProgs)
                                        .Include(x => x.ApplicationUser)
                                        .Include(x => x.EpProgLocation)
                                        .Include(x => x.EpProg)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.EpProgId == Id)
                                        .ToList()
                };

                var result = FindBy(Id);
                result.PauseState = (int)Helper.ePauseState.Pause;
                foreach (var episode in model.EpisodeProgs)
                {
                    episode.PauseState = (int)Helper.ePauseState.Pause;
                }
                _context.Progs.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Resume(Guid Id)
        {
            try
            {

                EpisodeProgViewModel model = new()
                {
                    EpisodeProgs = _context.EpisodeProgs
                                        .Include(x => x.StudentProgs)
                                        .Include(x => x.ApplicationUser)
                                        .Include(x => x.EpProgLocation)
                                        .Include(x => x.EpProg)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.EpProgId == Id)
                                        .ToList()
                };

                var result = FindBy(Id);
                result.EpisodeProgs = model.EpisodeProgs;
                result.PauseState = (int)Helper.ePauseState.Active;

                foreach (var episode in model.EpisodeProgs)
                {
                    episode.PauseState = (int)Helper.ePauseState.Active;
                }
                _context.Progs.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Prog Details 
        public Prog? GetById(Guid id)
        {
            return _context.Progs
                .Include(st => st.ApplicationUser)
                .Include(st => st.EpisodeProgs)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }

        #endregion
    }
}
