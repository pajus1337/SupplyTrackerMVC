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
            var serviceResponse = await _productService.AddNewProductAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                if (serviceResponse.ErrorMessage != null)
                {
                    foreach (var error in serviceResponse.ErrorMessage)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }

                TempData["Header"] = "Add new product";
                return View("NewProduct", model);
            }

            TempData["Header"] = "Successfully added new product";
            return RedirectToAction("ViewProductDetails", new { productId = serviceResponse.ObjectId });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int productId, CancellationToken cancellationToken)
        {
            var model = await _productService.GetPreparedUpdateProductVmAsync(productId, cancellationToken);
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
            var serviceResponse = await _productService.AddNewProductTypeAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                if (serviceResponse.ErrorMessage != null)
                {
                    foreach (var error in serviceResponse.ErrorMessage)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
                TempData["Header"] = "Add new product type";

                return View("NewProductType", model);
            }

            TempData["Header"] = "Successfully added new product type";
            return RedirectToAction("ViewProductType", new { productTypeId = serviceResponse.ObjectId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewProductType(int productTypeId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.GetProductTypeByIdAsync(productTypeId, cancellationToken);
            if (!serviceResponse.Success)
            {
                // TODO: Add unsuccess case 
                return View();
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProductDetails(int productId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.GetProductDetailsByIdAsync(productId, cancellationToken);
            if (!serviceResponse.Success)
            {
                // TODO: Add !success handler 
                return View();
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.GetAllProductsForListAsync(cancellationToken);

            if (!serviceResponse.Success)
            {
                // TODO: Add !success handler 
                return View();
            }

            return View(serviceResponse.Data);
        }

        [HttpGet] 
        // TODO: Create Razor View
        public async Task<IActionResult> ProductTypesList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.GetAllProductTypesForListAsync(cancellationToken);

            if (!serviceResponse.Success)
            {
                // TODO: Add !success handler 
                return View();
            }

            return View(serviceResponse.Data);
        }
    }
}
