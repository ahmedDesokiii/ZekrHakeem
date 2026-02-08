using ZekrHakeem_v11.IRepository.ServicesRepository;

namespace ZekrHakeem_v11.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IServicesRepository<Episode> _servicesEpisode;
        private readonly IServicesRepository<Demand> _servicesDemand;
        private readonly IServicesEpisode _servicesOfEpisode;
        private readonly IServicesDemand _servicesOfDemand;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public HomeController(ILogger<HomeController> logger, IServicesRepository<Episode> servicesEpisode, IServicesRepository<Demand> servicesDemand, IServicesEpisode servicesOfEpisode, IServicesDemand servicesOfDemand, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _servicesEpisode = servicesEpisode;
            _servicesDemand = servicesDemand;
            _servicesOfEpisode = servicesOfEpisode;
            _servicesOfDemand = servicesOfDemand;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MasterViewModel master = new()
            {
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


            //TotalViewModel model = new TotalViewModel();
            ////model.total_Centers = _context.Centers.Count();
            ////model.total_Episodes = _context.Episodes.Count();
            ////model.total_Locations = _context.Locations.Count();
            ////model.total_Students = _context.Students.Count();
            //model.total_Teachers = 50;
            //model.total_Centers = 0;
            //model.total_Episodes = 0;
            //model.total_Locations = 0;
            //model.total_Students = 0;


            return View(master);
        }


        [HttpPost]
        public IActionResult Index(string selectedDate)
        {

            ViewBag.Message = selectedDate;
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
