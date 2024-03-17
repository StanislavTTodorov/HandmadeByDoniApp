﻿using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;

namespace HandmadeByDoniApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGlassService glassService;
        private readonly IDecanterService decanterService;
        private readonly IBoxService boxService;
        private readonly ISetService setService;

        public ProductController(IGlassService glassService,
                                 IDecanterService decanterService,
                                 IBoxService boxService,
                                 ISetService setService)
        {
            this.glassService = glassService;
            this.decanterService = decanterService;
            this.boxService = boxService;
            this.setService = setService;
        }

        public async Task<IActionResult> Details(string id)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if(isGlass)
            {
                return this.RedirectToAction("Details", "Glass", new { id });
            }

            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if(isDecanter)
            {
                return this.RedirectToAction("Details","Decanter",new { id });
            }

            bool isBox =  await this.boxService.ExistsByIdAsync(id);
            if(isBox)
            {
                return this.RedirectToAction("Details", "Box", new { id });
            }

            bool isSet = await this.setService.ExistsByIdAsync(id);
            if(isSet)
            {
                return this.RedirectToAction("Details", "Set", new { id });
            }

            this.TempData[ErrorMessage] = "This product does not exist! These are all the products you can choose.";
            return this.RedirectToAction("All", "Product");

        }
    }
}
