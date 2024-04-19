using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IReceiverService
    {
        Task<(bool Success, IEnumerable<string>? Errors, int? ReceiverId)> AddNewReceiverAsync(NewReceiverVm model, CancellationToken cancellationToken);
        ListReceiverForListVm GetAllActiveReceiversForList();
        ReceiverDetailsVm GetReceiverDetailsById(int receiverId);
        ReceiverSelectListVm GetAllActiveReceiversForSelectList();
        ReceiverBranchSelectList GetAllActiveReceiverBranchesForSelectList();
    }
}