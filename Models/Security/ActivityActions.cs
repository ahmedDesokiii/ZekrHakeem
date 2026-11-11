namespace ZekrHakeem_v11.Models.Security
{
    public static class ActivityActions
    {
        public const string Login = "Login";
        public const string LoginFailed = "LoginFailed";
        public const string Logout = "Logout";
        public const string Register = "Register";
        public const string UserCreated = "UserCreated";
        public const string UserUpdated = "UserUpdated";
        public const string UserDeleted = "UserDeleted";
        public const string UserActivated = "UserActivated";
        public const string UserDeactivated = "UserDeactivated";
        public const string PasswordReset = "PasswordReset";
        public const string RolesChanged = "RolesChanged";
        public const string PermissionsChanged = "PermissionsChanged";
        public const string RoleCreated = "RoleCreated";
        public const string RoleUpdated = "RoleUpdated";
        public const string RoleDeleted = "RoleDeleted";
        public const string RolePermissionsChanged = "RolePermissionsChanged";
        public const string ProfileUpdated = "ProfileUpdated";

        public static string ToArabic(string action) => action switch
        {
            Login => "تسجيل دخول",
            LoginFailed => "محاولة دخول فاشلة",
            Logout => "تسجيل خروج",
            Register => "إنشاء حساب",
            UserCreated => "إضافة مستخدم",
            UserUpdated => "تعديل بيانات",
            UserDeleted => "حذف مستخدم",
            UserActivated => "تفعيل الحساب",
            UserDeactivated => "إيقاف الحساب",
            PasswordReset => "إعادة تعيين كلمة المرور",
            RolesChanged => "تعديل المجموعات",
            PermissionsChanged => "تعديل الصلاحيات المباشرة",
            RoleCreated => "إضافة مجموعة",
            RoleUpdated => "تعديل مجموعة",
            RoleDeleted => "حذف مجموعة",
            RolePermissionsChanged => "تعديل صلاحيات مجموعة",
            ProfileUpdated => "تحديث الملف الشخصي",
            _ => action
        };
    }
}
