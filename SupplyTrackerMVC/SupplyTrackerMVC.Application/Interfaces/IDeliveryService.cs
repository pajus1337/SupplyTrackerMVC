using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IDeliveryService
    {
        Task<ServiceResponse<VoidValue>> AddNewDeliveryAsync(NewDeliveryVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<DeliveryDetailsVm>> GetDeliveryDetailsByIdAsync(int deliveryId, CancellationToken cancellationToken);
        NewDeliveryVm PrepareNewDeliveryViewModel();
        ReceiverBranchSelectListVm GetReceiverBranchesByReceiverId(int receiverId);
        Task<ServiceResponse<ListDeliveryForListVm>> GetDeliveryForListAsync(int pageSize, int pageNo, string searchBy, string searchString, CancellationToken CancellationToken);
    }
}
