using System.ComponentModel.DataAnnotations;

namespace HandmadeByDoniApp.Web.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

    }

    public class UserProfileViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Phone]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; } 

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;
    }

    public class ChangeEmailViewModel
    {
        [Required, EmailAddress, Display(Name = "New Email")]
        public string NewEmail { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Current Password")]
        public string Password { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required, DataType(DataType.Password), Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }


}
