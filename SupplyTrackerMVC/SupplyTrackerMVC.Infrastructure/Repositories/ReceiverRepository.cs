using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Domain.Exceptions;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Interfaces.Common;
using SupplyTrackerMVC.Domain.Model.Receivers;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class ReceiverRepository : IReceiverRepository
    {
        private readonly Context _context;

        public ReceiverRepository(Context context)
        {
            _context = context;
        }

        public async Task<(bool Success, int? ReceiverId)> AddReceiverAsync(Receiver receiver, CancellationToken cancellationToken)
        {
            await _context.AddAsync(receiver, cancellationToken);
            int recordsAffected = await SaveChangesAsync(cancellationToken);

            return recordsAffected > 0 ? (true, receiver.Id) : (false, null);
        }

        public async Task<bool> DeleteReceiverAsync(int receiverId, CancellationToken cancellationToken)
        {
            var receiver = await _context.Receivers.FindAsync(receiverId, cancellationToken);
            if (receiver == null)
            {
                throw new EntityNotFoundException($"Receiver with ID {receiverId} not found.");
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

            int recordsAffected = await _context.SaveChangesAsync(cancellationToken);

            return recordsAffected > 0;
        }

        public async Task<bool> UpdateReceiverAsync(Receiver receiver, CancellationToken cancellationToken)
        {
            _context.Receivers.Update(receiver);
            int recordsAffected = await SaveChangesAsync(cancellationToken);

            return recordsAffected > 0;
        }

        public IQueryable<ReceiverBranch> GetAllActiveReceiverBranches(int receiverId)
        {
            var activeBranches = _context.ReceiverBranches.Where(b => b.ReceiverId == receiverId && !b.IsDeleted);
            return activeBranches;
        }

        public IQueryable<ReceiverBranch> GetAllReceiverBranches() => _context.ReceiverBranches;

        public IQueryable<Receiver> GetAllReceivers() => _context.Receivers;

        public IQueryable<Receiver> GetReceiverById(int receiverId) =>
            _context.Receivers.Where(p => p.Id == receiverId)
            .Include(p => p.Address)
            .Include(p => p.Contacts)
            .ThenInclude(p => p.ContactDetails)
            .Include(p => p.ReceiverBranches);

        public async Task<(bool Success, int? ReceiverBranchId)> AddReceiverBranchAsync(ReceiverBranch receiverBranch, CancellationToken cancellationToken)
        {
                await _context.AddAsync(receiverBranch, cancellationToken);
                int recordsAffected = await SaveChangesAsync(cancellationToken);

                return recordsAffected > 0 ? (true, receiverBranch.Id) : (false, null);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
