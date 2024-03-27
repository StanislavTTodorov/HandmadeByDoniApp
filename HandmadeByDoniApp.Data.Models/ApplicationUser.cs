
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static HandmadeByDoniApp.Common.EntityValidationConstants.User;

namespace HandmadeByDoniApp.Data.Models
{
    /// <summary>
    /// This is custom user class that works with the defaut ASP.NET Core Identiry. 
    /// You can add additional info to the built-in users.
    /// </summary>
    public class ApplicationUser :IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
       

        }
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        //public ICollection<Order> Orders { get; set; }
    }
}
