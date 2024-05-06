using Microsoft.AspNetCore.Identity.UI.V5.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.VisualBasic;
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
        public async Task<IActionResult> NewProduct(NewProductVm model, CancellationToken cancellationToken)
        {
           var (success, errors, productId) = await _productService.AddNewProductAsync(model, cancellationToken);
            if (!success)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                TempData["Header"] = "Add new product";
                return View("NewProduct", model);
            }

            TempData["Header"] = "Successfully added new product";
            return RedirectToAction("ViewProductDetails", new { productId = productId });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int productId, CancellationToken cancellationToken)
        {
            var model = await _productService.PrepareUpdateProductViewModel(productId, cancellationToken);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductVm model, CancellationToken cancellationToken)
        {
            await _productService.UpdateProductAsync(model, cancellationToken);
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
                TempData["Header"] = "Add new product type";

                return View("NewProductType", model);
            }

            TempData["Header"] = "Successfully added new product type";
            return RedirectToAction("ViewProductType", new { productTypeId = productTypeId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewProductType(int productTypeId, CancellationToken cancellationToken)
        {
            var (success, model) = await _productService.GetProductTypeByIdAsync(productTypeId, cancellationToken);
            if (!success)
            {
                // Add unsuccess case 
                return View();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProductDetails(int productId, CancellationToken cancellationToken)
        {
            var (success, model) = await _productService.GetProductDetailsByIdAsync(productId, cancellationToken);
            if (!success)
            {
                // Add !success handler 
                return View();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProductList(CancellationToken cancellationToken)
        {
           var model = await _productService.GetAllActiveProductsForListAsync(cancellationToken);
            return View(model);
        }
    }
}
