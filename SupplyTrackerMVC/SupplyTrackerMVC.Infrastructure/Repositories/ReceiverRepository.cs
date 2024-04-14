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
        private readonly Context _context;

        public ReceiverRepository(Context context)
        {
            _context = context;
        }

        public int AddReceiver(Receiver receiver)
        {
            throw new NotImplementedException();
        }

        public void DeleteReceiver(int receiverId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Receiver> GetAllActiveReceivers()
        {
            var allActiveReceiver = _context.Receivers.Where(p => p.isActive);
            return allActiveReceiver;
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
