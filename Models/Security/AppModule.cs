namespace ZekrHakeem_v11.Models.Security
{
    /// <summary>
    /// وحدة (موديول) في النظام مثل المراكز أو الطلاب. تُخزَّن في قاعدة البيانات
    /// ويتم مزامنتها تلقائيًا مع الصلاحيات المعرّفة في الكود عند تشغيل التطبيق.
    /// </summary>
    [Table("AppModules")]
    [Index(nameof(Key), IsUnique = true)]
    public class AppModule
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string NameAr { get; set; } = string.Empty;

        [MaxLength(60)]
        public string? Icon { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<AppPermission> Permissions { get; set; } = new List<AppPermission>();
    }
}
