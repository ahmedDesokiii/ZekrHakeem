using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class TeachersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IServicesTeacher _servicesOfTeacher;


        public TeachersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IServicesTeacher servicesOfTeacher)
        {
            _userManager = userManager;
            _context = context;
            _servicesOfTeacher = servicesOfTeacher;
        }

        #region List Of Teachers [View]

        public IActionResult Teachers()
        {
            //Users Grouped
            var allusers = _context.Users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                IdentityNum = user.IdentityNum,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToList();
            var teachersMen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.Teacher.ToString())).ToList();
            var teachersWomen = allusers.Where(x => x.Roles.Select(role => role).Contains(Roles.TeacherWomen.ToString())).ToList();
            var teachers = teachersMen;
            teachers.AddRange(teachersWomen);
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            TeacherViewModel tvm = new()
            {
                // AllTeachers = _servicesOfTeacher.GetAllTeachers(),
                AllTeachers = teachers
                //TeachersMen = teachersMen,
                //TeachersWomen = teachersWomen

            };
            return View(tvm);
        }
        #endregion



        //#region Delete Center [Delete & Delete Log]
        ////Delete
        //[Authorize(Permissions.Centers.Delete)]
        //public IActionResult Delete(Guid Id)
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (_servicesCenter.Delete(Id) && _servicesCenterLog.Delete(Id, Guid.Parse(userId)))
        //        return RedirectToAction(nameof(Centers));
        //    return RedirectToAction(nameof(Centers));

        //}
        ////Delete Log
        //[Authorize(Permissions.Centers.DeleteLog)]
        //public IActionResult DeleteLog(Guid Id)
        //{
        //    if (_servicesCenterLog.DeleteLog(Id))
        //        return RedirectToAction(nameof(Centers));
        //    return RedirectToAction(nameof(Centers));
        //}
        //#endregion

        //#region Add|Edit Center [Create & Update]
        //[Authorize(Permissions.Centers.Create), Authorize(Permissions.Centers.Edit)]
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult Save(CenterViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var userId = _userManager.GetUserId(User);

        //        if (model.NewCenter.Id == Guid.Parse(Guid.Empty.ToString()))
        //        { //Create
        //            //Exist
        //            if (_servicesCenter.FindBy(model.NewCenter.Name) != null)
        //                SessionMsg(Helper.Error, "Exist !", "Center is Exist");

        //            else
        //            {
        //                if (_servicesCenter.Save(model.NewCenter)
        //                    && _servicesCenterLog.Save(model.NewCenter.Id, Guid.Parse(userId)))
        //                    SessionMsg(Helper.Success, "Done !", "Center Added Successfully");
        //                else
        //                    SessionMsg(Helper.Error, "Not Saved", "Center Not Added !");
        //            }
        //        }
        //        else
        //        { //Update
        //            if (_servicesCenter.Save(model.NewCenter)
        //                    && _servicesCenterLog.Update(model.NewCenter.Id, Guid.Parse(userId)))
        //                SessionMsg(Helper.Success, "Center Update", "Center is Updated Successfully");
        //            else
        //                SessionMsg(Helper.Error, "Center Not Update", "Center isn't Updated !");

        //        }
        //    }
        //    return RedirectToAction(nameof(Centers));

        //}
        //#endregion

        //#region Details Of Center [List Of Locations]
        //[HttpGet]
        //public IActionResult SelectedCenter(Guid id)
        //{
        //    CenterViewModel CVM = new();
        //    {
        //        CVM.NewCenter = _servicesOfCenter.GetById(id);
        //        CVM.LocationsOfCenter = _servicesOfCenter.GetLocationsForCenter(id);
        //        CVM.EpisodesOfCenter = _servicesOfCenter.GetEpisodesForCenter(id);
        //        CVM.StudentsOfCenter = _servicesOfCenter.GetStudentsForCenter(id);
        //    }
        //    ;
        //    return View(CVM);
        //}
        //#endregion
    }
}
