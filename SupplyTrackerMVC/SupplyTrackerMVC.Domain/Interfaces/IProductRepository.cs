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
        int AddProduct(Product product);
        void UpdateProduct();
        void DeleteProduct(int productId);
        Product GetProductById(int productId);
        IQueryable<Product> GetAllActiveProducts();
        IQueryable<Product> GetProductsByProductTypeId(int productTypeId);
        IQueryable<ProductType> GetAllProductTypes();
    }
}
