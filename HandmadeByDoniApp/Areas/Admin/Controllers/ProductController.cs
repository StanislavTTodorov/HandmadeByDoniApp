using HandmadeByDoniApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static HandmadeByDoniApp.Common.NotificationMessagesConstants;
using static HandmadeByDoniApp.Common.GeneralApplicationConstants;

namespace HandmadeByDoniApp.Web.Areas.Admin.Controllers
{
    public class ProductController :BaseAdminController
    {
        private readonly IGlassService glassService;
        private readonly IDecanterService decanterService;
        private readonly IBoxService boxService;
        private readonly ISetService setService;
        private readonly IProductService productService;
        private readonly IGlassCategoryService categoryService;

        public ProductController(IGlassService glassService,
                                 IDecanterService decanterService,
                                 IBoxService boxService,
                                 ISetService setService,
                                 IProductService productService,
                                 IGlassCategoryService categoryService)
        {
            this.glassService = glassService;
            this.decanterService = decanterService;
            this.boxService = boxService;
            this.setService = setService;
            this.productService = productService;
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass)
            {
                return this.RedirectToAction("Edit", "Glass", new { id });
            }

            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if (isDecanter)
            {
                return this.RedirectToAction("Edit", "Decanter", new { id });
            }

            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox)
            {
                return this.RedirectToAction("Edit", "Box", new { id });
            }

            bool isSet = await this.setService.ExistsByIdAsync(id);
            if (isSet)
            {
                return this.RedirectToAction("Edit", "Set", new { id });
            }

            this.TempData[ErrorMessage] = "This product does not exist! These are all the products you can choose from.";
            return this.RedirectToAction("All", "Product", new { area = "" });

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass)
            {
                return this.RedirectToAction("Delete", "Glass", new { id,returnUrl });
            }

            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if (isDecanter)
            {
                return this.RedirectToAction("Delete", "Decanter", new { id, returnUrl });
            }

            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox)
            {
                return this.RedirectToAction("Delete", "Box", new { id, returnUrl });
            }

            bool isSet = await this.setService.ExistsByIdAsync(id);
            if (isSet)
            {
                return this.RedirectToAction("Delete", "Set", new { id, returnUrl });
            }

            this.TempData[ErrorMessage] = "This product does not exist! These are all the products you can choose from.";
            return this.RedirectToAction("All", "Product", new { area = "" });

        }
        [HttpGet]
        public async Task<IActionResult> Recovery(string id, string returnUrl)
        {
            bool isGlass = await this.glassService.ExistsByIdAsync(id);
            if (isGlass)
            {
                return this.RedirectToAction("Recovery", "Glass", new { id, returnUrl });
            }

            bool isDecanter = await this.decanterService.ExistsByIdAsync(id);
            if (isDecanter)
            {
                return this.RedirectToAction("Recovery", "Decanter", new { id, returnUrl });
            }

            bool isBox = await this.boxService.ExistsByIdAsync(id);
            if (isBox)
            {
                return this.RedirectToAction("Recovery", "Box", new { id, returnUrl });
            }

            bool isSet = await this.setService.ExistsByIdAsync(id);
            if (isSet)
            {
                return this.RedirectToAction("Recovery", "Set", new { id, returnUrl });
            }

            this.TempData[ErrorMessage] = "This product does not exist! These are all the products you can choose from.";
            return this.RedirectToAction("All", "Product", new { area = "" });

        }
    }
}
