using Microsoft.AspNetCore.SignalR;
using P = ZekrHakeem_v11.Constants.Permissions;

namespace ZekrHakeem_v11.Hubs
{
    /// <summary>
    /// كل عمليات شاشات إدارة المستخدمين والمجموعات تتم من خلال هذا الـ Hub (Server side)
    /// وأي تغيير يُبث لحظيًا لكل المسؤولين المفتوحين على نفس الشاشات.
    /// </summary>
    [Authorize]
    public class UsersAdminHub : Hub
    {
        private readonly IUserAdminService _users;
        private readonly IRoleAdminService _roles;
        private readonly IPermissionService _permissions;

        public UsersAdminHub(IUserAdminService users, IRoleAdminService roles, IPermissionService permissions)
        {
            _users = users;
            _roles = roles;
            _permissions = permissions;
        }

        private string ActorId => Context.UserIdentifier ?? string.Empty;

        private async Task Require(string permission)
        {
            if (!await _permissions.HasPermissionAsync(Context.User!, permission))
                throw new HubException("ليس لديك صلاحية لتنفيذ هذا الإجراء");
        }

        /// <summary>صلاحيات المستخدم الحالي على الشاشة (لإظهار/إخفاء الأزرار)</summary>
        public async Task<Dictionary<string, bool>> GetMyCapabilities()
        {
            var keys = new[] { P.Users.View, P.Users.Create, P.Users.Edit, P.Users.Delete, P.Users.Lock, P.Users.ResetPassword,
                               P.Users.ManageRoles, P.Users.ManagePermissions, P.Users.ViewActivity,
                               P.Roles.View, P.Roles.Create, P.Roles.Edit, P.Roles.Delete, P.Roles.ManagePermissions };
            var result = new Dictionary<string, bool>();
            foreach (var k in keys)
                result[k.Replace("Permissions.", "")] = await _permissions.HasPermissionAsync(Context.User!, k);
            return result;
        }

        // ============================ Users
        public async Task<UsersStatsDto> GetStats() { await Require(P.Users.View); return await _users.GetStatsAsync(); }
        public async Task<List<RoleChipDto>> GetRoleChips() { await Require(P.Users.View); return await _users.GetRoleChipsAsync(); }
        public async Task<PagedResult<UserListItemDto>> QueryUsers(UserQuery query) { await Require(P.Users.View); return await _users.QueryAsync(query); }
        public async Task<UserDetailsDto?> GetUser(string id) { await Require(P.Users.View); return await _users.GetDetailsAsync(id); }

        public async Task<OperationResult> SaveUser(UserEditDto dto)
        {
            await Require(string.IsNullOrEmpty(dto.Id) ? P.Users.Create : P.Users.Edit);
            if (dto.RoleIds.Count > 0 || !string.IsNullOrEmpty(dto.Id))
            {
                // تعديل المجموعات يحتاج صلاحية خاصة، بدونها نحتفظ بالمجموعات الحالية
                if (!await _permissions.HasPermissionAsync(Context.User!, P.Users.ManageRoles))
                {
                    if (string.IsNullOrEmpty(dto.Id)) dto.RoleIds.Clear();
                    else dto.RoleIds = (await _users.GetDetailsAsync(dto.Id!))?.Roles.Select(r => r.Id).ToList() ?? new();
                }
            }
            return await _users.SaveAsync(dto, ActorId);
        }

        public async Task<OperationResult> SetUserActive(string id, bool active) { await Require(P.Users.Lock); return await _users.SetActiveAsync(id, active, ActorId); }
        public async Task<OperationResult> ResetPassword(string id, string? password) { await Require(P.Users.ResetPassword); return await _users.ResetPasswordAsync(id, password); }
        public async Task<OperationResult> SetUserRoles(string id, List<string> roleIds) { await Require(P.Users.ManageRoles); return await _users.SetRolesAsync(id, roleIds, ActorId); }
        public async Task<PermissionMatrixDto?> GetUserPermissions(string id) { await Require(P.Users.ManagePermissions); return await _users.GetPermissionMatrixAsync(id); }
        public async Task<OperationResult> SetUserPermissions(string id, List<UserPermissionOverrideDto> overrides) { await Require(P.Users.ManagePermissions); return await _users.SetPermissionOverridesAsync(id, overrides, ActorId); }
        public async Task<OperationResult> DeleteUser(string id) { await Require(P.Users.Delete); return await _users.DeleteAsync(id, ActorId); }
        public async Task<PagedResult<ActivityDto>> GetActivity(string? userId, int page, int pageSize) { await Require(P.Users.ViewActivity); return await _users.GetActivityAsync(userId, page, pageSize); }
        public string GeneratePassword() => UserAdminService.GeneratePassword();

        // ============================ Roles
        public async Task<List<RoleDto>> GetRoles() { await Require(P.Roles.View); return await _roles.GetRolesAsync(); }
        public async Task<OperationResult> SaveRole(RoleEditDto dto) { await Require(string.IsNullOrEmpty(dto.Id) ? P.Roles.Create : P.Roles.Edit); return await _roles.SaveAsync(dto, ActorId); }
        public async Task<OperationResult> DeleteRole(string id) { await Require(P.Roles.Delete); return await _roles.DeleteAsync(id); }
        public async Task<PermissionMatrixDto?> GetRolePermissions(string id) { await Require(P.Roles.View); return await _roles.GetMatrixAsync(id); }
        public async Task<OperationResult> SetRolePermissions(string id, List<int> permissionIds) { await Require(P.Roles.ManagePermissions); return await _roles.SetPermissionsAsync(id, permissionIds, ActorId); }
        public async Task<List<UserListItemDto>> GetRoleUsers(string id) { await Require(P.Roles.View); return await _roles.GetRoleUsersAsync(id); }
    }
}
