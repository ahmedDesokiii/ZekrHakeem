namespace ZekrHakeem_v11.IRepository.ServicesRepository
{
    public class ServicesDemand : IServicesRepository<Demand>, IServicesDemand
    {
        private readonly ApplicationDbContext _context;

        public ServicesDemand(ApplicationDbContext context)
        {
            _context = context;
        }

        #region AllDemands
        // Get All Demands | admin role
        public List<Demand> GetAll()
        {
            try
            {
                return _context.Demands
                            .Include(x => x.Student)
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.Student.StEpisode)
                            .Include(x => x.Student.StEpisode.EpLocation)
                            .Include(x => x.Student.StEpisode.EpLocation.LoCenter)
                            .OrderBy(x => x.IsOpened).ThenByDescending(x => x.Request_Date)
                            .Where(x => x.CurrentState > 0)
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Demands For Men
        //Get Men Demands | Supervisor men
        //TODO : get list of Demands where Demand type is men 

        public List<Demand> GetDemandsForMen()
        {
            try
            {
                return _context.Demands
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.Student)
                            .Include(x => x.Student.StEpisode)
                            .Include(x => x.Student.StEpisode.EpLocation)
                            .Include(x => x.Student.StEpisode.EpLocation.LoCenter)
                            .OrderBy(x => x.IsOpened).ThenByDescending(x => x.Request_Date)
                            .Where(x => x.CurrentState > 0 && x.Student.StEpisode.EpLocation.LoCenter.Type == "men".ToLower())
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Demand> GetDemandsForMen_Notify()
        {
            try
            {
                return _context.Demands
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.Student)
                            .Include(x => x.Student.StEpisode)
                            .Include(x => x.Student.StEpisode.EpLocation)
                            .Include(x => x.Student.StEpisode.EpLocation.LoCenter)
                            .OrderBy(x => x.IsOpened).ThenByDescending(x => x.Request_Date)
                            .Where(x => x.CurrentState > 0 && x.IsOpened == false && x.Student.StEpisode.EpLocation.LoCenter.Type == "men".ToLower())
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Demands For Women
        //Get Women Demands | Supervisor Women
        //TODO : get list of Demands where Demand type is women
        public List<Demand> GetDemandsForWomen()
        {
            try
            {
                return _context.Demands
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.Student)
                            .Include(x => x.Student.StEpisode)
                            .Include(x => x.Student.StEpisode.EpLocation)
                            .Include(x => x.Student.StEpisode.EpLocation.LoCenter)
                            .OrderBy(x => x.IsOpened).ThenByDescending(x => x.Request_Date)
                            .Where(x => x.CurrentState > 0 && x.Student.StEpisode.EpLocation.LoCenter.Type == "women".ToLower())
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Demand> GetDemandsForWomen_Notify()
        {
            try
            {
                return _context.Demands
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.Student)
                            .Include(x => x.Student.StEpisode)
                            .Include(x => x.Student.StEpisode.EpLocation)
                            .Include(x => x.Student.StEpisode.EpLocation.LoCenter)
                            .OrderBy(x => x.IsOpened).ThenByDescending(x => x.Request_Date)
                            .Where(x => x.CurrentState > 0 && x.IsOpened == false && x.Student.StEpisode.EpLocation.LoCenter.Type == "women".ToLower())
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Demands with Teacher
        // Demands with Supervisor Demand men
        //TODO : get list of Demands where Demand type is men && ApplicationUserId = supervisorDemandId 

        public List<Demand> GetDemandsWithTeacher(string userid)
        {
            try
            {
                return _context.Demands
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.Student)
                            .Include(x => x.ApplicationUser)
                            .OrderBy(x => x.IsOpened).ThenByDescending(x => x.Request_Date)
                            .Where(x => x.ApplicationUserId == userid)
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Demand> GetDemandsWithTeacher_Notify(string userid)
        {
            try
            {
                return _context.Demands
                            .Include(x => x.ApplicationUser)
                            .Include(x => x.Student)
                            .Include(x => x.ApplicationUser)
                            .OrderBy(x => x.IsOpened).ThenByDescending(x => x.Request_Date)
                            .Where(x => x.ApplicationUserId == userid && x.IsOpened == false)
                            .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }



        #endregion

        #region Find Demand by ...
        //FindDemandBy => Id | Name | Type
        public Demand FindBy(Guid Id) => _context.Demands.FirstOrDefault(x => x.Id.Equals(Id));
        public Student FindStudentBy(Guid stId) => _context.Students.FirstOrDefault(x => x.Id.Equals(stId));
        public Episode FindEpisodetBy(Guid epId) => _context.Episodes.FirstOrDefault(x => x.Id.Equals(epId));
        public Demand FindBy(string Type) => _context.Demands.FirstOrDefault(x => x.Request_Type.Equals(Type.Trim()));
        public ApplicationUser FindAppUserBy(string Id) => _context.Users.FirstOrDefault(x => x.Id.Equals(Id));

        #endregion

        #region Create & Edit Demand
        // Add | Update Demand Without User
        public bool Save(Demand model)
        {
            //try
            //{
            //    var result = FindBy(model.Id);
            //    if (result == null)
            //    {
            //        model.Id = Guid.NewGuid();
            //        _context.Demands.Add(model);
            //    }
            //    else
            //    {
            //        result.Request_Date = model.Request_Date;
            //        result.Request_Type = model.Request_Type;
            //        result.Request_Reason = model.Request_Reason;
            //        result.ApplicationUserId = model.ApplicationUserId;

            //        _context.Demands.Update(result);
            //    }
            //    _context.SaveChanges();
            //    return true;
            //}
            //catch (Exception)
            //{}
            return false;

        }
        #endregion

        #region Requests 
        // طلب اذن
        public bool AddAskRequest(Demand model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.Request_Type = "طلب إذن";
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.StartHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    model.EndHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    model.IsAccepted = false;
                    model.IsOpened = false;
                    model.Student = FindStudentBy((Guid)model.StudentId);
                    var resEp = FindEpisodetBy((Guid)model.Student.StEpisodeId);
                    model.ApplicationUserId = resEp.ApplicationUserId;
                    model.OldEpisode = model.NewEpisode;
                    model.NotificationText = $"طلب إذن - {model.Student.Name}";
                    model.StName = model.Student.Name;
                    model.StIdentityNum = model.Student.IdentityNumber;
                    model.StParPhoneNum = model.Student.ParentPhoneNumber;
                    model.StAdress = model.Student.Adress;
                    model.StNotes = model.Student.Notes;
                    _context.Demands.Add(model);
                }
                else
                {
                    result.Request_Date = model.Request_Date;
                    result.Request_Type = model.Request_Type;
                    result.Request_Reason = model.Request_Reason;
                    result.ApplicationUserId = model.ApplicationUserId;

                    _context.Demands.Update(result);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // طلب شكوي
        public bool AddComplaintRequest(Demand model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.Request_Type = "طلب شكوي";
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.StartHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    model.EndHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    model.IsAccepted = false;
                    model.IsOpened = false;
                    model.Student = FindStudentBy((Guid)model.StudentId);
                    model.OldEpisode = model.NewEpisode;
                    model.NotificationText = $"طلب شكوي - {model.Student.Name}";
                    model.StName = model.Student.Name;
                    model.StIdentityNum = model.Student.IdentityNumber;
                    model.StParPhoneNum = model.Student.ParentPhoneNumber;
                    model.StAdress = model.Student.Adress;
                    model.StNotes = model.Student.Notes;
                    _context.Demands.Add(model);
                }
                else
                {
                    result.Request_Date = model.Request_Date;
                    result.Request_Type = model.Request_Type;
                    result.Request_Reason = model.Request_Reason;
                    result.ApplicationUserId = model.ApplicationUserId;

                    _context.Demands.Update(result);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // طلب تعديل بيانات
        public bool AddEditPersonalRequest(Demand model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.Request_Type = "طلب تعديل بيانات";
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.StartHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    model.EndHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    model.IsAccepted = false;
                    model.IsOpened = false;
                    model.Student = FindStudentBy((Guid)model.StudentId);

                    var studentRes = FindStudentBy((Guid)model.StudentId);
                    studentRes.Name = model.StName;
                    studentRes.IdentityNumber = model.StIdentityNum;
                    studentRes.ParentPhoneNumber = model.StParPhoneNum;
                    studentRes.Adress = model.StAdress;
                    studentRes.Notes = model.StNotes;

                    model.NotificationText = $"طلب تعديل بيانات - {model.Student.Name}";

                    var resEp = FindEpisodetBy((Guid)model.Student.StEpisodeId);
                    model.NewEpisode = resEp.Name;
                    model.OldEpisode = resEp.Name;


                    if (model.Request_Reason == null) { model.Request_Reason = ""; }

                    _context.Demands.Add(model);
                    _context.Students.Update(studentRes);
                }
                else
                {
                    result.Request_Date = model.Request_Date;
                    result.Request_Type = model.Request_Type;
                    result.Request_Reason = model.Request_Reason;
                    result.ApplicationUserId = model.ApplicationUserId;

                    _context.Demands.Update(result);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            { return false; }


        }
        // طلب اجازة
        public bool AddHolidayRequest(Demand model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.Request_Type = "طلب اجازة";
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.StartHoliday = Convert.ToDateTime(Convert.ToDateTime(model.StartHoliday).ToString("yyyy-MM-dd"));
                    model.EndHoliday = Convert.ToDateTime(Convert.ToDateTime(model.EndHoliday).ToString("yyyy-MM-dd"));

                    model.IsAccepted = false;
                    model.IsOpened = false;
                    model.Student = FindStudentBy((Guid)model.StudentId);
                    var resEp = FindEpisodetBy((Guid)model.Student.StEpisodeId);
                    model.ApplicationUserId = resEp.ApplicationUserId;
                    model.OldEpisode = model.NewEpisode;
                    model.NotificationText = $"طلب اجازة - {model.Student.Name}";
                    model.StName = model.Student.Name;
                    model.StIdentityNum = model.Student.IdentityNumber;
                    model.StParPhoneNum = model.Student.ParentPhoneNumber;
                    model.StAdress = model.Student.Adress;
                    model.StNotes = model.Student.Notes;
                    _context.Demands.Add(model);
                }
                else
                {
                    result.Request_Date = model.Request_Date;
                    result.Request_Type = model.Request_Type;
                    result.Request_Reason = model.Request_Reason;
                    result.ApplicationUserId = model.ApplicationUserId;

                    _context.Demands.Update(result);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // طلب نقل
        public bool AddTransferRequest(Demand model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.Request_Type = "طلب نقل";
                    model.CurrentState = (int)Helper.eCurrentState.Active;
                    model.StartHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    model.EndHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    model.IsAccepted = false;
                    model.IsOpened = false;
                    model.Student = FindStudentBy((Guid)model.StudentId);

                    model.NewEpisode = FindEpisodetBy(Guid.Parse(model.NewEpisode)).Name;
                    model.NotificationText = $"طلب نقل - {model.Student.Name}";
                    model.StName = model.Student.Name;
                    model.StIdentityNum = model.Student.IdentityNumber;
                    model.StParPhoneNum = model.Student.ParentPhoneNumber;
                    model.StAdress = model.Student.Adress;
                    model.StNotes = model.Student.Notes;
                    _context.Demands.Add(model);
                }
                else
                {
                    result.Request_Date = model.Request_Date;
                    result.Request_Type = model.Request_Type;
                    result.Request_Reason = model.Request_Reason;
                    result.ApplicationUserId = model.ApplicationUserId;

                    _context.Demands.Update(result);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // طلب اضافة طالب جديد
        public bool AddNewStudentRequest(Student model)
        {
            try
            {
                var result = FindBy(model.Id);
                if (result == null) // create new student
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentState = (int)Helper.eCurrentState.Delete;
                    model.PlanDataId = Guid.Parse("64944ffb-f494-4141-b93c-e8ec32194a8b"); // الذكر الحكيم
                    model.LessonDataId = 1;
                    model.VerseId = 6231;
                    model.SurahId = 114;
                    model.newLessonState = false;
                    model.ApplicationUserId = null;
                    model.SaveDirection = "ptc";

                    //Demand
                    var demand = new Demand();
                    demand.Id = Guid.NewGuid();
                    demand.Request_Type = "طلب إضافة طالب جديد";
                    demand.CurrentState = (int)Helper.eCurrentState.Active;
                    demand.StartHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    demand.EndHoliday = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    demand.IsAccepted = false;
                    demand.IsOpened = false;
                    demand.Student = model;

                    demand.NewEpisode = FindEpisodetBy(model.StEpisodeId).Name;
                    demand.NotificationText = $"طلب إضافة طالب جديد - {model.Name}";
                    demand.StName = model.Name;
                    demand.StIdentityNum = model.IdentityNumber;
                    demand.StParPhoneNum = model.ParentPhoneNumber;
                    demand.StAdress = model.Adress;
                    demand.StNotes = model.Notes;

                    _context.Students.Add(model);
                    _context.Demands.Add(demand);
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

        #region Delete Demand
        // Delete Demand Without User
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);

                _context.Demands.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion

        #region Demand Details
        public Demand? GetById(Guid id)
        {
            return _context.Demands
                .Include(st => st.Student)
                .Include(st => st.ApplicationUser)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }
        public List<Demand> GetStudentDemands(Guid? stid)
        {
            return _context.Demands
                .Include(st => st.Student)
                .Include(st => st.ApplicationUser)
                .Where(g => g.StudentId == stid)
                .ToList();
        }
        public void OpenDemand(Demand model)
        {
            var result = FindBy(model.Id);
            if (result != null)
            {
                result.IsOpened = true;
                result.IsAccepted = true;
                _context.Demands.Update(result);
                _context.SaveChanges();
            }
        }
        #endregion

        #region Accept New Student Requet
        //AcceptNewStudentRequest
        public bool AcceptedNewStudent(Guid id)
        {
            try
            {
                var result = FindStudentBy(id);
                if (result != null)
                {
                    result.CurrentState = (int)Helper.eCurrentState.Active;

                    _context.Students.Update(result);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}
