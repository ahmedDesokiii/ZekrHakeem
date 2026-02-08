namespace ZekrHakeem_v11.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; }
        public string IdentityNum { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }


    }
}