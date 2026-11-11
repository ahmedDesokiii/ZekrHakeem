using System.Reflection;

namespace ZekrHakeem_v11.Constants
{
    public record PermissionDefinition(string Key, string Module, string Action, string NameAr, int SortOrder);
    public record ModuleDefinition(string Key, string NameAr, string Icon, int SortOrder, IReadOnlyList<PermissionDefinition> Permissions);

    /// <summary>
    /// المصدر الوحيد لتعريف الصلاحيات في الكود.
    /// يقرأ كل الثوابت داخل <see cref="Permissions"/> بالـ Reflection،
    /// ويتم مزامنتها مع قاعدة البيانات عند بدء التشغيل (إضافة الجديد فقط وعدم مسح تعديلات المسؤول).
    /// </summary>
    public static class PermissionCatalog
    {
        private static readonly Dictionary<string, (string Name, string Icon, int Order)> ModuleMeta = new()
        {
            ["Users"] = ("المستخدمين", "bi-person-badge-fill", 1),
            ["Roles"] = ("المجموعات والصلاحيات", "bi-shield-lock-fill", 2),
            ["Centers"] = ("المراكز", "bi-building-fill", 3),
            ["Districts"] = ("الأحياء", "bi-geo-fill", 4),
            ["Locations"] = ("المواقع", "bi-geo-alt-fill", 5),
            ["Episodes"] = ("الحلقات الأساسية", "bi-diagram-3-fill", 6),
            ["Progs"] = ("البرامج النوعية", "bi-collection-fill", 7),
            ["ProgEpisodes"] = ("الحلقات النوعية", "bi-list-stars", 8),
            ["Students"] = ("الطلاب", "bi-mortarboard-fill", 9),
            ["Teachers"] = ("المعلمين", "bi-person-video3", 10),
            ["Plans"] = ("الخطط التعليمية", "bi-journal-bookmark-fill", 11),
            ["Demands"] = ("طلبات أولياء الأمور", "bi-chat-left-quote-fill", 12),
        };

        private static readonly Dictionary<string, (string Name, int Order)> ActionMeta = new()
        {
            ["View"] = ("عرض", 1),
            ["ViewDetails"] = ("عرض التفاصيل", 2),
            ["Create"] = ("إضافة", 3),
            ["Edit"] = ("تعديل", 4),
            ["Delete"] = ("حذف", 5),
            ["Pause"] = ("إيقاف مؤقت", 6),
            ["Resume"] = ("استئناف", 7),
            ["DeleteLog"] = ("حذف السجل", 8),
            ["Lock"] = ("إيقاف / تفعيل الحساب", 9),
            ["ResetPassword"] = ("إعادة تعيين كلمة المرور", 10),
            ["ManageRoles"] = ("إدارة المجموعات", 11),
            ["ManagePermissions"] = ("إدارة الصلاحيات", 12),
            ["ViewActivity"] = ("عرض سجل النشاط", 13),
        };

        private static readonly Lazy<IReadOnlyList<ModuleDefinition>> _modules = new(Build);

        public static IReadOnlyList<ModuleDefinition> Modules => _modules.Value;

        public static IEnumerable<string> AllKeys => Modules.SelectMany(m => m.Permissions).Select(p => p.Key);

        private static IReadOnlyList<ModuleDefinition> Build()
        {
            var result = new List<ModuleDefinition>();
            foreach (var nested in typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.Static))
            {
                var moduleKey = nested.Name;
                var meta = ModuleMeta.TryGetValue(moduleKey, out var m) ? m : (moduleKey, "bi-grid-fill", 99);

                var perms = nested.GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.IsLiteral && f.FieldType == typeof(string))
                    .Select(f => (string)f.GetRawConstantValue()!)
                    .Distinct()
                    .Select(key =>
                    {
                        var action = key.Split('.').Last();
                        var am = ActionMeta.TryGetValue(action, out var a) ? a : (action, 50);
                        return new PermissionDefinition(key, moduleKey, action, am.Item1, am.Item2);
                    })
                    .OrderBy(p => p.SortOrder)
                    .ToList();

                result.Add(new ModuleDefinition(moduleKey, meta.Item1, meta.Item2, meta.Item3, perms));
            }
            return result.OrderBy(m => m.SortOrder).ToList();
        }
    }
}
