

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
            this.Sets = new HashSet<Set>();
            this.Glasses = new HashSet<Glass>();
            this.Decanters = new HashSet<Decanter>();
            this.Boxes = new HashSet<Box>();
        }
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName {  get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public ICollection<Set> Sets { get; set; }

        public ICollection<Glass> Glasses {  get; set; }  

        public ICollection<Decanter> Decanters { get; set; }

        public ICollection<Box> Boxes { get; set; }
    }
}
