using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IReceiverService
    {
        Task<ActionResponse<VoidValue>> AddReceiverAsync(NewReceiverVm model, CancellationToken cancellationToken);
        Task<ActionResponse<ReceiverDetailsVm>> UpdateReceiverAsync(UpdateReceiverVm model, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> DeleteReceiverByIdAsync(int receiverId, CancellationToken cancellationToken);
        Task<ActionResponse<ListReceiverForListVm>> GetReceiversForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<ActionResponse<ReceiverDetailsVm>> GetReceiverDetailsByIdAsync(int receiverId, CancellationToken cancellationToken);
        Task<ActionResponse<ReceiverSelectListVm>> GetReceiversForSelectListAsync(CancellationToken cancellationToken);
        Task<ActionResponse<ReceiverBranchSelectListVm>> GetReceiverBranchesForSelectListAsync(CancellationToken cancellationToken);
        Task<ActionResponse<NewContactVm>> AddReceiverContactAsync(NewContactVm newContactVm, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> AddReceiverBranchAsync(NewReceiverBranchVm model, CancellationToken cancellationToken);
        Task<ActionResponse<ReceiverBranchDetailsVm>> GetReceiverBranchDetailsAsync(int receiverBranchId, CancellationToken cancellationToken);
        Task<NewReceiverBranchVm> PrepareNewReceiverBranchVm(CancellationToken cancellationToken, int receiverId);
        Task<ActionResponse<UpdateReceiverVm>> GetReceiverForEditAsync(int receiverId,CancellationToken cancellationToken);
        Task<ActionResponse<ReceiverForDeleteVm>> GetReceiverForDeleteAsync(int receiverId, CancellationToken cancellationToken);
    }
}