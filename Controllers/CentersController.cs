using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ZekrHakeem_v11.Data;
using ZekrHakeem_v11.IRepository.ServicesRepository;
using ZekrHakeem_v11.Models;
using ZekrHakeem_v11.ViewModels;

namespace ZekrHakeem_v11.Controllers
{
    public class CentersController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesRepository<Center> _servicesCenter;
        private readonly IServicesRepositoryLog<LogCenter> _servicesCenterLog;
        private readonly IServicesCenter _servicesOfCenter;
        private readonly IServicesLocation _servicesOfLoction;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CentersController(
            IServicesRepository<Demand> servicesDemand,
            IServicesDemand servicesOfDemand,
            IServicesRepository<Center> servicesCenter,
            IServicesRepositoryLog<LogCenter> servicesCenterLog,
            IServicesCenter servicesOfCenter,
            IServicesLocation servicesOfLoction,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _servicesCenter = servicesCenter;
            _servicesCenterLog = servicesCenterLog;
            _servicesOfCenter = servicesOfCenter;
            _servicesOfLoction = servicesOfLoction;
            _userManager = userManager;
            _context = context;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #region List Of Centers [View]

        [Authorize(Permissions.Centers.View)]
        public async Task<IActionResult> Centers()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // ==============================
            // Supervisors
            // ==============================

            var supervisorsMen = await _userManager
                .GetUsersInRoleAsync(Roles.SupervisorCenterMen.ToString());

            var supervisorsWomen = await _userManager
                .GetUsersInRoleAsync(Roles.SupervisorCenterWomen.ToString());

            // ==============================
            // Centers Query Optimized
            // ==============================

            var centers = await _context.Centers
                .AsNoTracking()
                .Include(x => x.ApplicationUser)
                .Include(x => x.Locations)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            // ==============================
            // Role Filtering
            // ==============================

            List<Center> filteredCenters = centers;

            if (User.IsInRole(Roles.Supervisor.ToString()))
            {
                filteredCenters = centers
                    .Where(x => x.Type == "men")
                    .ToList();
            }
            else if (User.IsInRole(Roles.SupervisorWomen.ToString()))
            {
                filteredCenters = centers
                    .Where(x => x.Type == "women")
                    .ToList();
            }

            // ==============================
            // ViewBag Permissions
            // ==============================

            ViewBag.CanViewDetails = User.HasClaim("Permission", Permissions.Centers.ViewDetails);
            ViewBag.CanCreate = User.HasClaim("Permission", Permissions.Centers.Create);
            ViewBag.CanEdit = User.HasClaim("Permission", Permissions.Centers.Edit);
            ViewBag.CanDelete = User.HasClaim("Permission", Permissions.Centers.Delete);
            ViewBag.CanPause = User.HasClaim("Permission", Permissions.Centers.Pause);

            // ==============================
            // Model
            // ==============================

            MasterViewModel master = new()
            {
                CenterVM = new()
                {
                    Centers = filteredCenters,
                    LogCenters = await _context.LogCenters
                        .AsNoTracking()
                        .ToListAsync(),

                    NewCenter = new Center(),

                    SupervisorsMenCenter = supervisorsMen
                        .Select(x => new SelectListItem
                        {
                            Value = x.Id,
                            Text = x.FullName
                        }),

                    SupervisorsWomenCenter = supervisorsWomen
                        .Select(x => new SelectListItem
                        {
                            Value = x.Id,
                            Text = x.FullName
                        }),

                    CentersForSupervisorCenter = centers
                        .Where(x => !string.IsNullOrEmpty(x.ApplicationUserId)
                                 && x.ApplicationUserId == userId)
                        .ToList()
                },

                DemandVM = new()
                {
                    NewDemand = new Demand()
                }
            };

            return View(master);
        }

        #endregion
    
#region Pause | Resume Center [Pause & Resume]
[Authorize(Permissions.Centers.Pause)]
        public IActionResult Pause(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfCenter.Pause(Id))
                return RedirectToAction("Centers", "Centers");
            return RedirectToAction("Centers", "Centers");

        }
        [Authorize(Permissions.Centers.Resume)]
        public IActionResult Resume(Guid Id)
        {
            //var userId = _userManager.GetUserId(User);
            if (_servicesOfCenter.Resume(Id))
                return RedirectToAction("Centers", "Centers");
            return RedirectToAction("Centers", "Centers");

        }
        #endregion

        #region Delete Center [Delete & Delete Log]
        //Delete
        [Authorize(Permissions.Centers.Delete)]
        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesCenter.Delete(Id) && _servicesCenterLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction("Centers", "Centers");
            return RedirectToAction("Centers", "Centers");

        }
        //Delete Log
        [Authorize(Permissions.Centers.DeleteLog)]
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesCenterLog.DeleteLog(Id))
                return RedirectToAction("Centers", "Centers");
            return RedirectToAction("Centers", "Centers");
        }
        #endregion

        #region Add|Edit Center [Create & Update]
        [Authorize(Permissions.Centers.Create), Authorize(Permissions.Centers.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.CenterVM.NewCenter.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                    //Exist
                    if (_servicesCenter.FindBy(model.CenterVM.NewCenter.Name) != null)
                        SessionMsg(Helper.Error, "مركز مكرر !", "اسم المركز موجود من قبل");

                    else
                    {
                        if (_servicesCenter.Save(model.CenterVM.NewCenter)
                            && _servicesCenterLog.Save(model.CenterVM.NewCenter.Id, Guid.Parse(userId)))
                            SessionMsg(Helper.Success, "تم الإضافة !", "تم اضافة المركز بنجاح ");
                        else
                            SessionMsg(Helper.Error, "خطأ في الإضافة", "حدوث خطأ اثناء ادخال بعض البيانات !");
                    }
                }
                else
                { //Update
                    if (_servicesCenter.Save(model.CenterVM.NewCenter)
                            && _servicesCenterLog.Update(model.CenterVM.NewCenter.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, "تم التعديل", "تم تعديل بيانات المركز بنجاح !");
                    else
                        SessionMsg(Helper.Error, "مشكلة في التعديل", "! حدوث خطأ اثناء تعديل بعض البيانات");

                }
            }
            return RedirectToAction("Centers", "Centers");

        }
        #endregion

        #region Details Of Center [Dashboard]
        [HttpGet]
        public IActionResult SelectedCenter(Guid id)
        {
            var center = _servicesOfCenter.GetById(id);

            if (center == null)
                return NotFound();

            MasterViewModel master = new()
            {
                CenterVM = new()
                {
                    NewCenter = center,
                    LocationsOfCenter = _servicesOfCenter.GetLocationsForCenter(id),
                    EpisodesOfCenter = _servicesOfCenter.GetEpisodesForCenter(id),
                    StudentsOfCenter = _servicesOfCenter.GetStudentsForCenter(id)
                }
            };

            return View(master);
        }
        #endregion

        #region Details Of Every Location in Center [List Of Episodes]
        [HttpGet]
        public IActionResult SelectedLocationCenter(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MasterViewModel master = new()
            {
                LocationVM = new()
                {
                    NewLocation = _servicesOfLoction.GetById(id),
                    EpisodesOfLocation = _servicesOfLoction.GetEpisodesForLocation(id),
                    StudentsOfLocation = _servicesOfLoction.GetStudentsForLocation(id),
                },
                DemandVM = new()
                {
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                    NewDemand = new Demand(),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId)
                }
            };
            return View(master);
        }
        #endregion
    }
}
