using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Interfaces.Common;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly Context _context;
        public DeliveryRepository(Context context)
        {
            _context = context;
        }
        public async Task<(bool Success, int? DeliveryId)> AddDeliveryAsync(Delivery delivery, CancellationToken cancellationToken)
        {
            if (delivery == null)
            {
                return (false, null);
            }

            try
            {
                await _context.Deliveries.AddAsync(delivery, cancellationToken);
                var success = await _context.SaveChangesAsync(cancellationToken);

                return success > 0 ? (true, delivery.Id) : (false, null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDeliveryAsync(int deliveryId, CancellationToken cancellationToken)
        {
            if (deliveryId == 0)
            {
                return false;
            }
            try
            {
                var delivery = await _context.Deliveries.FindAsync(deliveryId, cancellationToken);
                if (delivery == null)
                {
                    return false;
                }
                else if (delivery is ISoftDeletable softDeletable)
                {
                    softDeletable.IsDeleted = true;
                    softDeletable.DeletedOnUtc = DateTime.UtcNow;
                }
                else
                {
                    _context.Deliveries.Remove(delivery);
                }

                int success = await _context.SaveChangesAsync(cancellationToken);

                return success > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<Delivery> GetAllDeliveries() => _context.Deliveries;

        public IQueryable<Delivery> GetDeliveryById(int deliveryId)
        {
            var delivery = _context.Deliveries.Where(d => d.Id == deliveryId);
            return delivery;
        }

        public async Task<bool> UpdateDeliveryAsync(Delivery delivery, CancellationToken cancellationToken)
        {
            if (delivery == null)
            {
                return false;
            }

            try
            {
                _context.Deliveries.Update(delivery);

                int success = await _context.SaveChangesAsync(cancellationToken);

                return success > 0;
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}
