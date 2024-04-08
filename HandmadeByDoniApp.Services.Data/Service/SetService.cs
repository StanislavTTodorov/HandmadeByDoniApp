
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
                SetId = set.Id.ToString()

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
                SetId = set.Id.ToString()

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
                    SetId = set.Id.ToString()

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
                    SetId = set.Id.ToString()

                };
                glasses.Add(glassFour);

                await this.repository.AddAsync(glassThree);
                await this.repository.AddAsync(glassFour);
            }

            set.Glasss = glasses;

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
                    SetId = set.Id.ToString()
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
            //List<GlassFormModel> glassModels = new List<GlassFormModel>();
            //glassModels.Add(formModel.GlassOne);
            //glassModels.Add(formModel.GlassTwo);
            //if(formModel.NumberOfCups==4 &&
            //    formModel.GlassThree!=null &&
            //    formModel.GlassFour!=null)
            //{
            //    glassModels.Add(formModel.GlassThree);
            //    glassModels.Add(formModel.GlassFour);
            //}

            //ICollection<Glass> glasses = new HashSet<Glass>();

            //Glass glassOne = await this.repository
            //   .All<Glass>()
            //   .FirstAsync(g => g.Id.ToString() == glassModels[0].Id);

            //glassOne.Title = glassModels[0].Title;
            //glassOne.Description = glassModels[0].Description;
            //glassOne.ImageUrl = glassModels[0].ImageUrl;
            //glassOne.Capacity = glassModels[0].Capacity;
            //glassOne.GlassCategoryId = glassModels[0].CategoryId;

            //glasses.Add(glassOne);

            //Glass glassTwo = await this.repository
            //   .All<Glass>()
            //   .FirstAsync(g => g.Id.ToString() == glassModels[1].Id);

            //glassOne.Title = glassModels[1].Title;
            //glassOne.Description = glassModels[1].Description;
            //glassOne.ImageUrl = glassModels[1].ImageUrl;
            //glassOne.Capacity = glassModels[1].Capacity;
            //glassOne.GlassCategoryId = glassModels[1].CategoryId;

            //glasses.Add(glassTwo);

            //if(formModel.NumberOfCups == 4)
            //{
            //    Glass glassThree = await this.repository
            // .All<Glass>()
            // .FirstAsync(g => g.Id.ToString() == glassModels[2].Id);

            //    glassThree.Title = glassModels[2].Title;
            //    glassThree.Description = glassModels[2].Description;
            //    glassThree.ImageUrl = glassModels[2].ImageUrl;
            //    glassThree.Capacity = glassModels[2].Capacity;
            //    glassThree.GlassCategoryId = glassModels[2].CategoryId;

            //    glasses.Add(glassThree);

            //    Glass glassFour = await this.repository
            //       .All<Glass>()
            //       .FirstAsync(g => g.Id.ToString() == glassModels[3].Id);

            //    glassFour.Title = glassModels[3].Title;
            //    glassFour.Description = glassModels[3].Description;
            //    glassFour.ImageUrl = glassModels[3].ImageUrl;
            //    glassFour.Capacity = glassModels[3].Capacity;
            //    glassFour.GlassCategoryId = glassModels[3].CategoryId;

            //    glasses.Add(glassFour);
            //}
            Set set = await this.repository
              .All<Set>()
              .FirstAsync(g => g.Id.ToString() == id);

            set.Title = formModel.Title;
            set.Description = formModel.Description;
            set.ImageUrl = formModel.ImageUrl;
            set.Price = formModel.Price;
            //set.Glasss = glasses;

            //if (formModel.Decanter != null)
            //{
            //    Decanter decanter = await this.repository
            //    .All<Decanter>()
            //    .FirstAsync(g => g.Id == set.DecanterId);

            //    decanter.Title = formModel.Decanter.Title;
            //    decanter.Description = formModel.Decanter.Description;
            //    decanter.ImageUrl = formModel.Decanter.ImageUrl;
            //    decanter.Capacity = formModel.Decanter.Capacity;

            //    set.DecanterId = decanter.Id;
            //    set.Decanter = decanter;
            //}

           
            await this.repository.SaveChangesAsync();


        }

        public async Task<bool> ExistsByIdAsync(string setId)
        {
            bool result = await this.repository
                .All<Set>()
                .AnyAsync(s => s.Id.ToString() == setId.ToLower());

            return result;
        }



        public async Task<AllProductCommentViewModel> GetSetCommentByIdAsync(string setId)
        {
            Set set = await this.repository
              .AllReadOnly<Set>()
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
                 .Include(s => s.Glasss)
                 .Include(s => s.Decanter)
                 .FirstAsync(s => s.Id.ToString() == setId);

            List<AllProductViewModel> setProducts = set.Glasss.Select(g => new AllProductViewModel
            {
                Id = g.Id.ToString(),
                Title = g.Title,
                Description = g.Description,
                ImageUrl = g.ImageUrl,

            }).ToList();

            if (set.DecanterId != null && set.Decanter != null)
            {
                AllProductViewModel decanter = new AllProductViewModel
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
               .Include(h => h.Glasss)
               .ThenInclude(s=>s.GlassCategory)
               .Where(h => h.IsActive)
               .FirstAsync(h => h.Id.ToString() == id);

            List<GlassFormModel> glasses = set.Glasss.Select(g=> new GlassFormModel
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
                NumberOfCups = set.Glasss.Count(),
                GlassOne = glasses[0],
                GlassTwo = glasses[1],
                GlassThree = (set.Glasss.Count()>2)? glasses[2] : null,
                GlassFour = (set.Glasss.Count() == 4) ? glasses[3] : null,
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
    }
}
