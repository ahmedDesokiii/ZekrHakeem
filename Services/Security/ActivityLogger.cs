namespace ZekrHakeem_v11.Services.Security
{
    public interface IActivityLogger
    {
        Task LogAsync(string action, string? userId, string? details = null, bool save = true);
    }

    public class ActivityLogger : IActivityLogger
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _http;
        private readonly ILogger<ActivityLogger> _logger;

        public ActivityLogger(ApplicationDbContext db, IHttpContextAccessor http, ILogger<ActivityLogger> logger)
        {
            _db = db;
            _http = http;
            _logger = logger;
        }

        public async Task LogAsync(string action, string? userId, string? details = null, bool save = true)
        {
            try
            {
                var ctx = _http.HttpContext;
                var actor = ctx?.User;
                var ua = ctx?.Request.Headers.UserAgent.ToString();
                _db.UserActivities.Add(new UserActivity
                {
                    Action = action,
                    UserId = userId,
                    ActorId = actor?.FindFirstValue(ClaimTypes.NameIdentifier),
                    ActorName = actor?.Identity?.Name,
                    Details = details?.Length > 1000 ? details[..1000] : details,
                    IpAddress = ctx?.Connection.RemoteIpAddress?.ToString(),
                    UserAgent = ua?.Length > 400 ? ua[..400] : ua,
                    CreatedAt = DateTime.UtcNow
                });
                if (save) await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // سجل النشاط لا يجب أن يوقف العملية الأساسية
                _logger.LogWarning(ex, "Failed to write activity log {Action}", action);
            }
        }
    }
}
