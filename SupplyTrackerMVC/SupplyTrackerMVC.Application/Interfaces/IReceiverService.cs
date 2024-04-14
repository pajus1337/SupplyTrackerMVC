using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
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
        ListReceiverForListVm GetAllActiveReceiversForList();
        ReceiverDetailsVm GetReceiverDetailsById(int receiverId);
    }
}
