using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly IServicesRepository<District> _servicesDistrict;
        private readonly IServicesRepository<Center> _servicesCenter;
        private readonly IServicesDistrict _servicesOfDistrict;
        private readonly IServicesCenter _servicesOfCenter;


        public DistrictsController(IServicesRepository<Demand> servicesDemand, IServicesDemand servicesOfDemand, IServicesRepository<District> servicesDistrict, IServicesRepository<Center> servicesCenter, IServicesDistrict servicesOfDistrict, IServicesCenter servicesOfCenter)
        {
            _servicesDemand = servicesDemand;
            _servicesOfDemand = servicesOfDemand;
            _servicesDistrict = servicesDistrict;
            _servicesCenter = servicesCenter;
            _servicesOfDistrict = servicesOfDistrict;
            _servicesOfCenter = servicesOfCenter;
        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #region List Of Districts [View]
        [Authorize(Permissions.Districts.View)]
        public IActionResult Districts()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MasterViewModel master = new()
            {
                DistrictVM = new DistrictViewModel
                {
                    Districts = _servicesDistrict.GetAll(),
                    Centers = _servicesCenter.GetAll(),
                    CentersForMen = _servicesOfCenter.GetCentersForMen(),
                    CentersForWomen = _servicesOfCenter.GetCentersForWomen(),
                    NewDistrict = new District()
                },
                DemandVM = new()
                {
                    NewDemand = new Demand(),
                    Demands = _servicesDemand.GetAll(),
                    DemandsForMen = _servicesOfDemand.GetDemandsForMen(),
                    DemandsForWomen = _servicesOfDemand.GetDemandsForWomen(),
                    DemandsForTeacher = _servicesOfDemand.GetDemandsWithTeacher(userId),
                    DemandsForMen_Notify = _servicesOfDemand.GetDemandsForMen_Notify(),
                    DemandsForWomen_Notify = _servicesOfDemand.GetDemandsForWomen_Notify(),
                    DemandsForTeacher_Notify = _servicesOfDemand.GetDemandsWithTeacher_Notify(userId)
                }

            };
            return View(master);
        }
        #endregion

        #region Delete District [Delete]
        [Authorize(Permissions.Districts.Delete)]
        public IActionResult Delete(Guid Id)
        {
            if (_servicesDistrict.Delete(Id))
                return RedirectToAction("Districts", "Districts");
            return RedirectToAction("Districts", "Districts");

        }
        #endregion

        #region Add|Edit District [Create & Update]
        [Authorize(Permissions.Districts.Create), Authorize(Permissions.Districts.Edit)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(MasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.DistrictVM.NewDistrict.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Create
                    //Exist
                    if (_servicesDistrict.FindBy(model.DistrictVM.NewDistrict.Name) != null)
                        SessionMsg(Helper.Error, "حي مكرر", "اسم الحي موجود من قبل");
                    else
                    {
                        if (_servicesDistrict.Save(model.DistrictVM.NewDistrict))
                            SessionMsg(Helper.Success, "تم الاضافة  !", "تم اضافة الحي بنجاح");
                        else
                            SessionMsg(Helper.Error, "خطأ في الاضافة", "حدث خطأ اثناء عملية اضافة الحي بسبب خطأ في البيانات");
                    }
                }
                else
                { //Update
                    if (_servicesDistrict.Save(model.DistrictVM.NewDistrict))
                        SessionMsg(Helper.Success, "تم التعديل", "تم تعديل بيانات الحي بنجاح");
                    else
                        SessionMsg(Helper.Error, "خطأ في التعديل", "حدث خطأ اثناء عملية تعديل الحي بسبب خطأ في البيانات!");

                }
            }

            return RedirectToAction("Districts", "Districts");
        }
        #endregion

    }
}
