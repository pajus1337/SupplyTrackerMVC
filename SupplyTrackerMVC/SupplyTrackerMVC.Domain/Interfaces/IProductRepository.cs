using SupplyTrackerMVC.Domain.Exceptions;
using SupplyTrackerMVC.Domain.Model.Products;
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
        Product GetProoductById(int productId);
        IQueryable<Product> GetProductsByProductTypeId(int productTypeId);
        IQueryable<ProductType> GetAllProductTypes();
    }
}
