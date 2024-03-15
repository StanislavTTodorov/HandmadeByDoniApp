﻿
using HandmadeByDoniApp.Data;
using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Box;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data
{
    public class BoxService : IBoxService
    {
        private readonly HandmadeByDoniAppDbContext dbContext;
        public BoxService(HandmadeByDoniAppDbContext dbContext)
        {
            this.dbContext = dbContext;
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
            };
            await dbContext.Boxs.AddRangeAsync(newBox);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
