

using Microsoft.AspNetCore.Identity;

namespace HandmadeByDoniApp.Data.Models
{
    public class ApplicationUser :IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Sets = new HashSet<Set>();
            this.Glasses = new HashSet<Glass>();
            this.Decaners = new HashSet<Decanter>();
            this.Boxes = new HashSet<Box>();
        }


        public ICollection<Set> Sets { get; set; }

        public ICollection<Glass> Glasses {  get; set; }  

        public ICollection<Decanter> Decaners { get; set; }

        public ICollection<Box> Boxes { get; set; }
    }
}
