using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class DecanterService : IDecanterService
    {
        private readonly IRepository repository;

        public DecanterService(IRepository repository)
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
                Decanter decanter = await this.repository.All<Decanter>().FirstAsync(g => g.Id.ToString() == productId);
                decanter.Comments.Add(newComment);

                await repository.AddAsync(newComment);
                await repository.SaveChangesAsync();
            }
        }

        public async Task CreateDecanterAsync(DecanterFormModel formModel)
        {
            Decanter newDecanter = new Decanter()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
                Capacity = formModel.Capacity,
                Price = formModel.Price,
                IsSet = false,
                IsActive = true,
            };

            await repository.AddAsync(newDecanter);
            await repository.SaveChangesAsync();
        }

        public async Task EditDecanterByIdAndFormModelAsync(string id, DecanterFormModel formModel)
        {
            Decanter decanter = await this.repository
          .All<Decanter>()
          .FirstAsync(g => g.Id.ToString() == id);

            decanter.Title = formModel.Title;
            decanter.Description = formModel.Description;
            decanter.ImageUrl = formModel.ImageUrl;
            decanter.Capacity = formModel.Capacity;
            decanter.Price = formModel.Price;
            decanter.IsSet = formModel.IsSet;

            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string decanterId)
        {
            bool result = await repository.All<Decanter>()
                .AnyAsync(d => d.Id.ToString() == decanterId);

            return result;
        }

        public async Task<AllProductCommentViewModel> GetDecanterCommentByIdAsync(string glassId)
        {
            Decanter decanter = await this.repository
                                .AllReadOnly<Decanter>()
                                .Include(g => g.Comments)
                                .ThenInclude(c => c.Comments)
                                 .FirstAsync(g => g.Id.ToString() == glassId);

            return new AllProductCommentViewModel
            {
                Id = decanter.Id.ToString(),
                Title = decanter.Title,
                Description = decanter.Description,
                ImageUrl = decanter.ImageUrl,
                Price = decanter.Price,
                Comments = decanter.Comments.OrderByDescending(c => c.CreatedOn).Select(c => new CommentViewModel
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

        public async Task<DecanterDetailsViewModel> GetDecanterDetailsByIdAsync(string decanterId)
        {
            Decanter decanter = await repository
               .All<Decanter>()
               .FirstAsync(g => g.Id.ToString() == decanterId);

            return new DecanterDetailsViewModel
            {
                Id = decanter.Id.ToString(),
                Title = decanter.Title,
                Description = decanter.Description,
                ImageUrl = decanter.ImageUrl,
                Capacity = decanter.Capacity,
                Price = decanter.Price,
                IsSet = decanter.IsSet,
                SetId = decanter.SetId.ToString(),
            };
        }

        public async Task<DecanterFormModel> GetDecanterForEditByIdAsync(string id)
        {
            Decanter decanter = await this.repository
              .All<Decanter>()
              .FirstAsync(g => g.Id.ToString() == id);

            return new DecanterFormModel
            {
                Title = decanter.Title,
                Description = decanter.Description,
                ImageUrl = decanter.ImageUrl,
                Capacity = decanter.Capacity,
                Price = decanter.Price,
                IsSet = decanter.IsSet,                
            };
        }

        public async Task RecoveryByIdAsync(string id)
        {
            Decanter decanter = await this.repository
              .All<Decanter>()
              .FirstAsync(b => b.Id.ToString() == id);

            decanter.IsActive = true;
            await this.repository.SaveChangesAsync();
        }

        public async Task SoftDeleteByIdAsync(string id)
        {
            Decanter decanter = await this.repository
               .All<Decanter>()
               .FirstAsync(b => b.Id.ToString() == id);

            decanter.IsActive = false;
            await this.repository.SaveChangesAsync();
        }
    }
}
