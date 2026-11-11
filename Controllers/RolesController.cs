using P = ZekrHakeem_v11.Constants.Permissions;

namespace ZekrHakeem_v11.Controllers
{
    /// <summary>شاشة المجموعات والصلاحيات — البيانات والعمليات عبر SignalR</summary>
    [Authorize(Policy = P.Roles.View)]
    public class RolesController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Permissions(string id)
        {
            ViewData["RoleId"] = id;
            return View();
        }

        // رابط قديم
        public IActionResult ManagePermissions(string id) => RedirectToAction(nameof(Permissions), new { id });
    }
}
