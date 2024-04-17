using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class SenderRepository : ISenderRepository
    {
        private Context _context;
        public SenderRepository(Context context)
        {
            _context = context;
        }
        public int AddSender(Sender sender)
        {
            throw new NotImplementedException();
        }

        public void DeleteSender(int senderId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sender> GetAllActiveSenders()
        {
            var senders = _context.Senders.Where(s => s.IsActive);
            return senders;
        }

        public Sender GetSenderById(int senderId)
        {
            throw new NotImplementedException();
        }

        public void UpdateSender()
        {
            throw new NotImplementedException();
        }
    }
}
