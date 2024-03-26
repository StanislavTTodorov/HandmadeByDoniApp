using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class CommentService : ICommentService
    {
        private readonly IRepository repository;

        public CommentService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateCommentToCommentByUserIdAndByCommentIdAsync(string userId, CommentFormModel formModel, string commentId)
        {
            ApplicationUser? user = await this.repository.All<ApplicationUser>().FirstAsync(u => u.Id.ToString() == userId);
            if (user != null)
            {
                Comment newComment = new Comment()
                {
                    Id = Guid.NewGuid(),
                    UserName = $"{user.FirstName} {user.LastName}",
                    Text = formModel.Text,
                    CreatedOn = DateTime.Now,
                    UserId = user.Id,
                    User = user,


                };
                Comment comment = await this.repository.All<Comment>().FirstAsync(g => g.Id.ToString() == commentId);
                comment.Comments.Add(newComment);

                await repository.AddAsync(newComment);
                await repository.SaveChangesAsync();
            }
        }

        public async Task EditCommentByIdAndFormModelAsync(string commentId, CommentFormModel formModel)
        {
           Comment comment = await this.repository
                .All<Comment>()
                .FirstAsync(c=>c.Id.ToString()==commentId);

            comment.Text = formModel.Text;
            comment.CreatedOn = DateTime.Now;

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string Id)
        {
            bool exists = await this.repository
                .All<Comment>()
                .AnyAsync(c => c.Id.ToString() == Id);

            return exists;
        }

        public async Task<CommentFormModel> GetCommentForEditByIdAsync(string commentId)
        {
            Comment comment = await this.repository
                .All<Comment>()
                .FirstAsync(c => c.Id.ToString() == commentId);

            return new CommentFormModel
            {
                Text = comment.Text
            };
        }

        public async Task<bool> HasUserCommentByUserIdAndByCommentIdAsync(string userId, string commentId)
        {
            Comment comment = await this.repository
                .AllReadOnly<Comment>()
                .Include(c => c.User)
                .FirstAsync(c => c.Id.ToString() == commentId);
            bool isYour = comment.User.Id.ToString() == userId;
            return isYour;
        }
    }
}
