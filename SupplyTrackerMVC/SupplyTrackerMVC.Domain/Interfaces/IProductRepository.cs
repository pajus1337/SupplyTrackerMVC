using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<(int ProductId, bool Success)> AddProductAsync(Product product, CancellationToken cancellationToken);
        Task<int> AddProductTypeAsync(ProductType productType, CancellationToken cancellationToken);
        Task<bool> DeleteProductAsync(int productId, CancellationToken cancellationToken);
        Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken);
        IQueryable<Product> GetAllProducts();
        IQueryable<ProductType> GetAllProductTypes();
        IQueryable<Product> GetProductById(int productId);
        IQueryable<ProductType> GetProductTypeById(int productTypeId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
