namespace ZekrHakeem_v11.Filters
{
    /// <summary>
    /// التحقق من الصلاحية يتم الآن من قاعدة البيانات (RolePermissions + UserPermissions)
    /// بدلاً من الـ Claims الثابتة، مع كاش وإلغاء فوري عند التعديل.
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissions;

        public PermissionAuthorizationHandler(IPermissionService permissions)
        {
            _permissions = permissions;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null) return;
            if (await _permissions.HasPermissionAsync(context.User, requirement.Permission))
                context.Succeed(requirement);
        }
    }
}
