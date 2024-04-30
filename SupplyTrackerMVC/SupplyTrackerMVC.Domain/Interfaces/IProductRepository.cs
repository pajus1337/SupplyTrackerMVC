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
        Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task<bool> DeleteProductAsync(int productId, CancellationToken cancellationToken);
        Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
        IQueryable<Product> GetAllActiveProducts();
        IQueryable<Product> GetProductsByProductTypeId(int productTypeId);
        Task<int> AddProductTypeAsync(ProductType productType, CancellationToken cancellationToken);
        IQueryable<ProductType> GetAllProductTypes();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<ProductType> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken);
    }
}
