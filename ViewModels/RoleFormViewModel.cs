namespace ZekrHakeem_v11.ViewModels
{
    public class RoleFormViewModel
    {
        public String RoleId { get; set; }
        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

    }
}