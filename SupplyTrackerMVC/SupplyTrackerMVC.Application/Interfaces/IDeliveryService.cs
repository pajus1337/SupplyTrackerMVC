using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IDeliveryService
    {
        int AddNewDelivery(NewDeliveryVm model);
        DeliveryDetailsVm GetDeliveryDetailsById(int deliveryId);
        NewDeliveryVm PrepareNewDeliveryViewModel();
        ReceiverBranchSelectListVm GetReceiverBranchesByReceiverId(int receiverId);
    }
}
