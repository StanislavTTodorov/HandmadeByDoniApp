using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class GlassService : IGlassService
    {
        private readonly IRepository repository;

        public GlassService(IRepository repository)
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
                Glass glass = await this.repository.All<Glass>().FirstAsync(g => g.Id.ToString() == productId);
                glass.Comments.Add(newComment);

                await repository.AddAsync(newComment);
                await repository.SaveChangesAsync();
            }
        }

        public async Task CreateGlassAsync(GlassFormModel formModel)
        {
            Glass newGlass = new Glass()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Capacity = formModel.Capacity,
                Price = formModel.Price,
                GlassCategoryId = formModel.CategoryId,
                IsSet = formModel.IsSet,
            };

            await repository.AddRangeAsync(newGlass);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string glassId)
        {
            bool exists = await this.repository.AllReadOnly<Glass>()
                 .AnyAsync(g => g.Id.ToString() == glassId);

            return exists;
        }

        public async Task<AllProductCommentViewModel> GetGlassCommentByIdAsync(string glassId)
        {
            Glass glass = await this.repository
                .AllReadOnly<Glass>()
                .Include(g => g.GlassCategory)
                .Include(g=>g.Comments)
                .ThenInclude(c=>c.Comments)
                 .FirstAsync(g => g.Id.ToString() == glassId);

            return new AllProductCommentViewModel
            {
                Id = glass.Id.ToString(),
                Title = glass.Title,
                Description = glass.Description,
                ImageUrl = glass.ImageUrl,
                Price = glass.Price,
                Comments = glass.Comments.OrderByDescending(c=>c.CreatedOn).Select(c => new CommentViewModel
                {
                    Id = c.Id.ToString(),
                    UserName = c.UserName,
                    Text = c.Text,
                    Time = c.CreatedOn.ToString(),
                    Comments = c.Comments.OrderByDescending(c => c.CreatedOn).Select(c => new CommentViewModel
                    {
                        Id = c.Id.ToString(),
                        UserName = c.UserName,
                        Text = c.Text,
                        Time = c.CreatedOn.ToString()
                    })

                })
            };
        }

        public async Task<GlassDetailsViewModel> GetGlassDetailsByIdAsync(string glassId)
        {
            Glass glass = await this.repository
                .AllReadOnly<Glass>()
                .Include(g => g.GlassCategory)
                .FirstAsync(g => g.Id.ToString() == glassId);

            return new GlassDetailsViewModel
            {
                Id = glass.Id.ToString(),
                Title = glass.Title,
                Description = glass.Description,
                ImageUrl = glass.ImageUrl,
                Capacity = glass.Capacity,
                Price = glass.Price,
                Category = glass.GlassCategory.Name,
                IsSet = glass.IsSet,
            };

        }
    }
}
