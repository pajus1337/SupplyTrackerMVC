using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface ISenderService
    {
        Task<ServiceResponse<NewSenderVm>> AddNewSenderAsync(NewSenderVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<SenderDetailsVm>> UpdateSenderByIdAsync(UpdateSenderVm updateSenderVm, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> DeleteSenderAsync(int senderId, CancellationToken cancellationToken);
        Task<ServiceResponse<ListSenderForListVm>> GetSendersForListAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<SenderDetailsVm>> GetSenderDetailsByIdAsync(int senderId, CancellationToken cancellationToken);
        Task<ServiceResponse<SenderSelectListVm>> GetAllSendersForSelectListAsync(CancellationToken cancellationToken);
    }
}
