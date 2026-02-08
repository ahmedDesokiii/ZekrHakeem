
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
                               .Include(l => l.LoCenter)
                               .Include(l => l.District)
                               .Include(l => l.Episodes)
                               .Include(l => l.EpisodeProgs)
                               .OrderBy(x => x.LoCenter.Type)
                               .ThenBy(x => x.LoCenter.Name)
                               .ThenBy(x => x.Type)
                               .ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Locations Men
        public List<Location> GetLocationsForMen()
        {
            try
            {
                return _context.Locations
                               .Include(l => l.LoCenter)
                               .Include(l => l.District)
                               .Include(l => l.Episodes)
                               .Include(l => l.EpisodeProgs)

                               .OrderBy(x => x.LoCenter.Type)
                               .ThenBy(x => x.LoCenter.Name)
                               .ThenBy(x => x.Type)
                               .ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.LoCenter.Type == "men")
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Locations Women
        public List<Location> GetLocationsForWomen()
        {
            try
            {
                return _context.Locations
                               .Include(l => l.LoCenter)
                               .Include(l => l.District)
                               .Include(l => l.Episodes)
                               .Include(l => l.EpisodeProgs)

                               .OrderBy(x => x.LoCenter.Type)
                               .ThenBy(x => x.LoCenter.Name)
                               .ThenBy(x => x.Type)
                               .ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.LoCenter.Type == "women")
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Locations With Supervisor
        public List<Location> GetLocationsWithSupervisor(string userid)
        {
            try
            {
                return _context.Locations
                               .Include(l => l.LoCenter)
                               .Include(l => l.District)
                               .Include(l => l.Episodes)
                               .Include(l => l.EpisodeProgs)

                               .OrderBy(x => x.LoCenter.Type)
                               .ThenBy(x => x.LoCenter.Name)
                               .ThenBy(x => x.Type)
                               .ThenBy(x => x.Name)
                               .Where(x => x.CurrentState > 0 && x.PauseState > 0 && x.LoCenter.ApplicationUserId == userid)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Find Location By ...
        public Location FindBy(Guid Id)
        {
            try
            {
                return _context.Locations.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
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
                return _context.Locations.FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
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
                return _context.Locations.FirstOrDefault(x => x.Type.Equals(Type.Trim()) && x.CurrentState > 0);
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
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.Locations.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    //result.Type = model.Type;
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
                EpisodeViewModel model2 = new()
                {
                    Episodes = _context.Episodes
                                       .Include(x => x.Students)
                                       .Include(x => x.ApplicationUser)
                                       .Include(x => x.EpLocation)
                                       .Include(x => x.EpisodeType)
                                       .OrderBy(x => x.Name)
                                       .Where(x => x.CurrentState > 0 && x.EpLocationId == Id)
                                       .ToList()
                };
                var result = FindBy(Id);
                result.Episodes = model2.Episodes;
                result.PauseState = (int)Helper.ePauseState.Pause;
                foreach (var episode in result.Episodes)
                {
                    episode.PauseState = (int)Helper.ePauseState.Pause;
                }
                _context.Locations.Update(result);
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
                EpisodeViewModel model2 = new()
                {
                    Episodes = _context.Episodes
                                       .Include(x => x.Students)
                                       .Include(x => x.ApplicationUser)
                                       .Include(x => x.EpLocation)
                                       .Include(x => x.EpisodeType)
                                       .OrderBy(x => x.Name)
                                       .Where(x => x.CurrentState > 0 && x.EpLocationId == Id)
                                       .ToList()
                };
                var result = FindBy(Id);
                result.Episodes = model2.Episodes;
                result.PauseState = (int)Helper.ePauseState.Active;
                foreach (var episode in result.Episodes)
                {
                    episode.PauseState = (int)Helper.ePauseState.Active;
                }
                _context.Locations.Update(result);
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
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.Locations.Update(result);
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
            return _context.Locations
                .Include(st => st.Episodes)
                .Include(st => st.LoCenter)
                .Include(st => st.District)
                .Include(st => st.LoCenter.ApplicationUser)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }
        public EpisodeViewModel GetEpisodesForLocation(Guid locationid)
        {
            try
            {
                EpisodeViewModel episodeViewModel = new EpisodeViewModel()
                {
                    Episodes = _context.Episodes
                                        .Include(x => x.EpLocation)
                                        .Include(x => x.Students)
                                        .Include(x => x.EpisodeType)
                                        .Include(x => x.ApplicationUser)
                                        //.Include(x => x.PrayerTime)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.EpLocationId == locationid)
                                        .ToList()
                };

                return episodeViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public StudentViewModel GetStudentsForLocation(Guid locationid)
        {
            try
            {
                StudentViewModel studentViewModel = new StudentViewModel()
                {
                    Students = _context.Students
                                        .Include(x => x.StEpisode)
                                        .Include(x => x.ApplicationUser)
                                        .Include(x => x.Demands)
                                        .Include(x => x.Nationality)
                                        .Include(x => x.Evaluations)
                                        .Include(x => x.LessonData)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocationId == locationid)
                                        .ToList()
                };

                return studentViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

    }
}
