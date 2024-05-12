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
        int AddReceiver(Receiver receiver);
        Task<bool> UpdateReceiverAsync(Receiver receiver, CancellationToken cancellationToken);
        Task<bool> DeleteReceiverAsync(int receiverId, CancellationToken cancellationToken);
        Task<(bool Success, Receiver? Receiver)> GetReceiverByIdAsync(int receiverId, CancellationToken cancellationToken);
        IQueryable<Receiver> GetAllReceivers();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IQueryable<ReceiverBranch> GetAllActiveReceiverBranches();
    }
}
