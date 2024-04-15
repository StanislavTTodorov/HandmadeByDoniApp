﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HandmadeByDoniApp.Common.EntityValidationConstants.Glass;


namespace HandmadeByDoniApp.Data.Models
{
    public class Glass
    {
        public Glass()
        {
            this.Id = Guid.NewGuid();
            this.Comments = new HashSet<Comment>();
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl {  get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }


        [Required]
        [ForeignKey(nameof(GlassCategory))]
        public int GlassCategoryId { get; set; }
        public virtual GlassCategory GlassCategory { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public bool IsSet { get; set; }

        [ForeignKey(nameof(Set))]
        public Guid? SetId { get; set; }
        public virtual Set? Set { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }
        public virtual Order? Order { get; set; }

    }
}
