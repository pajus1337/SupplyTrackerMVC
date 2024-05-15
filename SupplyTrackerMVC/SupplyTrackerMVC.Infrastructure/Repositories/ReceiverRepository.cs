using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Interfaces.Common;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class ReceiverRepository : IReceiverRepository
    {
        private readonly Context _context;

        public ReceiverRepository(Context context)
        {
            _context = context;
        }

        // TODO: Change into Async AddReceiver
        public int AddReceiver(Receiver receiver)
        {
            _context.Add(receiver);
            return receiver.Id;
        }

        public async Task<bool> DeleteReceiverAsync(int receiverId, CancellationToken cancellationToken)
        {
            try
            {
                var receiver = await _context.Receivers.FirstOrDefaultAsync(p => p.Id == receiverId, cancellationToken);
                if (receiver == null)
                {
                    return false;
                }

                if (receiver is ISoftDeletable softDeletable)
                {
                    softDeletable.IsDeleted = true;
                    softDeletable.DeletedOnUtc = DateTime.UtcNow; 
                }
                else
                {
                    _context.Receivers.Remove(receiver);
                }

                int success = await SaveChangesAsync(cancellationToken);

                return success > 0 ? true : false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateReceiverAsync(Receiver receiver, CancellationToken cancellationToken)
        {
            if (receiver == null)
            {
                return false;
            }

            try
            {
                _context.Receivers.Update(receiver);
                int success = await SaveChangesAsync(cancellationToken);
                return success > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<ReceiverBranch> GetAllActiveReceiverBranches(int receiverId)
        {
            var activeBranches = _context.DeliveryBranches.Where(b => b.ReceiverId == receiverId && b.isActive);
            return activeBranches;
        }

        public IQueryable<ReceiverBranch> GetAllActiveReceiverBranches()
        {
            var activeBranches = _context.DeliveryBranches.Where(b => b.isActive);
            return activeBranches;
        }

        public IQueryable<Receiver> GetAllReceivers()
        {
            var allActiveReceiver = _context.Receivers;
            return allActiveReceiver;
        }

        public IQueryable<Receiver> GetReceiverById(int receiverId) => _context.Receivers.Where(p => p.Id == receiverId);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
