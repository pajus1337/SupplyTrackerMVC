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
        Task<ActionResponse<VoidValue>> AddProductAsync(NewProductVm model,CancellationToken cancellationToken);
        Task<ActionResponse<UpdateProductVm>> UpdateProductAsync(UpdateProductVm model, CancellationToken cancellationToken);
        Task<ActionResponse<UpdateProductVm>> PrepareUpdateProductVmAsync(int productId, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> DeleteProductASync(int productId, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> AddProductTypeAsync(NewProductTypeVm model, CancellationToken cancellationToken);
        Task<ActionResponse<ProductTypeVm>> GetProductTypeByIdAsync(int productTypeId, CancellationToken cancellationToken);
        Task<ActionResponse<ListProductTypeForListVm>> GetProductTypesForListAsync(CancellationToken cancellationToken);
        Task<ActionResponse<UpdateProductTypeVm>> GetProductTypeToEditAsync(int productTypeId, CancellationToken cancellationToken);
        Task<ActionResponse<UpdateProductTypeVm>> UpdateProductTypeAsync(UpdateProductTypeVm model , CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> DeleteProductTypeAsync(int productTypeId, CancellationToken cancellationToken);
        Task<ActionResponse<ListProductForListVm>> GetProductsForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<ActionResponse<ProductDetailVm>> GetProductDetailsByIdAsync(int productId, CancellationToken cancellationToken);
        Task<ActionResponse<ProductSelectListVm>> GetProductsForSelectList(CancellationToken cancellationToken);
        NewProductVm PrepareNewProductVm();
    }
}
