using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Domain.Interfaces;
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
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<Contact> GetContactById(int contactId)
        {
            var contactQuery = _context.Contacts.Where(p => p.Id == contactId);
            return contactQuery;
        }

        public async Task<(int ContactTypeId, bool Success)> AddContactDetailTypeAsync(ContactDetailType contactDetailType, CancellationToken cancellationToken)
        {
            try
            {
                await _context.ContactDetailTypes.AddAsync(contactDetailType, cancellationToken);
                int success = await SaveChangesAsync(cancellationToken);
                if (success < 1)
                {
                    throw new InvalidOperationException("Failed to save new contact detail type.");
                }
                return (contactDetailType.Id, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<ContactDetailType> GetContactDetailTypeById(int contactDetailTypeId) => _context.ContactDetailTypes.Where(p => p.Id == contactDetailTypeId);

        public async Task<bool> UpdateContactDetailTypeAsync(ContactDetailType contactTypeDetail, CancellationToken cancellationToken)
        {
            if (contactTypeDetail == null)
            {
                throw new InvalidOperationException($"Didn't Received the object to update");
            }

            try
            {
                _context.ContactDetailTypes.Update(contactTypeDetail);
                int success = await SaveChangesAsync(cancellationToken);
                if (success <= 0)
                {
                    throw new InvalidOperationException($"Failed to update contact type with ID {contactTypeDetail.Id}");
                }

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<ContactDetailType> GetContactDetailTypes() => _context.ContactDetailTypes;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public Task<bool> DeleteAddressAsync(int contact, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAddressAsync(Contact contact, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        // TODO: Refine?
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


        // TODO: Less complex logic ? -> Replace with GetContact? 
        public IQueryable<ContactDetail> GetContactDetailsById(int contactDetailsId)
        {
            var contactDetailQuery = _context.ContactDetails.Where(p => p.Id == contactDetailsId);
            return contactDetailQuery;
        }
    }
}
