using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<VoidValue>> AddProductAsync(NewProductVm model,CancellationToken cancellationToken);
        Task<ServiceResponse<UpdateProductVm>> UpdateProductAsync(UpdateProductVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<UpdateProductVm>> PrepareUpdateProductVmAsync(int productId, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> DeleteProductASync(int productId, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> AddProductTypeAsync(NewProductTypeVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<ProductTypeVm>> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken);
        Task<ServiceResponse<ListProductTypeForListVm>> GetProductTypesForListAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<UpdateProductTypeVm>> UpdateProductTypeAsync(int productTYpeId, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> DeleteProductTypeAsync(int productTypeId, CancellationToken cancellationToken);
        Task<ServiceResponse<ListProductForListVm>> GetProductsForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<ServiceResponse<ProductDetailVm>> GetProductDetailsByIdAsync(int productId, CancellationToken cancellationToken);
        Task<ServiceResponse<ProductSelectListVm>> GetProductsForSelectList(CancellationToken cancellationToken);
        NewProductVm PrepareNewProductVm();
    }
}
