using SupplyTrackerMVC.Domain.Exceptions;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
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

        public int AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product to add can't be null");
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }   

        public void UpdateProduct()
        {

        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product to delete can't be null");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
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

        public Product GetProductById(int productId)
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
    }
}
