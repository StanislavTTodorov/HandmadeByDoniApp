using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace HandmadeByDoniApp.Services.Data.Service
{
    public class BoxService : IBoxService
    {
        private readonly IRepository repository;

        public BoxService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateBoxAsync(BoxFormModel formModel)
        {
            Box newBox = new Box()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Capacity = formModel.Capacity,
                Price = formModel.Price,
                IsActive = true,
            };

            await repository.AddAsync(newBox);
            await repository.SaveChangesAsync();
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
                Box box = await this.repository.All<Box>().FirstAsync(g => g.Id.ToString() == productId);
                box.Comments.Add(newComment);

                await repository.AddAsync(newComment);
                await repository.SaveChangesAsync();
            }
        }

        public async Task EditBoxByIdAndFormModelAsync(string id, BoxFormModel formModel)
        {
            Box box = await this.repository
               .All<Box>()
               .FirstAsync(g => g.Id.ToString() == id);

            box.Title = formModel.Title;
            box.Description = formModel.Description;
            box.ImageUrl = formModel.ImageUrl;
            box.Capacity = formModel.Capacity;
            box.Price = formModel.Price;

            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string boxId)
        {
            bool exists = await repository.All<Box>()
                .AnyAsync(b => b.Id.ToString() == boxId);

            return exists;
        }

        public async Task<AllProductCommentViewModel> GetBoxCommentByIdAsync(string glassId)
        {
            Box box = await this.repository
               .AllReadOnly<Box>()
               .Include(g => g.Comments)
               .ThenInclude(c => c.Comments)
                .FirstAsync(g => g.Id.ToString() == glassId);

            return new AllProductCommentViewModel
            {
                Id = box.Id.ToString(),
                Title = box.Title,
                Description = box.Description,
                ImageUrl = box.ImageUrl,
                Price = box.Price,
                Comments = box.Comments.OrderByDescending(c => c.CreatedOn).Select(c => new CommentViewModel
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

        public async Task<BoxDetailsViewModel> GetBoxDetailsByIdAsync(string boxId)
        {
            Box box = await this.repository
                .All<Box>()
                .FirstAsync(g => g.Id.ToString() == boxId);

            return new BoxDetailsViewModel
            {
                Id = box.Id.ToString(),
                Title = box.Title,
                Description = box.Description,
                ImageUrl = box.ImageUrl,
                Capacity = box.Capacity,
                Price = box.Price,
            };

        }

        public async Task<BoxFormModel> GetBoxForEditByIdAsync(string id)
        {
            Box box = await this.repository
               .All<Box>()
               .FirstAsync(g => g.Id.ToString() == id);

            return new BoxFormModel
            {
                Title = box.Title,
                Description = box.Description,
                ImageUrl = box.ImageUrl,
                Capacity = box.Capacity,
                Price = box.Price
            };
        }

        public async Task RecoveryByIdAsync(string id)
        {
            Box box = await this.repository
               .All<Box>()
               .FirstAsync(b => b.Id.ToString() == id);

            box.IsActive = true;
            await this.repository.SaveChangesAsync();
        }

        public async Task SoftDeleteByIdAsync(string id)
        {
            Box box = await this.repository
                .All<Box>()
                .FirstAsync(b => b.Id.ToString() == id);

            box.IsActive = false;
            await this.repository.SaveChangesAsync();
        }
    }
}
