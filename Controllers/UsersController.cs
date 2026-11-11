using ClosedXML.Excel;
using P = ZekrHakeem_v11.Constants.Permissions;

namespace ZekrHakeem_v11.Controllers
{
    /// <summary>
    /// شاشة إدارة المستخدمين. الصفحة نفسها خفيفة، وكل البيانات والعمليات
    /// تتم لحظيًا عبر SignalR (UsersAdminHub) من جهة السيرفر.
    /// </summary>
    [Authorize(Policy = P.Users.View)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserAdminService _users;
        private readonly IPermissionService _permissions;

        public UsersController(ApplicationDbContext context, IUserAdminService users, IPermissionService permissions)
        {
            _context = context;
            _users = users;
            _permissions = permissions;
        }

        public IActionResult Index(string? user = null, string? tab = null)
        {
            ViewData["OpenUser"] = user;
            ViewData["OpenTab"] = tab;
            return View();
        }

        [Authorize(Policy = P.Users.ViewActivity)]
        public IActionResult Activity() => View();

        // روابط قديمة من شاشات أخرى
        public IActionResult AddUser() => RedirectToAction(nameof(Index), new { user = "new" });
        public IActionResult EditUser(string userId) => RedirectToAction(nameof(Index), new { user = userId });
        public IActionResult ManageRoles(string userId) => RedirectToAction(nameof(Index), new { user = userId, tab = "roles" });

        /// <summary>صورة المستخدم (مع كاش في المتصفح)</summary>
        [AllowAnonymous]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> Avatar(string id)
        {
            if (User.Identity?.IsAuthenticated != true) return Unauthorized();
            var me = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (me != id && !await _permissions.HasPermissionAsync(User, P.Users.View)) return Forbid();

            var pic = await _context.Users.Where(u => u.Id == id).Select(u => u.ProfilePicture).FirstOrDefaultAsync();
            if (pic == null || pic.Length == 0) return NotFound();
            return File(pic, DetectImageType(pic));
        }

        private static string DetectImageType(byte[] b)
        {
            if (b.Length > 3 && b[0] == 0x89 && b[1] == 0x50) return "image/png";
            if (b.Length > 3 && b[0] == 0x47 && b[1] == 0x49) return "image/gif";
            if (b.Length > 11 && b[8] == 0x57 && b[9] == 0x45) return "image/webp";
            return "image/jpeg";
        }

        /// <summary>تصدير المستخدمين (حسب الفلاتر الحالية) إلى Excel</summary>
        public async Task<IActionResult> Export([FromQuery] UserQuery q)
        {
            q.Page = 1; q.PageSize = 100;
            var all = new List<UserListItemDto>();
            PagedResult<UserListItemDto> page;
            do
            {
                page = await _users.QueryAsync(q);
                all.AddRange(page.Items);
                q.Page++;
            } while (q.Page <= page.Pages);

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("المستخدمين");
            ws.RightToLeft = true;
            var headers = new[] { "الاسم الكامل", "اسم المستخدم", "البريد الإلكتروني", "الجوال", "رقم الهوية", "النوع", "المجموعات", "الحالة", "تاريخ الإنشاء", "آخر دخول" };
            for (int i = 0; i < headers.Length; i++) ws.Cell(1, i + 1).Value = headers[i];
            var r = 2;
            foreach (var u in all)
            {
                ws.Cell(r, 1).Value = u.FullName;
                ws.Cell(r, 2).Value = u.UserName;
                ws.Cell(r, 3).Value = u.Email;
                ws.Cell(r, 4).Value = u.PhoneNumber;
                ws.Cell(r, 5).Value = u.IdentityNum;
                ws.Cell(r, 6).Value = u.Gender == "male" ? "ذكر" : u.Gender == "female" ? "أنثى" : "";
                ws.Cell(r, 7).Value = string.Join("، ", u.Roles.Select(x => x.Display));
                ws.Cell(r, 8).Value = u.IsActive ? "مفعل" : "موقوف";
                ws.Cell(r, 9).Value = u.CreatedAt.ToLocalTime();
                ws.Cell(r, 10).Value = u.LastLoginAt?.ToLocalTime();
                r++;
            }
            var header = ws.Range(1, 1, 1, headers.Length);
            header.Style.Font.Bold = true;
            header.Style.Font.FontColor = XLColor.White;
            header.Style.Fill.BackgroundColor = XLColor.FromHtml("#0F5E4F");
            ws.Columns().AdjustToContents();
            ws.SheetView.FreezeRows(1);

            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Users_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
        }
    }
}
