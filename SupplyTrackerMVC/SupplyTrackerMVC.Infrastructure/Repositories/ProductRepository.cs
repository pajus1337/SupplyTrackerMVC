using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class ProductRepository
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

        public void EditProduct()
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
    }
}
