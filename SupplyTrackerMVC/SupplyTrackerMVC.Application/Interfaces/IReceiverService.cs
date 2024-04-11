using SupplyTrackerMVC.Application.ViewModels.Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IReceiverService
    {
        int AddNewReceiver(NewReceiverVm receiver);
        ListReceiverForListVm GetAllReceiversForList();
        ReceiverDetailsVm GetReceiiverDetailsById(int receiverId);
    }
}
