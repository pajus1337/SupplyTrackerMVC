using AutoMapper;
using AutoMapper.QueryableExtensions;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
            var productId = _productRepository.AddProduct(product);
            int saved = await _productRepository.SaveChangesAsync(cancellationToken);
            if (saved == 0)
            {
                return (false, null, null);
            }

            return (true, null, productId);
        }

        public Task<(bool Success, string Error)> DeleteProductASync(int productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public ProductSelectListVm GetAllActiveProductsForSelectList()
        {
            var products = _productRepository.GetAllActiveProducts().ProjectTo<ProductForSelectListVm>(_mapper.ConfigurationProvider);
            var productsVm = new ProductSelectListVm()
            {
                Products = products
            };

            return productsVm;
        }

        public ListProductForList GetAllActiveProductsForList()
        {
            throw new NotImplementedException();
        }

        public ProductDetailVm GetProductDetailsById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string Error)> UpdateProductAsync(int productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        public async Task<(bool Success, ProductTypeVm)> GetProductTypeById(int productTypeId, CancellationToken cancellationToken)
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
