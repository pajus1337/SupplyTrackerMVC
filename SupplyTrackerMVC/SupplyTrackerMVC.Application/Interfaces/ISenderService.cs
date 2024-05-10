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
        Task<(bool Success, IEnumerable<string>? Errors, int? SenderId)> AddNewSenderAsync(NewSenderVm model, CancellationToken cancellationToken);
        Task<(bool Success, IEnumerable<string>? Errors)> UpdateSenderByIdAsync(int senderId, CancellationToken cancellationToken);
        Task<bool> DeleteSenderAsync(int senderId, CancellationToken cancellationToken);
        Task<(bool Success, ListSenderForListVm ListOfActiveSenders)> GetAllActiveSendersForListAsync(CancellationToken cancellationToken);
        Task<(bool Success, SenderDetailsVm)> GetSenderDetailsByIdAsync(int senderId, CancellationToken cancellationToken);
        Task<(bool Success, SenderSelectListVm)> GetAllActiveSendersForSelectList();
    }
}
