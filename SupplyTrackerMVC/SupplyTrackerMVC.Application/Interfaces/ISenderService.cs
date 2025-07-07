using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
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
        Task<ActionResponse<NewSenderVm>> AddNewSenderAsync(NewSenderVm model, CancellationToken cancellationToken);
        Task<ActionResponse<SenderDetailsVm>> UpdateSenderAsync(UpdateSenderVm updateSenderVm, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> DeleteSenderByIdAsync(int senderId, CancellationToken cancellationToken);
        Task<ActionResponse<ListSenderForListVm>> GetSendersForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<ActionResponse<SenderDetailsVm>> GetSenderDetailsByIdAsync(int senderId, CancellationToken cancellationToken);
        Task<ActionResponse<UpdateSenderVm>> GetSenderForEditAsync(int senderId, CancellationToken cancellationToken);
        Task<ActionResponse<SenderForDeleteVm>> GetSenderForDeleteAsync(int senderId, CancellationToken cancellationToken);
        Task<ActionResponse<SenderSelectListVm>> GetAllSendersForSelectListAsync(CancellationToken cancellationToken);
        Task<ActionResponse<NewContactVm>> AddSenderContactAsync(NewContactVm newContactVm, CancellationToken cancellationToken);
        Task<ActionResponse<NewContactVm>> PrepareAddContactVm(int senderId);
        Task<ActionResponse<ContactVm>> UpdateSenderContactAsync(UpdateContactVm updateContactVm, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> DeleteSenderContactAsync(int senderId, int senderContactId, CancellationToken cancellationToken);
    }
}
