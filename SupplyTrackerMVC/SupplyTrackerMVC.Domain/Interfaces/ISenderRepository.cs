using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface ISenderRepository
    {
        int AddSender(Sender sender);
        void UpdateSender();
        void DeleteSender(int senderId);
        Product GetSenderById(int senderId);
    }
}
