namespace ZekrHakeem_v11.ViewModels
{
    public class ProfileFormViewModel
    {
        public string Id { get; set; }
        [Required, MaxLength(200)]
        public string FullName { get; set; } = string.Empty;
        [Required, MaxLength(200)]
        public string IdentityNum { get; set; } = string.Empty;

        [Phone]
        [Required]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required, Display(Name = "User Name")]
        public string? UserName { get; set; }


    }
}
