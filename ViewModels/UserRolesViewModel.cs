namespace ZekrHakeem_v11.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; } = string.Empty;
        public List<CheckBoxViewModel>? Roles { get; set; }


    }
}