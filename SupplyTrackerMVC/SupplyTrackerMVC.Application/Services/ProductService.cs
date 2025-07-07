using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
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

        public async Task<ActionResponse<VoidValue>> AddProductAsync(NewProductVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<NewProductVm>();
                var result = await validator.ValidateAsync(model, cancellationToken);
                if (!result.IsValid)
                {
                    return ActionResponse<VoidValue>.Failed(result.Errors.Select(e => e.ErrorMessage), true);
                }

                var product = _mapper.Map<Product>(model);
                var (productId, isSuccess) = await _productRepository.AddProductAsync(product, cancellationToken);
                if (!isSuccess)
                {
                    return ActionResponse<VoidValue>.Failed(new string[] { "Failed to add new product" });
                }

                return ActionResponse<VoidValue>.Success(null, productId);
            }
            catch (Exception ex)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<UpdateProductVm>> UpdateProductAsync(UpdateProductVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ActionResponse<UpdateProductVm>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<UpdateProductVm>();
                var result = await validator.ValidateAsync(model, cancellationToken);
                if (!result.IsValid)
                {
                    return ActionResponse<UpdateProductVm>.Failed(result.Errors.Select(e => e.ErrorMessage), true);
                }

                var product = _mapper.Map<Product>(model);
                var isSuccess = await _productRepository.UpdateProductAsync(product, cancellationToken);

                if (!isSuccess)
                {
                    return ActionResponse<UpdateProductVm>.Failed(new string[] { "Failed to update product" });
                }

                return ActionResponse<UpdateProductVm>.Success(null);
            }
            catch (Exception ex)
            {
                return ActionResponse<UpdateProductVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<VoidValue>> DeleteProductASync(int productId, CancellationToken cancellationToken)
        {
            if (productId == 0)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { "Wrong product Id" });
            }

            var isSuccess = await _productRepository.DeleteProductAsync(productId, cancellationToken);

            return isSuccess ? ActionResponse<VoidValue>.Success(null) : ActionResponse<VoidValue>.Failed(new string[] { "An error occurred while deleting" });
        }

        public async Task<ActionResponse<ProductSelectListVm>> GetProductsForSelectList(CancellationToken cancellationToken)
        {
            var productsQuery = _productRepository.GetAllProducts().ProjectTo<ProductForSelectListVm>(_mapper.ConfigurationProvider);
            try
            {
                // TODO: Make manual test
                var products = await productsQuery.ToListAsync(cancellationToken);
                if (products == null)
                {
                    return ActionResponse<ProductSelectListVm>.Failed(new string[] { "ProductTypes call returned null" });
                }

                var productsVm = new ProductSelectListVm()
                {
                    Products = products
                };

                return ActionResponse<ProductSelectListVm>.Success(productsVm);

            }
            catch (Exception ex)
            {
                return ActionResponse<ProductSelectListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ListProductForListVm>> GetProductsForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            try
            {
                var productsQuery = _productRepository.GetAllProducts().Where(p => p.Name.StartsWith(searchString)).OrderBy(p => p.Id).ProjectTo<ProductForListVm>(_mapper.ConfigurationProvider);
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

                return ActionResponse<ListProductForListVm>.Success(listProductForListVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<ListProductForListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ProductDetailVm>> GetProductDetailsByIdAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0)
            {
                return ActionResponse<ProductDetailVm>.Failed(new string[] { "Wrong product Id" });
            }

            var productQuery = _productRepository.GetProductById(productId);
            try
            {
                var product = await productQuery.SingleOrDefaultAsync(cancellationToken);
                if (product == null)
                {
                    return ActionResponse<ProductDetailVm>.Failed(new string[] { "Product not found" });
                }

                var productVm = _mapper.Map<ProductDetailVm>(product);

                return ActionResponse<ProductDetailVm>.Success(productVm);
            }

            catch (Exception ex)
            {
                return ActionResponse<ProductDetailVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<UpdateProductVm>> PrepareUpdateProductVmAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0)
            {
                return ActionResponse<UpdateProductVm>.Failed(new string[] { "Error: Invalid product Id" });
            }

            try
            {
                var productQuery = _productRepository.GetProductById(productId);
                var product = await productQuery.SingleOrDefaultAsync(cancellationToken);
                if (product == null)
                {
                    return ActionResponse<UpdateProductVm>.Failed(new string[] { "Error: Product not found in Database" });
                }

                var productVm = _mapper.Map<UpdateProductVm>(product);
                productVm.ProductType = GetProductTypes();

                return ActionResponse<UpdateProductVm>.Success(productVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<UpdateProductVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
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

        public async Task<ActionResponse<VoidValue>> AddProductTypeAsync(NewProductTypeVm model, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.GetValidator<NewProductTypeVm>();
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return ActionResponse<VoidValue>.Failed(result.Errors.Select(e => e.ErrorMessage), true);
            }

            var productType = _mapper.Map<ProductType>(model);
            var productTypeId = await _productRepository.AddProductTypeAsync(productType, cancellationToken);

            return ActionResponse<VoidValue>.Success(null, productTypeId);
        }

        public async Task<ActionResponse<ListProductTypeForListVm>> GetProductTypesForListAsync(CancellationToken cancellationToken)
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

                return ActionResponse<ListProductTypeForListVm>.Success(result);
            }

            catch (Exception ex)
            {
                return ActionResponse<ListProductTypeForListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ProductTypeVm>> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken)
        {
            if (productTypeId <= 0)
            {
                return ActionResponse<ProductTypeVm>.Failed(new string[] { "Error: Invalid product type Id" });
            }

            try
            {
                var productTypeQuery = _productRepository.GetProductTypeById(productTypeId);
                var productType = await productTypeQuery.SingleOrDefaultAsync(cancellationToken);
                if (productType == null)
                {
                    return ActionResponse<ProductTypeVm>.Failed(new string[] { "Error: Product type is null" });
                }

                var productTypeVm = _mapper.Map<ProductTypeVm>(productType);

                return ActionResponse<ProductTypeVm>.Success(productTypeVm);
            }

            catch (Exception ex)
            {
                return ActionResponse<ProductTypeVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        private ProductTypeSelectListVm GetProductTypes() => new ProductTypeSelectListVm()
        {
            ProductTypes = _productRepository.GetAllProductTypes().ProjectTo<ProductTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };

        public Task<ActionResponse<UpdateProductTypeVm>> UpdateProductTypeAsync(int productTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResponse<VoidValue>> DeleteProductTypeAsync(int productTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResponse<UpdateProductTypeVm>> GetProductTypeToEditAsync(int productTypeId, CancellationToken cancellationToken)
        {
            if (productTypeId < 1 )
            {
                return ActionResponse<UpdateProductTypeVm>.Failed(new string[] { "Invalid Product Type ID" });
            }

            try
            {
                var productTypeVm = await _productRepository.GetProductTypeById(productTypeId).ProjectTo<UpdateProductTypeVm>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                if (productTypeVm == null)
                {
                    return ActionResponse<UpdateProductTypeVm>.Failed(new string[] { "Product Type not found" });
                }

                return ActionResponse<UpdateProductTypeVm>.Success(productTypeVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<UpdateProductTypeVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public Task<ActionResponse<UpdateProductTypeVm>> UpdateProductTypeAsync(UpdateProductTypeVm model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
