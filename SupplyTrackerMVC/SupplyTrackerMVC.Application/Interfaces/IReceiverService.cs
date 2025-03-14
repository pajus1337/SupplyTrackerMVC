using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Model.Receivers;
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
        Task<ServiceResponse<VoidValue>> AddReceiverAsync(NewReceiverVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<UpdateReceiverVm>> UpdateReceiverAsync(UpdateReceiverVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> DeleteReceiverByIdAsync(int receiverId, CancellationToken cancellationToken);
        Task<ServiceResponse<ListReceiverForListVm>> GetReceiversForListAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<ReceiverDetailsVm>> GetReceiverDetailsByIdAsync(int receiverId, CancellationToken cancellationToken);
        Task<ServiceResponse<ReceiverSelectListVm>> GetReceiversForSelectListAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<ReceiverBranchSelectListVm>> GetReceiverBranchesForSelectListAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<NewContactVm>> AddReceiverContactAsync(NewContactVm newContactVm, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> AddReceiverBranchAsync(NewReceiverBranchVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<ReceiverBranchDetailsVm>> GetReceiverBranchDetailsAsync(int receiverBranchId, CancellationToken cancellationToken);
        Task<NewReceiverBranchVm> PrepareNewReceiverBranchVm(CancellationToken cancellationToken, int receiverId);
        Task<ServiceResponse<UpdateReceiverVm>> GetReceiverForEditAsync(int receiverId,CancellationToken cancellationToken);
        Task<ServiceResponse<ReceiverForDeleteVm>> GetReceiverForDeleteAsync(int receiverId, CancellationToken cancellationToken);
    }
}