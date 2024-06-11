
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Glass;
using HandmadeByDoniApp.Web.ViewModels.Product;
using HandmadeByDoniApp.Web.ViewModels.Set;
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

        public async Task CreateSetAsync(SetFormModel model)
        {
            ICollection<Glass> glasses = new HashSet<Glass>();
            Set set = new Set()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            Glass glassOne = new Glass()
            {
                Title = model.GlassOne.Title,
                Description = model.GlassOne.Description,
                ImageUrl = model.GlassOne.ImageUrl,
                Capacity = model.GlassOne.Capacity,
                Price = 0,
                GlassCategoryId = model.GlassOne.CategoryId,
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsSet = true,
                SetId = set.Id

            };
            glasses.Add(glassOne);

            Glass glassTwo = new Glass()
            {
                Title = model.GlassTwo.Title,
                Description = model.GlassTwo.Description,
                ImageUrl = model.GlassTwo.ImageUrl,
                Capacity = model.GlassTwo.Capacity,
                Price = 0,
                GlassCategoryId = model.GlassTwo.CategoryId,
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsSet = true,
                SetId = set.Id

            };
            glasses.Add(glassTwo);
            if (model.NumberOfCups == 4)
            {
                Glass glassThree = new Glass()
                {
                    Title = model.GlassOne.Title,
                    Description = model.GlassOne.Description,
                    ImageUrl = model.GlassOne.ImageUrl,
                    Capacity = model.GlassOne.Capacity,
                    Price = 0,
                    GlassCategoryId = model.GlassOne.CategoryId,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    IsSet = true,
                    SetId = set.Id

                };
                glasses.Add(glassThree);
                Glass glassFour = new Glass()
                {
                    Title = model.GlassTwo.Title,
                    Description = model.GlassTwo.Description,
                    ImageUrl = model.GlassTwo.ImageUrl,
                    Capacity = model.GlassTwo.Capacity,
                    Price = 0,
                    GlassCategoryId = model.GlassTwo.CategoryId,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    IsSet = true,
                    SetId = set.Id

                };
                glasses.Add(glassFour);

                await this.repository.AddAsync(glassThree);
                await this.repository.AddAsync(glassFour);
            }

            set.Glass = glasses;

            if (model.Decanter != null)
            {
                Decanter decanter = new Decanter()
                {
                    Title = model.Decanter.Title,
                    Description = model.Decanter.Description,
                    ImageUrl = model.Decanter.ImageUrl,
                    Capacity = model.Decanter.Capacity,
                    Price = 0,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    IsSet = true,
                    SetId = set.Id
                };
                set.DecanterId = decanter.Id;
                set.Decanter = decanter;

                await this.repository.AddAsync(decanter);
            }


            await this.repository.AddAsync(glassOne);
            await this.repository.AddAsync(glassTwo);
            await this.repository.AddAsync(set);
            await this.repository.SaveChangesAsync();

        }

        public async Task EditSetByIdAndFormModelAsync(string id, OnlySetFormModel formModel)
        {       
            
            Set set = await this.repository
              .All<Set>()
              .FirstAsync(g => g.Id.ToString() == id);

            set.Title = formModel.Title;
            set.Description = formModel.Description;
            set.ImageUrl = formModel.ImageUrl;
            set.Price = formModel.Price;
            
            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string setId)
        {
            bool result = await this.repository
                .All<Set>()
                .AnyAsync(s => s.Id.ToString() == setId.ToLower());

            return result;
        }



        public async Task<ProductCommentViewModel> GetSetCommentByIdAsync(string setId)
        {
            Set set = await this.repository
              .AllReadOnly<Set>()
              .Include(g => g.Comments)
              .ThenInclude(c => c.Comments)
              .FirstAsync(g => g.Id.ToString() == setId);

            return new ProductCommentViewModel
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

        public async Task<SetDetailsViewModel> GetSetDetailsByIdAsync(string setId)
        {
            Set set = await this.repository.AllReadOnly<Set>()
                 .Include(s => s.Glass)
                 .Include(s => s.Decanter)
                 .FirstAsync(s => s.Id.ToString() == setId);

            List<ProductViewModel> setProducts = set.Glass.Select(g => new ProductViewModel
            {
                Id = g.Id.ToString(),
                Title = g.Title,
                Description = g.Description,
                ImageUrl = g.ImageUrl,

            }).ToList();

            if (set.DecanterId != null && set.Decanter != null)
            {
                ProductViewModel decanter = new ProductViewModel
                {
                    Id = set.Decanter.Id.ToString(),
                    Title = set.Decanter.Title,
                    Description = set.Decanter.Description,
                    ImageUrl = set.Decanter.ImageUrl,
                };
                setProducts.Add(decanter);
            }

            SetDetailsViewModel model = new SetDetailsViewModel
            {
                Id = set.Id.ToString(),
                Title = set.Title,
                Description = set.Description,
                ImageUrl = set.ImageUrl,
                Price = set.Price,
                SetProducts = setProducts
            };

            return model;
        }

        public async Task<SetFormModel> GetSetForEditByIdAsync(string id)
        {
            Set set = await this.repository
               .All<Set>()
               .Include(s => s.Decanter)
               .Include(h => h.Glass)
               .ThenInclude(s=>s.GlassCategory)
               .Where(h => h.IsActive)
               .FirstAsync(h => h.Id.ToString() == id);

            List<GlassFormModel> glasses = set.Glass.Select(g=> new GlassFormModel
            {
                 Id = g.Id.ToString(),
                 Title = g.Title,
                 Description= g.Description,
                 ImageUrl= g.ImageUrl,
                 Capacity = g.Capacity,
                 CategoryId = g.GlassCategoryId, 
                 
            }).ToList();
            
            SetFormModel model = new SetFormModel
            {
                Title = set.Title,
                Description = set.Description,
                ImageUrl = set.ImageUrl,
                Price = set.Price,
                NumberOfCups = set.Glass.Count(),
                GlassOne = glasses[0],
                GlassTwo = glasses[1],
                GlassThree = (set.Glass.Count()>2)? glasses[2] : null,
                GlassFour = (set.Glass.Count() == 4) ? glasses[3] : null,
            };
            if (set.Decanter != null)
            {
                DecanterFormModel decanter = new DecanterFormModel
                {
                    Id = set.Decanter.Id.ToString(),
                    Title = set.Decanter.Title,
                    Description = set.Decanter.Description,
                    ImageUrl = set.Decanter.ImageUrl,
                    Capacity = set.Decanter.Capacity,
                };
                model.Decanter = decanter;
            }
            return model;
        }

        public async Task RecoveryByIdAsync(string id)
        {
            Set set = await this.repository
                  .All<Set>()
                  .Include(set => set.Decanter)
                  .Include(g => g.Glass)
                  .FirstAsync(b => b.Id.ToString() == id);

            set.IsActive = true;

            foreach (var glass in set.Glass)
            {
                glass.IsActive = true;
            }

            if (set.DecanterId != null && set.Decanter != null)
            {
                set.Decanter.IsActive = true;
            }
            await this.repository.SaveChangesAsync();
        }

        public async Task SoftDeleteByIdAsync(string id)
        {
            Set set = await this.repository
                 .All<Set>()
                 .Include(set => set.Decanter)
                 .Include(g => g.Glass)
                 .FirstAsync(b => b.Id.ToString() == id);

            set.IsActive = false;

            foreach (var glass in set.Glass)
            {
                glass.IsActive = false;
            }

            if(set.DecanterId!=null&&set.Decanter!=null)
            {
                set.Decanter.IsActive = false;
            }
            await this.repository.SaveChangesAsync();
        }
    }
}
