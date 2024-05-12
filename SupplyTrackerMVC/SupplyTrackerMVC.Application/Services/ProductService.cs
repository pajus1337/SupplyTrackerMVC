using AutoMapper;
using AutoMapper.QueryableExtensions;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Products;

namespace SupplyTrackerMVC.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IFluentValidatorFactory _validatorFactory;
        public ProductService(IProductRepository productRepository, IMapper mapper, IFluentValidatorFactory validatorFactory)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validatorFactory = validatorFactory;
        }

        public async Task<(bool Success, IEnumerable<string>? Errors, int? ProductId)> AddNewProductAsync(NewProductVm model, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.GetValidator<NewProductVm>();
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return (false, result.Errors.Select(e => e.ErrorMessage), null);
            }

            var product = _mapper.Map<Product>(model);
            var (productId, isSuccess) = await _productRepository.AddProductAsync(product, cancellationToken);
            if (!isSuccess)
            {
                return (false, null, null);
            }

            return (true, null, productId);
        }

        public async Task<(bool Success, string? Error)> UpdateProductAsync(UpdateProductVm updateProductVm, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(updateProductVm);
            var success = await _productRepository.UpdateProductAsync(product, cancellationToken);
            if (!success)
            {
                // Add Error message Handler
                return (false, null);
            }
            return (true, null);
        }

        public async Task<bool> DeleteProductASync(int productId, CancellationToken cancellationToken) => await _productRepository.DeleteProductAsync(productId, cancellationToken);

        public ProductSelectListVm GetAllActiveProductsForSelectList()
        {
            var products = _productRepository.GetAllProducts().ProjectTo<ProductForSelectListVm>(_mapper.ConfigurationProvider);
            var productsVm = new ProductSelectListVm()
            {
                Products = products
            };

            return productsVm;
        }

        public async Task<ListProductForList> GetAllActiveProductsForListAsync(CancellationToken cancellationToken)
        {
            // Refactor, only test version.
            var products = _productRepository.GetAllProducts().ProjectTo<ProductForListVm>(_mapper.ConfigurationProvider);

            var ProductListVm = new ListProductForList
            {
                Products = products.ToList(),
                Count = products.Count()
            };

            return ProductListVm;

        }

        public async Task<(bool Success, ProductDetailVm)> GetProductDetailsByIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
            var productVm = _mapper.Map<ProductDetailVm>(product);

            // success ? 
            return (true, productVm);
        }

        public async Task<UpdateProductVm> PrepareUpdateProductViewModel(int productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
            var productVm = _mapper.Map<UpdateProductVm>(product);
            return productVm;
        }

        public NewProductVm PrepareNewProductViewModel()
        {
            var model = new NewProductVm
            {
                isActive = true,
                ProductType = GetProductTypes()
            };

            return model;
        }

        public async Task<(bool Success, IEnumerable<string>? Errors, int? ProductTypeId)> AddNewProductTypeAsync(NewProductTypeVm model, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.GetValidator<NewProductTypeVm>();
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return (false, result.Errors.Select(e => e.ErrorMessage), null);
            }

            var productType = _mapper.Map<ProductType>(model);
            var productTypeId = await _productRepository.AddProductTypeAsync(productType, cancellationToken);

            return (true, null, productTypeId);
        }

        public async Task<(bool Success, ProductTypeVm)> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken)
        {
            var productType = await _productRepository.GetProductTypeByIdAsync(productTypeId, cancellationToken);
            if (productType == null)
            {
                return (false, null);
            }
            var productTypeVm = _mapper.Map<ProductTypeVm>(productType);

            return (true, productTypeVm);  
        }

        private ProductTypeSelectListVm GetProductTypes() => new ProductTypeSelectListVm()
        {
            ProductTypes = _productRepository.GetAllProductTypes().ProjectTo<ProductTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };
    }
}
