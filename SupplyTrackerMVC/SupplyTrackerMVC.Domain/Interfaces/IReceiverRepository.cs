using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface IReceiverRepository
    {
        Task<(bool Success, int? ReceiverId)> AddReceiverAsync(Receiver receiver, CancellationToken cancellationToken);
        Task<bool> UpdateReceiverAsync(Receiver receiver, CancellationToken cancellationToken);
        Task<bool> DeleteReceiverAsync(int receiverId, CancellationToken cancellationToken);
        IQueryable<Receiver> GetReceiverById(int receiverId);
        IQueryable<Receiver> GetAllReceivers();
        IQueryable<ReceiverBranch> GetAllReceiverBranches();
        IQueryable<ReceiverBranch> GetAllActiveReceiverBranches(int receiverId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<(bool Success, int? ReceiverBranchId)> AddReceiverBranchAsync(ReceiverBranch receiverBranch, CancellationToken cancellationToken);
    }
}
