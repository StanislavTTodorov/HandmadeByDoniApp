

using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace HandmadeByDoniApp.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly HandmadeByDoniAppDbContext dbContext;

        public ProductService(HandmadeByDoniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> LastTwelveProductsAsync()
        {
            IEnumerable<IndexViewModel>? lastThreeGlass = await this.dbContext
                 .Glasses
                 .OrderByDescending(g => g.CreateOn)
                 .Take(3)
                 .Select(g => new IndexViewModel()
                 {
                     Id = g.Id.ToString(),
                     Title = g.Title,
                     ImageUrl = g.ImageUrl,
                     CreatedOn = g.CreateOn
                     
                 }).ToArrayAsync();

            IEnumerable<IndexViewModel>? lastThreeBoxs = await this.dbContext
               .Boxs
               .OrderByDescending(b => b.CreateOn)
               .Take(3)
               .Select(b => new IndexViewModel()
               {
                   Id = b.Id.ToString(),
                   Title = b.Title,
                   ImageUrl = b.ImageUrl,
                    CreatedOn = b.CreateOn
               }).ToArrayAsync();

            IEnumerable<IndexViewModel>? lastThreeDecanters = await this.dbContext
               .Decaners
               .OrderByDescending(d => d.CreateOn)
               .Take(3)
               .Select(d => new IndexViewModel()
               {
                   Id = d.Id.ToString(),
                   Title = d.Title,
                   ImageUrl = d.ImageUrl,
                   CreatedOn = d.CreateOn
               }).ToArrayAsync();

            IEnumerable<IndexViewModel>? lastThreeSet = await this.dbContext
              .Sets
              .OrderByDescending(s => s.CreateOn)
              .Take(3)
              .Select(s => new IndexViewModel()
              {
                  Id = s.Id.ToString(),
                  Title = s.Title,
                  ImageUrl = s.ImageUrl,
                  CreatedOn = s.CreateOn
              }).ToArrayAsync();

            List<IndexViewModel> lastProduct = new List<IndexViewModel>();
            lastProduct.AddRange(lastThreeGlass);
            lastProduct.AddRange(lastThreeBoxs);
            lastProduct.AddRange(lastThreeSet);
            lastProduct.AddRange(lastThreeDecanters);
            IEnumerable<IndexViewModel> models = lastProduct.OrderByDescending(g=>g.CreatedOn);

            return models;

        }
    }
}
