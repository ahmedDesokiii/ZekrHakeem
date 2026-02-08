namespace ZekrHakeem_v11.ViewModels
{
    public class AddUserViewModel
    {
        public string Id { get; set; }
        [Required, MaxLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string IdentityNum { get; set; } = string.Empty;

        [Phone]
        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required, MaxLength(20)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Gender")]
        public string Nationality { get; set; }
        public string? Adress { get; set; }
        public string? Notes { get; set; }
        public int? ExperienceYears { get; set; }
        [MaxLength(250)]
        public string? ExperienceDetails { get; set; }
        public bool ActiveUser { get; set; }
        public List<CheckBoxViewModel>? Roles { get; set; }
    }
}
