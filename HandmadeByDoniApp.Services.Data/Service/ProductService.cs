
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Servises.Data.Models.Product;
using HandmadeByDoniApp.Web.ViewModels.Comment;
using HandmadeByDoniApp.Web.ViewModels.Glass;
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

            IQueryable<Glass> glassesQuery = this.repository
                .All<Glass>();

            IQueryable<Decanter> decanterQuery = this.repository
               .All<Decanter>()
               .AsQueryable();

            IQueryable<Box> boxQuery = this.repository
               .All<Box>()
               .AsQueryable();

            IQueryable<Set> setQuery = this.repository
               .All<Set>()
               .AsQueryable();

            if (string.IsNullOrEmpty(queryModel.GlassCategory) == false)
            {
                glassesQuery = glassesQuery
                    .Where(g => g.GlassCategory.Name == queryModel.GlassCategory);
            }

            if (string.IsNullOrWhiteSpace(queryModel.SearchString) == false)
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                glassesQuery = glassesQuery
                    .Where(g => EF.Functions.Like(g.Title, wildCard) ||
                               (!string.IsNullOrEmpty(g.Description) && EF.Functions.Like(g.Description, wildCard)));

                decanterQuery = decanterQuery
                .Where(g => EF.Functions.Like(g.Title, wildCard) ||
                           (!string.IsNullOrEmpty(g.Description) && EF.Functions.Like(g.Description, wildCard)));

                boxQuery = boxQuery
                .Where(g => EF.Functions.Like(g.Title, wildCard) ||
                           (!string.IsNullOrEmpty(g.Description) && EF.Functions.Like(g.Description, wildCard)));

                setQuery = setQuery
                .Where(g => EF.Functions.Like(g.Title, wildCard) ||
                           (!string.IsNullOrEmpty(g.Description) && EF.Functions.Like(g.Description, wildCard)));
            }

            glassesQuery = queryModel.ProductSorting switch
            {
                ProductSorting.Newest => glassesQuery
                    .Where(h => h.IsActive)
                    .OrderByDescending(h => h.CreatedOn),
                ProductSorting.Oldest => glassesQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.CreatedOn),
                ProductSorting.PriceAscending => glassesQuery
                    .Where(h => h.IsActive)
                    .Where(h => h.IsSet == false)
                    .OrderBy(h => h.Price),
                ProductSorting.PriceDescending => glassesQuery
                    .Where(h => h.IsActive)
                    .Where(h => h.IsSet == false)
                    .OrderByDescending(h => h.Price),
                ProductSorting.Active => glassesQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.IsActive)
                    .ThenByDescending(h => h.CreatedOn),
               ProductSorting.NotActive =>glassesQuery
                     .Where(h => !h.IsActive)
                     .OrderBy(h => h.CreatedOn),
                _ => glassesQuery
                     .Where(h => h.IsActive)
                     .OrderBy(h => h.CreatedOn)
            };
            decanterQuery = queryModel.ProductSorting switch
            {
                ProductSorting.Newest => decanterQuery
                    .Where(h => h.IsActive)
                    .OrderByDescending(h => h.CreatedOn),
                ProductSorting.Oldest => decanterQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.CreatedOn),
                ProductSorting.PriceAscending => decanterQuery
                    .Where(h => h.IsActive)
                    .Where(h => h.IsSet == false)
                    .OrderBy(h => h.Price),
                ProductSorting.PriceDescending => decanterQuery
                    .Where(h => h.IsActive)
                    .Where(h => h.IsSet ==false)
                    .OrderByDescending(h => h.Price),
                ProductSorting.Active => decanterQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.IsActive)
                    .ThenByDescending(h => h.CreatedOn),
                ProductSorting.NotActive => decanterQuery
                .Where(h => !h.IsActive)
                .OrderBy(h => h.CreatedOn),
                _ => decanterQuery
                     .Where(h => h.IsActive)
                     .OrderBy(h => h.CreatedOn)
            };
            boxQuery = queryModel.ProductSorting switch
            {
                ProductSorting.Newest => boxQuery
                    .Where(h => h.IsActive)
                    .OrderByDescending(h => h.CreatedOn),
                ProductSorting.Oldest => boxQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.CreatedOn),
                ProductSorting.PriceAscending => boxQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.Price),
                ProductSorting.PriceDescending => boxQuery
                    .Where(h => h.IsActive)                   
                    .OrderByDescending(h => h.Price),
                ProductSorting.Active => boxQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.IsActive)
                    .ThenByDescending(h => h.CreatedOn),
                ProductSorting.NotActive => boxQuery
                .Where(h => !h.IsActive)
                .OrderBy(h => h.CreatedOn),
                _ => boxQuery
                     .Where(h => h.IsActive)
                     .OrderBy(h => h.CreatedOn)
            };
            setQuery = queryModel.ProductSorting switch
            {
                ProductSorting.Newest => setQuery
                .Where(h => h.IsActive)
                    .OrderByDescending(h => h.CreatedOn),
                ProductSorting.Oldest => setQuery
                .Where(h => h.IsActive)
                    .OrderBy(h => h.CreatedOn),
                ProductSorting.PriceAscending => setQuery
                .Where(h => h.IsActive)
                    .OrderBy(h => h.Price),
                ProductSorting.PriceDescending => setQuery
                .Where(h => h.IsActive)
                    .OrderByDescending(h => h.Price),
                ProductSorting.Active => setQuery
                    .Where(h => h.IsActive)
                    .OrderBy(h => h.IsActive)
                    .ThenByDescending(h => h.CreatedOn),
                ProductSorting.NotActive => setQuery
                .Where(h => !h.IsActive)
                .OrderBy(h => h.CreatedOn),
                _ => setQuery
                     .Where(h => h.IsActive)
                     .OrderBy(h => h.CreatedOn)
            };

            List<ProductsAllViewModel> allProductsModels = new List<ProductsAllViewModel>();
            switch (queryModel.ProductsName)
            {
                case ProductsName.All:
                    allProductsModels = await Sort(allProductsModels, glassesQuery, decanterQuery, setQuery, boxQuery, queryModel.ProductSorting);
                    break;
                case ProductsName.Glass:
                    allProductsModels = await glassesQuery.Select(g => new ProductsAllViewModel
                    {
                        Id = g.Id.ToString(),
                        Title = g.Title,
                        Description = g.Description,
                        ImageUrl = g.ImageUrl,
                        Price = g.Price,
                        CreatedOn = g.CreatedOn,
                        IsActive = g.IsActive,
                        IsSet = g.IsSet,
                        
                    }).ToListAsync();
                    break;
                case ProductsName.Decanter:
                    allProductsModels = await decanterQuery.Select(d => new ProductsAllViewModel
                    {
                        Id = d.Id.ToString(),
                        Title = d.Title,
                        Description = d.Description,
                        ImageUrl = d.ImageUrl,
                        Price = d.Price,
                        CreatedOn = d.CreatedOn,
                        IsActive = d.IsActive,
                        IsSet = d.IsSet,
                        
                    }).ToListAsync();
                    break;
                case ProductsName.Box:
                    allProductsModels = await boxQuery.Select(b => new ProductsAllViewModel
                    {
                        Id = b.Id.ToString(),
                        Title = b.Title,
                        Description = b.Description,
                        ImageUrl = b.ImageUrl,
                        Price = b.Price,
                        CreatedOn = b.CreatedOn,
                        IsActive = b.IsActive,
                        IsSet = false

                    }).ToListAsync();
                    break;
                case ProductsName.Set:
                    allProductsModels = await setQuery.Select(s => new ProductsAllViewModel
                    {
                        Id = s.Id.ToString(),
                        Title = s.Title,
                        Description = s.Description,
                        ImageUrl = s.ImageUrl,
                        Price = s.Price,
                        CreatedOn = s.CreatedOn,
                        IsActive = s.IsActive,
                        IsSet = false
                        
                    }).ToListAsync();
                    break;
                default:
     
                    break;
            }

            IEnumerable<ProductsAllViewModel> allViewModels = allProductsModels.Skip((queryModel.CurrentPage - 1) * queryModel.ProductPerPage)
                                                                            .Take(queryModel.ProductPerPage).ToArray();

            int totalProduct = allProductsModels.Count();

            return new AllProductFilteredAndPagedServiceModel()
            {
                TotalProductCount = totalProduct,
                Products = allViewModels
            };

        }

        private async Task<List<ProductsAllViewModel>> Sort(List<ProductsAllViewModel> allProductModels,
                                               IQueryable<Glass> glassesQuery,
                                               IQueryable<Decanter> decanterQuery,
                                               IQueryable<Set> setQuery,
                                               IQueryable<Box> boxQuery,
                                               ProductSorting? productSorting)
        {
            List<ProductsAllViewModel> glassModels = await glassesQuery.Select(x => new ProductsAllViewModel
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                CreatedOn = x.CreatedOn,
                IsActive = x.IsActive,
                IsSet = x.IsSet,

            }).ToListAsync();
            List<ProductsAllViewModel> decanterModels = await decanterQuery.Select(x => new ProductsAllViewModel
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                CreatedOn = x.CreatedOn,
                IsActive = x.IsActive,
                IsSet = x.IsSet,

            }).ToListAsync();
            List<ProductsAllViewModel> boxModels = await boxQuery.Select(x => new ProductsAllViewModel
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                CreatedOn = x.CreatedOn,
                IsActive = x.IsActive,
                IsSet = false
            }).ToListAsync();
            List<ProductsAllViewModel> setModels = await setQuery.Select(x => new ProductsAllViewModel
            {
                Id = x.Id.ToString(),
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                CreatedOn = x.CreatedOn,
                IsActive = x.IsActive,
                IsSet = false
            }).ToListAsync();

            allProductModels.AddRange(glassModels);
            allProductModels.AddRange(decanterModels);
            allProductModels.AddRange(boxModels);
            allProductModels.AddRange(setModels);

            List<ProductsAllViewModel> allProductModelsSort;
            switch (productSorting)
            {
                case ProductSorting.Newest:
                    allProductModelsSort = allProductModels.OrderByDescending(h => h.CreatedOn).ToList();
                    break;
                case ProductSorting.Oldest:
                    allProductModelsSort = allProductModels.OrderBy(h => h.CreatedOn).ToList();
                    break;
                case ProductSorting.PriceAscending:
                    allProductModelsSort = allProductModels.OrderBy(h => h.Price).ToList();
                    break;
                case ProductSorting.PriceDescending:
                    allProductModelsSort = allProductModels.OrderByDescending(h => h.Price).ToList();
                    break;
                case ProductSorting.Active:
                    allProductModelsSort = allProductModels.OrderBy(h => h.IsActive)
                          .ThenByDescending(h => h.CreatedOn).ToList();
                    break;
                case ProductSorting.NotActive:
                    allProductModelsSort = allProductModels.Where(h => !h.IsActive)
                           .OrderBy(h => h.CreatedOn).ToList();
                    break;
                default:
                    allProductModelsSort = allProductModels.ToList();
                    break;
            }
            return allProductModelsSort;
        }



        //private ProductsAllViewModel Sort<T>(this IQueryable<T> productQuery, ProductSorting? productSorting)where T : class
      
 

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
            bool exists = await repository.All<Box>()
                 .AnyAsync(b => b.Id.ToString() == productId);

            return exists;
        }

        public async Task CreateProductAsync(ProductFormModel formModel)
        {
            Product newProduct = new Product()
            {
                Title = formModel.Title,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,               
                Price = formModel.Price,
                CategoryId = formModel.CategoryId,
                IsActive = true,
            };

            await repository.AddRangeAsync(newProduct);
            await repository.SaveChangesAsync();
        }

        public Task CreateCommentByUserIdAndByProductIdAsync(string userId, CommentFormModel formModel, string productId)
        {
            throw new NotImplementedException();
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
                ImageUrl = product.ImageUrl,              
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }

        public async Task EditProductByIdAndFormModelAsync(string id, ProductFormModel formModel)
        {
            Product product = await this.repository
               .All<Product>()
               .FirstAsync(g => g.Id.ToString() == id);

            product.Title = formModel.Title;
            product.Description = formModel.Description;
            product.ImageUrl = formModel.ImageUrl;
            product.Price = formModel.Price;
            product.CategoryId = formModel.CategoryId;

            await this.repository.SaveChangesAsync();
        }

        public async Task SoftDeleteByIdAsync(string id)
        {
            Product product = await this.repository
                   .All<Product>()
                   .FirstAsync(b => b.Id.ToString() == id);

            product.IsActive = false;
            await this.repository.SaveChangesAsync();
        }

        public async Task RecoveryByIdAsync(string id)
        {
            Product product = await this.repository
                   .All<Product>()
                   .FirstAsync(b => b.Id.ToString() == id);

            product.IsActive = true;
            await this.repository.SaveChangesAsync();
        }
    }
}
