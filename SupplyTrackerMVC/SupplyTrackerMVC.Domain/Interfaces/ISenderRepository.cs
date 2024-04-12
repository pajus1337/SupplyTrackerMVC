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
        Sender GetSenderById(int senderId);
    }
}
