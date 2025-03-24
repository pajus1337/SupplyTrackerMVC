using Microsoft.AspNetCore.Identity.UI.V5.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.VisualBasic;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ProductController : BaseController
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
        public IActionResult AddProduct()
        {
            var model = _productService.PrepareNewProductVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(NewProductVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.AddProductAsync(model, cancellationToken);
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
                return View("AddProduct", model);
            }

            TempData["Header"] = "Successfully added new product";
            return RedirectToAction("ViewProductDetails", new { productId = serviceResponse.ObjectId });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int productId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.PrepareUpdateProductVmAsync(productId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }


        // TODO: Refine, ( Error Handling ) Display success message of update process )
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductVm model, CancellationToken cancellationToken)
        {
            await _productService.UpdateProductAsync(model, cancellationToken);
            return View();
        }

        [HttpGet]
        public IActionResult AddProductType()
        {
            return View(new NewProductTypeVm());
        }


        // TODO: Refactor Model invalid stat  display method, ( using implementation from BaseController), Check if it comfor with isValid property.
        [HttpPost]
        public async Task<IActionResult> AddProductType(NewProductTypeVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.AddProductTypeAsync(model, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
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
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProductDetails(int productId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.GetProductDetailsByIdAsync(productId, cancellationToken);
            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProductList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.GetProductsForListAsync(5, 1, "", cancellationToken);

            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ViewProductList(int pageSize, CancellationToken cancellationToken, int pageNo = 1, string searchString = "")
        {
            var serviceResponse = await _productService.GetProductsForListAsync(pageSize, pageNo, searchString, cancellationToken);

            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }

        [HttpGet] 
        // TODO: Create Razor View
        public async Task<IActionResult> ViewProductTypesList(CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.GetProductTypesForListAsync(cancellationToken);

            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            return View(serviceResponse.Data);
        }


        // Implement PRG Pattern.
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId, CancellationToken cancellationToken)
        {
            var serviceResponse = await _productService.DeleteProductASync(productId, cancellationToken);

            if (!serviceResponse.Success)
            {
                return HandleErrors(serviceResponse);
            }

            TempData["SuccessMessage"] = $"Product with ID {productId} has been successfully deleted.";
            return RedirectToAction("ViewProductList");
        }
    }
}
