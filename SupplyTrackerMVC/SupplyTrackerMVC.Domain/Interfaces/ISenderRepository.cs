using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface ISenderRepository
    {
        Task<(int SenderId, bool Success)> AddSenderAsync(Sender sender, CancellationToken cancellationToken);
        Task<bool> UpdateSenderAsync(Sender sender, CancellationToken cancellationToken);
        Task<bool> DeleteSenderAsync(int senderId, CancellationToken cancellationToken);
        Task<(bool Success, Sender SenderObject)> GetSenderByIdAsync(int senderId,CancellationToken cancellationToken);
        IQueryable<Sender> GetAllActiveSenders();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
