
namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesDistrict : IServicesRepository<District>, IServicesDistrict
    {
        private readonly ApplicationDbContext _context;
        public ServicesDistrict(ApplicationDbContext context)
        {
            _context = context;
        }

        #region All Districts
        public List<District> GetAll()
        {
            try
            {
                return _context.Districts

                               .Include(l => l.Locations)
                               .OrderBy(x => x.Name)
                               .Where(x => x.CurrentState > 0)
                               .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //public List<District> GetDistrictsForCenter(Guid centerId)
        //{
        //    try
        //    {
        //        return _context.Districts
        //                       .Include(l => l.Center)
        //                       .Include(l => l.Locations)
        //                       .OrderBy(x => x.Center.Type)
        //                       .ThenBy(x => x.Center.Name)
        //                       .ThenBy(x => x.Name)
        //                       .Where(x => x.CurrentState > 0 && x.CenterId == centerId)
        //                       .ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        #endregion

        #region Find District By ...
        public District FindBy(Guid Id)
        {
            try
            {
                return _context.Districts.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public District FindBy(string Name)
        {
            try
            {
                return _context.Districts.FirstOrDefault(x => x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }


        #endregion

        #region Create & Update District
        public bool Save(District model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.Districts.Add(model);
                }
                else
                {
                    result.Name = model.Name;
                    result.Description = model.Description;
                    //result.CenterId = model.CenterId;
                    result.CurrentState = (int)Helper.eCurrentState.Active;
                    _context.Districts.Update(result);
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

        #region Delete District
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.CurrentState = (int)Helper.eCurrentState.Delete;
                _context.Districts.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region District Details 
        public District? GetById(Guid id)
        {
            return _context.Districts
                .Include(st => st.Locations)

                .SingleOrDefault(g => g.Id == id);
        }
        #endregion

    }
}
