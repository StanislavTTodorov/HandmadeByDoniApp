
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Servises.Data.Models.Product;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Home;
using HandmadeByDoniApp.Web.ViewModels.Product;
using HandmadeByDoniApp.Web.ViewModels.Product.Enums;
using Microsoft.EntityFrameworkCore;


namespace HandmadeByDoniApp.Services.Data.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository repository;

        public ProductService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AllProductFilteredAndPagedServiceModel> AllProductsAsync(AllProductsQueryModel queryModel)
        {
            IQueryable<Product> productsQuery = this.repository
                .All<Product>();

            if (string.IsNullOrEmpty(queryModel.GlassCategory) == false)
            {
                productsQuery = productsQuery
                   .Where(g => g.Category.Name == queryModel.GlassCategory);

            }

            if (string.IsNullOrWhiteSpace(queryModel.SearchString) == false)
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                productsQuery = productsQuery.Where(p => EF.Functions.Like(p.Title, wildCard) ||
                                 (!string.IsNullOrEmpty(p.Description) && EF.Functions.Like(p.Description, wildCard)));
            }

            productsQuery = queryModel.ProductSorting switch
            {
                ProductSorting.Newest => productsQuery
                    .Where(h => h.IsActive)
                    .OrderByDescending(h => h.CreatedOn),
                ProductSorting.Oldest => productsQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.CreatedOn),
                ProductSorting.PriceAscending => productsQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.Price),
                ProductSorting.PriceDescending => productsQuery
                    .Where(h => h.IsActive)
                    .OrderByDescending(h => h.Price),
                ProductSorting.Active => productsQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.IsActive)
                    .ThenByDescending(h => h.CreatedOn),
                ProductSorting.NotActive => productsQuery
                      .Where(h => !h.IsActive)
                      .OrderBy(h => h.CreatedOn),
                _ => productsQuery
                     .Where(h => h.IsActive)
                     .OrderBy(h => h.CreatedOn)
            };

            List<ProductsAllViewModel> allProductsModels = new List<ProductsAllViewModel>();
            switch (queryModel.ProductsName)
            {
                case ProductsName.All:
                    allProductsModels = await productsQuery.Select(x => new ProductsAllViewModel
                    {
                        Id = x.Id.ToString(),
                        Title = x.Title,
                        Description = x.Description,
                        ImageUrl = x.ImageUrl,
                        Price = x.Price,
                        CreatedOn = x.CreatedOn,
                        IsActive = x.IsActive
                    }).ToListAsync();

                    break;
                case ProductsName.Glass:
                    allProductsModels = await productsQuery
                        .Include(p => p.Category)
                        .Where(p => EF.Functions.Like(p.Category.Name.ToLower(), $"%glass%"))
                        .Select(g => new ProductsAllViewModel
                        {
                            Id = g.Id.ToString(),
                            Title = g.Title,
                            Description = g.Description,
                            ImageUrl = g.ImageUrl,
                            Price = g.Price,
                            CreatedOn = g.CreatedOn,
                            IsActive = g.IsActive

                        }).ToListAsync();
                    break;
                case ProductsName.Decanter:
                    allProductsModels = await productsQuery
                        .Include(p => p.Category)
                        .Where(p => EF.Functions.Like(p.Category.Name.ToLower(), $"%decanter%"))
                        .Select(p => new ProductsAllViewModel
                        {
                            Id = p.Id.ToString(),
                            Title = p.Title,
                            Description = p.Description,
                            ImageUrl = p.ImageUrl,
                            Price = p.Price,
                            CreatedOn = p.CreatedOn,
                            IsActive = p.IsActive,
                        }).ToListAsync();
                    break;
                case ProductsName.Box:
                    allProductsModels = await productsQuery
                        .Include(p => p.Category)
                        .Where(p => EF.Functions.Like(p.Category.Name.ToLower(), $"%box%"))
                        .Select(p => new ProductsAllViewModel
                        {
                            Id = p.Id.ToString(),
                            Title = p.Title,
                            Description = p.Description,
                            ImageUrl = p.ImageUrl,
                            Price = p.Price,
                            CreatedOn = p.CreatedOn,
                            IsActive = p.IsActive
                        }).ToListAsync();
                    break;
                case ProductsName.Set:
                    allProductsModels = await productsQuery
                        .Include(p => p.Category)
                        .Where(p => EF.Functions.Like(p.Category.Name.ToLower(), $"%set%"))
                        .Select(p => new ProductsAllViewModel
                        {
                            Id = p.Id.ToString(),
                            Title = p.Title,
                            Description = p.Description,
                            ImageUrl = p.ImageUrl,
                            Price = p.Price,
                            CreatedOn = p.CreatedOn,
                            IsActive = p.IsActive
                        }).ToListAsync();
                    break;
                default:

                    break;
            }

            IEnumerable<ProductsAllViewModel> allViewModels = allProductsModels
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductPerPage)
                .Take(queryModel.ProductPerPage)
                .ToArray();

            int totalProduct = allProductsModels.Count();

            return new AllProductFilteredAndPagedServiceModel()
            {
                TotalProductCount = totalProduct,
                Products = allViewModels
            };

        }

        public async Task<IEnumerable<IndexViewModel>> LastTwelveProductsAsync()
        {
            IEnumerable<IndexViewModel>? lastTwelveProducts = await this.repository
                .All<Product>()
                .Where(g => g.IsActive)
                .OrderByDescending(g => g.CreatedOn)
                .Take(12)
                .Select(g => new IndexViewModel()
                {
                    Id = g.Id.ToString(),
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    CreatedOn = g.CreatedOn

                }).ToArrayAsync();

            return lastTwelveProducts;

        }

        public async Task<bool> ExistsByIdAsync(string productId)
        {
            bool exists = await repository.All<Product>()
                 .AnyAsync(b => b.Id.ToString() == productId);

            return exists;
        }

        public async Task CreateProductAsync(ProductFormModel formModel)
        {
            Product newProduct = new Product()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrls,
                Price = formModel.Price,
                CategoryId = formModel.CategoryId,
                IsActive = true,
            };

            await repository.AddRangeAsync(newProduct);
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

                Product product = await GetProductByIdAsync(productId);

                product.Comments.Add(newComment);

                await repository.AddAsync(newComment);
                await repository.SaveChangesAsync();
            }
        }

        public async Task<ProductFormModel> GetProductForEditByIdAsync(string id)
        {
            Product product = await this.repository
                .All<Product>()
                .Include(h => h.Category)
                .FirstAsync(h => h.Id.ToString() == id);

            return new ProductFormModel
            {
                Title = product.Title,
                Description = product.Description,
                ImageUrls = product.ImageUrl,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }

        public async Task EditProductByIdAndFormModelAsync(string id, ProductFormModel formModel)
        {
            Product product = await GetProductByIdAsync(id);
       
            product.Title = formModel.Title;
            product.Description = formModel.Description;
            product.ImageUrl = formModel.ImageUrls;
            product.Price = formModel.Price;
            product.CategoryId = formModel.CategoryId;

            await this.repository.SaveChangesAsync();
        }

        public async Task SoftDeleteByIdAsync(string id)
        {
            Product product = await GetProductByIdAsync(id);
            
            product.IsActive = false;
            await this.repository.SaveChangesAsync();
        }
      
        public async Task RecoveryByIdAsync(string id)
        {
            Product product = await GetProductByIdAsync(id);

            product.IsActive = true;
            await this.repository.SaveChangesAsync();
        }

        public async Task<ProductViewModel> GetProductDetailsByIdAsync(string id)
        {
            Product glass = await this.repository
                .AllReadOnly<Product>()
                .Include(g => g.Category)
                .FirstAsync(g => g.Id.ToString() == id);

            return new ProductViewModel
            {
                Id = glass.Id.ToString(),
                Title = glass.Title,
                Description = glass.Description,
                ImageUrl = glass.ImageUrl,
                Price = glass.Price,
                CategoryName = glass.Category.Name,
            };
        }

        public async Task<ProductCommentViewModel> GetProductCommentByIdAsync(string id)
        {
            Product glass = await this.repository
                 .AllReadOnly<Product>()
                 .Include(g => g.Category)
                 .Include(g => g.Comments)
                 .ThenInclude(c => c.Comments)
                  .FirstAsync(g => g.Id.ToString() == id);

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

        private async Task<Product> GetProductByIdAsync(string id)
        {
            return await this.repository
                   .All<Product>()
                   .FirstAsync(b => b.Id.ToString() == id);
        }
    }
}
