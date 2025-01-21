using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<(int ContactId, bool Success)> AddContactAsync(Contact contact, CancellationToken cancellationToken);
        Task<bool> UpdateContactAsync(Contact contact, CancellationToken cancellationToken);
        IQueryable<Contact> GetContactById(int contactId);
        IQueryable<ContactDetailType> GetContactDetailTypes();
        IQueryable<ContactDetailType> GetContactDetailTypeById(int contactDetailTypeId);
        IQueryable<ContactDetail> GetContactDetailById(int contactDetailsId);
        Task<(int ContactTypeId, bool Success)> AddContactDetailTypeAsync(ContactDetailType contactDetailType, CancellationToken cancellationToken);
        Task<bool> UpdateContactDetailTypeAsync(ContactDetailType contactDetailType, CancellationToken cancellationToken);
        Task<bool> DeleteContactDetailTypeAsync(int contactDetailTypeId, CancellationToken cancellationToken);
    }
}
