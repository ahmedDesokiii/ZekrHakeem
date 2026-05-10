namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesLocation : IServicesRepository<Location>, IServicesLocation
    {
        private readonly ApplicationDbContext _context;

        public ServicesLocation(ApplicationDbContext context)
        {
            _context = context;
        }

        #region All Locations

        public List<Location> GetAll()
        {
            try
            {
                return _context.Locations
                    .AsNoTracking()
                    .Where(x => x.CurrentState > 0)

                    .Include(l => l.LoCenter)
                    .Include(l => l.District)

                    .OrderBy(x => x.LoCenter.Type)
                    .ThenBy(x => x.LoCenter.Name)
                    .ThenBy(x => x.Type)
                    .ThenBy(x => x.Name)

                    .ToList();
            }
            catch (Exception)
            {
                return new List<Location>();
            }
        }

        #endregion

        #region Locations Men

        public List<Location> GetLocationsForMen()
        {
            try
            {
                return _context.Locations
                    .AsNoTracking()

                    .Where(x =>
                        x.CurrentState > 0 &&
                        x.LoCenter.Type == "men")

                    .Include(l => l.LoCenter)
                    .Include(l => l.District)

                    .OrderBy(x => x.LoCenter.Type)
                    .ThenBy(x => x.LoCenter.Name)
                    .ThenBy(x => x.Type)
                    .ThenBy(x => x.Name)

                    .ToList();
            }
            catch (Exception)
            {
                return new List<Location>();
            }
        }

        #endregion

        #region Locations Women

        public List<Location> GetLocationsForWomen()
        {
            try
            {
                return _context.Locations
                    .AsNoTracking()

                    .Where(x =>
                        x.CurrentState > 0 &&
                        x.LoCenter.Type == "women")

                    .Include(l => l.LoCenter)
                    .Include(l => l.District)

                    .OrderBy(x => x.LoCenter.Type)
                    .ThenBy(x => x.LoCenter.Name)
                    .ThenBy(x => x.Type)
                    .ThenBy(x => x.Name)

                    .ToList();
            }
            catch (Exception)
            {
                return new List<Location>();
            }
        }

        #endregion

        #region Locations With Supervisor

        public List<Location> GetLocationsWithSupervisor(string userid)
        {
            try
            {
                return _context.Locations
                    .AsNoTracking()

                    .Where(x =>
                        x.CurrentState > 0 &&
                        x.PauseState > 0 &&
                        x.LoCenter.ApplicationUserId == userid)

                    .Include(l => l.LoCenter)
                    .Include(l => l.District)

                    .OrderBy(x => x.LoCenter.Type)
                    .ThenBy(x => x.LoCenter.Name)
                    .ThenBy(x => x.Type)
                    .ThenBy(x => x.Name)

                    .ToList();
            }
            catch (Exception)
            {
                return new List<Location>();
            }
        }

        #endregion

        #region Find Location By ...

        public Location FindBy(Guid Id)
        {
            try
            {
                return _context.Locations
                    .FirstOrDefault(x =>
                        x.Id == Id &&
                        x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Location FindBy(string Name)
        {
            try
            {
                Name = Name?.Trim() ?? "";

                return _context.Locations
                    .FirstOrDefault(x =>
                        x.Name == Name &&
                        x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Location FindBy(Guid Id, string Type)
        {
            try
            {
                Type = Type?.Trim() ?? "";

                return _context.Locations
                    .FirstOrDefault(x =>
                        x.Id == Id &&
                        x.Type == Type &&
                        x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Create & Update Location

        public bool Save(Location model)
        {
            try
            {
                if (model == null)
                    return false;

                var result = _context.Locations
                    .FirstOrDefault(x => x.Id == model.Id);

                // Create
                if (result == null)
                {
                    model.Id = Guid.NewGuid();

                    model.CurrentState = (int)Helper.eCurrentState.Active;

                    if (model.PauseState == 0)
                        model.PauseState = (int)Helper.ePauseState.Active;

                    _context.Locations.Add(model);
                }

                // Update
                else
                {
                    result.Name = model.Name?.Trim();
                    result.Description = model.Description;
                    result.LoCenterId = model.LoCenterId;
                    result.Latitude = model.Latitude;
                    result.Longitude = model.Longitude;
                    result.DistrictId = model.DistrictId;

                    result.CurrentState = (int)Helper.eCurrentState.Active;

                    _context.Locations.Update(result);
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

        #region Pause | Resume Location

        public bool Pause(Guid Id)
        {
            try
            {
                var result = _context.Locations
                    .Include(x => x.Episodes)
                    .FirstOrDefault(x =>
                        x.Id == Id &&
                        x.CurrentState > 0);

                if (result == null)
                    return false;

                result.PauseState = (int)Helper.ePauseState.Pause;

                if (result.Episodes != null && result.Episodes.Any())
                {
                    foreach (var episode in result.Episodes)
                    {
                        episode.PauseState = (int)Helper.ePauseState.Pause;
                    }
                }

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
                var result = _context.Locations
                    .Include(x => x.Episodes)
                    .FirstOrDefault(x =>
                        x.Id == Id &&
                        x.CurrentState > 0);

                if (result == null)
                    return false;

                result.PauseState = (int)Helper.ePauseState.Active;

                if (result.Episodes != null && result.Episodes.Any())
                {
                    foreach (var episode in result.Episodes)
                    {
                        episode.PauseState = (int)Helper.ePauseState.Active;
                    }
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

        #region Delete Location

        public bool Delete(Guid Id)
        {
            try
            {
                var result = _context.Locations
                    .Include(x => x.Episodes)
                    .FirstOrDefault(x =>
                        x.Id == Id &&
                        x.CurrentState > 0);

                if (result == null)
                    return false;

                result.CurrentState = (int)Helper.eCurrentState.Delete;

                if (result.Episodes != null && result.Episodes.Any())
                {
                    foreach (var episode in result.Episodes)
                    {
                        episode.CurrentState = (int)Helper.eCurrentState.Delete;
                    }
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

        #region Location Details

        public Location? GetById(Guid id)
        {
            try
            {
                return _context.Locations
                    .AsNoTracking()

                    .Include(st => st.Episodes)
                    .Include(st => st.LoCenter)
                    .Include(st => st.District)
                    .Include(st => st.LoCenter.ApplicationUser)

                    .SingleOrDefault(g => g.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EpisodeViewModel GetEpisodesForLocation(Guid locationid)
        {
            try
            {
                return new EpisodeViewModel()
                {
                    Episodes = _context.Episodes
                        .AsNoTracking()

                        .Where(x =>
                            x.CurrentState > 0 &&
                            x.EpLocationId == locationid)

                        .Include(x => x.EpLocation)
                        .Include(x => x.Students)
                        .Include(x => x.EpisodeType)
                        .Include(x => x.ApplicationUser)

                        .OrderBy(x => x.Name)

                        .ToList()
                };
            }
            catch (Exception)
            {
                return new EpisodeViewModel();
            }
        }

        public StudentViewModel GetStudentsForLocation(Guid locationid)
        {
            try
            {
                return new StudentViewModel()
                {
                    Students = _context.Students
                        .AsNoTracking()

                        .Where(x =>
                            x.CurrentState > 0 &&
                            x.StEpisode.EpLocationId == locationid)

                        .Include(x => x.StEpisode)
                        .Include(x => x.ApplicationUser)
                        .Include(x => x.Demands)
                        .Include(x => x.Nationality)
                        .Include(x => x.Evaluations)
                        .Include(x => x.LessonData)
                        .OrderBy(x => x.Name)

                        .ToList()
                };
            }
            catch (Exception)
            {
                return new StudentViewModel();
            }
        }

        #endregion
    }
}