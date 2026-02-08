namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesCenter : IServicesRepository<Center>, IServicesCenter
    {
        private readonly ApplicationDbContext _context;
        public ServicesCenter(ApplicationDbContext context)
        {
            _context = context;
        }

        #region AllCenters
        // Get All Centers | admin role
        public List<Center> GetAll()
        {
            try
            {
                var centers = _context.Centers
                            //.Include(x => x.Districts)
                            .Include(x => x.Locations)
                            .OrderBy(x => x.Type).ThenBy(x => x.Name)
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

        #region Centers For Men
        public List<Center> GetCentersForMen()
        {
            try
            {
                return _context.Centers
                            .Include(x => x.Locations)
                            //.Include(x => x.Districts)
                            .OrderBy(x => x.Type)
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

        #region Centers For Women
        public List<Center> GetCentersForWomen()
        {
            try
            {
                return _context.Centers
                            .Include(x => x.Locations)
                             //.Include(x => x.Districts)
                             .OrderBy(x => x.Type)
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

        #region Centers with supervisor
        public List<Center> GetCentersWithSupervisor(string userid)
        {
            try
            {
                return _context.Centers
                            .Include(x => x.Locations)
                            //.Include(x => x.Districts)
                            .Include(x => x.ApplicationUser)
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
        //FindCenterBy => Id | Name | Type
        public Center FindBy(Guid Id) => _context.Centers.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
        public Center FindBy(string Name) => _context.Centers.FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);

        #endregion

        #region Create & Edit Center
        // Add | Update Center Without User
        public bool Save(Center model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.PauseState = (int)Helper.eCurrentState.Active;
                    _context.Centers.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    result.Description = model.Description;
                    result.ApplicationUserId = model.ApplicationUserId;
                    result.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.Centers.Update(result);
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

        #region Delete Center
        // Delete Center Without User
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.Centers.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Pause | Resume Center
        // Delete Center Without User
        public bool Pause(Guid Id)
        {
            try
            {
                LocationViewModel model = new()
                {
                    Locations = _context.Locations
                                        .Include(x => x.LoCenter)
                                        .Include(x => x.Episodes)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.LoCenterId == Id)
                                        .ToList()
                };
                EpisodeViewModel model2 = new()
                {
                    Episodes = _context.Episodes
                                        .Include(x => x.Students)
                                        .Include(x => x.ApplicationUser)
                                        .Include(x => x.EpLocation)
                                        .Include(x => x.EpisodeType)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.EpLocation.LoCenterId == Id)
                                        .ToList()
                };

                var result = FindBy(Id);
                result.Locations = model.Locations;
                result.PauseState = (int)Helper.ePauseState.Pause;
                foreach (var location in result.Locations)
                {
                    location.PauseState = (int)Helper.ePauseState.Pause;
                    location.Episodes = model2.Episodes;
                    foreach (var episode in location.Episodes)
                    {
                        episode.PauseState = (int)Helper.ePauseState.Pause;
                    }
                }
                _context.Centers.Update(result);
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
                LocationViewModel model = new()
                {
                    Locations = _context.Locations
                                        .Include(x => x.LoCenter)
                                        .Include(x => x.Episodes)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.LoCenterId == Id)
                                        .ToList()
                };
                EpisodeViewModel model2 = new()
                {
                    Episodes = _context.Episodes
                                        .Include(x => x.Students)
                                        .Include(x => x.ApplicationUser)
                                        .Include(x => x.EpLocation)
                                        .Include(x => x.EpisodeType)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.EpLocation.LoCenterId == Id)
                                        .ToList()
                };

                var result = FindBy(Id);
                result.Locations = model.Locations;
                result.PauseState = (int)Helper.ePauseState.Active;
                foreach (var location in result.Locations)
                {
                    location.PauseState = (int)Helper.ePauseState.Active;
                    location.Episodes = model2.Episodes;
                    foreach (var episode in location.Episodes)
                    {
                        episode.PauseState = (int)Helper.ePauseState.Active;
                    }
                }
                _context.Centers.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Center Details 
        public Center? GetById(Guid id)
        {
            return _context.Centers
                .Include(st => st.Locations)
                //.Include(st => st.Districts)
                .Include(st => st.ApplicationUser)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }
        public LocationViewModel GetLocationsForCenter(Guid centerid)
        {
            try
            {
                StudentViewModel svm = new();

                LocationViewModel locationViewModel = new LocationViewModel()
                {
                    Locations = _context.Locations
                                        .Include(x => x.LoCenter)
                                        .Include(x => x.Episodes)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.LoCenterId == centerid)
                                        .ToList()
                };
                foreach (var location in locationViewModel.Locations)
                {
                    svm.Students = _context.Students
                                       .Include(x => x.StEpisode)
                                       .Include(x => x.ApplicationUser)
                                       .Include(x => x.Demands)
                                       .Include(x => x.Evaluations)
                                       .Include(x => x.LessonData)
                                       .OrderBy(x => x.Name)
                                       .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocationId == location.Id)
                                       .ToList();
                    location.TotalStudents = svm.Students.Count().ToString();

                }

                return locationViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public EpisodeViewModel GetEpisodesForCenter(Guid centerid)
        {
            try
            {
                //StudentViewModel svm = new();
                EpisodeViewModel episodeViewModel = new EpisodeViewModel()
                {
                    Episodes = _context.Episodes
                                        .Include(x => x.EpLocation)
                                        .Include(x => x.Students)
                                        .Include(x => x.EpisodeType)
                                        .Include(x => x.ApplicationUser)
                                        .OrderBy(x => x.Name)
                                        .Where(x => x.CurrentState > 0 && x.EpLocation.LoCenterId == centerid)
                                        .ToList()
                };
                //foreach (var episode in episodeViewModel.Episodes)
                //{
                //    svm.Students = _context.Students
                //                       .Include(x => x.StEpisode)
                //                       .Include(x => x.StPlan)
                //                       .Include(x => x.ApplicationUser)
                //                       .Include(x => x.Demands)
                //                       .Include(x => x.Evaluations)
                //                       .Include(x => x.LessonData)
                //                       .Include(x => x.StLevel)
                //                       .OrderBy(x => x.Name)
                //                       .Where(x => x.CurrentState > 0 && x.StEpisodeId== episode.Id)
                //                       .ToList();
                //    episode.Description = svm.Students.Count().ToString();

                //}

                return episodeViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public StudentViewModel GetStudentsForCenter(Guid centerid)
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
                                        .Where(x => x.CurrentState > 0 && x.StEpisode.EpLocation.LoCenterId == centerid)
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
