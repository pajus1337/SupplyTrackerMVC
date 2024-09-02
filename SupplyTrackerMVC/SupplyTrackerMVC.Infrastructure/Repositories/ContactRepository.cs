using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly Context _context;

        public ContactRepository(Context context)
        {
            _context = context;
        }

        public async Task<(int ContactId, bool Success)> AddContactAsync(Contact contact, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Contacts.AddAsync(contact, cancellationToken);
                int success = await SaveChangesAsync(cancellationToken);
                if (success < 1)
                {
                    throw new InvalidOperationException("Failed to save new contact.");
                }

                return (contact.Id, true);
            }
            catch (Exception ex)
            {
                 throw; 
            }
        }

        public Task<bool> DeleteAddressAsync(int contact, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Contact> GetContactById(int contactId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContactDetailType> GetContactDetailTypes() => _context.ContactDetailTypes;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<bool> UpdateAddressAsync(Contact contact, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
