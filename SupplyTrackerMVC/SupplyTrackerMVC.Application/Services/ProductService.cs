using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Products;
using System.Net.Http.Headers;

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

        public async Task<ServiceResponse<VoidValue>> AddNewProductAsync(NewProductVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            var validator = _validatorFactory.GetValidator<NewProductVm>();
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return ServiceResponse<VoidValue>.CreateFailed(result.Errors.Select(e => e.ErrorMessage));
            }

            var product = _mapper.Map<Product>(model);
            try
            {
                var (productId, isSuccess) = await _productRepository.AddProductAsync(product, cancellationToken);
                if (!isSuccess)
                {
                    return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Failed to add new product" });
                }

                return ServiceResponse<VoidValue>.CreateSuccess(null, productId);
            }
            catch (Exception ex)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<UpdateProductVm>> UpdateProductAsync(UpdateProductVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            // TODO : Add Validation

            var product = _mapper.Map<Product>(model);
            try
            {
                var isSuccess = await _productRepository.UpdateProductAsync(product, cancellationToken);

                if (!isSuccess)
                {
                    return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { "Failed to update product" });
                }

                // TODO: Consider retruning bool or model
                return ServiceResponse<UpdateProductVm>.CreateSuccess(null);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<VoidValue>> DeleteProductASync(int productId, CancellationToken cancellationToken)
        {
            if (productId == 0)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Wrong product Id" });
            }

            var isSuccess = await _productRepository.DeleteProductAsync(productId, cancellationToken);

            return isSuccess ? ServiceResponse<VoidValue>.CreateSuccess(null) : ServiceResponse<VoidValue>.CreateFailed(new string[] { "An error occurred while deleting" });
        }

        public async Task<ServiceResponse<ProductSelectListVm>> GetAllActiveProductsForSelectList(CancellationToken cancellationToken)
        {
            var productsQuery = _productRepository.GetAllProducts().ProjectTo<ProductForSelectListVm>(_mapper.ConfigurationProvider);
            try
            {
                // TODO: Make manual test
                var products = await productsQuery.ToListAsync(cancellationToken);
                if (products == null)
                {
                    return ServiceResponse<ProductSelectListVm>.CreateFailed(new string[] { "ProductTypes call returned null" });
                }

                var productsVm = new ProductSelectListVm()
                {
                    Products = products
                };

                return ServiceResponse<ProductSelectListVm>.CreateSuccess(productsVm);

            }
            catch (Exception ex)
            {
                return ServiceResponse<ProductSelectListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ListProductForListVm>> GetAllProductsForListAsync(CancellationToken cancellationToken)
        {
            // HACK: Refactor, only test version.
            var productsQuery = _productRepository.GetAllProducts().ProjectTo<ProductForListVm>(_mapper.ConfigurationProvider);

            try
            {
                var products = await productsQuery.ToListAsync(cancellationToken);

                // TODO: Add null check ? 

                ListProductForListVm listProductForListVm = new ListProductForListVm()
                {
                    Products = products,
                    Count = products.Count
                };

                return ServiceResponse<ListProductForListVm>.CreateSuccess(listProductForListVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ListProductForListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ProductDetailVm>> GetProductDetailsByIdAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0)
            {
                return ServiceResponse<ProductDetailVm>.CreateFailed(new string[] { "Wrong product Id" });
            }

            var productQuery = _productRepository.GetProductById(productId);
            try
            {
                var product = await productQuery.SingleOrDefaultAsync(cancellationToken);
                if (product == null)
                {
                    return ServiceResponse<ProductDetailVm>.CreateFailed(new string[] { "Product not found" });
                }

                var productVm = _mapper.Map<ProductDetailVm>(product);

                return ServiceResponse<ProductDetailVm>.CreateSuccess(productVm);
            }

            catch (Exception ex)
            {
                return ServiceResponse<ProductDetailVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<UpdateProductVm>> GetPreparedUpdateProductVmAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0)
            {
                return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { "Error: Invalid product Id" });
            }

            var productQuery = _productRepository.GetProductById(productId);
            try
            {
                var product = await productQuery.SingleOrDefaultAsync(cancellationToken);
                if (product == null)
                {
                    return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { "Error: Sender is null" });
                }

                var productVm = _mapper.Map<UpdateProductVm>(product);
                // TODO: check
                // productVm.ProductType = GetProductTypes();

                return ServiceResponse<UpdateProductVm>.CreateSuccess(productVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
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

        public async Task<ServiceResponse<VoidValue>> AddNewProductTypeAsync(NewProductTypeVm model, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.GetValidator<NewProductTypeVm>();
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return ServiceResponse<VoidValue>.CreateFailed(result.Errors.Select(e => e.ErrorMessage));
            }

            var productType = _mapper.Map<ProductType>(model);
            var productTypeId = await _productRepository.AddProductTypeAsync(productType, cancellationToken);

            return ServiceResponse<VoidValue>.CreateSuccess(null, productTypeId);
        }

        public Task<ServiceResponse<ListProductTypeForListVm>> GetAllProductTypesForListAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ProductTypeVm>> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken)
        {
            if (productTypeId <= 0)
            {
                return ServiceResponse<ProductTypeVm>.CreateFailed(new string[] { "Error: Invalid product type Id" });
            }

            var productTypeQuery = _productRepository.GetProductTypeById(productTypeId);

            try
            {
                var productType = await productTypeQuery.SingleOrDefaultAsync(cancellationToken);
                if (productType == null)
                {
                    return ServiceResponse<ProductTypeVm>.CreateFailed(new string[] { "Error: Product type is null" });
                }

                var productTypeVm = _mapper.Map<ProductTypeVm>(productType);

                return ServiceResponse<ProductTypeVm>.CreateSuccess(productTypeVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ProductTypeVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        private ProductTypeSelectListVm GetProductTypes() => new ProductTypeSelectListVm()
        {
            ProductTypes = _productRepository.GetAllProductTypes().ProjectTo<ProductTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };
    }
}
