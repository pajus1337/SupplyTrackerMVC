using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
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

        public async Task<ServiceResponse<VoidValue>> AddProductAsync(NewProductVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<NewProductVm>();
                var result = await validator.ValidateAsync(model, cancellationToken);
                if (!result.IsValid)
                {
                    return ServiceResponse<VoidValue>.CreateFailed(result.Errors.Select(e => e.ErrorMessage), true);
                }

                var product = _mapper.Map<Product>(model);
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

            try
            {
                var validator = _validatorFactory.GetValidator<UpdateProductVm>();
                var result = await validator.ValidateAsync(model, cancellationToken);
                if (!result.IsValid)
                {
                    return ServiceResponse<UpdateProductVm>.CreateFailed(result.Errors.Select(e => e.ErrorMessage), true);
                }

                var product = _mapper.Map<Product>(model);
                var isSuccess = await _productRepository.UpdateProductAsync(product, cancellationToken);

                if (!isSuccess)
                {
                    return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { "Failed to update product" });
                }

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

        public async Task<ServiceResponse<ProductSelectListVm>> GetProductsForSelectList(CancellationToken cancellationToken)
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

        public async Task<ServiceResponse<ListProductForListVm>> GetProductsForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            try
            {
                var productsQuery = _productRepository.GetAllProducts().Where(p => p.Name.StartsWith(searchString)).ProjectTo<ProductForListVm>(_mapper.ConfigurationProvider);
                var productsToShow = productsQuery.Skip(pageSize * (pageNo - 1)).Take(pageSize);
                var products = await productsToShow.ToListAsync(cancellationToken);

                ListProductForListVm listProductForListVm = new ListProductForListVm()
                {
                    Products = products,
                    Count = productsQuery.Count(),
                    PageSize = pageSize,
                    CurrentPage = pageNo,
                    SearchString = searchString,
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

        public async Task<ServiceResponse<UpdateProductVm>> PrepareUpdateProductVmAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0)
            {
                return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { "Error: Invalid product Id" });
            }

            try
            {
                var productQuery = _productRepository.GetProductById(productId);
                var product = await productQuery.SingleOrDefaultAsync(cancellationToken);
                if (product == null)
                {
                    return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { "Error: Product not found in Database" });
                }

                var productVm = _mapper.Map<UpdateProductVm>(product);
                productVm.ProductType = GetProductTypes();

                return ServiceResponse<UpdateProductVm>.CreateSuccess(productVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateProductVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public NewProductVm PrepareNewProductVm()
        {
            var model = new NewProductVm
            {
                isActive = true,
                ProductType = GetProductTypes()
            };

            return model;
        }

        public async Task<ServiceResponse<VoidValue>> AddProductTypeAsync(NewProductTypeVm model, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.GetValidator<NewProductTypeVm>();
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return ServiceResponse<VoidValue>.CreateFailed(result.Errors.Select(e => e.ErrorMessage), true);
            }

            var productType = _mapper.Map<ProductType>(model);
            var productTypeId = await _productRepository.AddProductTypeAsync(productType, cancellationToken);

            return ServiceResponse<VoidValue>.CreateSuccess(null, productTypeId);
        }

        public async Task<ServiceResponse<ListProductTypeForListVm>> GetProductTypesForListAsync(CancellationToken cancellationToken)
        {
            try
            {
                var productTypesQuery = _productRepository.GetAllProductTypes();
                var productTypes = await productTypesQuery.ProjectTo<ProductTypeForListVm>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

                ListProductTypeForListVm result = new ListProductTypeForListVm()
                {
                    ProductTypes = productTypes,
                    Count = productTypesQuery.Count(),
                };

                return ServiceResponse<ListProductTypeForListVm>.CreateSuccess(result);
            }

            catch (Exception ex)
            {
                return ServiceResponse<ListProductTypeForListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ProductTypeVm>> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken)
        {
            if (productTypeId <= 0)
            {
                return ServiceResponse<ProductTypeVm>.CreateFailed(new string[] { "Error: Invalid product type Id" });
            }

            try
            {
                var productTypeQuery = _productRepository.GetProductTypeById(productTypeId);
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

        public Task<ServiceResponse<UpdateProductTypeVm>> UpdateProductTypeAsync(int productTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<VoidValue>> DeleteProductTypeAsync(int productTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<UpdateProductTypeVm>> GetProductTypeToEditAsync(int productTypeId, CancellationToken cancellationToken)
        {
            if (productTypeId < 1 )
            {
                return ServiceResponse<UpdateProductTypeVm>.CreateFailed(new string[] { "Invalid Product Type ID" });
            }

            try
            {
                var productTypeVm = await _productRepository.GetProductTypeById(productTypeId).ProjectTo<UpdateProductTypeVm>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                if (productTypeVm == null)
                {
                    return ServiceResponse<UpdateProductTypeVm>.CreateFailed(new string[] { "Product Type not found" });
                }

                return ServiceResponse<UpdateProductTypeVm>.CreateSuccess(productTypeVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateProductTypeVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public Task<ServiceResponse<UpdateProductTypeVm>> UpdateProductTypeAsync(UpdateProductTypeVm model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
