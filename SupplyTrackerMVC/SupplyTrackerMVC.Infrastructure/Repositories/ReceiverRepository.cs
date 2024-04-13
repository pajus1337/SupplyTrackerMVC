using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class ReceiverRepository : IReceiverRepository
    {
        public int AddReceiver(Receiver receiver)
        {
            throw new NotImplementedException();
        }

        public void DeleteReceiver(int receiverId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Receiver> GetAllReceivers()
        {
            throw new NotImplementedException();
        }

        public Receiver GetReceiverById(int receiverId)
        {
            throw new NotImplementedException();
        }

        public void UpdateReceiver()
        {
            throw new NotImplementedException();
        }
    }
}
