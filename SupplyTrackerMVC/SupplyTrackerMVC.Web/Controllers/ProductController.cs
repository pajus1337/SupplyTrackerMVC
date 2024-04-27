using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewProduct()
        {
            var model = _productService.PrepareNewProductViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult NewProduct(NewProductVm newProductVm)
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewProductType()
        {
            return View(new NewProductTypeVm());
        }

        [HttpPost]
        public async Task<IActionResult> NewProductType(NewProductTypeVm model, CancellationToken cancellationToken)
        {
            var (success, errors, productTypeId) = await _productService.AddNewProductTypeAsync(model, cancellationToken);
            if (!success)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View("NewProductType", model);
            }

            //Add details info about successfully add of new product.
            return View();
        }

        public async Task<IActionResult> ViewProductType(int productTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
