

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration.SeedData
{
    public class SeedCommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
           builder.HasData(SeedComment());
        }

        private ICollection<Comment> SeedComment()
        {
            ICollection<Comment> comments = new HashSet<Comment>();
            Comment comment = new Comment()
            {
                Id = Guid.Parse("CAF75E4F-1CDE-485D-A4B6-8DD88906C84A"),
                UserName = "Ralka Slavova",
                Text = "I am very satisfied great product",
                UserId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                CreatedOn = DateTime.Parse("2024-04-15 15:51:44.9339021"),
               // GlassId = Guid.Parse("62848C82-CF7B-4367-ADCE-6779103E87F6")


            };
            comments.Add(comment);

            Comment comment2 = new Comment()
            {
                Id = Guid.Parse("A80DB61A-2DF1-4BC2-864F-19058D7C1AC4"),
                UserName = "Admin Admin",
                Text = "I am very happy :)",
                UserId = Guid.Parse("80255D94-AEFE-4C1D-ABB6-715604DB71B0"),
                CreatedOn = DateTime.Parse("2024-04-15 16:01:08.6673314"),
                CommentId = Guid.Parse("CAF75E4F-1CDE-485D-A4B6-8DD88906C84A"),
                
            };
            comments.Add(comment2);
            

            Comment secondComment = new Comment()
            {
                Id = Guid.Parse("EE9E3A69-FB0C-4F9C-BCD3-875EEE0A42C6"),
                UserName = "Ralka Slavova",
                Text = "I can't wait for it to arrive.",
                UserId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
                CreatedOn = DateTime.Parse("2024-04-15 16:03:09.9164662"),
               // DecanterId = Guid.Parse("4EF3846B-6C81-4B91-A702-C69B399EF550")
            };
            comments.Add(secondComment);

            return comments;
        }
    }
}
