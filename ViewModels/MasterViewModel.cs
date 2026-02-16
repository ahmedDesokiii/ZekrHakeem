
namespace ZekrHakeem_v11.ViewModels
{
    public class MasterViewModel
    {
        public bool IsUserModalOpen { get; set; }
        public CenterViewModel CenterVM { get; set; }
        public LocationViewModel LocationVM { get; set; }
        public DemandViewModel DemandVM { get; set; }
        public DistrictViewModel DistrictVM { get; set; }
        public EpisodeViewModel EpisodeVM { get; set; }
        public PlanViewModel PlanVM { get; set; }
        public LevelViewModel LevelVM { get; set; }
        public ProgViewModel ProgVM { get; set; }
        public EpisodeProgViewModel EpProgVM { get; set; }
        public StudentProgViewModel StProgVM { get; set; }
        public StudentViewModel StudentVM { get; set; }
        public ReportViewModel ReportVM { get; set; }
        public PermissionsFormViewModel PermissionVM { get; set; }
        public IEnumerable<UserViewModel> UserVM { get; set; }
        public List<IdentityRole>? RoleVM { get; set; }
        public RoleFormViewModel AddRoleVM { get; set; }
        public AccessDeniedModel AccessDeniedVM { get; set; }

        public AddUserViewModel AddUserVM { get; set; }
        public CheckBoxViewModel CheckBoxVM { get; set; }
        public ProfileFormViewModel ProfileFormVM { get; set; }
        public UserRolesViewModel UserRolesVM { get; set; }
        ///// Area ////////////////////
        public ForgotPasswordModel ForgotPasswordVM { get; set; }





    }
}
