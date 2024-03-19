﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandmadeByDoniApp.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();
            this.Boxs = new HashSet<Box>();
            this.Decanters = new HashSet<Decanter>();
            this.Sets = new HashSet<Set>();
            this.Glasses = new HashSet<Glass>();

        }
        [Key]
        public Guid Id { get; set; }

       // [Required]
       // public DateTime CreaateOn { get; set; }

        ICollection<Box> Boxs { get; set; }

        ICollection<Decanter> Decanters { get; set; }

        ICollection<Set> Sets { get; set; }

        ICollection<Glass> Glasses { get; set; }

        [ForeignKey(nameof(User))]
        public Guid ClientId { get; set; }
        public ApplicationUser User { get; set; } = null!;


    }
}
