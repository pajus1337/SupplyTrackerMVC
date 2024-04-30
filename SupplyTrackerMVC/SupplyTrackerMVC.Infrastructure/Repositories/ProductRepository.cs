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

        public IQueryable<Product> GetProductsByProductTypeId(int productTypeId)
        {
            if (productTypeId <= 0)
            {
                throw new ArgumentException("Product TypeId must be '> 0'", nameof(productTypeId));
            }
            var products = _context.Products.Where(p => p.ProductTypeId == productTypeId);
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Product Id must be '> 0'", nameof(productId));
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                throw new ProductNotFoundException($"No product with ID {productId} found.", productId);
            }
            return product;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<ProductType> GetAllProductTypes()
        {
            var productTypes = _context.ProductTypes;
            return productTypes;
        }

        public IQueryable<Product> GetAllActiveProducts()
        {
            var products = _context.Products.Where(p => p.isActive);
            return products;
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

        public async Task<ProductType> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(p => p.Id == productTypeId, cancellationToken);

            return productType;
        }
    }
}
