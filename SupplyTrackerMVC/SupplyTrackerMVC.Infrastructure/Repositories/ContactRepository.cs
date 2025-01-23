using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

        public async Task<int> AddContactAsync(Contact contact, CancellationToken cancellationToken)
        {
            await _context.Contacts.AddAsync(contact, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return contact.Id;
        }


        public IQueryable<Contact> GetContactById(int contactId)
        {
            var contactQuery = _context.Contacts.Where(p => p.Id == contactId);
            return contactQuery;
        }

        public async Task<int> AddContactDetailTypeAsync(ContactDetailType contactDetailType, CancellationToken cancellationToken)
        {
            await _context.ContactDetailTypes.AddAsync(contactDetailType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return contactDetailType.Id;
        }

        public IQueryable<ContactDetailType> GetContactDetailTypeById(int contactDetailTypeId) => _context.ContactDetailTypes.Where(p => p.Id == contactDetailTypeId);

        public async Task<bool> UpdateContactDetailTypeAsync(ContactDetailType contactTypeDetail, CancellationToken cancellationToken)
        {
            _context.ContactDetailTypes.Update(contactTypeDetail);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? true : false;
        }

        public IQueryable<ContactDetailType> GetContactDetailTypes() => _context.ContactDetailTypes;

        public async Task<bool> DeleteContactDetailTypeAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {
            var contactDetailType = await _context.ContactDetailTypes.FindAsync(contactDetailTypeId, cancellationToken);
            if (contactDetailType == null)
            {
                return false;
            }

            _context.ContactDetailTypes.Remove(contactDetailType);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public IQueryable<ContactDetail> GetContactDetailById(int contactDetailsId)
        {
            var contactDetailQuery = _context.ContactDetails.Where(p => p.Id == contactDetailsId);
            return contactDetailQuery;
        }

        public async Task<bool> UpdateContactAsync(Contact contact, CancellationToken cancellationToken)
        {
            _context.Contacts.Update(contact);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ? true : false;
        }

        public async Task UpdateContactDetailAsync(ContactDetail contactDetail, CancellationToken cancellationToken)
        {
            _context.ContactDetails.Update(contactDetail);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
