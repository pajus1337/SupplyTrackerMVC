using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }
        public int AddNewDelivery(NewDeliveryVm model)
        {

            throw new NotImplementedException();
        }

        public DeliveryDetailsVm GetDeliveryDetailsById(int deliveryId)
        {
            throw new NotImplementedException();
        }
    }
}
