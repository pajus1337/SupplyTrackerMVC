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
        int AddDelivery(Delivery delivery);
        void UpdateDelivery();
        void DeleteDelivery(int deliveryId);
        Delivery GetDeliveryById(int deliveryId);
    }
}
