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
                IsActive = true,
            };

            await repository.AddRangeAsync(newGlass);
            await repository.SaveChangesAsync();
        }

        public async Task EditGlassByIdAndFormModelAsync(string id, GlassFormModel formModel)
        {
            Glass glass = await this.repository
               .All<Glass>()
               .FirstAsync(g => g.Id.ToString() == id);

            glass.Title = formModel.Title;
            glass.Description = formModel.Description;
            glass.ImageUrl = formModel.ImageUrl;
            glass.Capacity = formModel.Capacity;
            glass.Price = formModel.Price;
            glass.GlassCategoryId = formModel.CategoryId;
            glass.IsSet = formModel.IsSet;

            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string glassId)
        {
            bool exists = await this.repository.AllReadOnly<Glass>()
                 .AnyAsync(g => g.Id.ToString() == glassId);

            return exists;
        }

        public async Task<ProductCommentViewModel> GetGlassCommentByIdAsync(string glassId)
        {
            Glass glass = await this.repository
                .AllReadOnly<Glass>()
                .Include(g => g.GlassCategory)
                .Include(g => g.Comments)
                .ThenInclude(c => c.Comments)
                 .FirstAsync(g => g.Id.ToString() == glassId);

            return new ProductCommentViewModel
            {
                Id = glass.Id.ToString(),
                Title = glass.Title,
                Description = glass.Description,
                ImageUrl = glass.ImageUrl,
                Price = glass.Price,
                Comments = glass.Comments.OrderByDescending(c => c.CreatedOn).Select(c => new CommentViewModel
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
                SetId = glass.SetId.ToString(),
            };

        }

        public async Task<GlassFormModel> GetGlassForEditByIdAsync(string id)
        {
            Glass glass = await this.repository
                .All<Glass>()
                .Include(h => h.GlassCategory)
                .FirstAsync(h => h.Id.ToString() == id);

            return new GlassFormModel
            {
                Title = glass.Title,
                Description = glass.Description,
                ImageUrl = glass.ImageUrl,
                Capacity = glass.Capacity,
                Price = glass.Price,
                CategoryId = glass.GlassCategoryId,
                IsSet = glass.IsSet
            };
        }

        public async Task RecoveryByIdAsync(string id)
        {
            Glass glass = await this.repository
                    .All<Glass>()
                    .FirstAsync(b => b.Id.ToString() == id);

            glass.IsActive = true;
            await this.repository.SaveChangesAsync();
        }

        public async Task SoftDeleteByIdAsync(string id)
        {
            Glass glass = await this.repository
                    .All<Glass>()
                    .FirstAsync(b => b.Id.ToString() == id);

            glass.IsActive = false;
            await this.repository.SaveChangesAsync();
        }
    }
}
