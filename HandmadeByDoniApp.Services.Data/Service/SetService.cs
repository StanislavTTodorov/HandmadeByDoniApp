using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class SetService : ISetService
    {
        private readonly IRepository repository;

        public SetService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateCommentByUserIdAndByProductIdAsync(string userId, CommentFormModel formModel, string productId)
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
                Set set = await this.repository.All<Set>().FirstAsync(g => g.Id.ToString() == productId);
                set.Comments.Add(newComment);

                await repository.AddAsync(newComment);
                await repository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByIdAsync(string setId)
        {
            bool result = await this.repository
                .All<Set>()
                .AnyAsync(s => s.Id.ToString() == setId);

            return result;
        }

        public async Task<AllProductCommentViewModel> GetSetCommentByIdAsync(string setId)
        {
            Set set = await this.repository
    .         AllReadOnly<Set>()
            .Include(g => g.Comments)
          .ThenInclude(c => c.Comments)
          .FirstAsync(g => g.Id.ToString() == setId);

            return new AllProductCommentViewModel
            {
                Id = set.Id.ToString(),
                Title = set.Title,
                Description = set.Description,
                ImageUrl = set.ImageUrl,
                Price = set.Price,
                Comments = set.Comments.OrderByDescending(c => c.CreatedOn).Select(c => new CommentViewModel
                {
                    Id = c.Id.ToString(),
                    UserName = c.UserName,
                    Text = c.Text,
                    Time = c.CreatedOn.ToString(),

                })
            };
        }
    }
}
