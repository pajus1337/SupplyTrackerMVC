using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class SenderRepository : ISenderRepository
    {
        private Context _context;
        public SenderRepository(Context context)
        {
            _context = context;
        }
        public async Task<(int SenderId, bool Success)> AddSenderAsync(Sender sender, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Senders.AddAsync(sender);
                int success = await SaveChangesAsync(cancellationToken);
                if (success == 0)
                {
                    throw new InvalidOperationException("Failed to add new sender.");
                }

                return (sender.Id, true);
            }
            catch (DbUpdateException)
            {
                // TODO: Test ? And Add better solution for ex Handling
                throw;
            }
        }

        public async Task<bool> DeleteSenderAsync(int senderId, CancellationToken cancellationToken)
        {
            try
            {
                var sender = await _context.Senders.FindAsync(senderId, cancellationToken);
                if (sender == null)
                {
                    throw new InvalidOperationException($"Failed to find sender with ID {senderId}");
                }

                _context.Senders.Remove(sender);
                int success = await SaveChangesAsync(cancellationToken);
                if (success == 0)
                {
                    throw new InvalidOperationException($"Failed to save changes in DataBase");
                }

                return true;
            }
            catch (Exception)
            {
                // TODO: Add better Exception handler
                throw;
            }
        }

        public async Task<(bool Success, Sender? SenderObject)> GetSenderByIdAsync(int senderId, CancellationToken cancellationToken)
        {
            var sender = await _context.Senders.FindAsync(senderId, cancellationToken);

            return (sender != null, sender);
        }


        public async Task<bool> UpdateSenderAsync(Sender sender, CancellationToken cancellationToken)
        {
            if (sender == null)
            {
                throw new InvalidOperationException($"Sender can'T be null in this place");
            }

            try
            {
                _context.Senders.Update(sender);
                int success = await SaveChangesAsync(cancellationToken);
                if (success == 0)
                {
                    throw new InvalidOperationException($"Failed to update Sender with ID {sender.Id}");
                }

                return true;
            }
            catch (Exception)
            {
                // TODO: Better ex handling
                throw;
            }
        }

        public IQueryable<Sender> GetAllActiveSenders()
        {
            var senders = _context.Senders.Where(s => s.IsActive);
            return senders;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
