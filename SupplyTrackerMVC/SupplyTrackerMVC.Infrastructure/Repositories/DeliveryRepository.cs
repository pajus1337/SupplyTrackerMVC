using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        public Task<int> AddDeliveryAsync(Delivery delivery, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDeliveryAsync(int deliveryId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Delivery> GetDeliveryByIdAsync(int deliveryId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDeliveryAsync(Delivery delivery, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
