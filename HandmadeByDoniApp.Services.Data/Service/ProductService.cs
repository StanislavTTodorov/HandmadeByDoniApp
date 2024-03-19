using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository repository;

        public ProductService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<IndexViewModel>> LastTwelveProductsAsync()
        {
            IEnumerable<IndexViewModel>? lastThreeGlass = await this.repository
                 .All<Glass>()
                 .Where(g=>g.IsActive &&
                           g.IsSet==false)
                 .OrderByDescending(g => g.CreateOn)
                 .Take(3)
                 .Select(g => new IndexViewModel()
                 {
                     Id = g.Id.ToString(),
                     Title = g.Title,
                     ImageUrl = g.ImageUrl,
                     CreatedOn = g.CreateOn

                 }).ToArrayAsync();

            IEnumerable<IndexViewModel>? lastThreeBoxs = await this.repository
               .All<Box>()
               .Where(b => b.IsActive)
               .OrderByDescending(b => b.CreateOn)
               .Take(3)
               .Select(b => new IndexViewModel()
               {
                   Id = b.Id.ToString(),
                   Title = b.Title,
                   ImageUrl = b.ImageUrl,
                   CreatedOn = b.CreateOn
               }).ToArrayAsync();

            IEnumerable<IndexViewModel>? lastThreeDecanters = await this.repository
               .All<Decanter>()
               .Where(g => g.IsActive &&
                           g.IsSet == false)
               .OrderByDescending(d => d.CreateOn)
               .Take(3)
               .Select(d => new IndexViewModel()
               {
                   Id = d.Id.ToString(),
                   Title = d.Title,
                   ImageUrl = d.ImageUrl,
                   CreatedOn = d.CreateOn
               }).ToArrayAsync();

            IEnumerable<IndexViewModel>? lastThreeSet = await this.repository
              .All<Set>()
              .Where(b => b.IsActive)
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
            IEnumerable<IndexViewModel> models = lastProduct.OrderByDescending(g => g.CreatedOn).ToArray();

            return models;

        }
    }
}
