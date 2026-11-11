#nullable disable

namespace ZekrHakeem_v11.Areas.Identity.Pages.Account.Manage
{
    public class ActivityModel : PageModel
    {
        private readonly IUserAdminService _users;
        public ActivityModel(IUserAdminService users) => _users = users;

        public List<ActivityDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Items = (await _users.GetActivityAsync(id, 1, 50)).Items;
        }
    }
}
