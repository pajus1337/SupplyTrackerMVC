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
        Task<ServiceResponse<VoidValue>> AddNewProductAsync(NewProductVm model,CancellationToken cancellationToken);
        Task<ServiceResponse<UpdateProductVm>> UpdateProductAsync(UpdateProductVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<UpdateProductVm>> GetPreparedUpdateProductVmAsync(int productId, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> DeleteProductASync(int productId, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> AddNewProductTypeAsync(NewProductTypeVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<ProductTypeVm>> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken);
        Task<ServiceResponse<ListProductTypeForListVm>> GetAllProductTypesForListAsync(CancellationToken);
        Task<ServiceResponse<ListProductForListVm>> GetAllProductsForListAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<ProductDetailVm>> GetProductDetailsByIdAsync(int productId, CancellationToken cancellationToken);
        Task<ServiceResponse<ProductSelectListVm>> GetAllActiveProductsForSelectList(CancellationToken cancellationToken);
        NewProductVm PrepareNewProductViewModel();
    }
}
