using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Domain.Exceptions;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<(int ProductId, bool Success)> AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Products.AddAsync(product, cancellationToken);
                int success = await SaveChangesAsync(cancellationToken);
                if (success < 1)
                {
                    throw new InvalidOperationException("Failed to add the product.");
                }

                return (product.Id, true);
            }
            catch (DbUpdateException ex)
            {
                // Add Log ?
                throw;
            }
        }

        public async Task<int> AddProductTypeAsync(ProductType productType, CancellationToken cancellationToken)
        {
            if (productType == null)
            {
                throw new ArgumentNullException(nameof(productType), "ProductType to add can't be null");
            }
            await _context.ProductTypes.AddAsync(productType, cancellationToken);
            var id = await _context.SaveChangesAsync(cancellationToken);
            return productType.Id;
        }


        // TODO: Add soft delete for product and ProductDetail
        public async Task<bool> DeleteProductAsync(int productId, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);

                if (product == null)
                {
                    throw new ProductNotFoundException($"No product with ID {productId} found.", productId);
                }

                _context.Products.Remove(product);
                int success = await SaveChangesAsync(cancellationToken);
                if (success != 1)
                {
                    throw new InvalidOperationException("Failed to delete the product.");
                }
                return true;
            }
            catch (DbUpdateException ex)
            {
                // Add log ?
                throw;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                _context.Products.Update(product);
                int success = await SaveChangesAsync(cancellationToken);
                if (success != 1)
                {
                    throw new InvalidOperationException("Failed to add the product.");
                }
                return true;
            }
            catch (DbUpdateException ex)
            {

                throw;
            }
        }
        public IQueryable<Product> GetAllProducts() => _context.Products;
        public IQueryable<ProductType> GetAllProductTypes() => _context.ProductTypes;
        public IQueryable<Product> GetProductById(int productId) => _context.Products.Where(p => p.Id == productId).Include(p => p.ProductDetail).Include(p => p.ProductType);
        public IQueryable<ProductType> GetProductTypeById(int productTypeId) => _context.ProductTypes.Where(p => p.Id == productTypeId);
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
    }
}
