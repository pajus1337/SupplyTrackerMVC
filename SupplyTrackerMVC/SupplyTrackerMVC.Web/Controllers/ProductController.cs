using Microsoft.AspNetCore.Mvc;
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
        public IActionResult NewProductType(NewProductTypeVm newProductTypeVm)
        {
            return View();
        }
    }
}
