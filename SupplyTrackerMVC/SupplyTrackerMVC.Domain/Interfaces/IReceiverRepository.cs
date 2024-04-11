using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface IReceiverRepository
    {
        int AddReceiver(Receiver receiver);
        void UpdateReceiver();
        void DeleteReceiver(int receiverId);
        Receiver GetReceiverById(int receiverId);
        IQueryable<Receiver> GetAllActiveReceivers();

    }
}
