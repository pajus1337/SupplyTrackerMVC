using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<(bool Success, int? DeliveryId)> AddDeliveryAsync(Delivery delivery, CancellationToken cancellationToken);
        Task<bool> UpdateDeliveryAsync(Delivery delivery, CancellationToken cancellationToken);
        Task<bool> DeleteDeliveryAsync(int deliveryId, CancellationToken cancellationToken);
        Task<Delivery> GetDeliveryByIdAsync(int deliveryId, CancellationToken cancellationToken);
    }
}
