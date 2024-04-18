using AutoMapper;
using AutoMapper.QueryableExtensions;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class ProduktService : IProduktService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProduktService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<(bool Success, IEnumerable<string>? Errors, int? ProductId)> AddNewProductAsync(NewProductVm model)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, IEnumerable<string>? Errors, int? ProductId)> AddNewReceiverAsync(NewProductVm model)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string Error)> DeleteProductASync(int productId)
        {
            throw new NotImplementedException();
        }

        public ProductSelectListVm GetAllActiveProdcutsForSelectList()
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

        public ProductDetailsVm GetProductDetailsById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string Error)> UpdateProductAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
