using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        public Task<bool> AddContactAsync(Contact contact, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAddressAsync(int contact, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Contact> GetContactById(int contactId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAddressAsync(Contact contact, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
